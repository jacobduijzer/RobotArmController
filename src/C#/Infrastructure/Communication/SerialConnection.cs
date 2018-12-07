using System.IO.Ports;
using Domain.Communication.Contracts;

namespace Infrastructure.Communication
{
    public class SerialConnection : ISerialConnection
    {
        public string PortName { get; private set; }

        public int BaudRate { get; private set; }

        public ILogger Logger { get; private set; }

        private SerialPort _serialPort;

        private SerialConnection() { }
        
        public bool Open()
        {
            _serialPort = new SerialPort(PortName, BaudRate);
            _serialPort.Open();

            if(Logger != null)
                _serialPort.DataReceived += SerialPort_DataReceived;

            return _serialPort.IsOpen;
        }
        public bool Close()
        {
            _serialPort.DataReceived -= SerialPort_DataReceived;
            _serialPort.Close();

            return !_serialPort.IsOpen;
        }
        
        public void SendData(string message) => _serialPort.WriteLine(message);

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            var dataReceived = sp.ReadExisting();
            Logger.Write(dataReceived);
        }

        #region Builder

        public static SerialConnectionBuilder Builder() => new SerialConnectionBuilder();

        public class SerialConnectionBuilder
        {
            private string _portName;
            private int _baudRate = 9600;
            private ILogger _logger;

            public SerialConnectionBuilder WithPortName(string portName)
            {
                _portName = portName;
                return this;
            }

            public SerialConnectionBuilder WithBaudRate(int baudRate)
            {
                _baudRate = baudRate;
                return this;
            }

            public SerialConnectionBuilder WithLogger(ILogger logger)
            {
                _logger = logger;
                return this;
            }

            public SerialConnection Build()
            {
                return new SerialConnection
                {
                    PortName = this._portName,
                    BaudRate = _baudRate,
                    Logger = _logger
                };
            }
        }

        #endregion
    }
}
