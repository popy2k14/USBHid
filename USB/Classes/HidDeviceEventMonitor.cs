using System;
using System.Threading;

namespace UsbHid.USB.Classes
{
    internal class HidDeviceEventMonitor
    {
        public event ConnectedEventHandler Connected;
        public event DisconnectedEventHandler Disconnected;

        public delegate void ConnectedEventHandler();
        public delegate void DisconnectedEventHandler();

        private readonly UsbHidDevice _device;
        private bool _wasConnected;

        public HidDeviceEventMonitor(UsbHidDevice device)
        {
            _device = device;
        }

        public void Init()
        {
            var eventMonitor = new Action(DeviceEventMonitor);
            eventMonitor.BeginInvoke((IAsyncResult ar) => ((Action)ar.AsyncState).EndInvoke(ar), eventMonitor);
        }

        private void DeviceEventMonitor()
        {
            var isConnected = _device.IsDeviceConnected;

            if (isConnected != _wasConnected)
            {
                if (isConnected && Connected != null) Connected();
                else if (!isConnected && Disconnected != null) Disconnected();
                _wasConnected = isConnected;
            }
            SpinWait.SpinUntil(() => false, 500);

            if (_device.MonitorDeviceEvents) Init();
            else if (!isConnected && Disconnected != null) Disconnected();
        }
    }
}
