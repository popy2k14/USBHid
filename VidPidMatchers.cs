using UsbHid.USB.Classes;

namespace UsbHid
{
    public class VidPidMatchers : IUsbDeviceMatchable
    {
        public (uint Vid, uint Pid)[] VidPids;

        public VidPidMatchers((uint Vid, uint Pid)[] VidPids)
        {
            this.VidPids = VidPids;
        }

        public bool BasicMatch(string deviceInstancePath)
        {
            for (int i = 0; i < VidPids.Length; i++)
            {
                if (deviceInstancePath.IndexOf("#vid_" + VidPids[i].Vid.ToString("x4") + "&") != -1
                    && deviceInstancePath.IndexOf("&pid_" + VidPids[i].Pid.ToString("x4") + "#") != -1)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool DescriptorsMatch(UsbDescriptorStrings descriptorStrings)
        {
            return true;
        }
    }
}
