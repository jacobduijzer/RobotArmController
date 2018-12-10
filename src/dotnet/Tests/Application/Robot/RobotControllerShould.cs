﻿using System;
using Application.Robot;
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
    public class RobotControllerShould
    {
        private readonly Mock<ICommunicationService> _mockCommunicationService;
        private readonly Mock<IInstructionsRepository> _mockInstructionsRepository;

        private readonly IRobot _robot;

        private readonly IInstructions _fakeInstructions;

        public RobotControllerShould()
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
            new RobotController(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot)
            .Should().BeOfType<RobotController>();

        [Fact]
        public void ThrowWhenCommunicationServiceIsNull() =>
            new Action(() => new RobotController(null,
                                                _mockInstructionsRepository.Object,
                                                _robot))
            .Should().Throw<ArgumentNullException>()
                        .WithMessage("*communicationService*");

        [Fact]
        public void ThrowWhenInstructionsRepositoryIsNull() =>
            new Action(() => new RobotController(_mockCommunicationService.Object,
                                                null,
                                                _robot))
            .Should().Throw<ArgumentNullException>()
                        .WithMessage("*instructionsRepository*");

        [Fact]
        public void ThrowWhenRobotIsNull() =>
            new Action(() => new RobotController(_mockCommunicationService.Object,
                                                _mockInstructionsRepository.Object,
                                                null))
            .Should().Throw<ArgumentNullException>()
                        .WithMessage("*robot*");

        [Fact]
        public void OpenSerialPortOnInitialize()
        {
            var robotController = new RobotController(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot);
            robotController.Initialize().Should().BeTrue();
            _mockCommunicationService.Verify(x => x.Connect(), Times.Once);
        }

        [Fact]
        public void LoadInstructions()
        {
            var robotController = new RobotController(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot);
            robotController.LoadInstructions("TEST01").Should().BeTrue();
            _mockInstructionsRepository.Verify(x => x.GetByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void PlayInstructionsOnce()
        {
            var robotController = new RobotController(_mockCommunicationService.Object,
                                _mockInstructionsRepository.Object,
                                _robot);
            robotController.LoadInstructions("TEST01");
        }
    }
}
