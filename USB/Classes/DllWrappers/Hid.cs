using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UsbHid.USB.Structures;

namespace UsbHid.USB.Classes.DllWrappers
{
    public static class Hid
    {
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FlushQueue(SafeFileHandle hidDeviceObject);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FreePreparsedData(IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetAttributes(SafeFileHandle hidDeviceObject, ref HiddAttributes attributes);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetFeature(SafeFileHandle hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetInputReport(SafeFileHandle hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(ref Guid hidGuid);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetNumInputBuffers(SafeFileHandle hidDeviceObject, ref int numberBuffers);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetPreparsedData(SafeFileHandle hidDeviceObject, ref IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetFeature(SafeFileHandle hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetNumInputBuffers(SafeFileHandle hidDeviceObject, int numberBuffers);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetOutputReport(SafeFileHandle hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern int HidP_GetCaps(IntPtr preparsedData, ref HidpCaps capabilities);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern int HidP_GetValueCaps(int reportType, byte[] valueCaps, ref int valueCapsLength, IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetManufacturerString(SafeFileHandle HidDeviceObject, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder Buffer, uint BufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetProductString(SafeFileHandle HidDeviceObject, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder Buffer, uint BufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetSerialNumberString(SafeFileHandle HidDeviceObject, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder Buffer, uint BufferLength);
    }
}
