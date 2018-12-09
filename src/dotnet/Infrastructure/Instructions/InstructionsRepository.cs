using System;
using System.IO;
using Domain.Instructions.Contracts;
using Domain.Program.Contracts;

namespace Infrastructure.Instructions
{
    public class InstructionsRepository : IInstructionsRepository
    {
        public InstructionsRepository()
        {

        }

        public void Create(IInstructions instructions)
        {
            var newFile = File.Create($"{instructions.Name}.gcode");
            var fileWriter = new System.IO.StreamWriter(newFile);
            foreach(var line in instructions.Lines)
            {
                fileWriter.WriteLine(line);
            }
            fileWriter.Dispose();
        }

        public void Delete(IInstructions instructions)
        {
            throw new NotImplementedException();
        }

        public IInstructions GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(IInstructions instructions)
        {
            throw new NotImplementedException();
        }
    }
}
