// OPCGroup.cpp : Implementation of COPCGroup

#include "stdafx.h"
#include "OPCGroup.h"


// COPCGroup

STDMETHODIMP COPCGroup::AddItems(DWORD dwNumItems, OPCITEMDEF * pItemArray, OPCITEMRESULT **ppAddResults, HRESULT ** ppErrors)
{
    if ((NULL == pItemArray) || (NULL == ppAddResults) || (NULL == ppErrors))
        return E_INVALIDARG;

    for (int i = 0; i<(int)dwNumItems; ++i)
		itemArray.push_back(COPCItem(pItemArray+i, *ppAddResults, *ppErrors));
		
	return S_OK;
}

STDMETHODIMP COPCGroup::Read(OPCDATASOURCE  dwSource, DWORD dwCount, OPCHANDLE* phServer, OPCITEMSTATE** ppItemValues, HRESULT**ppErrors)
{
    if ((NULL == phServer) || (NULL == ppItemValues) || (NULL == ppErrors))
        return E_INVALIDARG;

    *ppItemValues = (OPCITEMSTATE*)CoTaskMemAlloc(dwCount * sizeof(OPCITEMSTATE));
    *ppErrors = (HRESULT*)CoTaskMemAlloc(dwCount * sizeof(HRESULT));

    for (int i = 0; i < (int)dwCount; ++i)
	{
        int index = phServer[i];
        (*ppItemValues)[index].vDataValue.vt = VT_R8;
        (*ppErrors)[index] = itemArray[index].getSignalValue(&((*ppItemValues)[index].vDataValue.dblVal), &((*ppItemValues)[index].ftTimeStamp));
        (*ppItemValues)[index].hClient = 0;
	}

	return S_OK;
}