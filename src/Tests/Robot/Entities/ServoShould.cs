using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Robot.Entities
{
    public class ServoShould
    {
        [Fact]
        public void Build()
        {
            var servo = Domain.Robot.Entities.Servo.Builder.WithServoId(1).Build();

            servo.Should().BeOfType<Domain.Robot.Entities.Servo>();
        }

        [Fact]
        public void BuildWithDefaults()
        {
            var servo = Domain.Robot.Entities.Servo.Builder.WithServoId(1).Build();

            servo.MinimumAngle.Should().Be(0);
            servo.MaximumAngle.Should().Be(180);
        }
    }
}
