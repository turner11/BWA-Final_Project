#include <stdio.h>
#include <unistd.h>
#include <math.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <stdint.h>
#ifdef HAVE_CONFIG_H
#include "config.h"
#endif
#include "bwtaln.h"
#include "bwtgap.h"
#include "utils.h"
#include "bwa.h"


#ifdef HAVE_PTHREAD
#include <pthread.h>
#endif

#ifdef USE_MALLOC_WRAPPERS
#  include "malloc_wrap.h"
#endif



ubyte_t *ToBytes(bwa_seq_t *this, int *bytesLength)
{
	/*Calculate total size of bytes to convert*/
	size_t mySize = sizeof(bwa_seq_t);//size of this struct (excluding referenced objects)
	//Name + size indicator
	size_t nameLengthIndicator_s = sizeof(int); //Number to hold name length
	size_t nameLength = strlen(this->name)+1;//Will hold length of name (+1 for null termination)
	size_t nameCharSize = nameLength * sizeof(char);//number of chars for name
	//referenced objects sizes
	size_t sizeOf_bwt_aln1_t = sizeof(bwt_aln1_t);
	int isAlnNull = this->aln == '\0';
	size_t sizeOf_bwt_bwt_multi1_t = sizeof(bwt_multi1_t);

	/*rest of pointers*/
	size_t sizeOf_seq			= sizeof(ubyte_t);
	size_t sizeOf_rseq			= sizeof(ubyte_t);
	size_t sizeOf_qual			= sizeof(ubyte_t);
	size_t sizeOf_cigar			= sizeof(bwa_cigar_t);
	size_t mdLengthIndicator_s	= sizeof(int);
	size_t md_length			= 0;
	if(this->md != '\0')
	{
		md_length			= strlen(this->md) + 1; //null terminated
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
	memcpy(offsettedBytes, this, mySize);
	offsettedBytes += mySize;

	// size of name
	memcpy(offsettedBytes, &nameLength, nameLengthIndicator_s);
	offsettedBytes += nameLengthIndicator_s;
	//Name
	memcpy(offsettedBytes, this->name, nameCharSize);
	offsettedBytes += nameCharSize;
	//bwt_aln1_t
	bwt_aln1_t dummyAln;
	bwt_aln1_t *alnObj = (isAlnNull == 1)?&dummyAln:this->aln;
	memcpy(offsettedBytes, alnObj , sizeOf_bwt_aln1_t);
	offsettedBytes += sizeOf_bwt_aln1_t;
	//bwt_multi1_t
	int isMultiAlnNull = this->multi == '\0';
	bwt_multi1_t dummyMultiAln;
	bwt_multi1_t *alnMultiObj = (isMultiAlnNull  == 1)?&dummyMultiAln:this->multi;
	memcpy(offsettedBytes, alnMultiObj, sizeOf_bwt_bwt_multi1_t);
	offsettedBytes += sizeOf_bwt_bwt_multi1_t;
	//seq
	memcpy(offsettedBytes, this->seq, sizeOf_seq);
	offsettedBytes += sizeOf_seq;
	//rseq
	memcpy(offsettedBytes, this->rseq, sizeOf_rseq);
	offsettedBytes += sizeOf_rseq;
	//qual
	memcpy(offsettedBytes, this->qual, sizeOf_qual);
	offsettedBytes += sizeOf_qual;
	//cigar
	bwa_cigar_t dummyCiagar;
	bwa_cigar_t *cigarObj = this->cigar == '\0'? &dummyCiagar:this->cigar;
	memcpy(offsettedBytes, cigarObj , sizeOf_cigar);
	offsettedBytes += sizeOf_cigar;
	//mdLengthIndicator_s
	memcpy(offsettedBytes, &md_length, mdLengthIndicator_s);
	offsettedBytes += mdLengthIndicator_s;
	//md
	if(this->md != '\0')
	{
		memcpy(offsettedBytes, this->md, md_length);
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
	size_t sizeOf_seq = sizeof(ubyte_t);
	instance->seq = (ubyte_t*)malloc(sizeOf_seq);
	memcpy(instance->seq, offsettedBytes, sizeOf_seq);
	offsettedBytes += sizeOf_seq;
	//rseq
	size_t sizeOf_rseq = sizeof(ubyte_t);
	instance->rseq = (ubyte_t*)malloc(sizeOf_rseq);
	memcpy(instance->rseq, offsettedBytes, sizeOf_rseq);
	offsettedBytes += sizeOf_rseq;
	//qual
	size_t sizeOf_qual = sizeof(ubyte_t);
	instance->qual = (ubyte_t*)malloc(sizeOf_qual);
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

gap_opt_t *gap_init_opt()
{
	gap_opt_t *o;
	o = (gap_opt_t*)calloc(1, sizeof(gap_opt_t));
	/* IMPORTANT: s_mm*10 should be about the average base error
	   rate. Voilating this requirement will break pairing! */
	o->s_mm = 3; o->s_gapo = 11; o->s_gape = 4;
	o->max_diff = -1; o->max_gapo = 1; o->max_gape = 6;
	o->indel_end_skip = 5; o->max_del_occ = 10; o->max_entries = 2000000;
	o->mode = BWA_MODE_GAPE | BWA_MODE_COMPREAD;
	o->seed_len = 32; o->max_seed_diff = 2;
	o->fnr = 0.04;
	o->n_threads = 1;
	o->max_top2 = 30;
	o->trim_qual = 0;
	return o;
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

void bwa_cal_sa_reg_gap(int tid, bwt_t *const bwt, int n_seqs, bwa_seq_t *seqs, const gap_opt_t *opt)
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

#ifdef HAVE_PTHREAD
typedef struct {
	int tid;
	bwt_t *bwt;
	int n_seqs;
	bwa_seq_t *seqs;
	const gap_opt_t *opt;
} thread_aux_t;

static void *worker(void *data)
{
	thread_aux_t *d = (thread_aux_t*)data;
	bwa_cal_sa_reg_gap(d->tid, d->bwt, d->n_seqs, d->seqs, d->opt);
	return 0;
}
#endif

bwa_seqio_t *bwa_open_reads(int mode, const char *fn_fa)
{
	bwa_seqio_t *ks;
	if (mode & BWA_MODE_BAM) { // open BAM
		int which = 0;
		if (mode & BWA_MODE_BAM_SE) which |= 4;
		if (mode & BWA_MODE_BAM_READ1) which |= 1;
		if (mode & BWA_MODE_BAM_READ2) which |= 2;
		if (which == 0) which = 7; // then read all reads
		ks = bwa_bam_open(fn_fa, which);
	} else ks = bwa_seq_open(fn_fa);
	return ks;
}

void bwa_aln_core(const char *prefix, const char *fn_fa, const gap_opt_t *opt)
{
	int i, n_seqs, tot_seqs = 0;
	bwa_seq_t *seqs;
	bwa_seqio_t *ks;
	clock_t t;
	bwt_t *bwt;

	// initialization
	ks = bwa_open_reads(opt->mode, fn_fa);

	{ // load BWT
		char *str = (char*)calloc(strlen(prefix) + 10, 1);
		strcpy(str, prefix); strcat(str, ".bwt");  bwt = bwt_restore_bwt(str);
		free(str);
	}

	// core loop
	err_fwrite(SAI_MAGIC, 1, 4, stdout);
	err_fwrite(opt, sizeof(gap_opt_t), 1, stdout);
	while ((seqs = bwa_read_seq(ks, 0x40000, &n_seqs, opt->mode, opt->trim_qual)) != 0) {
		tot_seqs += n_seqs;



		int bytesLength1;
		ubyte_t *bytes1 = ToBytes(seqs, &bytesLength1);
		bwa_seq_t* clone = (bwa_seq_t* )FromBytes(bytes1);
		int bytesLength2;
		ubyte_t *bytes2 = ToBytes(clone,&bytesLength2);
		int isEqual = IsEqual(bytes1, bytes2, bytesLength1, bytesLength2);

		bwa_seq_t* doubleClone = (bwa_seq_t* )FromBytes(bytes2);
		int bytesLength3;
		ubyte_t *bytes3 = ToBytes(doubleClone ,&bytesLength3);
		int isEqual2 = IsEqual(bytes2, bytes3, bytesLength2, bytesLength3);
		isEqual2 = isEqual && isEqual2;
		/*-------------------------*/
		FILE * pFile;

		  pFile = fopen ("myfile.bin", "wb");
		  fwrite (bytes1 , sizeof(char), bytesLength1, pFile);
		  fclose (pFile);
		/*-------------------------*/
		seqs =clone;

		t = clock();

		fprintf(stderr, "[bwa_aln_core] calculate SA coordinate... ");

#ifdef HAVE_PTHREAD
		if (opt->n_threads <= 1) { // no multi-threading at all
			bwa_cal_sa_reg_gap(0, bwt, n_seqs, seqs, opt);
		} else {
			pthread_t *tid;
			pthread_attr_t attr;
			thread_aux_t *data;
			int j;
			pthread_attr_init(&attr);
			pthread_attr_setdetachstate(&attr, PTHREAD_CREATE_JOINABLE);
			data = (thread_aux_t*)calloc(opt->n_threads, sizeof(thread_aux_t));
			tid = (pthread_t*)calloc(opt->n_threads, sizeof(pthread_t));
			for (j = 0; j < opt->n_threads; ++j) {
				data[j].tid = j; data[j].bwt = bwt;
				data[j].n_seqs = n_seqs; data[j].seqs = seqs; data[j].opt = opt;
				pthread_create(&tid[j], &attr, worker, data + j);
			}
			for (j = 0; j < opt->n_threads; ++j) pthread_join(tid[j], 0);
			free(data); free(tid);
		}
#else


		bwa_cal_sa_reg_gap(0, bwt, n_seqs, clone, opt);
#endif

		fprintf(stderr, "%.2f sec\n", (float)(clock() - t) / CLOCKS_PER_SEC);

		t = clock();
		fprintf(stderr, "[bwa_aln_core] write to the disk... ");
		for (i = 0; i < n_seqs; ++i) {
			bwa_seq_t *p = seqs + i;
			err_fwrite(&p->n_aln, 4, 1, stdout);
			if (p->n_aln) err_fwrite(p->aln, sizeof(bwt_aln1_t), p->n_aln, stdout);
		}
		fprintf(stderr, "%.2f sec\n", (float)(clock() - t) / CLOCKS_PER_SEC);

		bwa_free_read_seq(n_seqs, seqs);
		fprintf(stderr, "[bwa_aln_core] %d sequences have been processed.\n", tot_seqs);
	}

	// destroy
	bwt_destroy(bwt);
	bwa_seq_close(ks);
}

int bwa_aln(int argc, char *argv[])
{
	int c, opte = -1;
	gap_opt_t *opt;
	char *prefix;

	opt = gap_init_opt();
	while ((c = getopt(argc, argv, "n:o:e:i:d:l:k:LR:m:t:NM:O:E:q:f:b012IYB:")) >= 0) {
		switch (c) {
		case 'n':
			if (strstr(optarg, ".")) opt->fnr = atof(optarg), opt->max_diff = -1;
			else opt->max_diff = atoi(optarg), opt->fnr = -1.0;
			break;
		case 'o': opt->max_gapo = atoi(optarg); break;
		case 'e': opte = atoi(optarg); break;
		case 'M': opt->s_mm = atoi(optarg); break;
		case 'O': opt->s_gapo = atoi(optarg); break;
		case 'E': opt->s_gape = atoi(optarg); break;
		case 'd': opt->max_del_occ = atoi(optarg); break;
		case 'i': opt->indel_end_skip = atoi(optarg); break;
		case 'l': opt->seed_len = atoi(optarg); break;
		case 'k': opt->max_seed_diff = atoi(optarg); break;
		case 'm': opt->max_entries = atoi(optarg); break;
		case 't': opt->n_threads = atoi(optarg); break;
		case 'L': opt->mode |= BWA_MODE_LOGGAP; break;
		case 'R': opt->max_top2 = atoi(optarg); break;
		case 'q': opt->trim_qual = atoi(optarg); break;
		case 'N': opt->mode |= BWA_MODE_NONSTOP; opt->max_top2 = 0x7fffffff; break;
		case 'f': xreopen(optarg, "wb", stdout); break;
		case 'b': opt->mode |= BWA_MODE_BAM; break;
		case '0': opt->mode |= BWA_MODE_BAM_SE; break;
		case '1': opt->mode |= BWA_MODE_BAM_READ1; break;
		case '2': opt->mode |= BWA_MODE_BAM_READ2; break;
		case 'I': opt->mode |= BWA_MODE_IL13; break;
		case 'Y': opt->mode |= BWA_MODE_CFY; break;
		case 'B': opt->mode |= atoi(optarg) << 24; break;
		default: return 1;
		}
	}
	if (opte > 0) {
		opt->max_gape = opte;
		opt->mode &= ~BWA_MODE_GAPE;
	}

	if (optind + 2 > argc) {
		fprintf(stderr, "\n");
		fprintf(stderr, "Usage:   bwa aln [options] <prefix> <in.fq>\n\n");
		fprintf(stderr, "Options: -n NUM    max #diff (int) or missing prob under %.2f err rate (float) [%.2f]\n",
				BWA_AVG_ERR, opt->fnr);
		fprintf(stderr, "         -o INT    maximum number or fraction of gap opens [%d]\n", opt->max_gapo);
		fprintf(stderr, "         -e INT    maximum number of gap extensions, -1 for disabling long gaps [-1]\n");
		fprintf(stderr, "         -i INT    do not put an indel within INT bp towards the ends [%d]\n", opt->indel_end_skip);
		fprintf(stderr, "         -d INT    maximum occurrences for extending a long deletion [%d]\n", opt->max_del_occ);
		fprintf(stderr, "         -l INT    seed length [%d]\n", opt->seed_len);
		fprintf(stderr, "         -k INT    maximum differences in the seed [%d]\n", opt->max_seed_diff);
		fprintf(stderr, "         -m INT    maximum entries in the queue [%d]\n", opt->max_entries);
		fprintf(stderr, "         -t INT    number of threads [%d]\n", opt->n_threads);
		fprintf(stderr, "         -M INT    mismatch penalty [%d]\n", opt->s_mm);
		fprintf(stderr, "         -O INT    gap open penalty [%d]\n", opt->s_gapo);
		fprintf(stderr, "         -E INT    gap extension penalty [%d]\n", opt->s_gape);
		fprintf(stderr, "         -R INT    stop searching when there are >INT equally best hits [%d]\n", opt->max_top2);
		fprintf(stderr, "         -q INT    quality threshold for read trimming down to %dbp [%d]\n", BWA_MIN_RDLEN, opt->trim_qual);
        fprintf(stderr, "         -f FILE   file to write output to instead of stdout\n");
		fprintf(stderr, "         -B INT    length of barcode\n");
		fprintf(stderr, "         -L        log-scaled gap penalty for long deletions\n");
		fprintf(stderr, "         -N        non-iterative mode: search for all n-difference hits (slooow)\n");
		fprintf(stderr, "         -I        the input is in the Illumina 1.3+ FASTQ-like format\n");
		fprintf(stderr, "         -b        the input read file is in the BAM format\n");
		fprintf(stderr, "         -0        use single-end reads only (effective with -b)\n");
		fprintf(stderr, "         -1        use the 1st read in a pair (effective with -b)\n");
		fprintf(stderr, "         -2        use the 2nd read in a pair (effective with -b)\n");
		fprintf(stderr, "         -Y        filter Casava-filtered sequences\n");
		fprintf(stderr, "\n");
		return 1;
	}
	if (opt->fnr > 0.0) {
		int i, k;
		for (i = 17, k = 0; i <= 250; ++i) {
			int l = bwa_cal_maxdiff(i, BWA_AVG_ERR, opt->fnr);
			if (l != k) fprintf(stderr, "[bwa_aln] %dbp reads: max_diff = %d\n", i, l);
			k = l;
		}
	}
	if ((prefix = bwa_idx_infer_prefix(argv[optind])) == 0) {
		fprintf(stderr, "[%s] fail to locate the index\n", __func__);
		free(opt);
		return 1;
	}
	bwa_aln_core(prefix, argv[optind+1], opt);
	free(opt); free(prefix);
	return 0;
}





