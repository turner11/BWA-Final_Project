#ifndef __BACKEND_H_
#define __BACKEND_H_

typedef __int64 bwtint_t;
typedef unsigned char ubyte_t;
typedef unsigned __int32 uint32_t;
typedef unsigned __int64 uint64_t;
typedef unsigned __int16 bwa_cigar_t;
typedef uint32_t u_int32_t;

#define BWA_MAX_BCLEN 63 // maximum barcode length; 127 is the maximum

#define BWA_MODE_GAPE       0x01
#define BWA_MODE_COMPREAD   0x02
#define BWA_MODE_LOGGAP     0x04
#define BWA_MODE_CFY        0x08
#define BWA_MODE_NONSTOP    0x10
#define BWA_MODE_BAM        0x20
#define BWA_MODE_BAM_SE     0x40
#define BWA_MODE_BAM_READ1  0x80
#define BWA_MODE_BAM_READ2  0x100
#define BWA_MODE_IL13       0x200

#define STATE_M 0
#define STATE_I 1
#define STATE_D 2

// requirement: (OCC_INTERVAL%16 == 0); please DO NOT change this line because some part of the code assume OCC_INTERVAL=0x80
#define OCC_INTV_SHIFT 7
#define OCC_INTERVAL   (1LL<<OCC_INTV_SHIFT)
#define OCC_INTV_MASK  (OCC_INTERVAL - 1)

#define BWA_AVG_ERR 0.02
#define BWA_MIN_RDLEN 35 // for read trimming

#define BWA_TYPE_NO_MATCH 0
#define BWA_TYPE_UNIQUE 1
#define BWA_TYPE_REPEAT 2
#define BWA_TYPE_MATESW 3


typedef struct {
	uint64_t n_mm:8, n_gapo:8, n_gape:8, score:20, n_ins:10, n_del:10;
	bwtint_t k, l;
} bwt_aln1_t;

typedef struct {
	uint32_t n_cigar:15, gap:8, mm:8, strand:1;
	int ref_shift;
	bwtint_t pos;
	bwa_cigar_t *cigar;
} bwt_multi1_t;

typedef struct {
	char *name;//0-7
	ubyte_t *seq, *rseq, *qual; //8-31
	uint32_t len:20, strand:1, type:2, dummy:1, extra_flag:8; //32-35
	uint32_t n_mm:8, n_gapo:8, n_gape:8, mapQ:8;//36-39
	int score;//40-43
	int clip_len;//44-47
	// alignments in SA coordinates
	int n_aln;//48-51
	bwt_aln1_t *aln;//52-59
	// multiple hits
	int n_multi;//60-63
	bwt_multi1_t *multi;//64-71
	// alignment information
	bwtint_t sa, pos;//72-87
	uint64_t c1:28, c2:28, seQ:8; // number of top1 and top2 hits; single-end mapQ //88-95
	int ref_shift;//96-99
	int n_cigar;//100-103
	bwa_cigar_t *cigar;//104-111
	// for multi-threading only
	int tid;//112-115
	// barcode
	char bc[BWA_MAX_BCLEN+1]; // null terminated; up to BWA_MAX_BCLEN bases //116-179
	// NM and MD tags
	uint32_t full_len:20, nm:12;//180-183
	char *md;//184-191
} bwa_seq_t;

typedef struct {
	bwtint_t primary; // S^{-1}(0), or the primary index of BWT
	bwtint_t L2[5]; // C(), cumulative count
	bwtint_t seq_len; // sequence length
	bwtint_t bwt_size; // size of bwt, about seq_len/4
	uint32_t *bwt; // BWT
	// occurance array, separated to two parts
	uint32_t cnt_table[256];
	// suffix array
	int sa_intv;
	bwtint_t n_sa;
	bwtint_t *sa;
} bwt_t;

typedef struct {
	bwtint_t w;
	int bid;
} bwt_width_t;

typedef struct {
	int s_mm, s_gapo, s_gape;
	int mode; // bit 24-31 are the barcode length
	int indel_end_skip, max_del_occ, max_entries;
	float fnr;
	int max_diff, max_gapo, max_gape;
	int max_seed_diff, seed_len;
	int n_threads;
	int max_top2;
	int trim_qual;
} gap_opt_t;

typedef struct { // recursion stack
	u_int32_t info; // score<<21 | i
	u_int32_t n_mm:8, n_gapo:8, n_gape:8, state:2, n_seed_mm:6;
	u_int32_t n_ins:16, n_del:16;
	int last_diff_pos;
	bwtint_t k, l; // (k,l) is the SA region of [i,n-1]
} gap_entry_t;


typedef struct {
	int n_entries, m_entries;
	gap_entry_t *stack;
} gap_stack1_t;

typedef struct {
	int n_stacks, best, n_entries;
	gap_stack1_t *stacks;
} gap_stack_t;

bwa_seq_t* FromBytes(ubyte_t* bytes);
ubyte_t *ToBytes(bwa_seq_t *thisP, int *bytesLength);
int IsEqual(ubyte_t* bytes1, ubyte_t* bytes2,int l1, int l2);

bwt_t *bwt_restore_bwt(const char *fn);

bwt_aln1_t *bwt_match_gap(bwt_t *const bwt, int len, const ubyte_t *seq, bwt_width_t *width,
						  bwt_width_t *seed_width, const gap_opt_t *opt, int *_n_aln, gap_stack_t *stack);

void bwa_cal_sa_reg_gapKernel(int tid, bwt_t *const bwt, int n_seqs, bwa_seq_t *seqs, const gap_opt_t *opt);




#endif