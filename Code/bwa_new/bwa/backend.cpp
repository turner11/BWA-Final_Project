#include "backend.h"
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <stdio.h>
#include "utils.h"


#define aln_sbcore(m,o,e,p) ((m)*(p)->s_mm + (o)*(p)->s_gapo + (e)*(p)->s_gape)
#define aln_score(m,o,e,p) ((m)*(p)->s_mm + (o)*(p)->s_gapo + (e)*(p)->s_gape)
// The following two lines are ONLY correct when OCC_INTERVAL==0x80
#define bwt_bwt(b, k) ((b)->bwt[((k)>>7<<4) + sizeof(bwtint_t) + (((k)&0x7f)>>4)])
#define bwt_occ_intv(b, k) ((b)->bwt + ((k)>>7<<4))




int IsEmptyBytes(ubyte_t* bytes, size_t length)
{
	int foundNonZero = 0;
	size_t i;
	for (i = 0; i < length && !foundNonZero; ++i) {
		ubyte_t currByte = *(bytes+i);
		foundNonZero = currByte != 0;
	}
	return !foundNonZero;
}

ubyte_t *ToBytes(bwa_seq_t *thisP, int *bytesLength)
{
	/*Calculate total size of bytes to convert*/
	size_t mySize = sizeof(bwa_seq_t);//size of this struct (excluding referenced objects)
	//Name + size indicator
	size_t nameLengthIndicator_s = sizeof(int); //Number to hold name length
	size_t nameLength = strlen(thisP->name)+1;//Will hold length of name (+1 for null termination)
	size_t nameCharSize = nameLength * sizeof(char);//number of chars for name
	//referenced objects sizes
	size_t sizeOf_bwt_aln1_t = sizeof(bwt_aln1_t);
	int isAlnNull = thisP->aln == '\0';
	size_t sizeOf_bwt_bwt_multi1_t = sizeof(bwt_multi1_t);

	/*rest of pointers*/
	size_t sizeOf_seq			= sizeof(ubyte_t) * thisP->len;
	size_t sizeOf_rseq			= sizeof(ubyte_t) * thisP->full_len;
	size_t sizeOf_qual			= sizeof(ubyte_t) * thisP->len;;
	size_t sizeOf_cigar			= sizeof(bwa_cigar_t);
	size_t mdLengthIndicator_s	= sizeof(int);
	size_t md_length			= 0;

	if(thisP->md != '\0')
	{
		md_length			= strlen(thisP->md) + 1; //null terminated
	}

	size_t fullSize =
		mySize
		+ nameLengthIndicator_s
		+ nameCharSize
		+ sizeOf_bwt_aln1_t
		+ sizeOf_bwt_bwt_multi1_t
		+ sizeOf_seq
		+ sizeOf_rseq
		+ sizeOf_qual
		+ sizeOf_cigar
		+ mdLengthIndicator_s
		+ md_length;

	char *bytes = (char*)calloc(1, fullSize);
	char *offsettedBytes = bytes;
	/*Convert to bytes*/
	//value type members
	memcpy(offsettedBytes, thisP, mySize);
	offsettedBytes += mySize;

	// size of name
	memcpy(offsettedBytes, &nameLength, nameLengthIndicator_s);
	offsettedBytes += nameLengthIndicator_s;
	//Name
	memcpy(offsettedBytes, thisP->name, nameCharSize);
	offsettedBytes += nameCharSize;
	//bwt_aln1_t
	bwt_aln1_t dummyAln = {}; //make sure all data members are zeros
	bwt_aln1_t *alnObj = (isAlnNull == 1)?&dummyAln:thisP->aln;
	memcpy(offsettedBytes, alnObj , sizeOf_bwt_aln1_t);
	offsettedBytes += sizeOf_bwt_aln1_t;
	//bwt_multi1_t
	int isMultiAlnNull = thisP->multi == '\0';
	bwt_multi1_t dummyMultiAln= {}; //make sure all data members are zeros
	bwt_multi1_t *alnMultiObj = (isMultiAlnNull  == 1)?&dummyMultiAln:thisP->multi;
	memcpy(offsettedBytes, alnMultiObj, sizeOf_bwt_bwt_multi1_t);
	offsettedBytes += sizeOf_bwt_bwt_multi1_t;
	//seq
	memcpy(offsettedBytes, thisP->seq, sizeOf_seq);
	offsettedBytes += sizeOf_seq;
	//rseq
	memcpy(offsettedBytes, thisP->rseq, sizeOf_rseq);
	offsettedBytes += sizeOf_rseq;
	//qual
	int isQualEmpty = thisP->qual == '\0';
	ubyte_t dummyQual = 0; //make sure all data members are zeros
	ubyte_t* qualObj = isQualEmpty ? &dummyQual :thisP->qual;
	memcpy(offsettedBytes, qualObj, sizeOf_qual);
	offsettedBytes += sizeOf_qual;
	//cigar
	bwa_cigar_t dummyCiagar = 0;
	bwa_cigar_t *cigarObj = thisP->cigar == '\0'? &dummyCiagar:thisP->cigar;
	memcpy(offsettedBytes, cigarObj , sizeOf_cigar);
	offsettedBytes += sizeOf_cigar;
	//mdLengthIndicator_s
	memcpy(offsettedBytes, &md_length, mdLengthIndicator_s);
	offsettedBytes += mdLengthIndicator_s;
	//md
	if(thisP->md != '\0')
	{
		memcpy(offsettedBytes, thisP->md, md_length);
		offsettedBytes += md_length;
	}

	*bytesLength = fullSize;
	//ubyte_t *bytes = (ubyte_t *)this;
	return (ubyte_t *)bytes;
}

