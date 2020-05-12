using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NotRange
    {
        public ushort Usage;
        public ushort Reserved1;
        public ushort StringIndex;
        public ushort Reserved2;
        public ushort DesignatorIndex;
        public ushort Reserved3;
        public ushort DataIndex;
        public ushort Reserved4;
    }
}