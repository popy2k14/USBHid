namespace UsbHid.USB.Classes.Messaging
{
    public interface IMesage
    {
        int ReportLength { get; }
        byte[] MessageData { get; }
    }
}