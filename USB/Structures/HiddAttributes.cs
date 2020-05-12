using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HiddAttributes
    {
        public int Size; // = sizeof (struct _HIDD_ATTRIBUTES) = 10

        //
        // Vendor ids of this hid device
        //
        public ushort VendorID;
        public ushort ProductID;
        public ushort VersionNumber;

        //
        // Additional fields will be added to the end of this structure.
        //
    }
}