bwa_seq_t* FromBytes(ubyte_t* bytes)
{
	/*Calculate total size of bytes to convert*/
	ubyte_t *offsettedBytes = bytes;
	/*Convert from bytes*/
	//The instance to return eventually
	size_t mySize = sizeof(bwa_seq_t);//size of this struct (excluding referenced objects)
	bwa_seq_t* instance = (bwa_seq_t*)malloc(mySize);
	//value type members
	memcpy(instance, offsettedBytes, mySize);
	offsettedBytes += mySize;

	// size of name
	size_t nameLengthIndicator_s = sizeof(int);
	//number of chars for name
	int* nameLength = (int*)malloc(nameLengthIndicator_s);
	memcpy(nameLength, offsettedBytes, nameLengthIndicator_s);
	offsettedBytes += nameLengthIndicator_s;

	//Name
	size_t nameCharSize = *nameLength * sizeof(char);

	char* name = (char*)malloc(nameCharSize);
	memcpy(name, offsettedBytes, nameCharSize);
	instance->name = name;
	//size_t strLength = strlen(instance->name);

	offsettedBytes += nameCharSize;

	//sizeOf_bwt_aln1_t
	size_t sizeOf_bwt_aln1_t = sizeof(bwt_aln1_t);
	instance->aln = (bwt_aln1_t*)malloc(sizeOf_bwt_aln1_t);
	memcpy(instance->aln, offsettedBytes, sizeOf_bwt_aln1_t);
	offsettedBytes += sizeOf_bwt_aln1_t;
	//bwt_multi1_t
	size_t sizeOf_bwt_bwt_multi1_t = sizeof(bwt_multi1_t);
	instance->multi = (bwt_multi1_t*)malloc(sizeOf_bwt_bwt_multi1_t);
	memcpy(instance->multi, offsettedBytes, sizeOf_bwt_bwt_multi1_t);
	offsettedBytes += sizeOf_bwt_bwt_multi1_t;
	/*-----------------------*/
	//seq
	size_t sizeOf_seq = sizeof(ubyte_t) * instance->len;
	instance->seq = (ubyte_t*)malloc(sizeOf_seq  );
	memcpy(instance->seq, offsettedBytes, sizeOf_seq);
	offsettedBytes += sizeOf_seq;
	//rseq
	size_t sizeOf_rseq = sizeof(ubyte_t)* instance->full_len;
	instance->rseq = (ubyte_t*)malloc(sizeOf_rseq  );
	memcpy(instance->rseq, offsettedBytes, sizeOf_rseq);
	offsettedBytes += sizeOf_rseq;
	//qual
	size_t sizeOf_qual = sizeof(ubyte_t)* instance->len;
	instance->qual = (ubyte_t*)malloc(sizeOf_qual  );
	memcpy(instance->qual, offsettedBytes, sizeOf_qual);
	offsettedBytes += sizeOf_qual;

	//cigar
	size_t sizeOf_cigar = sizeof(bwa_cigar_t);
	instance->cigar = (bwa_cigar_t*)malloc(sizeOf_cigar);
	memcpy(instance->cigar, offsettedBytes, sizeOf_cigar);
	offsettedBytes += sizeOf_cigar;
	//mdLengthIndicator_s
	size_t mdLengthIndicator_s = sizeof(int);
	int* mdLength = (int*)malloc(mdLengthIndicator_s);
	memcpy(mdLength, offsettedBytes, mdLengthIndicator_s);
	offsettedBytes += mdLengthIndicator_s;
	//md
	size_t sizeOf_md = *mdLength*sizeof(char);
	if(sizeOf_md != 0)
	{
		instance->md = (char*)malloc(sizeOf_md);
		memcpy(instance->md, offsettedBytes, sizeOf_md);
		offsettedBytes += sizeOf_md;
	}else {
		instance->md = '\0';
	}

	//restore null pointers ----------------------------------------------
	if(IsEmptyBytes((ubyte_t*)instance->aln, sizeof(bwt_aln1_t)))
	{
		free(instance->aln);
		instance->aln = NULL;
	}

	if(IsEmptyBytes((ubyte_t*)instance->multi, sizeof(bwt_multi1_t)))
	{
		free(instance->multi);
		instance->multi = NULL;
	}

	if(IsEmptyBytes((ubyte_t*)instance->cigar, sizeof(bwa_cigar_t)))
	{
		free(instance->cigar);
		instance->cigar = NULL;
	}

	if(IsEmptyBytes((ubyte_t*)instance->qual, sizeOf_qual))
	{
		free(instance->qual);
		instance->qual = NULL;
	}

	return instance;
}

