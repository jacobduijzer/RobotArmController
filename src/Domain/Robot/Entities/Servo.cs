using System;
using Domain.Robot.Contracts;

namespace Domain.Robot.Entities
{
    public class Servo : IServo
    {
        public int ServoId { get; private set; }

        public int MinimumAngle { get; private set; } 

        public int MaximumAngle { get; private set; }

        public int StartAngle { get; private set; }

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

            private int _startAngle = 0;

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

            public ServoBuilder WithStartAngle(int startAngle)
            {
                _startAngle = startAngle;
                return this;
            }

            public ServoBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Servo Build()
            {
                if (_servoId < 0)
                    throw new InvalidOperationException($"Servo ID {_servoId} is invalid");

                if(_minimumAngle >= _maximumAngle)
                    throw new InvalidOperationException("The minimum angle must be smaller than the maximum angle");

                if (_startAngle < _minimumAngle || _startAngle > _maximumAngle)
                    throw new InvalidOperationException($"Start angle must be between {_minimumAngle} and {_maximumAngle}");
                               
                return new Servo
                {
                    ServoId = _servoId,
                    MinimumAngle = _minimumAngle,
                    MaximumAngle = _maximumAngle,
                    StartAngle = _startAngle,
                    Name = _name
                };
            }
        }
    }
}
