using System;
using System.Runtime.InteropServices;
using OPCServerLib;


namespace ServerTest_CS
{

    class Program
    {
        static void Main(string[] args)
        {
            
            Server server = new Server();
           
            int TimeBias = 0;
            float DeadBand = 0.0f;
            uint phServerGroup;
            uint UpdateRate;
            var id = typeof(IOPCItemMgt).GUID;
            object p;

            server.AddGroup("Group1", 1, 10, 1, ref TimeBias, ref DeadBand, 0, out phServerGroup, out UpdateRate, ref id, out p);


            IOPCItemMgt pOPCItemMgt = (IOPCItemMgt)p;
                     
            Item[] items = new Item[3];
            items[0].Active = false;
            items[0].AccessPath = null;
            items[0].ClientId=1;
            items[0].ItemId="ItemY1";
            items[0].RequestedDataSubType = VarEnum.VT_R8;
            items[0].RequestedDataType=VarEnum.VT_EMPTY;
            items[1].Active = false;
            items[1].AccessPath = null;
            items[1].ClientId = 2;
            items[1].ItemId = "ItemY2";
            items[1].RequestedDataSubType = VarEnum.VT_R8;
            items[1].RequestedDataType = VarEnum.VT_EMPTY;
            items[2].Active = false;
            items[2].AccessPath = null;
            items[2].ClientId = 3;
            items[2].ItemId = "ItemY3";
            items[2].RequestedDataSubType = VarEnum.VT_R8;
            items[2].RequestedDataType = VarEnum.VT_EMPTY;

            IntPtr itemPtr = OPCHelper.GetIntPtrfromItemArray(items);
            IntPtr dataPtr;
            IntPtr errorsPtr;
            pOPCItemMgt.AddItems(3, itemPtr, out dataPtr, out errorsPtr);



            IOPCSyncIO syncIO = (IOPCSyncIO)p;

            int[] serverIds = { 0, 1, 2 };
            IntPtr dataPtr1;
            IntPtr errorsPtr1;

            Console.WriteLine("Y1                    Y2            Y3");
            for (int i = 0; i < 100; ++i)
            {
                syncIO.Read(DataSource.Cache, 3, serverIds, out dataPtr1, out errorsPtr1);

                ItemValue[] temItem = OPCHelper.GetSignalsState(3, dataPtr1, errorsPtr1);
                for (int k = 0; k < 3; ++k)
                {
                    if (temItem[k].Error < 0)
                    {
                        Console.WriteLine("Error!");
                        return;
                    }
                }

                double signal_Y1=(double)temItem[0].Value;
                double signal_Y2=(double)temItem[1].Value;
                double signal_Y3=(double)temItem[2].Value;
                Console.WriteLine("{0}       {1}     {2}", signal_Y1, signal_Y2, signal_Y3);
                System.Threading.Thread.Sleep(100);       
            }
       
        }
    }   
}
