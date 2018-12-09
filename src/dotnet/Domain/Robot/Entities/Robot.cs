using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Robot.Contracts;

namespace Domain.Robot.Entities
{
    public class Robot : IRobot
    {
        public IEnumerable<IServo> Servos { get; private set; }
       
        #region Builder

        public static RobotBuilder Builder() => new RobotBuilder();

        public class RobotBuilder
        {
            private readonly List<IServo> _servos = new List<IServo>();
            
            public RobotBuilder WithServo(IServo servo)
            {
                if (servo == null) throw new ArgumentNullException(nameof(servo));

                if (_servos.Any(x => x.ServoId.Equals(servo.ServoId)))
                    throw new InvalidOperationException($"Servo with id {servo.ServoId} already exists");

                _servos.Add(servo);
                return this;
            }

            public Robot Build()
            {
                if (!_servos.Any()) throw new InvalidOperationException("No servos are added");

                return new Robot { Servos = _servos };
            }
        }

        #endregion
    }
}
