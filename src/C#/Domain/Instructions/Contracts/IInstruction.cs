using System.Collections.Generic;

namespace Domain.Instructions.Contracts
{
    public interface IInstruction
    {
        string Name { get; }

        IList<string> Lines { get; }

        void AddLine(string code);

        void InsertLine(int lineNumber, string code);

        void UpdateLine(int lineNumber, string code);        

        void DeleteLine(int lineNumber);
    }
}
