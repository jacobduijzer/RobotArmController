﻿using System.Collections.Generic;

namespace Domain.Instructions.Contracts
{
    public interface IInstructions
    {
        string Name { get; }

        IList<string> Lines { get; }

        IInstructions AddLine(string code);

        void InsertLine(int lineNumber, string code);

        void UpdateLine(int lineNumber, string code);        

        void DeleteLine(int lineNumber);
    }
}
