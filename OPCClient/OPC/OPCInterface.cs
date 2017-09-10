using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OPC
{
    [Guid("39C13A4D-011E-11D0-9675-0020AFD8ADB3"), ComImport, InterfaceType((short)1)]
    interface IOPCServer
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddGroup(
            [In, MarshalAs(UnmanagedType.LPWStr)] string name,
            [In] int active,
            [In] int requestedUpdateRate,
            [In] int clientGroup,
            [In] ref int timeBias,
            [In] ref float percentDeadband,
            [In] uint lcid,
            [Out] out int serverGroup,
            [Out] out int revisedUpdateRate,
            [In] ref Guid iid,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object group);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetErrorString(
            [In, MarshalAs(UnmanagedType.Error)] int error,
            [In] uint locale,
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string @string);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetGroupByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string name,
            [In] ref Guid iid,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object group);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetStatus([Out] out IntPtr serverStatus);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveGroup([In] int serverGroup, [In] int force);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateGroupEnumerator(
            [In] EnumScope scope,
            [In] ref Guid iid,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object enumerator);
    }


    [Guid("39C13A54-011E-11D0-9675-0020AFD8ADB3"), ComImport, InterfaceType((short)1)]
    interface IOPCItemMgt
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddItems(
            [In] uint count,
            [In] IntPtr itemArray,
            [Out] out IntPtr addResults,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ValidateItems(
            [In] uint count,
            [In] IntPtr itemArray,
            [In] int blobUpdate,
            [Out] out IntPtr validationResults,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveItems(
            [In] uint count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] serverIds,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetActiveState(
            [In] uint count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] serverIds,
            [In] int active,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetClientHandles(
            [In] uint count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] serverIds,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] clientIds,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetDatatypes(
            [In] uint count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] serverIds,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] short[] requestedDatatypes,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateEnumerator(
            [In] ref Guid iid,
            [MarshalAs(UnmanagedType.IUnknown)] out object @enum);
    }


    [Guid("39C13A52-011E-11D0-9675-0020AFD8ADB3"), ComImport, InterfaceType((short)1)]
    interface IOPCSyncIO
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Read(
            [In] DataSource source,
            [In] uint count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] serverIds,
            [Out] out IntPtr values,
            [Out] out IntPtr errors);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Write(
            [In] uint count,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] serverIds,
            [In] IntPtr values,
            [Out] out IntPtr errors);
    }    
}
