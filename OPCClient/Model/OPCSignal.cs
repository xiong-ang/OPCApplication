using System;
using OPCServerLib;
using OPC;
using System.Runtime.InteropServices;

namespace Client
{
    class OPCSignal : SignalBase
    {        
        public OPCSignal()
        {
            InitializeServer();
        }

        private OPC.IOPCSyncIO syncIO;

        //Initialize OPC Server and obtain the IOPCSyncIO interface
        private bool InitializeServer()
        {
            OPCServerLib.Server server = new OPCServerLib.Server();
            int TimeBias = 0;
            float DeadBand = 0.0f;
            uint phServerGroup;
            uint UpdateRate;
            var id = typeof(OPC.IOPCItemMgt).GUID;
            object p;

            server.AddGroup("Group1", 1, 10, 1, ref TimeBias, ref DeadBand, 0, out phServerGroup, out UpdateRate, ref id, out p);

            //Obtain IOPCItemMgt interface
            OPC.IOPCItemMgt pOPCItemMgt = (OPC.IOPCItemMgt)p;
            Item[] items = {    new Item("ItemY1",1,VarEnum.VT_R8,VarEnum.VT_EMPTY,false,null),
                                new Item("ItemY2",2,VarEnum.VT_R8,VarEnum.VT_EMPTY,false,null),
                                new Item("ItemY3",3,VarEnum.VT_R8,VarEnum.VT_EMPTY,false,null) };

            IntPtr itemPtr = OPCHelper.GetIntPtrfromItemArray(items);
            IntPtr dataPtr;
            IntPtr errorsPtr;
            pOPCItemMgt.AddItems(3, itemPtr, out dataPtr, out errorsPtr);

            //Obtain IOPCSyncIO interface
            syncIO = (OPC.IOPCSyncIO)p;

            return true;
        }

        //Obtain signals from IOPCSyncIO
        private ItemValue[] ReadSignals()
        {
            int[] serverIds = { 0, 1, 2 };
            IntPtr dataPtr1;
            IntPtr errorsPtr1;

            syncIO.Read(DataSource.Cache, 3, serverIds, out dataPtr1, out errorsPtr1);

            ItemValue[] temItem = OPCHelper.GetSignalsState(3, dataPtr1, errorsPtr1);
            for (int k = 0; k < 3; ++k)
            {
                if (temItem[k].Error < 0)
                {
                    Console.WriteLine("Error!");
                    return null;
                }
           }
            return temItem;

        }

        override protected SignalEventArgs Update()
        {
            //Obtain signals from OPC server
            ItemValue[] signalItem = ReadSignals();

            double signal_Y1 = Math.Round((double)signalItem[0].Value, 2);
            double signal_Y2 = Math.Round((double)signalItem[1].Value, 2);
            double signal_Y3 = Math.Round((double)signalItem[2].Value, 2);

            return new SignalEventArgs(new SimplingPoint(signalItem[0].Timestamp, signal_Y1, signal_Y2, signal_Y3));
        }
    }
}
