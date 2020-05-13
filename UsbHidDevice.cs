using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UsbHid.USB.Classes;
using UsbHid.USB.Classes.Messaging;
using UsbHid.USB.Structures;

namespace UsbHid
{
    public class UsbHidDevice : IDisposable
    {
        #region Variables

        private DeviceInformationStructure _deviceInformation;
        public string DevicePath => _deviceInformation.DevicePathName;
        public ushort InputReportByteLength => _deviceInformation.Capabilities.InputReportByteLength;
        public ushort OutputReportByteLength => _deviceInformation.Capabilities.OutputReportByteLength;
        public bool IsDeviceConnected => _deviceInformation.IsDeviceAttached && DeviceDiscovery.FindAllHidDevices().Any(x => x.Equals(_deviceInformation.DevicePathName));
        public UsbDescriptorStrings DescriptorStrings => _deviceInformation.DescriptorStrings;
        private bool _monitorDeviceEvents;
        private readonly HidDeviceEventMonitor _deviceEventMonitor;
        public bool MonitorDeviceEvents
        {
            get => _monitorDeviceEvents;
            set
            {
                if (value && !_monitorDeviceEvents) _deviceEventMonitor.Init();
                _monitorDeviceEvents = value;
            }
        }

        private readonly BackgroundWorker _worker;
        private FileStream _fsDeviceRead;

        #endregion

        #region Delegates

        public delegate void DataReceivedDelegate(byte[] data);
        public event DataReceivedDelegate DataReceived;

        public delegate void ConnectedDelegate();
        public event ConnectedDelegate OnConnected;

        public delegate void DisConnectedDelegate();
        public event DisConnectedDelegate OnDisConnected;

        #endregion

        #region Construction

        public UsbHidDevice(string devicePath)
        {
            _deviceInformation.DevicePathName = devicePath;
            _worker = new BackgroundWorker();
            _worker.DoWork += WorkerDoWork;
            if (DeviceDiscovery.FindTargetDevice(ref _deviceInformation))
            {
                _worker.RunWorkerAsync();
            }

            _deviceInformation.ConnectedChanged += DeviceConnectedChanged;
            _deviceEventMonitor = new HidDeviceEventMonitor(this);
            _deviceEventMonitor.Connected += ReportConnected;
            _deviceEventMonitor.Disconnected += ReportDisConnected;
        }

        ~UsbHidDevice()
        {
            Disconnect();
        }

        #endregion

        #region Event Handlers

        private void ReadCompleted(IAsyncResult iResult)
        {
            // Retrieve the stream and read buffer.
            var syncObj = (SyncObjT)iResult.AsyncState;
            try
            {
                // call end read : this throws any exceptions that happened during the read
                syncObj.Fs.EndRead(iResult);
                try
                {
                    DataReceived?.Invoke(syncObj.Buf);
                }
                finally
                {
                    // when all that is done, kick off another read for the next report
                    BeginAsyncRead(ref syncObj.Fs, syncObj.Buf.Length);
                }
            }
            catch (IOException ex)	// if we got an IO exception, the device was removed
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void DeviceConnectedChanged(bool isConnected)
        {
            if (isConnected)
            {
                ReportConnected();
                _worker.RunWorkerAsync();
            }
            else
            {
                ReportDisConnected();
            }
        }

        #endregion

        #region Methods

        #region Public 

        public bool Connect()
        {
            //TODO: FIX THIS!
            return IsDeviceConnected;
        }

        public void Disconnect()
        {
            _fsDeviceRead?.Close();

            if (IsDeviceConnected)
            {
                _deviceInformation.HidHandle.Close();
                _deviceInformation.ReadHandle.Close();
                _deviceInformation.WriteHandle.Close();

                _deviceInformation.IsDeviceAttached = false;
            }
        }

        public bool SendMessage(IMesage message)
        {
            return DeviceCommunication.WriteRawReportToDevice(message.MessageData, ref _deviceInformation);
        }

        public bool SendCommandMessage(byte command)
        {
            var message = new CommandMessage(command);
            return DeviceCommunication.WriteRawReportToDevice(message.MessageData, ref _deviceInformation);
        }

        public bool SetFeature(byte[] FeatureReport)
        {
            return USB.Classes.DllWrappers.Hid.HidD_SetFeature(_deviceInformation.ReadHandle, FeatureReport, _deviceInformation.Capabilities.FeatureReportByteLength);
        }

        public bool GetFeature(out byte[] FeatureReport)
        {
            if (_deviceInformation.Capabilities.FeatureReportByteLength <= 0)
            {
                FeatureReport = new byte[0];
                return false;
            }
            FeatureReport = new byte[_deviceInformation.Capabilities.FeatureReportByteLength];
            byte[] buffer = new byte[_deviceInformation.Capabilities.FeatureReportByteLength];
            bool success;
            try
            {
                success = USB.Classes.DllWrappers.Hid.HidD_GetFeature(_deviceInformation.ReadHandle, buffer, _deviceInformation.Capabilities.FeatureReportByteLength);
                if (success)
                    FeatureReport = buffer;
            }
            catch (Exception e)
            {
                throw new Exception($"Error accessing HID device '{_deviceInformation.DevicePathName}'.", e);
            }
            return success;
        }
        #endregion

        #region Private

        private void BeginAsyncRead(ref FileStream fs, int iBufLen)
        {
            var syncObj = new SyncObjT { Fs = fs, Buf = new byte[iBufLen] };
            try
            {
                fs.BeginRead(syncObj.Buf, 0, iBufLen, ReadCompleted, syncObj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            _fsDeviceRead = new FileStream(_deviceInformation.ReadHandle, FileAccess.Read, 0x1000, true);
            BeginAsyncRead(ref _fsDeviceRead, _deviceInformation.Capabilities.InputReportByteLength);
        }

        private void ReportConnected()
        {
            OnConnected?.Invoke();
        }

        private void ReportDisConnected()
        {
            OnDisConnected?.Invoke();
        }

        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (MonitorDeviceEvents) MonitorDeviceEvents = false;
            Disconnect();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
