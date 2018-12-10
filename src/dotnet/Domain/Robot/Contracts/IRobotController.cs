namespace Domain.Robot.Contracts
{
    public interface IRobotController
    {
        bool Initialize();

        bool LoadInstructions(string name);

        //void MoveServo(int servoId, int angle);
    }
}
