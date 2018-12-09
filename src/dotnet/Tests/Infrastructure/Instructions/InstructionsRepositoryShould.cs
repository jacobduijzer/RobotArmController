using System.IO;
using FluentAssertions;
using Infrastructure.Instructions;
using Xunit;

using InstructionsObject = Domain.Instructions.Entities.Instructions;

namespace Tests.Infrastructure.Instructions
{
    public class InstructionsRepositoryShould
    {
        [Fact]
        public void Construct() =>
            new InstructionsRepository().Should().BeOfType<InstructionsRepository>();

        //[Fact]
        //public void WriteToFile()
        //{
        //    var instructions = InstructionsObject.Builder().WithName("TestInstructions")
        //        .Build();

        //    instructions.AddLine("test01")
        //                .AddLine("test02");

        //    new InstructionsRepository()
        //        .Create(instructions);

        //    File.Exists($"instructions/{instructions.Name}.gcode").Should().BeTrue();
        //}

        //[Fact]
        //public void ReadFromFile()
        //{

        //}
    }
}