int IsEqual(ubyte_t* bytes1, ubyte_t* bytes2,int l1, int l2)
{
	if (l1 != l2)
	{
		return 0;
	}

	int isMatch = 1;

	size_t idx ;
	for (idx = 0; idx < l1 && isMatch; idx++)
	{
		if ((idx >=0 && idx <=31)
			|| (idx >= 52 && idx <= 59)
			|| (idx >= 64 && idx <= 71)
			|| (idx >= 104 && idx <= 111)
			|| (idx >=184 && idx <=191))
		{
			//pointer index don't bother comparing...
			continue;
		}
		ubyte_t byte1 = bytes1[idx];
		ubyte_t byte2 = bytes2[idx];
		isMatch = byte1 == byte2;
		//TODO:Exclude pointers address from comparison

	}


	/*
	while (--n>0 && a[n] == a[0]);
	return n != 0;*/
	return isMatch;
}


void gap_destroy_stack(gap_stack_t *stack)
{
	int i;
	for (i = 0; i != stack->n_stacks; ++i) free(stack->stacks[i].stack);
	free(stack->stacks);
	free(stack);
}

static void gap_reset_stack(gap_stack_t *stack)
{
	int i;
	for (i = 0; i != stack->n_stacks; ++i)
		stack->stacks[i].n_entries = 0;
	stack->best = stack->n_stacks;
	stack->n_entries = 0;
}


static inline void gap_push(gap_stack_t *stack, int i, bwtint_t k, bwtint_t l, int n_mm, int n_gapo, int n_gape, int n_ins, int n_del,
							int state, int is_diff, const gap_opt_t *opt)
{
	int score;
	gap_entry_t *p;
	gap_stack1_t *q;
	score = aln_score(n_mm, n_gapo, n_gape, opt);
	q = stack->stacks + score;
	if (q->n_entries == q->m_entries) {
		q->m_entries = q->m_entries? q->m_entries<<1 : 4;
		q->stack = (gap_entry_t*)realloc(q->stack, sizeof(gap_entry_t) * q->m_entries);
	}
	p = q->stack + q->n_entries;
	p->info = (u_int32_t)score<<21 | i; p->k = k; p->l = l;
	p->n_mm = n_mm; p->n_gapo = n_gapo; p->n_gape = n_gape;
	p->n_ins = n_ins; p->n_del = n_del;
	p->state = state; 
	p->last_diff_pos = is_diff? i : 0;
	++(q->n_entries);
	++(stack->n_entries);
	if (stack->best > score) stack->best = score;
}

static inline void gap_pop(gap_stack_t *stack, gap_entry_t *e)
{
	gap_stack1_t *q;
	q = stack->stacks + stack->best;
	*e = q->stack[q->n_entries - 1];
	--(q->n_entries);
	--(stack->n_entries);
	if (q->n_entries == 0 && stack->n_entries) { // reset best
		int i;
		for (i = stack->best + 1; i < stack->n_stacks; ++i)
			if (stack->stacks[i].n_entries != 0) break;
		stack->best = i;
	} else if (stack->n_entries == 0) stack->best = stack->n_stacks;
}

static inline void gap_shadow(int x, int len, bwtint_t max, int last_diff_pos, bwt_width_t *w)
{
	int i, j;
	for (i = j = 0; i < last_diff_pos; ++i) {
		if (w[i].w > x) w[i].w -= x;
		else if (w[i].w == x) {
			w[i].bid = 1;
			w[i].w = max - (++j);
		} // else should not happen
	}
}
static inline int int_log2(uint32_t v)
{
	int c = 0;
	if (v & 0xffff0000u) { v >>= 16; c |= 16; }
	if (v & 0xff00) { v >>= 8; c |= 8; }
	if (v & 0xf0) { v >>= 4; c |= 4; }
	if (v & 0xc) { v >>= 2; c |= 2; }
	if (v & 0x2) c |= 1;
	return c;
}


static inline int __occ_aux(uint64_t y, int c)
{
	// reduce nucleotide counting to bits counting
	y = ((c&2)? y : ~y) >> 1 & ((c&1)? y : ~y) & 0x5555555555555555ull;
	// count the number of 1s in y
	y = (y & 0x3333333333333333ull) + (y >> 2 & 0x3333333333333333ull);
	return ((y + (y >> 4)) & 0xf0f0f0f0f0f0f0full) * 0x101010101010101ull >> 56;
}

