// Server.h : Declaration of the CServer

#pragma once
#include "resource.h"       // main symbols
#include "OPCServer_i.h"
#include "OPCGroup.h"
#include <vector>

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CServer
class ATL_NO_VTABLE CServer :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CServer, &CLSID_Server>,
	public IDispatchImpl<IOPCServer, &IID_IOPCServer, &LIBID_OPCServerLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:

DECLARE_REGISTRY_RESOURCEID(IDR_SERVER)


BEGIN_COM_MAP(CServer)
	COM_INTERFACE_ENTRY(IOPCServer)
END_COM_MAP()



DECLARE_PROTECT_FINAL_CONSTRUCT()

private:
    std::vector<CComObject<COPCGroup>*> m_pGroupArray; //Group Array

public:
	//Methods from interface IOPCServer
	STDMETHOD(CreateGroupEnumerator)(OPCENUMSCOPE dwScope, REFIID riid, LPUNKNOWN *ppUnk)
	{
		return S_OK;
	}
	STDMETHOD(GetGroupByName)(LPCWSTR szGroupName, REFIID riid, LPUNKNOWN *ppUnk)
	{
		return S_OK;
	}
	STDMETHOD(AddGroup)(LPCWSTR szName, BOOL Active,
		DWORD dwRequestedUpdateRate, OPCHANDLE hClientGroup,
		LONG *pTimeBias, FLOAT *pPercentDeadband,
		DWORD dwLCID, OPCHANDLE *phServerGroup,
		DWORD *pRevisedUpdateRate, REFIID riid,
		LPUNKNOWN *ppUnk);
    STDMETHOD(RemoveGroup)(OPCHANDLE groupHandleID, BOOL bForce);
	STDMETHOD(GetErrorString)(HRESULT hr, LCID locale, LPWSTR *ppstring)
	{
		return S_OK;
	}
	STDMETHOD(GetStatus)(OPCSERVERSTATUS **ppServerStatus)
	{
		return S_OK;
	}
};

OBJECT_ENTRY_AUTO(__uuidof(Server), CServer)
