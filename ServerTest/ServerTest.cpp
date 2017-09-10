// ServerTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "../OPCServer/OPCServer_i.h"
#include "../OPCServer/OPCServer_i.c"

#define SIGNALTYPE_Y1 1
#define SIGNALTYPE_Y2 2
#define SIGNALTYPE_Y3 3

#define FILETIME_TO_MS_TICK 10000
double FileTimeToTime(FILETIME *);

//Input the error message of failed HRESULT
void HresultErrorMSG(std::string ,HRESULT);

int main()
{
    HRESULT hr = E_FAIL;

    //COM initialization    
    if (FAILED( hr = CoInitialize(NULL)))
    {
        HresultErrorMSG("CoInitialize",hr);
        return 0;
    }

    //Create OPCServer object
	CComPtr<IOPCServer> pOPCServer; 
    if (FAILED(hr = pOPCServer.CoCreateInstance(CLSID_Server, NULL, CLSCTX_ALL)))
    {
        HresultErrorMSG("CoCreateInstance",hr);
        return 0;
    }

    //Create Group object
	LONG TimeBias = 0;
	FLOAT DeadBand = 0.0f; 
	OPCHANDLE phServerGroup;
	DWORD UpdateRate;
    CComPtr<IOPCItemMgt> pOPCItemMgt;
    hr = pOPCServer->AddGroup(L"", TRUE, 10, 1, &TimeBias, &DeadBand, 0, &phServerGroup, &UpdateRate, IID_IOPCItemMgt, (LPUNKNOWN*)&pOPCItemMgt);//Test with NULL szName
    if (FAILED(hr))
    {
        HresultErrorMSG("AddGroup",hr);
        return 0;
    }

	//Create Item object
    OPCITEMDEF OPCItem[3] = { { NULL, L"ItemY1", FALSE, SIGNALTYPE_Y1, 0, NULL, VT_R8, 0 },
                              { NULL, L"ItemY2", FALSE, SIGNALTYPE_Y2, 0, NULL, VT_R8, 0 },
                              { NULL, L"ItemY3", FALSE, SIGNALTYPE_Y3, 0, NULL, VT_R8, 0 } };
	OPCITEMRESULT *OPCItemResult;
	HRESULT *ErrorResult;
    if (FAILED(hr = pOPCItemMgt->AddItems(3, OPCItem, &OPCItemResult, &ErrorResult)))
    {
        HresultErrorMSG("AddItems",hr);
        return 0;
    }

    //Read the signal values
    CComPtr<IOPCSyncIO> pOPCSyncIO;
    if (FAILED(hr = pOPCItemMgt->QueryInterface(IID_IOPCSyncIO, (void**)&pOPCSyncIO)))
    {
        HresultErrorMSG("QueryInterface",hr);
        return 0;
    }
    OPCHANDLE phServer[3] = {0, 1, 2 };
    OPCITEMSTATE * pItemValues;
    HRESULT *pErrors;
    std::cout << std::setw(8)<<"TIME "
        << std::setw(10)<<"Y1   "
        << std::setw(8)<<"Y2"
        << std::setw(10)<<"Y3   " << std::endl;
    for (int i = 0; i < 100; ++i)
    {
        if (FAILED(hr = pOPCSyncIO->Read(OPC_DS_CACHE, 3, phServer, &pItemValues, &pErrors)))
        {
            HresultErrorMSG("Read",hr);
            return 0;
        }

        for (int k = 0; k < 3; ++k)
        {
            if (FAILED(hr=pErrors[k]))
            {
                HresultErrorMSG("getSignal",hr);
                return 0;
            }
        }
        
        std::cout <<std::setw(8)<< FileTimeToTime(&pItemValues[0].ftTimeStamp)
            << std::setw(10) << pItemValues[0].vDataValue.dblVal
            << std::setw(8) << pItemValues[1].vDataValue.dblVal
            << std::setw(10) << pItemValues[2].vDataValue.dblVal << std::endl;

        CoTaskMemFree(pItemValues);
        CoTaskMemFree(pErrors);
        Sleep(100);
    }

    if (FAILED(hr = pOPCServer->RemoveGroup(phServerGroup, true)))//Remove group
    {
        HresultErrorMSG("RemoveGroup",hr);
        return 0;
    }

    pOPCSyncIO.Release();
    pOPCItemMgt.Release();
    pOPCServer.Release();
    CoUninitialize();
    return 0;
}


double FileTimeToTime(FILETIME *ft)
{
    if (NULL == ft)
    {
        std::cout<<"Parameter Error!"<<std::endl;
        return 0.0;
    }
    return static_cast<double>(ft->dwLowDateTime / FILETIME_TO_MS_TICK);
}


void HresultErrorMSG(std::string methodName, HRESULT hr)
{
    std::cout << methodName<<": ";
    switch (hr)
    {
        case E_INVALIDARG:
            std::cout << "One or more arguments are invalid" << std::endl; break;
        case E_OUTOFMEMORY:
            std::cout << "Ran out of memory" << std::endl; break;
        case E_UNEXPECTED:
            std::cout << "Catastrophic failure" << std::endl; break;
        case E_NOTIMPL:
            std::cout << "Not implemented" << std::endl; break;
        case E_FAIL:
            std::cout << "Unspecified error" << std::endl; break;
        case E_POINTER:
            std::cout << "Invalid pointer" << std::endl; break;
        case E_HANDLE:
            std::cout << "Invalid handle" << std::endl; break;
        case E_ABORT:
            std::cout << "Operation aborted" << std::endl; break;
        case E_ACCESSDENIED:
            std::cout << "General access denied error" << std::endl; break;
        case E_NOINTERFACE:
            std::cout << "No such interface supported" << std::endl; break;
    }
}