bwtint_t bwt_occ(const bwt_t *bwt, bwtint_t k, ubyte_t c)
{
	bwtint_t n;
	uint32_t *p, *end;

	if (k == bwt->seq_len) return bwt->L2[c+1] - bwt->L2[c];
	if (k == (bwtint_t)(-1)) return 0;
	k -= (k >= bwt->primary); // because $ is not in bwt

	// retrieve Occ at k/OCC_INTERVAL
	n = ((bwtint_t*)(p = bwt_occ_intv(bwt, k)))[c];
	p += sizeof(bwtint_t); // jump to the start of the first BWT cell

	// calculate Occ up to the last k/32
	end = p + (((k>>5) - ((k&~OCC_INTV_MASK)>>5))<<1);
	for (; p < end; p += 2) n += __occ_aux((uint64_t)p[0]<<32 | p[1], c);

	// calculate Occ
	n += __occ_aux(((uint64_t)p[0]<<32 | p[1]) & ~((1ull<<((~k&31)<<1)) - 1), c);
	if (c == 0) n -= ~k&31; // corrected for the masked bits

	return n;
}


// an analogy to bwt_occ() but more efficient, requiring k <= l
void bwt_2occ(const bwt_t *bwt, bwtint_t k, bwtint_t l, ubyte_t c, bwtint_t *ok, bwtint_t *ol)
{
	bwtint_t _k, _l;
	_k = (k >= bwt->primary)? k-1 : k;
	_l = (l >= bwt->primary)? l-1 : l;
	if (_l/OCC_INTERVAL != _k/OCC_INTERVAL || k == (bwtint_t)(-1) || l == (bwtint_t)(-1)) {
		*ok = bwt_occ(bwt, k, c);
		*ol = bwt_occ(bwt, l, c);
	} else {
		bwtint_t m, n, i, j;
		uint32_t *p;
		if (k >= bwt->primary) --k;
		if (l >= bwt->primary) --l;
		n = ((bwtint_t*)(p = bwt_occ_intv(bwt, k)))[c];
		p += sizeof(bwtint_t);
		// calculate *ok
		j = k >> 5 << 5;
		for (i = k/OCC_INTERVAL*OCC_INTERVAL; i < j; i += 32, p += 2)
			n += __occ_aux((uint64_t)p[0]<<32 | p[1], c);
		m = n;
		n += __occ_aux(((uint64_t)p[0]<<32 | p[1]) & ~((1ull<<((~k&31)<<1)) - 1), c);
		if (c == 0) n -= ~k&31; // corrected for the masked bits
		*ok = n;
		// calculate *ol
		j = l >> 5 << 5;
		for (; i < j; i += 32, p += 2)
			m += __occ_aux((uint64_t)p[0]<<32 | p[1], c);
		m += __occ_aux(((uint64_t)p[0]<<32 | p[1]) & ~((1ull<<((~l&31)<<1)) - 1), c);
		if (c == 0) m -= ~l&31; // corrected for the masked bits
		*ol = m;
	}
}

#define __occ_aux4(bwt, b)											\
	((bwt)->cnt_table[(b)&0xff] + (bwt)->cnt_table[(b)>>8&0xff]		\
	+ (bwt)->cnt_table[(b)>>16&0xff] + (bwt)->cnt_table[(b)>>24])

void bwt_occ4(const bwt_t *bwt, bwtint_t k, bwtint_t cnt[4])
{
	bwtint_t x;
	uint32_t *p, tmp, *end;
	if (k == (bwtint_t)(-1)) {
		memset(cnt, 0, 4 * sizeof(bwtint_t));
		return;
	}
	k -= (k >= bwt->primary); // because $ is not in bwt
	p = bwt_occ_intv(bwt, k);
	memcpy(cnt, p, 4 * sizeof(bwtint_t));
	p += sizeof(bwtint_t); // sizeof(bwtint_t) = 4*(sizeof(bwtint_t)/sizeof(uint32_t))
	end = p + ((k>>4) - ((k&~OCC_INTV_MASK)>>4)); // this is the end point of the following loop
	for (x = 0; p < end; ++p) x += __occ_aux4(bwt, *p);
	tmp = *p & ~((1U<<((~k&15)<<1)) - 1);
	x += __occ_aux4(bwt, tmp) - (~k&15);
	cnt[0] += x&0xff; cnt[1] += x>>8&0xff; cnt[2] += x>>16&0xff; cnt[3] += x>>24;
}


