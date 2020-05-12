using UsbHid.USB.Classes;

namespace UsbHid
{
    public class SerialStringMatcher : VidPidMatcher, IUsbDeviceMatchable
    {
        private readonly string SerialString;

        public SerialStringMatcher(string SerialToMatch, uint Vid = 0x16c0, uint Pid = 0x27d9) : base(Vid, Pid)
        {
            SerialString = SerialToMatch;
        }

        public override bool DescriptorsMatch(UsbDescriptorStrings descriptorStrings)
        {
            return descriptorStrings.Serial.StartsWith(SerialString);
        }
    }
}
