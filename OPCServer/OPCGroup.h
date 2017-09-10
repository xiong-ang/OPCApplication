// OPCGroup.h : Declaration of the COPCGroup

#pragma once
#include "resource.h"       // main symbols
#include "OPCItem.h"
#include "OPCServer_i.h"
#include "_IXXXEvents_CP.h"
#include <vector>



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;

class COPCItem;
// COPCGroup

class ATL_NO_VTABLE COPCGroup :
	public CComObjectRootEx<CComSingleThreadModel>,
//	public IConnectionPointContainerImpl<COPCGroup>,
	public IDispatchImpl<IOPCItemMgt, &IID_IOPCItemMgt, &LIBID_OPCServerLib>,
	public IDispatchImpl<IOPCSyncIO, &IID_IOPCSyncIO, &LIBID_OPCServerLib>
{
public:

DECLARE_REGISTRY_RESOURCEID(IDR_OPCGROUP)


BEGIN_COM_MAP(COPCGroup)
//	COM_INTERFACE_ENTRY(IConnectionPointContainer)
	COM_INTERFACE_ENTRY(IOPCItemMgt)
	COM_INTERFACE_ENTRY(IOPCSyncIO)
END_COM_MAP()

BEGIN_CONNECTION_POINT_MAP(COPCGroup)
END_CONNECTION_POINT_MAP()


DECLARE_PROTECT_FINAL_CONSTRUCT()


public:
	//Methods from interface IOPCItemMgt
	STDMETHOD(CreateEnumerator) (REFIID riid, LPUNKNOWN* ppUnk)
	{
		return S_OK;
	}
	STDMETHOD(SetDatatypes)(DWORD dwNumItems, OPCHANDLE * phServer, VARTYPE *pRequestedDatatypes, HRESULT ** ppErrors)
	{
		return S_OK;
	}
	STDMETHOD(SetClientHandles)(DWORD dwNumItems, OPCHANDLE * phServer, OPCHANDLE *phClient, HRESULT ** ppErrors)
	{
		return S_OK;
	}
	STDMETHOD(SetActiveState)(DWORD dwNumItems, OPCHANDLE * phServer, BOOL bActive, HRESULT **ppErrors)
	{
		return S_OK;
	}
	STDMETHOD(RemoveItems)(DWORD dwNumItems, OPCHANDLE * phServer, HRESULT ** ppErrors)
	{
		return S_OK;
	}
	STDMETHOD(ValidateItems)(DWORD dwNumItems, OPCITEMDEF * pItemArray, BOOL bBlobUpdate, OPCITEMRESULT ** ppValidationResults, HRESULT ** ppErrors)
	{
		return S_OK;
	}
	STDMETHOD(AddItems)(DWORD dwNumItems, OPCITEMDEF * pItemArray, OPCITEMRESULT **ppAddResults, HRESULT ** ppErrors);

	//Methods from interface IOPCSyncIO
	STDMETHOD(Read)(OPCDATASOURCE  dwSource, DWORD dwCount, OPCHANDLE* phServer, OPCITEMSTATE** ppItemValues, HRESULT**ppErrors);

	STDMETHOD(Write)(DWORD dwCount, OPCHANDLE* phServer, VARIANT*   pItemValues, HRESULT**  ppErrors)
	{
		return S_OK;
	}

private:	
	struct GroupParam   //Get the value from addGroup()
	{
		LPCWSTR szName;
		BOOL bActive;
		DWORD dwRequestedUpdateRate;
		OPCHANDLE hClientGroup;
		LONG *pTimeBias;
		FLOAT *pPercentDeadband;
		DWORD dwLCID;
    } m_GroupParam;

	std::vector<COPCItem> itemArray;    //Item array

public:
    //Access GroupParam
    GroupParam * getGroupParam()
    {
        return &m_GroupParam;
    }
};
