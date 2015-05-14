
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include "backend.h"
#include "utils.h"
#include "Source.h"

#include <stdio.h>



int main()
{
	printf("Hello Avis\n");
	
	//---------------------------
	int seqsCount = 0;
	bwa_seq_t* seqs = GetAllSequencies(&seqsCount);	
	//----------------------------------
	bwt_t *bwt = GetBwt();
	//----------------------------------
	

	int tid = 0;	
	int n_seqs =seqsCount;//41; 
	gap_opt_t *opt = (gap_opt_t *)malloc(sizeof(gap_opt_t ));
	opt->s_mm = 3;
	opt->s_gapo = 11;
	opt->s_gape = 4;
	opt->mode = 3;
	opt->indel_end_skip = 5;
	opt->max_del_occ = 10;
	opt->max_entries = 2000000;
	opt->fnr = 0.39999991;
	opt->max_diff = -1;
	opt->max_gapo = 1;
	opt->max_gape = 6;
	opt->max_seed_diff = 2;
	opt->seed_len=32;
	opt->n_threads = 1;
	opt->max_top2 = 30;
	opt->trim_qual = 0;


	bwa_cal_sa_reg_gapWithCuda(tid, bwt, n_seqs, seqs, opt);
	

	return 0;
}
