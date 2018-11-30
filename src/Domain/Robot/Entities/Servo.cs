using Domain.Robot.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Robot.Entities
{
    public class Servo : IServo
    {
        public int ServoId { get; private set; }

        public int MinimumAngle { get; private set; }

        public int MaximumAngle { get; private set; }

        public int CurrentAngle { get; private set; }

        public string Name { get; private set; }

        public void SetAngle(int angle)
        {
            throw new NotImplementedException();
        }

        public static ServoBuilder Builder => new ServoBuilder ();

        private Servo() { }

        public class ServoBuilder
        {
            private int _servoId;

            private int _minimumAngle;

            private int _maximumAngle;

            private int _currentAngle;

            private string _name;

            public ServoBuilder WithServoId(int servoId)
            {
                _servoId = servoId;
                return this;
            }

            public ServoBuilder WithMinimumAngle(int minimumAngle)
            {
                _minimumAngle = minimumAngle;
                return this;
            }

            public ServoBuilder WithMaximumAngle(int maximumAngle)
            {
                _maximumAngle = maximumAngle;
                return this;
            }

            public ServoBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Servo Build()
            {
                return new Servo
                {
                    ServoId = _servoId,
                    MinimumAngle = _minimumAngle,
                    MaximumAngle = _maximumAngle,
                    Name = _name
                };
            }
        }
    }
}
