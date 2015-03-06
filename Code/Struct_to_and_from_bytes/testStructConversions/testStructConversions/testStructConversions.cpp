// testStructConversions.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "alnTypes.h"
#include <memory>



int main(int argc, char* argv[])
{
	ubyte_t *randomByts = (ubyte_t *)malloc(sizeof(bwa_seq_t));
	ubyte_t *bwt_aln1_t_Byts = (ubyte_t *)malloc(sizeof(bwt_aln1_t));
	ubyte_t *multi_Byts = (ubyte_t *)malloc(sizeof(bwt_multi1_t));
	ubyte_t *cigar_Byts = (ubyte_t *)malloc(sizeof(bwa_cigar_t));
	
	bwa_seq_t *first = (bwa_seq_t*)randomByts;//OK	
	first->name = "My Name";
	first->aln = (bwt_aln1_t*)bwt_aln1_t_Byts;//Need To Add
	first->multi = (bwt_multi1_t*)multi_Byts;//Need To Add	

	int bytesLength1;
	ubyte_t *bytes1 = first->ToBytes(&bytesLength1);
	
	bwa_seq_t* second = (bwa_seq_t* )bwa_seq_t::FromBytes(bytes1);
	int bytesLength2;
	ubyte_t *bytes2 = second->ToBytes(&bytesLength2);

	int isEqual = bwa_seq_t::IsEqual(bytes1, bytes2, bytesLength1, bytesLength2);

	return 0;
}

