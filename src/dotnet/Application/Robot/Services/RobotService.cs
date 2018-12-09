using System;
using Domain.Communication.Contracts;
using Domain.Program.Contracts;
using Domain.Robot.Contracts;

namespace Application.Robot.Services
{
    public class RobotService : IRobotService
    {
        private readonly ICommunicationService _communicationService;
        private readonly IInstructionsRepository _instructionsRepository;
        private readonly IRobot _robot;        

        public RobotService(ICommunicationService communicationService, 
                            IInstructionsRepository instructionsRepository,
                            IRobot robot)
        {
            _communicationService = communicationService;
            _instructionsRepository = instructionsRepository;
            _robot = robot;
        }

        public bool Initialize()
        {
            if (_communicationService == null) throw new InvalidOperationException("No communication service is provided");

            return _communicationService.Connect();
        }

        //public void MoveServo(int servoId, int angle)
        //{
        //    if (!Servos.Any(x => x.ServoId.Equals(servoId)))
        //        throw new InvalidOperationException($"Servo with id {servoId} does not exist");

        //    // TODO: error handling
        //    Servos.FirstOrDefault(x => x.ServoId.Equals(servoId))
        //        .SetNewAngle(angle);

        //    // TODO: handle communication
        //    _communicationService.SendData($"new angle: {angle}");

        //    Servos.FirstOrDefault(x => x.ServoId.Equals(servoId))
        //        .ServoHasMoved();
        //}
    }
}
