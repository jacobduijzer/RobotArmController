using Domain.Instructions.Contracts;

namespace Domain.Program.Contracts
{
    public interface IInstructionsRepository
    {
        IInstructions GetByName(string name);

        void Create(IInstructions instructions);

        void Update(IInstructions instructions);

        void Delete(IInstructions instructions);
    }
}
