using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    public struct HidpValueCaps
    {
        //
        [FieldOffset(0)]
        public ushort UsagePage;					// USHORT
        [FieldOffset(2)]
        public byte ReportID;						// UCHAR  ReportID;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(3)]
        public bool IsAlias;						// BOOLEAN  IsAlias;
        [FieldOffset(4)]
        public ushort BitField;						// USHORT  BitField;
        [FieldOffset(6)]
        public ushort LinkCollection;				//USHORT  LinkCollection;
        [FieldOffset(8)]
        public ushort LinkUsage;					// USAGE  LinkUsage;
        [FieldOffset(10)]
        public ushort LinkUsagePage;				// USAGE  LinkUsagePage;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(12)]
        public bool IsRange;					// BOOLEAN  IsRange;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(13)]
        public bool IsStringRange;				// BOOLEAN  IsStringRange;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(14)]
        public bool IsDesignatorRange;			// BOOLEAN  IsDesignatorRange;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(15)]
        public bool IsAbsolute;					// BOOLEAN  IsAbsolute;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(16)]
        public bool HasNull;					// BOOLEAN  HasNull;
        [FieldOffset(17)]
        public char Reserved;						// UCHAR  Reserved;
        [FieldOffset(18)]
        public ushort BitSize;						// USHORT  BitSize;
        [FieldOffset(20)]
        public ushort ReportCount;					// USHORT  ReportCount;
        [FieldOffset(22)]
        public ushort Reserved2a;					// USHORT  Reserved2[5];		
        [FieldOffset(24)]
        public ushort Reserved2b;					// USHORT  Reserved2[5];
        [FieldOffset(26)]
        public ushort Reserved2c;					// USHORT  Reserved2[5];
        [FieldOffset(28)]
        public ushort Reserved2d;					// USHORT  Reserved2[5];
        [FieldOffset(30)]
        public ushort Reserved2e;					// USHORT  Reserved2[5];
        [FieldOffset(32)]
        public ushort UnitsExp;					// ULONG  UnitsExp;
        [FieldOffset(34)]
        public ushort Units;						// ULONG  Units;
        [FieldOffset(36)]
        public short LogicalMin;					// LONG  LogicalMin;   ;
        [FieldOffset(38)]
        public short LogicalMax;					// LONG  LogicalMax
        [FieldOffset(40)]
        public short PhysicalMin;					// LONG  PhysicalMin, 
        [FieldOffset(42)]
        public short PhysicalMax;					// LONG  PhysicalMax;
        // The Structs in the Union			
        [FieldOffset(44)]
        public Range Range;
        [FieldOffset(44)]
        public Range NotRange;
    }
}