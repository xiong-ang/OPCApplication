// Signal.h : Declaration of the CSignal

#pragma once
#include "resource.h"       // main symbols



#include "SignalGen_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CSignal

class ATL_NO_VTABLE CSignal :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSignal, &CLSID_Signal>,
	public IDispatchImpl<ISignal, &IID_ISignal, &LIBID_SignalGenLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:

DECLARE_REGISTRY_RESOURCEID(IDR_SIGNAL)


BEGIN_COM_MAP(CSignal)
	COM_INTERFACE_ENTRY(ISignal)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

    STDMETHOD(getSignal_Y1)(DOUBLE t, VARIANT **signalValue);		//Generate signal Y1
    STDMETHOD(getSignal_Y2)(DOUBLE t, VARIANT **signalValue);		//Generate signal Y2
    STDMETHOD(getSignal_Y3)(DOUBLE t, VARIANT **signalValue);		//Generate signal Y3
};

OBJECT_ENTRY_AUTO(__uuidof(Signal), CSignal)
