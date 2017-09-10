#include "stdafx.h"
#include "Item.h"


Item::Item(unsigned type)
{
	m_Type = type;
	m_Value = 0.0;
	CoInitialize(NULL);						//Initialize the com library

	m_pSignal.CoCreateInstance(CLSID_Signal, NULL, CLSCTX_ALL); //Get the com Interface

		
	}
	

HRESULT Item::getSignalValue(double retval)
{
	double time = 0.0;					//Get the system time

	HRESULT result = 1;				//The returned result of ISignal
	//Get the signal values of Y1,Y2,Y3
	if(1==m_Type)
		result = m_pSignal->getSignal_Y1(time, &m_Value);	
	else if (2 == m_Type)
		result = m_pSignal->getSignal_Y2(time, &m_Value);
	else if (3 == m_Type)
		result = m_pSignal->getSignal_Y3(time, &m_Value);

	return result;
}

Item::~Item()
{
	CoUninitialize();		//Uninitialize the com library
}
