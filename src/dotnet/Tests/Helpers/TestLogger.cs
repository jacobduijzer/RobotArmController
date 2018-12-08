using Domain.Communication.Contracts;
using Xunit.Abstractions;

namespace Tests.Helpers
{
    public class TestLogger : ILogger
    {
        private readonly ITestOutputHelper _output;

        public TestLogger(ITestOutputHelper output) => _output = output;

        public void Write(string message) => _output.WriteLine(message);
    }
}
