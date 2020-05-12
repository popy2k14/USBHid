using System;

namespace UsbHid.USB.Classes.Messaging
{
    public class CommandMessage : IMesage
    {
        private byte[] _parameters;
        public byte[] MessageData => GetMessageData();
        private int _ReportLength = 65;
        public int ReportLength
        {
            get => _ReportLength; private set
            {
                if (value < 2)
                    throw new ArgumentOutOfRangeException("value", "Paramater limit is 2");
                _ReportLength = value;
            }
        }

        private byte[] GetMessageData()
        {
            var result = new byte[ReportLength];
            result[0] = 0;
            result[1] = Command;
            if (Parameters != null)
            {
                Array.Copy(Parameters, 0, result, 2, Parameters.Length);
            }
            return result;
        }

        public byte Command { get; set; }

        public byte[] Parameters
        {
            get => _parameters;
            set
            {
                if (value?.Length < 1) throw new ArgumentOutOfRangeException("value", "Paramater needs to be at least 1 byte long");
                if (value?.Length > ReportLength - 2) throw new ArgumentOutOfRangeException("value", $"Paramater canot be longer than {ReportLength - 2} bytes");
                _parameters = value;
            }
        }

        public CommandMessage(byte command)
        {
            Command = command;
        }

        public CommandMessage(byte command, byte[] parameters, ushort reportLength) : this(command)
        {
            ReportLength = reportLength;
            Parameters = parameters;
        }
    }
}
