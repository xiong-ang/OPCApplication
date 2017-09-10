#pragma once
#include "OPCGroup.h"
#include "OPCServer_i.h"

//The signal type
#define SIGNALTYPE_Y1 1
#define SIGNALTYPE_Y2 2
#define SIGNALTYPE_Y3 3


class COPCItem
{
public:
    COPCItem(OPCITEMDEF *pItemArray, OPCITEMRESULT *ppAddResults, HRESULT *ppErrors) :m_Type(pItemArray->hClient){}
    HRESULT getSignalValue(double *retval, FILETIME *fileTime);		//Return the signal value and the time
    static double FileTimeToTime(FILETIME *ft);                      //Convert the FILETIME to Time(ms)
private:
	unsigned m_Type;							                    //The signal type
};

