using Domain.Communication.Contracts;
using Domain.Robot.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Robot.Entities
{
    public class Robot : IRobot
    {
        private ICommunicationService _communicationService;

        public IEnumerable<IServo> Servos { get; private set; }
               
        public bool Initialize()
        {
            if(_communicationService.Connect())
            {
                foreach (var servo in Servos)
                {
                    // TODO
                    //_communicationService.SendData(servo.StartAngle.ToString());
                }
                return true;
            }

            return false;
        }

        public void MoveServo(int servoId, int angle)
        {
            if (!Servos.Any(x => x.ServoId.Equals(servoId)))
                throw new InvalidOperationException($"Servo with id {servoId} does not exist");
            
            // TODO: error handling
            Servos.FirstOrDefault(x => x.ServoId.Equals(servoId))
                .SetNewAngle(angle);

            // TODO: handle communication
            _communicationService.SendData($"new angle: {angle}");

            Servos.FirstOrDefault(x => x.ServoId.Equals(servoId))
                .ServoHasMoved();
        }

        #region Builder

        public static RobotBuilder Builder() => new RobotBuilder();

        public class RobotBuilder
        {
            private List<IServo> _servos = new List<IServo>();

            private ICommunicationService _communicationService;

            public RobotBuilder WithServo(IServo servo)
            {
                if (servo == null) throw new ArgumentNullException(nameof(servo));

                if (_servos.Any(x => x.ServoId.Equals(servo.ServoId)))
                    throw new InvalidOperationException($"Servo with id {servo.ServoId} already exists");

                _servos.Add(servo);
                return this;
            }

            public RobotBuilder WithCommunicationService(ICommunicationService communicationService)
            {
                _communicationService = communicationService ?? throw new ArgumentNullException(nameof(communicationService));
                return this;
            }

            public Robot Build()
            {
                if (!_servos.Any()) throw new InvalidOperationException("No servos are added");

                if (_communicationService == null) throw new InvalidOperationException("No communication service added");

                return new Robot
                {
                    Servos = _servos,
                    _communicationService = _communicationService
                };
            }
        }

        #endregion
    }
}
