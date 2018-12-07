using System;
using FluentAssertions;
using Xunit;

using ServoObject = Domain.Robot.Entities.Servo;

namespace Tests.Domain.Robot.Entities
{
    public class ServoShould
    {
        [Fact]
        public void Build() =>
            ServoObject.Builder().WithServoId(1).Build().Should().BeOfType<ServoObject>();

        [Fact]
        public void BuildWithDefaults()
        {
            var servo = ServoObject.Builder().WithServoId(1).Build();

            servo.MinimumAngle.Should().Be(0);
            servo.MaximumAngle.Should().Be(180);
            servo.StartAngle.Should().Be(0);
        }

        [Fact]
        public void BuildWithName() => 
            ServoObject.Builder().WithServoId(1).WithName("TestServo").Build()
                                    .Name.Should().Be("TestServo");

        [Fact]
        public void BuildWithCustomAngles()
        {
            var minAngle = 40;
            var maxAngle = 80;
            var startAngle = 70;

            var servo = ServoObject.Builder().WithServoId(1)
                                                            .WithMinimumAngle(minAngle)
                                                            .WithMaximumAngle(maxAngle)
                                                            .WithStartAngle(startAngle)
                                                            .Build();

            servo.MinimumAngle.Should().Be(minAngle);
            servo.MaximumAngle.Should().Be(maxAngle);
            servo.StartAngle.Should().Be(startAngle);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void ThrowWhenStartAngleIsInvalid(int startAngle)
        {
            var minAngle = 40;
            var maxAngle = 80;

            var action = new Action(() => ServoObject.Builder().WithServoId(1)
                                                                                .WithMinimumAngle(minAngle)
                                                                                .WithMaximumAngle(maxAngle)
                                                                                .WithStartAngle(startAngle)
                                                                                .Build());
                       
            action.Should().Throw<InvalidOperationException>().WithMessage($"Start angle must be between {minAngle} and {maxAngle}");
        }

        [Fact]
        public void ThrowWhenNewAngleIsInvalid()
        {
            var minAngle = 40;
            var maxAngle = 80;

            var servo = ServoObject.Builder().WithServoId(1)                                
                                                            .WithMinimumAngle(minAngle)
                                                            .WithMaximumAngle(maxAngle)
                                                            .WithStartAngle(minAngle)
                                                            .Build();

            var action = new Action(() => servo.SetNewAngle(30));
            action.Should().Throw<InvalidOperationException>().WithMessage($"Angle must be between {minAngle} and {maxAngle}");
        }

        [Theory]
        [InlineData(80)]
        [InlineData(40)]
        public void ThrowWhenMinimumAngleIsLargerThanOrEqualToMaximalAngle(int minimumAngle)
        {            
            var maxAngle = 40;

            var action = new Action(() => ServoObject.Builder().WithServoId(1)
                                                                                    .WithMinimumAngle(minimumAngle)
                                                                                    .WithMaximumAngle(maxAngle)
                                                                                    .Build());
                        
            action.Should().Throw<InvalidOperationException>().WithMessage("The minimum angle must be smaller than the maximum angle");
        }

        [Theory]
        [InlineData(20)]
        [InlineData(40)]
        public void ThrowWhenMaximumAngleIsSmallerThanOrEqualToMinimumAngle(int maximumAngle)
        {
            var minAngle = 40;

            var action = new Action(() => ServoObject.Builder().WithServoId(1)
                                                                                    .WithMinimumAngle(minAngle)
                                                                                    .WithMaximumAngle(maximumAngle)
                                                                                    .Build());

            action.Should().Throw<InvalidOperationException>().WithMessage("The minimum angle must be smaller than the maximum angle");
        }
    }
}
