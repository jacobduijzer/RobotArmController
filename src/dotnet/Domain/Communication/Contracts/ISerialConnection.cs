namespace Domain.Communication.Contracts
{
    public interface ISerialConnection
    {
        string PortName { get; }

        int BaudRate { get;  }
        
        bool Open();

        void SendData(string message);
    }
}
