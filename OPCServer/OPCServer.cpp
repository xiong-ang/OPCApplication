// OPCServer.cpp : Implementation of WinMain


#include "stdafx.h"
#include "resource.h"
#include "OPCServer_i.h"

#include "../SignalGen/SignalGen_i.h"
#include "../SignalGen/SignalGen_i.c"

using namespace ATL;

CComPtr<ISignal> g_pSignal = NULL;

class COPCServerModule : public ATL::CAtlExeModuleT< COPCServerModule >
{
public :
	DECLARE_LIBID(LIBID_OPCServerLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_OPCSERVER, "{32F18FCF-F109-424B-BD1F-3E08B18BF911}")
};

COPCServerModule _AtlModule;




extern "C" int WINAPI _tWinMain(HINSTANCE /*hInstance*/, HINSTANCE /*hPrevInstance*/, 
								LPTSTR /*lpCmdLine*/, int nShowCmd)
{
    HRESULT hr = E_FAIL;
    //COM initialization 
    if (FAILED(hr = CoInitialize(NULL)))    return -1;
    //Get the com Interface
    if (FAILED(hr = g_pSignal.CoCreateInstance(CLSID_Signal, NULL, CLSCTX_ALL))) return -1;

    return _AtlModule.WinMain(nShowCmd);
    /*
    int res = _AtlModule.WinMain(nShowCmd);
    CoUninitialize();		//Uninitialize the com library
    return res;
    */
}

