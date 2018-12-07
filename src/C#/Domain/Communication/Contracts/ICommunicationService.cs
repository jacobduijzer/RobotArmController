namespace Domain.Communication.Contracts
{
    public interface ICommunicationService
    {
        bool Connect();

        void SendData(string message);
    }
}
