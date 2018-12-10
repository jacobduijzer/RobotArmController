using Domain.Robot.Entities;

namespace Domain.Robot.Contracts
{
    public interface IRobotController
    {
        bool Initialize();

        bool LoadInstructions(string name);

        void PlayInstructions(PlayMode playMode, int numberOfTimes = 0);
    }
}
