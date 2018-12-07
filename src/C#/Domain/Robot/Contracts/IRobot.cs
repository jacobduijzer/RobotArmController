using System.Collections.Generic;

namespace Domain.Robot.Contracts
{
    public interface IRobot
    {
        IEnumerable<IServo> Servos { get; }

        bool Initialize();

        void MoveServo(int servoId, int angle);
    }
}
