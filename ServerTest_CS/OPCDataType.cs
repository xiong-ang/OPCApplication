using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace ServerTest_CS
{
    //Correspond to tagOPCITEMDEF
    public struct Item
    {
        public string ItemId;
        public int ClientId;
        public VarEnum RequestedDataType;
        public VarEnum RequestedDataSubType;
        public bool Active;
        public string AccessPath;
    }

    
    class OPCHelper
    {
        //Equal to tagOPCITEMDEF
        private static readonly int ItemSize =
            IntPtr.Size +
            IntPtr.Size +
            sizeof(int) +
            sizeof(int) +
            sizeof(int) +
            (IntPtr.Size == 8 ? sizeof(int) : 0) +
            IntPtr.Size +
            sizeof(short) +
            sizeof(short) +
            (IntPtr.Size == 8 ? sizeof(int) : 0);

        //Get IntPtr from ICollection<Item>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public static IntPtr GetIntPtrfromItemArray(ICollection<Item> items)
        {
            int size = items.Count;
            IntPtr Items = Marshal.AllocCoTaskMem(ItemSize * items.Count);
            List<IntPtr> stringsToClear = new List<IntPtr>(items.Count * 2);

            var position = 0;
            foreach (var item in items)
            {
                // string szAccessPath;
                var accessPath = Marshal.StringToCoTaskMemUni(item.AccessPath);
                stringsToClear.Add(accessPath);
                Marshal.WriteIntPtr(Items, position, accessPath);
                position += IntPtr.Size;

                // string szItemID;
                var itemId = Marshal.StringToCoTaskMemUni(item.ItemId);
                stringsToClear.Add(itemId);
                Marshal.WriteIntPtr(Items, position, itemId);
                position += IntPtr.Size;

                // int bActive;
                Marshal.WriteInt32(Items, position, item.Active ? 1 : 0);
                position += sizeof(int);

                // uint hClient;
                Marshal.WriteInt32(Items, position, item.ClientId);
                position += sizeof(int);

                // uint dwBlobSize;
                Marshal.WriteInt32(Items, position, 0);
                position += sizeof(int);
                if (IntPtr.Size == 8)
                    position += sizeof(int);

                // IntPtr pBlob;
                Marshal.WriteIntPtr(Items, position, IntPtr.Zero);
                position += IntPtr.Size;

                var type = (short)item.RequestedDataType;
                if (item.RequestedDataSubType != VarEnum.VT_EMPTY)
                    type = (short)(type | (short)item.RequestedDataSubType);

                // ushort vtRequestedDataType;
                Marshal.WriteInt16(Items, position, type);
                position += sizeof(short);

                // ushort wReserved;
                Marshal.WriteInt16(Items, position, 0);
                position += sizeof(short);

                if (IntPtr.Size == 8)
                    position += sizeof(int);
            }
            return Items;
        }

        //Get signals from syncIO.Read return
        [SecurityPermission(SecurityAction.LinkDemand)]
        public static ItemValue[] GetSignalsState(int size, IntPtr dataPtr, IntPtr errorsPtr)
        {
            try
            {
                var position = 0;
                var dataPtrAsLong = dataPtr.ToInt64();
                var result = new ItemValue[size];
                for (var i = 0; i < size; i++)
                {
                    // ReSharper disable UseObjectOrCollectionInitializer
                    result[i] = new ItemValue();
                    // ReSharper restore UseObjectOrCollectionInitializer

                    // uint hClient;
                    result[i].ClientId = Marshal.ReadInt32(dataPtr, position);
                    position += sizeof(int);

                    // FILETIME ftTimeStamp;
                    var time = Marshal.ReadInt64(dataPtr, position);
                    result[i].Timestamp = DateTime.FromFileTimeUtc(time);
                    position += sizeof(long);

                    // ushort wQuality;
                    result[i].Quality = Marshal.ReadInt16(dataPtr, position);
                    position += sizeof(short);

                    // ushort wReserved;
                    position += sizeof(short);

                    // VARIANT vDataValue;
                    var variant = new IntPtr(dataPtrAsLong + position);
                    if (variant != IntPtr.Zero)
                    {
                        result[i].Value = Marshal.GetObjectForNativeVariant(variant);
                        NativeMethods.VariantClear(variant);
                    }
                    position += NativeMethods.VariantSize;

                    result[i].Error = Marshal.ReadInt32(errorsPtr, i * sizeof(int));
                }

                return result;
            }
            finally
            {
                Marshal.FreeCoTaskMem(dataPtr);
                Marshal.FreeCoTaskMem(errorsPtr);
            }
        }
    }

    

    [SuppressUnmanagedCodeSecurity]
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    static class NativeMethods
    {
        public static readonly int VariantSize = IntPtr.Size == 4 ? 0x10 : 0x18;

        [SecurityCritical]
        [SuppressUnmanagedCodeSecurity]
        [DllImport("oleaut32.dll", PreserveSig = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static extern void VariantClear(IntPtr variant);
    }


    //Correspond to OPCITEMSTATES and HRESULT
    public struct ItemValue
    {
        public int ClientId;
        public DateTime Timestamp;
        public int Quality;
        public object Value;
        public int Error;
    }

    //Correspond to OPCDATASOURCE
    public enum DataSource
    {
        Cache = 1,
        Device = 2,
    }

    //Correspond to
    enum EnumScope
    {
        None = 0,
        PrivateConnections = 1,
        PublicConnections = 2,
        AllConnections = 3,
        Private = 4,
        Public = 5,
        All = 6,
    }
}
