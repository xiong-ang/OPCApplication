// Server.cpp : Implementation of CServer

#include "stdafx.h"
#include "Server.h"
#include <algorithm>


// CServer

STDMETHODIMP CServer::AddGroup(LPCWSTR szName, BOOL Active,
	DWORD dwRequestedUpdateRate, OPCHANDLE hClientGroup,
	LONG *pTimeBias, FLOAT *pPercentDeadband,
	DWORD dwLCID, OPCHANDLE *phServerGroup,
	DWORD *pRevisedUpdateRate, REFIID riid,
	LPUNKNOWN *ppUnk)
{

    if ((NULL == pTimeBias) || (NULL == pPercentDeadband) || (NULL == phServerGroup) || (NULL == pRevisedUpdateRate) || (NULL == ppUnk))
        return E_INVALIDARG;

    HRESULT hr = E_FAIL;
    CComObject<COPCGroup> *pTemGroup = NULL;
    if (FAILED(hr = CComObject<COPCGroup>::CreateInstance(&pTemGroup))) return hr;

    unsigned numGroup = m_pGroupArray.size();
	
    if ((NULL == szName) || (std::wstring(L"") == szName))//If the szname is NULL, the Server generate the unique name
	{
        wchar_t ch[100];
        _stprintf_s(ch, L"Group%d", numGroup);
        szName = ch;
	}else 
    if (std::find_if(std::begin(m_pGroupArray), std::end(m_pGroupArray), [szName](CComObject<COPCGroup> *iter)->bool {return szName == iter->getGroupParam()->szName; })
        != std::end(m_pGroupArray))   
        return OPC_E_DUPLICATENAME;

	//Get the parm
    pTemGroup->getGroupParam()->szName = szName;
    pTemGroup->getGroupParam()->bActive = Active;
    pTemGroup->getGroupParam()->dwRequestedUpdateRate = dwRequestedUpdateRate;
    pTemGroup->getGroupParam()->hClientGroup = hClientGroup;
    pTemGroup->getGroupParam()->pTimeBias = pTimeBias;
    pTemGroup->getGroupParam()->pPercentDeadband = pPercentDeadband;
    pTemGroup->getGroupParam()->dwLCID = dwLCID;

    m_pGroupArray.push_back(pTemGroup);

	// Return the requested interface point
    if (FAILED(hr = pTemGroup->QueryInterface(riid, (LPVOID*)ppUnk)))
        return hr;

	*phServerGroup = numGroup;
	*pRevisedUpdateRate = dwRequestedUpdateRate;    // keep the same updaterate with the client's requests
	
	return S_OK;
}


STDMETHODIMP CServer::RemoveGroup(OPCHANDLE groupHandleID, BOOL bForce)
{
    if (bForce)
    {
        m_pGroupArray[groupHandleID]->Release();
        m_pGroupArray[groupHandleID] = NULL;
    }
    return S_OK;
}