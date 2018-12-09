using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Robot.Contracts
{
    public interface IRobotService
    {
        bool Initialize();

        //void MoveServo(int servoId, int angle);
    }
}
