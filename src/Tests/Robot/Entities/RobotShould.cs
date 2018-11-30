using Domain.Communication.Contracts;
using Domain.Robot.Contracts;
using Domain.Robot.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Robot.Entities
{
    public class RobotShould
    {
        private readonly ICommunicationService _communicationService;

        private readonly IServo _testServo;

        public RobotShould()
        {
            var mockCommunicationService = new Mock<ICommunicationService>();
            _communicationService = mockCommunicationService.Object;

            _testServo = Servo.Builder.Build();
        }

        [Fact]
        public void Build()
        {
            var robot = Domain.Robot.Entities.Robot.Builder
                .WithServo(_testServo)
                .WithCommunicationService(_communicationService)
                .Build();

            robot.Should().BeOfType<Domain.Robot.Entities.Robot>();
        }

        [Fact]
        public void ThrowWhenNoServosAreAdded()
        {
            Action act = () => Domain.Robot.Entities.Robot.Builder
            .WithCommunicationService(_communicationService)
            .Build();

            act.Should().Throw<InvalidOperationException>().WithMessage("No servos are added");
        }

        [Fact]
        public void ThrowWhenNoCommunicationServiceIsAdded()
        {
            Action act = () => Domain.Robot.Entities.Robot.Builder.WithServo(_testServo).Build();
            act.Should().Throw<InvalidOperationException>().WithMessage("No communication service added");
        }

        [Fact]
        public void ContainServos()
        {
            var robot = Domain.Robot.Entities.Robot.Builder
                .WithCommunicationService(_communicationService)
                .WithServo(_testServo)
                .Build();

            robot.Servos.Should().NotBeNullOrEmpty().And.HaveCount(1);
        }
    }
}
