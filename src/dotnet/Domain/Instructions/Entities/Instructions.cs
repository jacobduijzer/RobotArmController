using System;
using System.Collections.Generic;
using Domain.Instructions.Contracts;

namespace Domain.Instructions.Entities
{
    public class Instructions : IInstructions
    {
        public string Name { get; private set; }

        public IList<string> Lines { get; private set; }
               
        private Instructions()
        {
            Lines = new List<string>();
        }

        public IInstructions AddLine(string code)
        {
            Lines.Add(code);
            return this;
        }

        public void InsertLine(int lineNumber, string code)
        {
            if (lineNumber >= Lines.Count)
                throw new InvalidOperationException($"Can not insert line, there is no line {lineNumber}!");

            Lines.Insert(lineNumber, code);
        }

        public void UpdateLine(int lineNumber, string code)
        {
            if (lineNumber >= Lines.Count)
                throw new InvalidOperationException($"Can not update line, there is no line {lineNumber}!");

            Lines[lineNumber] = code;
        } 

        public void DeleteLine(int lineNumber)
        {
            if (lineNumber >= Lines.Count)
                throw new InvalidOperationException($"Can not delete line, there is no line {lineNumber}!");

            Lines.RemoveAt(lineNumber);
        }        

        #region Builder

        public static InstructionsBuilder Builder() => new InstructionsBuilder();
        
        public class InstructionsBuilder
        {
            private string _name;

            public InstructionsBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Instructions Build()
            {
                return new Instructions()
                {
                    Name = _name ?? throw new InvalidOperationException("The instruction set needs a unique name to identify the set.")
                };
            }
        }

        #endregion
    }
}
