using Application.Communication.Services;
using Domain.Communication.Contracts;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.Application.Communication.Services
{
    public class CommunicationServiceShould
    {
        private readonly Mock<ISerialConnection> _mockSerialConnection;

        public CommunicationServiceShould()
        {
            _mockSerialConnection = new Mock<ISerialConnection>();
            _mockSerialConnection.Setup(x => x.SendData(It.IsAny<string>())).Verifiable();
        }

        [Fact]
        public void Construct() =>
            new CommunicationService(_mockSerialConnection.Object).Should().BeOfType<CommunicationService>();

        [Fact]
        public void ReturnTrueWhenPortCanBeOpened()
        {
            _mockSerialConnection.Setup(x => x.Open()).Returns(true);
            var communicationService = new CommunicationService(_mockSerialConnection.Object);
            communicationService.Connect().Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenPortCannotBeOpened()
        {
            _mockSerialConnection.Setup(x => x.Open()).Returns(false);
            var communicationService = new CommunicationService(_mockSerialConnection.Object);
            communicationService.Connect().Should().BeFalse();
        }

        [Fact]
        public void SendData()
        {
            var communicationService = new CommunicationService(_mockSerialConnection.Object);
            communicationService.SendData("test");

            _mockSerialConnection.Verify(x => x.SendData(It.Is<string>(y => y.Equals("test"))), Times.Once);
        }
    }
}
