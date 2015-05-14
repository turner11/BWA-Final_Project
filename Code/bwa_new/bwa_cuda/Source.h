#include <iostream>
#include "backend.h"

#define WINDOWS
#ifdef WINDOWS
	#include <direct.h>
	#define GetCurrentDir _getcwd
#else
	#include <unistd.h>
	#define GetCurrentDir getcwd
#endif

char* GetPathOfLocalFile(const char * fileName)
{
	char cwd[1024];
	if (getcwd(cwd, sizeof(cwd)) != NULL)
		fprintf(stdout, "Current working dir: %s\n", cwd);
	else
	{
		perror("getcwd() error");
		return NULL;
	}
	
	char* out = (char* )malloc(strlen(cwd) + strlen(fileName) + 1);

	if((out = (char *)malloc(strlen(cwd) + strlen(fileName) + 1)) != NULL)
	{
		strcpy(out, cwd);
		strcat(out, fileName);
	}
	else
	{
		//you don't have enough memory, handle it
	}

	return out;
}
char* GetBinaryFileName(int idx)
{
	char fileName[1024];
	sprintf(fileName,"\\seq_bin\\sequence (%d).bin",idx+1);	
	char* out = GetPathOfLocalFile(fileName);

	return out;
}

char* GetBwtFileName()
{
	char * fileName = "\\chr18.fa.bwt";// "\\human_g1k_v37.fasta.bwt";
	char* out = GetPathOfLocalFile(fileName);

	return out;

}


char* readFileBytes(const char *name)  
{  
	FILE *fl = fopen(name, "r");  
	fseek(fl, 0, SEEK_END);  
	long len = ftell(fl);  
	char *ret = (char *)malloc(len);  
	fseek(fl, 0, SEEK_SET);  
	fread(ret, 1, len, fl);  
	fclose(fl);  
	return ret;  
} 

ubyte_t*  GetSequencyBytes(char* fileName)
{		
	ubyte_t* bytes = (ubyte_t*)readFileBytes(fileName);
	return bytes;
}

bwt_t *GetBwt()
{
	char* fileName = GetBwtFileName();
	bwt_t * bwt = bwt_restore_bwt(fileName);
	return bwt;
}

int file_exists(const char * filename)
{
	if (FILE * file = fopen(filename, "r"))
	{
		fclose(file);
		return 1;
	}
	return 0;
}


bwa_seq_t* GetAllSequencies(int* seqsCount)
{
	char* fileName = NULL;
	bwa_seq_t* seqs = NULL;
	int count = 0;

	while ((fileName = GetBinaryFileName(count)) != NULL)
	{
		if(	file_exists(fileName)) {
			count++;
		}else
		{
			break;
		}
		
	}

	//allocate for all sequences
	seqs = (bwa_seq_t*)calloc(sizeof(bwa_seq_t),count);
	bwa_seq_t* first = seqs;

	//I am aware nothing guaranties that the file is still there or available, I just don't care at the moment. Quick and dirty it is....
	int idx = 0;
	for(idx= 0; idx < count; idx++) {
		fileName = GetBinaryFileName(idx);
		ubyte_t* bytes = GetSequencyBytes(fileName);
		bwa_seq_t* currSeqs = FromBytes(bytes);
		*seqs = *currSeqs ;
		seqs++;		
	}
	*seqsCount = count;
	return first;
}
/*

int main_temp(int argc, char* argv[])
{
	std::cout << "Hello Avis\n";
	
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


	bwa_cal_sa_reg_gap(tid, bwt, n_seqs, seqs, opt);
	

	return 0;
}
*/
