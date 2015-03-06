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
	size_t nameLength = std::strlen(this->name);//Will hold length of name
	size_t nameCharSize = nameLength * sizeof(char);//number of chars for name
	//referenced objects sizes
	size_t sizeOf_bwt_aln1_t = sizeof(bwt_aln1_t);
	size_t sizeOf_bwt_bwt_multi1_t = sizeof(bwt_multi1_t);

	size_t fullSize = 
			  mySize
			//+ nameLengthIndicator_s
			//+ nameCharSize
			+ sizeOf_bwt_aln1_t
			+ sizeOf_bwt_bwt_multi1_t;

	char *bytes = (char*)calloc(1, fullSize);
	char *offsettedBytes = bytes;
	/*Convert to bytes*/
	//value type members
	memcpy(offsettedBytes, this, mySize); 
	offsettedBytes += mySize;
	/*
	// size of name
	memcpy(offsettedBytes, &nameLength, nameLengthIndicator_s);
	offsettedBytes += nameLengthIndicator_s;
	//Name
	memcpy(offsettedBytes, this->name, nameCharSize);
	offsettedBytes += nameCharSize;
	*/
	//bwt_aln1_t
	memcpy(offsettedBytes, this->aln, sizeOf_bwt_aln1_t);
	offsettedBytes += sizeOf_bwt_aln1_t;
	//bwt_multi1_t
	memcpy(offsettedBytes, this->multi, sizeOf_bwt_bwt_multi1_t);
	offsettedBytes += sizeOf_bwt_bwt_multi1_t;

	
	*bytesLength = fullSize;
	//ubyte_t *bytes = (ubyte_t *)this;
	return (ubyte_t *)bytes;
}

void* bwa_seq_t::FromBytes(ubyte_t* bytes)
{	
	/*Calculate total size of bytes to convert*/	
	//Name + size indicator
	size_t nameLengthIndicator_s = sizeof(int); //Number to hold name length
	int* nameLength = (int*)malloc(nameLengthIndicator_s);//Will hold length of name
	
	ubyte_t *offsettedBytes = bytes;
	/*Convert from bytes*/
	//The instance to return eventually
	size_t mySize = sizeof(bwa_seq_t);//size of this struct (excluding referenced objects)
	bwa_seq_t* instance = (bwa_seq_t*)malloc(mySize);		
	//value type members
	memcpy(instance, offsettedBytes, mySize);
	offsettedBytes += mySize;	
	/*
	// size of name	
	memcpy(nameLength, offsettedBytes, nameLengthIndicator_s);
	offsettedBytes += nameLengthIndicator_s;
	size_t nameCharSize = *nameLength * sizeof(char);//number of chars for name	
	
	//Name
	instance->name = (char*)malloc(sizeof(nameCharSize));
	memcpy(instance->name, offsettedBytes, nameCharSize);
	offsettedBytes += nameCharSize;
	*/
	//sizeOf_bwt_aln1_t
	size_t sizeOf_bwt_aln1_t = sizeof(bwt_aln1_t);	
	//void* test = malloc(sizeof(bwt_aln1_t));
	instance->aln = (bwt_aln1_t*)malloc(sizeOf_bwt_aln1_t);
	memcpy(instance->aln, offsettedBytes, sizeOf_bwt_aln1_t);
	offsettedBytes += sizeOf_bwt_aln1_t;
	//bwt_multi1_t
	size_t sizeOf_bwt_bwt_multi1_t = sizeof(bwt_multi1_t);
	instance->multi = (bwt_multi1_t*)malloc(sizeOf_bwt_bwt_multi1_t);
	memcpy(instance->multi, offsettedBytes, sizeOf_bwt_bwt_multi1_t);
	offsettedBytes += sizeOf_bwt_bwt_multi1_t;
	
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