using Domain.Instructions.Entities;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Instrionctions.Entities
{
    public class InstructionShould
    {
        [Fact]
        public void Build() =>
            Instruction.Builder().WithName("Test").Build()
                .Should().BeOfType<Instruction>();

        [Fact]
        public void BuildWithName() =>
            Instruction.Builder().WithName("GrabMarbles").Build().Name
            .Should().Be("GrabMarbles");
    }
}
