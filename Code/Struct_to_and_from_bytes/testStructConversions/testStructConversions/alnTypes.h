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
	char *name;//Bytes 0-3
	ubyte_t *seq, *rseq, *qual; //bytes 4-15
	uint32_t len : 20, strand : 1, type : 2, dummy : 1, extra_flag : 8;//Bytes 16-19
	uint32_t n_mm : 8, n_gapo : 8, n_gape : 8, mapQ : 8;//bytes 20 - 23
	int score;//bytes 24-27
	int clip_len;//bytes 28-31
	// alignments in SA coordinates
	int n_aln;//bytes 32-35
	bwt_aln1_t *aln; //bytes 36-39
	// multiple hits
	int n_multi;//bytes 40-43
	bwt_multi1_t *multi; //bytes 44-47
	// alignment information
	bwtint_t sa, pos; //bytes 48 - 63
	uint64_t c1 : 28, c2 : 28, seQ : 8; // number of top1 and top2 hits; single-end mapQ //bytes 64 - 71
	int ref_shift;//bytes 72-75
	int n_cigar;//bytes 76-79
	bwa_cigar_t *cigar;//bytes 80-83
	// for multi-threading only
	int tid;//bytes 84-87
	// barcode
	char bc[BWA_MAX_BCLEN + 1]; // null terminated; up to BWA_MAX_BCLEN bases //bytes 88-151
	// NM and MD tags
	uint32_t full_len : 20, nm : 12;//bytes 152-155
	char *md;//bytes 155-159
	
	/*Added Functions for byte conversions*/
	ubyte_t *ToBytes(int *bytesLength);

	static void *FromBytes(ubyte_t* bytes);

	static int IsEqual(ubyte_t * first, ubyte_t * second, int l1, int l2);
	
} bwa_seq_t;

#endif