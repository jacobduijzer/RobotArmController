using System;
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
        private readonly IServo _testServo;

        public RobotShould()
        {
            //var mockCommunicationService = new Mock<ICommunicationService>();
            //_communicationService = mockCommunicationService.Object;

            _testServo = Servo.Builder().WithServoId(0).Build();
        }

        [Fact]
        public void Build() =>
            RobotObject.Builder().WithServo(_testServo).Build()
                       .Should().BeOfType<RobotObject>();

        [Fact]
        public void ThrowWhenNoServosAreAdded() =>
            new Action(() => RobotObject.Builder().Build())
            .Should().Throw<InvalidOperationException>().WithMessage("No servos are added");
        
        [Fact]
        public void ContainServos() =>
            RobotObject.Builder().WithServo(_testServo).Build()
            .Servos.Should().NotBeNullOrEmpty().And.HaveCount(1);

        [Fact(Skip = "Logic moved to service, need to move and test")]
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
                .WithServo(baseServo)
                .WithServo(shoulderServo)
                .WithServo(elbowServo)
                .WithServo(gripperServo)
                .Build();

            //robot.MoveServo(1, 30);

            robot.Servos.FirstOrDefault(x => x.ServoId.Equals(baseServo.ServoId))
                .CurrentAngle
                .Should()
                .Be(30);
        }
    }
}
