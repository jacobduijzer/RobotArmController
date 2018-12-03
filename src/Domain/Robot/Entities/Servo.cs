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

        public int NewAngle { get; private set; }

        public string Name { get; private set; }

        public void SetNewAngle(int angle)
        {
            if (angle < MinimumAngle || angle > MaximumAngle)
                throw new InvalidOperationException($"Angle must be between {MinimumAngle} and {MaximumAngle}");

            if (angle == CurrentAngle) return;

            NewAngle = angle;
        }

        public void ServoHasMoved() => CurrentAngle = NewAngle;

        public static ServoBuilder Builder => new ServoBuilder ();

        private Servo() { }

        public class ServoBuilder
        {
            private int _servoId = -1;

            private int _minimumAngle = 0;

            private int _maximumAngle = 180;

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
                if (_servoId < 0) throw new InvalidOperationException($"Servo ID {_servoId} is invalid");

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