// an analogy to bwt_occ4() but more efficient, requiring k <= l
void bwt_2occ4(const bwt_t *bwt, bwtint_t k, bwtint_t l, bwtint_t cntk[4], bwtint_t cntl[4])
{
	bwtint_t _k, _l;
	_k = k - (k >= bwt->primary);
	_l = l - (l >= bwt->primary);
	if (_l>>OCC_INTV_SHIFT != _k>>OCC_INTV_SHIFT || k == (bwtint_t)(-1) || l == (bwtint_t)(-1)) {
		bwt_occ4(bwt, k, cntk);
		bwt_occ4(bwt, l, cntl);
	} else {
		bwtint_t x, y;
		uint32_t *p, tmp, *endk, *endl;
		k -= (k >= bwt->primary); // because $ is not in bwt
		l -= (l >= bwt->primary);
		p = bwt_occ_intv(bwt, k);
		memcpy(cntk, p, 4 * sizeof(bwtint_t));
		p += sizeof(bwtint_t); // sizeof(bwtint_t) = 4*(sizeof(bwtint_t)/sizeof(uint32_t))
		// prepare cntk[]
		endk = p + ((k>>4) - ((k&~OCC_INTV_MASK)>>4));
		endl = p + ((l>>4) - ((l&~OCC_INTV_MASK)>>4));
		for (x = 0; p < endk; ++p) x += __occ_aux4(bwt, *p);
		y = x;
		tmp = *p & ~((1U<<((~k&15)<<1)) - 1);
		x += __occ_aux4(bwt, tmp) - (~k&15);
		// calculate cntl[] and finalize cntk[]
		for (; p < endl; ++p) y += __occ_aux4(bwt, *p);
		tmp = *p & ~((1U<<((~l&15)<<1)) - 1);
		y += __occ_aux4(bwt, tmp) - (~l&15);
		memcpy(cntl, cntk, 4 * sizeof(bwtint_t));
		cntk[0] += x&0xff; cntk[1] += x>>8&0xff; cntk[2] += x>>16&0xff; cntk[3] += x>>24;
		cntl[0] += y&0xff; cntl[1] += y>>8&0xff; cntl[2] += y>>16&0xff; cntl[3] += y>>24;
	}
}

int bwt_match_exact_alt(const bwt_t *bwt, int len, const ubyte_t *str, bwtint_t *k0, bwtint_t *l0)
{
	int i;
	bwtint_t k, l, ok, ol;
	k = *k0; l = *l0;
	for (i = len - 1; i >= 0; --i) {
		ubyte_t c = str[i];
		if (c > 3) return 0; // there is an N here. no match
		bwt_2occ(bwt, k - 1, l, c, &ok, &ol);
		k = bwt->L2[c] + ok + 1;
		l = bwt->L2[c] + ol;
		if (k > l) return 0; // no match
	}
	*k0 = k; *l0 = l;
	return l - k + 1;
}


// width must be filled as zero
int bwt_cal_width(const bwt_t *bwt, int len, const ubyte_t *str, bwt_width_t *width)
{
	bwtint_t k, l, ok, ol;
	int i, bid;
	bid = 0;
	k = 0; l = bwt->seq_len;
	for (i = 0; i < len; ++i) {
		ubyte_t c = str[i];
		if (c < 4) {
			bwt_2occ(bwt, k - 1, l, c, &ok, &ol);
			k = bwt->L2[c] + ok + 1;
			l = bwt->L2[c] + ol;
		}
		if (k > l || c > 3) { // then restart
			k = 0;
			l = bwt->seq_len;
			++bid;
		}
		width[i].w = l - k + 1;
		width[i].bid = bid;
	}
	width[len].w = 0;
	width[len].bid = ++bid;
	return bid;
}

gap_stack_t *gap_init_stack2(int max_score)
{
	gap_stack_t *stack;
	stack = (gap_stack_t*)calloc(1, sizeof(gap_stack_t));
	stack->n_stacks = max_score;
	stack->stacks = (gap_stack1_t*)calloc(stack->n_stacks, sizeof(gap_stack1_t));
	return stack;
}

gap_stack_t *gap_init_stack(int max_mm, int max_gapo, int max_gape, const gap_opt_t *opt)
{
	return gap_init_stack2(aln_score(max_mm+1, max_gapo+1, max_gape+1, opt));
}


int bwa_cal_maxdiff(int l, double err, double thres)
{
	double elambda = exp(-l * err); 
	double sum, y = 1.0;
	int k, x = 1;
	for (k = 1, sum = elambda; k < 1000; ++k) {
		y *= l * err;
		x *= k;
		sum += elambda * y / x;
		if (1.0 - sum < thres) return k;
	}
	return 2;
}

static bwtint_t fread_fix(FILE *fp, bwtint_t size, void *a)
{ // Mac/Darwin has a bug when reading data longer than 2GB. This function fixes this issue by reading data in small chunks
	const int bufsize = 0x1000000; // 16M block
	bwtint_t offset = 0;
	while (size) {
		int x = bufsize < size? bufsize : size;
		if ((x = err_fread_noeof(/*The casting was added by avi fot allowing the pointer arithmathic under VS cpp compiler*/(char *)a + offset, 1, x, fp)) == 0) break;
		size -= x; offset += x;
	}
	return offset;
}

void bwt_gen_cnt_table(bwt_t *bwt)
{
	int i, j;
	for (i = 0; i != 256; ++i) {
		uint32_t x = 0;
		for (j = 0; j != 4; ++j)
			x |= (((i&3) == j) + ((i>>2&3) == j) + ((i>>4&3) == j) + (i>>6 == j)) << (j<<3);
		bwt->cnt_table[i] = x;
	}
}



