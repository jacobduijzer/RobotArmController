﻿using System;
using Domain.Communication.Contracts;
using Domain.Instructions.Contracts;
using Domain.Program.Contracts;
using Domain.Robot.Contracts;
using Domain.Robot.Entities;

namespace Application.Robot
{
    public class RobotController : IRobotController
    {
        private readonly ICommunicationService _communicationService;
        private readonly IInstructionsRepository _instructionsRepository;
        private readonly IRobot _robot;

        public RobotController(ICommunicationService communicationService,
                           IInstructionsRepository instructionsRepository,
                           IRobot robot)
        {
            _communicationService = communicationService ?? throw new ArgumentNullException(nameof(communicationService));
            _instructionsRepository = instructionsRepository ?? throw new ArgumentNullException(nameof(instructionsRepository));
            _robot = robot ?? throw new ArgumentNullException(nameof(robot));
        }

        private IInstructions _instructions;

        private bool _isInitialized;

        public bool Initialize() => _isInitialized = _communicationService.Connect();

        public bool LoadInstructions(string name)
        {
            _instructions = _instructionsRepository.GetByName(name);

            return _instructions != null;
        }

        public void PlayInstructions(PlayMode playMode, int numberOfTimes = 0)
        {
            if(!_isInitialized)
                throw new InvalidOperationException("Can not play without initializing robot controller.");

            if (_instructions == null)
                throw new InvalidOperationException("Can not play without instructions.");

            if(playMode.Equals(PlayMode.NumberOfTimes) && numberOfTimes == 0)
                throw new InvalidOperationException("Please provide the number of times.");

            var repeatNumber = playMode.Equals(PlayMode.NumberOfTimes) ? numberOfTimes : 1;
            for(int i = 0; i < repeatNumber; i++)
            {
                foreach (var instruction in _instructions.Lines)
                {
                    _communicationService.SendData(instruction);
                }
            }
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
