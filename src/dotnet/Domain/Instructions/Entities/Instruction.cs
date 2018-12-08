using System;
using System.Collections.Generic;
using Domain.Instructions.Contracts;

namespace Domain.Instructions.Entities
{
    public class Instruction : IInstruction
    {
        public string Name { get; private set; }

        public IList<string> Lines { get; private set; }
               
        private Instruction()
        {
            Lines = new List<string>();
        }

        public void AddLine(string code) => 
            Lines.Add(code);

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

        public static InstructionBuilder Builder() => new InstructionBuilder();
        
        public class InstructionBuilder
        {
            private string _name;

            public InstructionBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Instruction Build()
            {
                return new Instruction()
                {
                    Name = _name ?? throw new ArgumentNullException("Name")
                };
            }
        }

        #endregion
    }
}
