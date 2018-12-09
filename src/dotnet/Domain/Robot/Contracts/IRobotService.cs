namespace Domain.Robot.Contracts
{
    public interface IRobotService
    {
        bool Initialize();

        bool LoadInstructions(string name);

        //void MoveServo(int servoId, int angle);
    }
}
