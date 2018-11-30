namespace Domain.Robot.Contracts
{
    public interface IServo
    {
        int ServoId { get; }

        int MinimumAngle { get; }

        int MaximumAngle { get; }

        int CurrentAngle { get; }

        string Name { get; }

        void SetAngle(int angle);
    }
}
