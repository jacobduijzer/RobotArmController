using System.Collections.Generic;

namespace Domain.Robot.Contracts
{
    public interface IRobot
    {
        IEnumerable<IServo> Servos { get; }

        void Initialize();

        void MoveServo(int servoId, int angle);
    }
}
