using UsbHid.USB.Classes;

namespace UsbHid
{
    public interface IUsbDeviceMatchable
    {
        bool BasicMatch(string deviceInstancePath);
        bool DescriptorsMatch(UsbDescriptorStrings descriptorStrings);
    }
}
