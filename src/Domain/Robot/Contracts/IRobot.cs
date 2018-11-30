using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Robot.Contracts
{
    public interface IRobot
    {
        IEnumerable<IServo> Servos { get; }

        void MoveServo(int servoId, int angle);
    }
}
