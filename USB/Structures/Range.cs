using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Range
    {
        public ushort UsageMin;			// USAGE	UsageMin; // USAGE  Usage; 
        public ushort UsageMax; 			// USAGE	UsageMax; // USAGE	Reserved1;
        public ushort StringMin;			// USHORT  StringMin; // StringIndex; 
        public ushort StringMax;			// USHORT	StringMax;// Reserved2;
        public ushort DesignatorMin;		// USHORT  DesignatorMin; // DesignatorIndex; 
        public ushort DesignatorMax;		// USHORT	DesignatorMax; //Reserved3; 
        public ushort DataIndexMin;		// USHORT  DataIndexMin;  // DataIndex; 
        public ushort DataIndexMax;		// USHORT	DataIndexMax; // Reserved4;
    }
}