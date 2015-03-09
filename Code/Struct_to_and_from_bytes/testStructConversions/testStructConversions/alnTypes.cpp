#include "stdafx.h"
#include "alnTypes.h"

#include <string>
#include <vector>
#include <memory>

using std::vector;
using std::string;

ubyte_t *bwa_seq_t::ToBytes(int *bytesLength)
{
	/*Calculate total size of bytes to convert*/
	size_t mySize = sizeof(bwa_seq_t);//size of this struct (excluding referenced objects)
	//Name + size indicator
	size_t nameLengthIndicator_s = sizeof(int); //Number to hold name length
	size_t nameLength = std::strlen(this->name)+1;//Will hold length of name (+1 for null termination)
	size_t nameCharSize = nameLength * sizeof(char);//number of chars for name
	//referenced objects sizes
	size_t sizeOf_bwt_aln1_t = sizeof(bwt_aln1_t);
	size_t sizeOf_bwt_bwt_multi1_t = sizeof(bwt_multi1_t);

	/*rest of pointers*/
	size_t sizeOf_seq			= sizeof(ubyte_t);
	size_t sizeOf_rseq			= sizeof(ubyte_t);
	size_t sizeOf_qual			= sizeof(ubyte_t);
	size_t sizeOf_cigar			= sizeof(bwa_cigar_t);
	size_t mdLengthIndicator_s	= sizeof(int);
	size_t md_length			= std::strlen(this->md) + 1; //null terminated

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
	memcpy(offsettedBytes, this->aln, sizeOf_bwt_aln1_t);
	offsettedBytes += sizeOf_bwt_aln1_t;
	//bwt_multi1_t
	memcpy(offsettedBytes, this->multi, sizeOf_bwt_bwt_multi1_t);
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
	memcpy(offsettedBytes, this->cigar, sizeOf_cigar);
	offsettedBytes += sizeOf_cigar;
	//mdLengthIndicator_s	
	memcpy(offsettedBytes, &md_length, mdLengthIndicator_s);
	offsettedBytes += mdLengthIndicator_s;
	//md
	memcpy(offsettedBytes, this->md, md_length);
	offsettedBytes += md_length;
		
	*bytesLength = fullSize;
	//ubyte_t *bytes = (ubyte_t *)this;
	return (ubyte_t *)bytes;
}

void* bwa_seq_t::FromBytes(ubyte_t* bytes)
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
	size_t strLength = std::strlen(instance->name);

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
	instance->md = (char*)malloc(sizeOf_md);
	memcpy(instance->md, offsettedBytes, sizeOf_md);
	offsettedBytes += sizeOf_md;

	return instance;
}

int bwa_seq_t::IsEqual(ubyte_t* bytes1, ubyte_t* bytes2,int l1, int l2)
{
	if (l1 != l2)
	{
		return 0;
	}

	bool isMatch = 1;
	int isEndOfArray = 0;
	size_t idx ;
	for (idx = 0; idx < l1 && isMatch; idx++)
	{
		if (idx >=0 && idx <=15
			|| idx >= 36 && idx <=39
			|| idx >= 44 && idx <= 47
			|| idx >= 80 && idx <= 89
			|| idx >=155 && idx <=159)
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

std::vector<ubyte_t> intToBytes(int paramInt)
{
	/*int x;
	static_cast<char*>(static_cast<void*>(&x));*/
	std::vector<ubyte_t> arrayOfByte(4);
	for (int i = 0; i < 4; i++)
		arrayOfByte[3 - i] = (paramInt >> (i * 8));
	return arrayOfByte;
}