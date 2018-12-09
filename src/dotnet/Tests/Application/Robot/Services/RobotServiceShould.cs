using System;
using System.Collections.Generic;
using System.Text;
using Application.Robot.Services;
using Domain.Communication.Contracts;
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

        public RobotServiceShould()
        {
            _mockCommunicationService = new Mock<ICommunicationService>();
            _mockInstructionsRepository = new Mock<IInstructionsRepository>();

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

        //[Fact]
        //public void ThrowWhenNoCommunicationServiceIsAdded() =>
        //    new Action(() => RobotObject.Builder().WithServo(_testServo).Build())
        //    .Should().Throw<InvalidOperationException>().WithMessage("No communication service added");
    }
}
