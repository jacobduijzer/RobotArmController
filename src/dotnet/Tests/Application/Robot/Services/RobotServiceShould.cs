using System;
using Application.Robot.Services;
using Domain.Communication.Contracts;
using Domain.Instructions.Contracts;
using Domain.Instructions.Entities;
using Domain.Program.Contracts;
using Domain.Robot.Contracts;
using Domain.Robot.Entities;
using FluentAssertions;
using Moq;
using Xunit;

using RobotObject = Domain.Robot.Entities.Robot;

namespace Tests.Application.Robot.Services
{
    public class RobotServiceShould
    {
        private readonly Mock<ICommunicationService> _mockCommunicationService;
        private readonly Mock<IInstructionsRepository> _mockInstructionsRepository;

        private readonly IRobot _robot;

        private readonly IInstructions _fakeInstructions;

        public RobotServiceShould()
        {
            _mockCommunicationService = new Mock<ICommunicationService>();
            _mockCommunicationService.Setup(x => x.Connect()).Returns(true).Verifiable();

            _fakeInstructions = Instructions.Builder().WithName("TEST01").Build();

            _mockInstructionsRepository = new Mock<IInstructionsRepository>();
            _mockInstructionsRepository.Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns(_fakeInstructions).Verifiable();           

            _robot = RobotObject.Builder()
                .WithServo(Servo.Builder().WithName("TestServo").WithServoId(1).Build())
                .Build();
        }

        [Fact]
        public void Construct() =>
            new RobotService(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot)
            .Should().BeOfType<RobotService>();

        [Fact]
        public void ThrowWhenCommunicationServiceIsNull() =>
            new Action(() => new RobotService(null,
                                                _mockInstructionsRepository.Object,
                                                _robot))
            .Should().Throw<ArgumentNullException>()
                        .WithMessage("*communicationService*");

        [Fact]
        public void ThrowWhenInstructionsRepositoryIsNull() =>
            new Action(() => new RobotService(_mockCommunicationService.Object,
                                                null,
                                                _robot))
            .Should().Throw<ArgumentNullException>()
                        .WithMessage("*instructionsRepository*");

        [Fact]
        public void ThrowWhenRobotIsNull() =>
            new Action(() => new RobotService(_mockCommunicationService.Object,
                                                _mockInstructionsRepository.Object,
                                                null))
            .Should().Throw<ArgumentNullException>()
                        .WithMessage("*robot*");

        [Fact]
        public void OpenSerialPortOnInitialize()
        {
            var robotService = new RobotService(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot);
            robotService.Initialize().Should().BeTrue();
            _mockCommunicationService.Verify(x => x.Connect(), Times.Once);
        }

        [Fact]
        public void LoadInstructions()
        {
            var robotService = new RobotService(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot);
            robotService.LoadInstructions("TEST01").Should().BeTrue();
            _mockInstructionsRepository.Verify(x => x.GetByName(It.IsAny<string>()), Times.Once);
        }
    }
}