bwt_t *bwt_restore_bwt(const char *fn)
{
	bwt_t *bwt;
	FILE *fp;

	bwt = (bwt_t*)calloc(1, sizeof(bwt_t));
	fp = xopen(fn, "rb");
	
	
	//err_fseek(fp, 0, SEEK_END);
	//bwtint_t size = (err_ftell(fp) - sizeof(bwtint_t) * 5) >> 2;
	//bwt->bwt_size = size;
	bwt->bwt_size = 19519320;// 775451201;

	bwt->bwt = (uint32_t*)calloc(bwt->bwt_size, 4);
	err_fseek(fp, 0, SEEK_SET);
	err_fread_noeof(&bwt->primary, sizeof(bwtint_t), 1, fp);
	err_fread_noeof(bwt->L2+1, sizeof(bwtint_t), 4, fp);
	fread_fix(fp, bwt->bwt_size<<2, bwt->bwt);
	bwt->seq_len = bwt->L2[4];
	err_fclose(fp);
	bwt_gen_cnt_table(bwt);

	return bwt;
}



bwt_aln1_t *bwt_match_gap(bwt_t *const bwt, int len, const ubyte_t *seq, bwt_width_t *width,
						  bwt_width_t *seed_width, const gap_opt_t *opt, int *_n_aln, gap_stack_t *stack)
{ // $seq is the reverse complement of the input read
	int best_score = aln_score(opt->max_diff+1, opt->max_gapo+1, opt->max_gape+1, opt);
	int best_diff = opt->max_diff + 1, max_diff = opt->max_diff;
	int best_cnt = 0;
	int max_entries = 0, j, _j, n_aln, m_aln;
	bwt_aln1_t *aln;

	m_aln = 4; n_aln = 0;
	aln = (bwt_aln1_t*)calloc(m_aln, sizeof(bwt_aln1_t));

	// check whether there are too many N
	for (j = _j = 0; j < len; ++j)
		if (seq[j] > 3) ++_j;
	if (_j > max_diff) {
		*_n_aln = n_aln;
		return aln;
	}

	//for (j = 0; j != len; ++j) printf("#0 %d: [%d,%u]\t[%d,%u]\n", j, w[0][j].bid, w[0][j].w, w[1][j].bid, w[1][j].w);
	gap_reset_stack(stack); // reset stack
	gap_push(stack, len, 0, bwt->seq_len, 0, 0, 0, 0, 0, 0, 0, opt);

	while (stack->n_entries) {
		gap_entry_t e;
		int i, m, m_seed = 0, hit_found, allow_diff, allow_M, tmp;
		bwtint_t k, l, cnt_k[4], cnt_l[4], occ;

		if (max_entries < stack->n_entries) max_entries = stack->n_entries;
		if (stack->n_entries > opt->max_entries) break;
		gap_pop(stack, &e); // get the best entry
		k = e.k; l = e.l; // SA interval
		i = e.info&0xffff; // length
		if (!(opt->mode & BWA_MODE_NONSTOP) && e.info>>21 > best_score + opt->s_mm) break; // no need to proceed

		m = max_diff - (e.n_mm + e.n_gapo);
		if (opt->mode & BWA_MODE_GAPE) m -= e.n_gape;
		if (m < 0) continue;
		if (seed_width) { // apply seeding
			m_seed = opt->max_seed_diff - (e.n_mm + e.n_gapo);
			if (opt->mode & BWA_MODE_GAPE) m_seed -= e.n_gape;
		}
		//printf("#1\t[%d,%d,%d,%c]\t[%d,%d,%d]\t[%u,%u]\t[%u,%u]\t%d\n", stack->n_entries, a, i, "MID"[e.state], e.n_mm, e.n_gapo, e.n_gape, width[i-1].bid, width[i-1].w, k, l, e.last_diff_pos);
		if (i > 0 && m < width[i-1].bid) continue;

		// check whether a hit is found
		hit_found = 0;
		if (i == 0) hit_found = 1;
		else if (m == 0 && (e.state == STATE_M || (opt->mode&BWA_MODE_GAPE) || e.n_gape == opt->max_gape)) { // no diff allowed
			if (bwt_match_exact_alt(bwt, i, seq, &k, &l)) hit_found = 1;
			else continue; // no hit, skip
		}

		if (hit_found) { // action for found hits
			int score = aln_score(e.n_mm, e.n_gapo, e.n_gape, opt);
			int do_add = 1;
			//printf("#2 hits found: %d:(%u,%u)\n", e.n_mm+e.n_gapo, k, l);
			if (n_aln == 0) {
				best_score = score;
				best_diff = e.n_mm + e.n_gapo;
				if (opt->mode & BWA_MODE_GAPE) best_diff += e.n_gape;
				if (!(opt->mode & BWA_MODE_NONSTOP))
					max_diff = (best_diff + 1 > opt->max_diff)? opt->max_diff : best_diff + 1; // top2 behaviour
			}
			if (score == best_score) best_cnt += l - k + 1;
			else if (best_cnt > opt->max_top2) break; // top2b behaviour
			if (e.n_gapo) { // check whether the hit has been found. this may happen when a gap occurs in a tandem repeat
				for (j = 0; j != n_aln; ++j)
					if (aln[j].k == k && aln[j].l == l) break;
				if (j < n_aln) do_add = 0;
			}
			if (do_add) { // append
				bwt_aln1_t *p;
				gap_shadow(l - k + 1, len, bwt->seq_len, e.last_diff_pos, width);
				if (n_aln == m_aln) {
					m_aln <<= 1;
					aln = (bwt_aln1_t*)realloc(aln, m_aln * sizeof(bwt_aln1_t));
					memset(aln + m_aln/2, 0, m_aln/2*sizeof(bwt_aln1_t));
				}
				p = aln + n_aln;
				p->n_mm = e.n_mm; p->n_gapo = e.n_gapo; p->n_gape = e.n_gape;
				p->n_ins = e.n_ins; p->n_del = e.n_del;
				p->k = k; p->l = l;
				p->score = score;
				//fprintf(stderr, "*** n_mm=%d,n_gapo=%d,n_gape=%d,n_ins=%d,n_del=%d\n", e.n_mm, e.n_gapo, e.n_gape, e.n_ins, e.n_del);
				++n_aln;
			}
			continue;
		}

		--i;
		bwt_2occ4(bwt, k - 1, l, cnt_k, cnt_l); // retrieve Occ values
		occ = l - k + 1;
		// test whether diff is allowed
		allow_diff = allow_M = 1;
		if (i > 0) {
			int ii = i - (len - opt->seed_len);
			if (width[i-1].bid > m-1) allow_diff = 0;
			else if (width[i-1].bid == m-1 && width[i].bid == m-1 && width[i-1].w == width[i].w) allow_M = 0;
			if (seed_width && ii > 0) {
				if (seed_width[ii-1].bid > m_seed-1) allow_diff = 0;
				else if (seed_width[ii-1].bid == m_seed-1 && seed_width[ii].bid == m_seed-1
					&& seed_width[ii-1].w == seed_width[ii].w) allow_M = 0;
			}
		}
		// indels
		tmp = (opt->mode & BWA_MODE_LOGGAP)? int_log2(e.n_gape + e.n_gapo)/2+1 : e.n_gapo + e.n_gape;
		if (allow_diff && i >= opt->indel_end_skip + tmp && len - i >= opt->indel_end_skip + tmp) {
			if (e.state == STATE_M) { // gap open
				if (e.n_gapo < opt->max_gapo) { // gap open is allowed
					// insertion
					gap_push(stack, i, k, l, e.n_mm, e.n_gapo + 1, e.n_gape, e.n_ins + 1, e.n_del, STATE_I, 1, opt);
					// deletion
					for (j = 0; j != 4; ++j) {
						k = bwt->L2[j] + cnt_k[j] + 1;
						l = bwt->L2[j] + cnt_l[j];
						if (k <= l) gap_push(stack, i + 1, k, l, e.n_mm, e.n_gapo + 1, e.n_gape, e.n_ins, e.n_del + 1, STATE_D, 1, opt);
					}
				}
			} else if (e.state == STATE_I) { // extention of an insertion
				if (e.n_gape < opt->max_gape) // gap extention is allowed
					gap_push(stack, i, k, l, e.n_mm, e.n_gapo, e.n_gape + 1, e.n_ins + 1, e.n_del, STATE_I, 1, opt);
			} else if (e.state == STATE_D) { // extention of a deletion
				if (e.n_gape < opt->max_gape) { // gap extention is allowed
					if (e.n_gape + e.n_gapo < max_diff || occ < opt->max_del_occ) {
						for (j = 0; j != 4; ++j) {
							k = bwt->L2[j] + cnt_k[j] + 1;
							l = bwt->L2[j] + cnt_l[j];
							if (k <= l) gap_push(stack, i + 1, k, l, e.n_mm, e.n_gapo, e.n_gape + 1, e.n_ins, e.n_del + 1, STATE_D, 1, opt);
						}
					}
				}
			}
		}
		// mismatches
		if (allow_diff && allow_M) { // mismatch is allowed
			for (j = 1; j <= 4; ++j) {
				int c = (seq[i] + j) & 3;
				int is_mm = (j != 4 || seq[i] > 3);
				k = bwt->L2[c] + cnt_k[c] + 1;
				l = bwt->L2[c] + cnt_l[c];
				if (k <= l) gap_push(stack, i, k, l, e.n_mm + is_mm, e.n_gapo, e.n_gape, e.n_ins, e.n_del, STATE_M, is_mm, opt);
			}
		} else if (seq[i] < 4) { // try exact match only
			int c = seq[i] & 3;
			k = bwt->L2[c] + cnt_k[c] + 1;
			l = bwt->L2[c] + cnt_l[c];
			if (k <= l) gap_push(stack, i, k, l, e.n_mm, e.n_gapo, e.n_gape, e.n_ins, e.n_del, STATE_M, 0, opt);
		}
	}

	*_n_aln = n_aln;
	//fprintf(stderr, "max_entries = %d\n", max_entries);
	return aln;
}



