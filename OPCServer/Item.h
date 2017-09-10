#pragma once

#include"../../SignalGen/SignalGen_i.h"
#include "../../SignalGen/SignalGen_i.c"
#include <atlbase.h>

class Item
{
public:
	Item(unsigned type);
	HRESULT getSignalValue(double retval);		//Return the signal value
	~Item();
private:
	ATL::CComPtr<ISignal> m_pSignal;			//The com Interface
	unsigned m_Type;							//The signal type£º1-Y1¡¢2-Y2¡¢3-Y3
	double m_Value;								//The signal value
};

