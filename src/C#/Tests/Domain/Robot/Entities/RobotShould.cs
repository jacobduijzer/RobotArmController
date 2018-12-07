﻿using System;
using System.Linq;
using Domain.Communication.Contracts;
using Domain.Robot.Contracts;
using Domain.Robot.Entities;
using FluentAssertions;
using Moq;
using Xunit;

using RobotObject = Domain.Robot.Entities.Robot;

namespace Tests.Domain.Robot.Entities
{
    public class RobotShould
    {
        private readonly ICommunicationService _communicationService;

        private readonly IServo _testServo;

        public RobotShould()
        {
            var mockCommunicationService = new Mock<ICommunicationService>();
            _communicationService = mockCommunicationService.Object;

            _testServo = Servo.Builder().WithServoId(0).Build();
        }

        [Fact]
        public void Build()
        {
            var robot = RobotObject.Builder()
                .WithServo(_testServo)
                .WithCommunicationService(_communicationService)
                .Build();

            robot.Should().BeOfType<RobotObject>();            
        }

        [Fact]
        public void ThrowWhenNoServosAreAdded()
        {
            Action act = () => RobotObject.Builder()
            .WithCommunicationService(_communicationService)
            .Build();

            act.Should().Throw<InvalidOperationException>().WithMessage("No servos are added");
        }

        [Fact]
        public void ThrowWhenNoCommunicationServiceIsAdded()
        {
            Action act = () => RobotObject.Builder().WithServo(_testServo).Build();
            act.Should().Throw<InvalidOperationException>().WithMessage("No communication service added");
        }

        [Fact]
        public void ContainServos()
        {
            var robot = RobotObject.Builder()
                .WithCommunicationService(_communicationService)
                .WithServo(_testServo)
                .Build();

            robot.Servos.Should().NotBeNullOrEmpty().And.HaveCount(1);
        }

        [Fact]
        public void RobotTester()
        {
            var baseServo = Servo.Builder()
                .WithName("Base")
                .WithMinimumAngle(0)
                .WithMaximumAngle(180)
                .WithServoId(1)
                .Build();

            var shoulderServo = Servo.Builder()
                .WithName("Shoulder")
                .WithMinimumAngle(0)
                .WithMaximumAngle(180)
                .WithServoId(2)
                .Build();

            var elbowServo = Servo.Builder()
                .WithName("Elbow")
                .WithMinimumAngle(0)
                .WithMaximumAngle(180)
                .WithServoId(3)
                .Build();

            var gripperServo = Servo.Builder()
                .WithName("Gripper")
                .WithMinimumAngle(0)
                .WithMaximumAngle(180)
                .WithServoId(4)
                .Build();

            var robot = RobotObject.Builder()
                .WithCommunicationService(_communicationService)
                .WithServo(baseServo)
                .WithServo(shoulderServo)
                .WithServo(elbowServo)
                .WithServo(gripperServo)
                .Build();

            robot.MoveServo(1, 30);

            robot.Servos.FirstOrDefault(x => x.ServoId.Equals(baseServo.ServoId))
                .CurrentAngle
                .Should()
                .Be(30);
        }
    }
}