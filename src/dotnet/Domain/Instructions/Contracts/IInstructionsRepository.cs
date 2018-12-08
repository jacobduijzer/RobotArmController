namespace Domain.Program.Contracts
{
    public interface IInstructionsRepository
    {
        string GetInstructions(string name);

        void UpdateInstruction(string name, int lineNumber, string instruction);
    }
}
