using System;
using System.Collections.Generic;
using Domain.Instructions.Contracts;

namespace Domain.Instructions.Entities
{
    public class Instruction : IInstruction
    {
        public string Name { get; private set; }

        public IEnumerable<string> Lines { get; private set; }
               
        private Instruction()
        {
            Lines = new List<string>();
        }

        public void CreateLine()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteLine()
        {
            throw new System.NotImplementedException();
        }

        public void ReadLine()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateLine()
        {
            throw new System.NotImplementedException();
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
