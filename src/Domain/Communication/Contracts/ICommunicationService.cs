using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Communication.Contracts
{
    public interface ICommunicationService
    {
        void SendData(string message);
    }
}
