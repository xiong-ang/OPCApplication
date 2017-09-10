#include "stdafx.h"
#include "OPCItem.h"
#include"../SignalGen/SignalGen_i.h"

extern CComPtr<ISignal> g_pSignal;

//Convert the FILETIME to Time(ms)
//Note:
//FILETIME contains a 64-bit value representing the number of 100-nanosecond intervals since starts 1601-01-01T00:00:00Z(UTC).
#define FILETIME_TO_MS_TICK 10000
double COPCItem::FileTimeToTime(FILETIME *ft)
{
    if (NULL == ft) return 0.0;
    return static_cast<double>(ft->dwLowDateTime / FILETIME_TO_MS_TICK);
}

HRESULT COPCItem::getSignalValue(double *retval, FILETIME *fileTime)
{
    if ((NULL == retval) || (NULL == fileTime))     return E_INVALIDARG;

	SYSTEMTIME time;					//Get the system time
    GetLocalTime(&time);
	//GetSystemTime(&time);             //Get UTC

	SystemTimeToFileTime(&time, fileTime);
    double dfTime = FileTimeToTime(fileTime);

    HRESULT hr = E_FAIL;

	//Get the signal values of Y1,Y2,Y3
    VARIANT *signalValue=NULL;
    if (SIGNALTYPE_Y1 == m_Type)        hr = g_pSignal->getSignal_Y1(dfTime, &signalValue);
    else if (SIGNALTYPE_Y2 == m_Type)   hr = g_pSignal->getSignal_Y2(dfTime, &signalValue);
    else if (SIGNALTYPE_Y3 == m_Type)   hr = g_pSignal->getSignal_Y3(dfTime, &signalValue);
    if (FAILED(hr)) return hr;

    *retval = signalValue->dblVal;
    CoTaskMemFree(signalValue);
    return hr;
}
