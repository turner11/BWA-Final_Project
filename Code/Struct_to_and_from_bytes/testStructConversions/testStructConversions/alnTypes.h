#ifndef ALN_TYPE_H
#define ALN_TYPE_H

typedef __int64 bwtint_t;
typedef unsigned char ubyte_t;
typedef unsigned __int32 uint32_t;
typedef unsigned __int64 uint64_t;
typedef unsigned __int16 bwa_cigar_t;

#define BWA_MAX_BCLEN 63 // maximum barcode length; 127 is the maximum


typedef struct {
	uint64_t n_mm : 8, n_gapo : 8, n_gape : 8, score : 20, n_ins : 10, n_del : 10;
	bwtint_t k, l;
} bwt_aln1_t;

typedef struct {
	uint32_t n_cigar : 15, gap : 8, mm : 8, strand : 1;
	int ref_shift;
	bwtint_t pos;
	bwa_cigar_t *cigar;
} bwt_multi1_t;

typedef struct {
	char *name;
	ubyte_t *seq, *rseq, *qual;
	uint32_t len : 20, strand : 1, type : 2, dummy : 1, extra_flag : 8;
	uint32_t n_mm : 8, n_gapo : 8, n_gape : 8, mapQ : 8;
	int score;
	int clip_len;
	// alignments in SA coordinates
	int n_aln;
	bwt_aln1_t *aln;
	// multiple hits
	int n_multi;
	bwt_multi1_t *multi;
	// alignment information
	bwtint_t sa, pos;
	uint64_t c1 : 28, c2 : 28, seQ : 8; // number of top1 and top2 hits; single-end mapQ
	int ref_shift;
	int n_cigar;
	bwa_cigar_t *cigar;
	// for multi-threading only
	int tid;
	// barcode
	char bc[BWA_MAX_BCLEN + 1]; // null terminated; up to BWA_MAX_BCLEN bases
	// NM and MD tags
	uint32_t full_len : 20, nm : 12;
	char *md;
	
	/*Added Functions for byte conversions*/
	ubyte_t *ToBytes(int *bytesLength);

	static void *FromBytes(ubyte_t* bytes);

	static int IsEqual(ubyte_t * first, ubyte_t * second, int l1, int l2);
	
} bwa_seq_t;

#endif