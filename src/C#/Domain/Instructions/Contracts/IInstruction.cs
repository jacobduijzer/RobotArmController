using System.Collections.Generic;

namespace Domain.Instructions.Contracts
{
    public interface IInstruction
    {
        string Name { get; }

        IEnumerable<string> Lines { get; }

        void CreateLine();

        void UpdateLine();

        void ReadLine();

        void DeleteLine();
    }
}
