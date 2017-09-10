// dllmain.h : Declaration of module class.

class CSignalGenModule : public ATL::CAtlDllModuleT< CSignalGenModule >
{
public :
	DECLARE_LIBID(LIBID_SignalGenLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_SIGNALGEN, "{586297A6-4A20-439F-8226-6E86341B8722}")
};

extern class CSignalGenModule _AtlModule;
