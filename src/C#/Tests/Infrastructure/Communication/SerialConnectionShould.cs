using FluentAssertions;
using Infrastructure.Communication;
using Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Infrastructure.Communication
{
    public class SerialConnectionShould
    {
        private readonly TestLogger _testLogger;

        public SerialConnectionShould(ITestOutputHelper outputHelper) =>
            _testLogger = new TestLogger(outputHelper);

        [Fact]
        public void Build() =>
            SerialConnection.Builder().Build().Should().BeOfType<SerialConnection>();

        [Fact]
        public void BuildWithPortName() =>
            SerialConnection.Builder().WithPortName("TESTPORT")
                                        .Build()
                                        .PortName
                                        .Should().Be("TESTPORT");

        [Fact]
        public void BuildWithDefaultBaudRate() =>
            SerialConnection.Builder().Build()
                                        .BaudRate
                                        .Should().Be(9600);

        [Fact]
        public void BuildWithBaudRate() =>
            SerialConnection.Builder().WithBaudRate(115200)
                                        .Build()
                                        .BaudRate
                                        .Should().Be(115200);

        [Fact]
        public void BuildWithLogger() =>
            SerialConnection.Builder().WithLogger(_testLogger)
                                        .Build()
                                        .Logger
                                        .Should()
                                        .BeOfType<TestLogger>();

        [Fact]
        public void BuildWithAllProperties()
        {
            var serialConnection = SerialConnection.Builder()
                                                        .WithPortName("COM4")
                                                        .WithBaudRate(115200)
                                                        .WithLogger(_testLogger)
                                                        .Build();

            serialConnection.PortName.Should().Be("COM4");
            serialConnection.BaudRate.Should().Be(115200);
            serialConnection.Logger.Should().Be(_testLogger);
        }
    }
}