void bwa_cal_sa_reg_gapKernel(int tid, bwt_t *const bwt, int n_seqs, bwa_seq_t *seqs, const gap_opt_t *opt)
{
	int i, j, max_l = 0, max_len;
	gap_stack_t *stack;
	bwt_width_t *w, *seed_w;
	gap_opt_t local_opt = *opt;

	// initiate priority stack
	for (i = max_len = 0; i != n_seqs; ++i)
		if (seqs[i].len > max_len) max_len = seqs[i].len;
	if (opt->fnr > 0.0) local_opt.max_diff = bwa_cal_maxdiff(max_len, BWA_AVG_ERR, opt->fnr);
	if (local_opt.max_diff < local_opt.max_gapo) local_opt.max_gapo = local_opt.max_diff;
	stack = gap_init_stack(local_opt.max_diff, local_opt.max_gapo, local_opt.max_gape, &local_opt);

	seed_w = (bwt_width_t*)calloc(opt->seed_len+1, sizeof(bwt_width_t));
	w = 0;
	for (i = 0; i != n_seqs; ++i) {
		bwa_seq_t *p = seqs + i;

		//--------------------------------------------------------------

		// 		int bytesLength1;
		// 		ubyte_t *bytes1 = ToBytes(p, &bytesLength1);
		// 		bwa_seq_t* clone = (bwa_seq_t* )FromBytes(bytes1);
		// 		int bytesLength2;
		// 		ubyte_t *bytes2 = ToBytes(clone,&bytesLength2);
		// 		int isEqual = IsEqual(bytes1, bytes2, bytesLength1, bytesLength2);
		// 		isEqual  = isEqual &&isEqual ;
		//bwa_seq_t* doubleClone = (bwa_seq_t* )FromBytes(bytes2);
		//int bytesLength3;
		//ubyte_t *bytes3 = ToBytes(doubleClone ,&bytesLength3);
		//int isEqual2 = IsEqual(bytes2, bytes3, bytesLength2, bytesLength3);
		//isEqual2 = isEqual && isEqual2;


		//		p = clone;


		//-------------------------
		int bytesLength;
		ubyte_t* bytes = ToBytes(p,&bytesLength);
		char postFileName[1024];
		sprintf(postFileName,"calculatedPost\\sequence_post (%d).bin",i+1);
		FILE *pFile;
		pFile = fopen (postFileName, "wb");
		fwrite (bytes , sizeof(char), bytesLength, pFile);
		fclose (pFile);
		//-------------------------

		//--------------------------------------------------------------



#ifdef HAVE_PTHREAD
		if (i % opt->n_threads != tid) continue;
#endif
		p->sa = 0; p->type = BWA_TYPE_NO_MATCH; p->c1 = p->c2 = 0; p->n_aln = 0; p->aln = 0;
		if (max_l < p->len) {
			max_l = p->len;
			w = (bwt_width_t*)realloc(w, (max_l + 1) * sizeof(bwt_width_t));
			memset(w, 0, (max_l + 1) * sizeof(bwt_width_t));
		}
		bwt_cal_width(bwt, p->len, p->seq, w);
		if (opt->fnr > 0.0) local_opt.max_diff = bwa_cal_maxdiff(p->len, BWA_AVG_ERR, opt->fnr);
		local_opt.seed_len = opt->seed_len < p->len? opt->seed_len : 0x7fffffff;
		if (p->len > opt->seed_len)
			bwt_cal_width(bwt, opt->seed_len, p->seq + (p->len - opt->seed_len), seed_w);
		// core function
		for (j = 0; j < p->len; ++j) // we need to complement
			p->seq[j] = p->seq[j] > 3? 4 : 3 - p->seq[j];  //Avi: Building the reverse complement of the input read
		p->aln = bwt_match_gap(bwt, p->len, p->seq, w, p->len <= opt->seed_len? 0 : seed_w, &local_opt, &p->n_aln, stack);
		//fprintf(stderr, "mm=%lld,ins=%lld,del=%lld,gapo=%lld\n", p->aln->n_mm, p->aln->n_ins, p->aln->n_del, p->aln->n_gapo);
		// clean up the unused data in the record
		free(p->name);
		free(p->seq);
		free(p->rseq);
		free(p->qual);
		p->name = 0; p->seq = p->rseq = p->qual = 0;
	}
	free(seed_w); free(w);
	gap_destroy_stack(stack);
}



