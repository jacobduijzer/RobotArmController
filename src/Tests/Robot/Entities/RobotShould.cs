using Domain.Robot.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Robot.Entities
{
    public class RobotShould
    {
        [Fact]
        public void Build()
        {
            var servo = new Servo();
            var robot = Domain.Robot.Entities.Robot.Builder.WithServo(servo).Build();

            robot.Should().BeOfType<Domain.Robot.Entities.Robot>();
        }

        [Fact]
        public void ThrowWhenNoServosAreAdded()
        {
            Action act = () => Domain.Robot.Entities.Robot.Builder.Build();
            act.Should().Throw<InvalidOperationException>().WithMessage("No servos are added");
        }

        [Fact]
        public void ContainServos()
        {
            var servo = new Servo();
            var robot = Domain.Robot.Entities.Robot.Builder.WithServo(servo).Build();

            robot.Servos.Should().NotBeNullOrEmpty().And.HaveCount(1);
        }
    }
}
