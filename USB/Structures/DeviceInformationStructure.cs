using Microsoft.Win32.SafeHandles;
using System;
using UsbHid.USB.Classes;

namespace UsbHid.USB.Structures
{
    public struct DeviceInformationStructure
    {
        public delegate void ConnectedChangedDelegate(bool isConnected);
        public event ConnectedChangedDelegate ConnectedChanged;

        public UsbDescriptorStrings DescriptorStrings;
        public HiddAttributes Attributes;      // HID Attributes
        public HidpCaps Capabilities;          // HID Capabilities
        public SafeFileHandle ReadHandle;       // Read handle from the device
        public SafeFileHandle WriteHandle;      // Write handle to the device
        public SafeFileHandle HidHandle;        // Handle used for communicating via hid.dll
        public IntPtr DeviceNotificationHandle; // The device's notification handle

        // The device's path name
        public string DevicePathName { get; internal set; }

        // Device attachment state flag
        private bool _isDeviceAttached;

        public bool IsDeviceAttached
        {
            get { return _isDeviceAttached; }
            set
            {
                if (_isDeviceAttached == value) return;
                _isDeviceAttached = value;
                if (ConnectedChanged == null) return;
                ConnectedChanged(value);
            }
        }
    }
}
