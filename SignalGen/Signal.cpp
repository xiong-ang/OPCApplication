// Signal.cpp : Implementation of CSignal

#include "stdafx.h"
#include "Signal.h"

#include <cmath>
#include <cassert>
// CSignal
#define K 50    //K1,K2,K3

/*
Generate signal Y1:
    Y1 = K1*Sin(t/T1)   £¬K1 = 50   T1 = 2000
*/
STDMETHODIMP CSignal::getSignal_Y1
(
DOUBLE t,				//The current time
VARIANT **signalValue		//The signal value Y
)
{
	// TODO: Add your implementation code here
    if (NULL == signalValue) return E_INVALIDARG;
    *signalValue = (VARIANT *)CoTaskMemAlloc(sizeof(VARIANT));

	const int T1 = 2000;

    (*signalValue)->vt = VT_R8;
	(*signalValue)->dblVal = K*sin(t / T1);

	return S_OK;
}

/*
Generate signal Y2:
    if N ¡Ü t/T2 < N+1/2 :       Y2=K2
    else if N+1/2 ¡Ü t/T2 < N+1: Y2=-K2
    there, K2 = 50    T2 = 12000
*/
STDMETHODIMP CSignal::getSignal_Y2
(
DOUBLE t,				//The current time
VARIANT **signalValue		//The signal value Y
)
{
	// TODO: Add your implementation code here
    if (NULL == signalValue) return E_INVALIDARG;
    *signalValue = (VARIANT *)CoTaskMemAlloc(sizeof(VARIANT));

	int T2 = 12000;
	int N = static_cast<int>(t / T2);

	double flag = t / T2 - N;

    (*signalValue)->vt = VT_R8;
	if (flag < 0.5)
        (*signalValue)->dblVal = K;
	else
        (*signalValue)->dblVal = -K;

	return S_OK;
}

/*
Generate signal Y3:
    if N- 1/2 ¡Ü t/T3 < N :     Y3=K3+K3/(T3/2) (t-N*T3 )
    else if N ¡Ü t/T3 < N+1/2:  Y3=K3-K3/(T3/2) (t-N*T3 )
    there, K3 = 50    T3 = 12000
*/
STDMETHODIMP CSignal::getSignal_Y3
(
DOUBLE t,				//The current time
VARIANT **signalValue		//The signal value Y
)
{
	// TODO: Add your implementation code here
    if (NULL == signalValue) return E_INVALIDARG;
    *signalValue = (VARIANT *)CoTaskMemAlloc(sizeof(VARIANT));

	int T3 = 12000;
	int N = static_cast<int>(t / T3 + 1.0 / 2);

	double flag = t / T3 - N;

    (*signalValue)->vt = VT_R8;
	if (flag < 0)
        (*signalValue)->dblVal = K + K / (T3 / 2.0)*(t - N*T3);
	else
        (*signalValue)->dblVal = K - K / (T3 / 2.0)*(t - N*T3);

	return S_OK;
}
