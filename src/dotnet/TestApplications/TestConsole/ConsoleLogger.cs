using System;
using Domain.Communication.Contracts;

namespace TestConsole
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message) =>
            Console.WriteLine(message);
    }
}
