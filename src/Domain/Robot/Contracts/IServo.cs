namespace Domain.Robot.Contracts
{
    public interface IServo
    {
        int ServoId { get; }

        int MinimumAngle { get; }

        int MaximumAngle { get; }

        int StartAngle { get; }

        int CurrentAngle { get; }

        string Name { get; }

        void SetNewAngle(int angle);

        void ServoHasMoved();
    }
}
