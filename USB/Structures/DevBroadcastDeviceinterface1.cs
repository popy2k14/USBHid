using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DevBroadcastDeviceinterface1
    {
        internal int dbcc_size; internal int dbcc_devicetype; internal int dbcc_reserved;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        internal byte[] dbcc_classguid;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        internal char[] dbcc_name;
    }
}