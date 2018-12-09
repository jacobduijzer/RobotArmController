using System.Collections.Generic;

namespace Domain.Robot.Contracts
{
    public interface IRobot
    {
        IEnumerable<IServo> Servos { get; }
    }
}
