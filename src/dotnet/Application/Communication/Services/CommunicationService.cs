using Domain.Communication.Contracts;

namespace Application.Communication.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ISerialConnection _serialConnection;

        public CommunicationService(ISerialConnection serialConnection) => 
            _serialConnection = serialConnection;

        public bool Connect() => _serialConnection.Open();

        public void SendData(string message) => _serialConnection.SendData(message);
    }
}
