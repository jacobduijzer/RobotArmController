﻿using System;
using Domain.Instructions.Contracts;
using Domain.Instructions.Entities;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Instrionctions.Entities
{
    public class InstructionShould
    {
        private readonly IInstruction _instruction;

        public InstructionShould()
        {
            _instruction = Instruction.Builder().WithName("TestInstruction").Build();
            _instruction.AddLine("TESTLINE01");
            _instruction.AddLine("TESTLINE02");
        }

        [Fact]
        public void Build() =>
            Instruction.Builder().WithName("Test").Build()
                .Should().BeOfType<Instruction>();

        [Fact]
        public void BuildWithName() =>
            Instruction.Builder().WithName("GrabMarbles").Build().Name
            .Should().Be("GrabMarbles");

        [Fact]
        public void AppendNewLine()
        {
            var count = _instruction.Lines.Count;
            _instruction.AddLine("TEST03");
            _instruction.Lines.Count.Should().Be(count + 1);
            _instruction.Lines.Should().Contain("TEST03");
        }

        [Fact]
        public void InsertNewLineAtPosition()
        {
            var line = _instruction.Lines[1];
            _instruction.InsertLine(0, "INSERTEDLINE");
            _instruction.Lines[0].Should().Be("INSERTEDLINE");
            _instruction.Lines[2].Should().Be(line);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(30)]
        public void ThrowWhenInsertingOnNonExistingLine(int lineNumber)
        {
            var instructionSet = Instruction.Builder().WithName("GrabMarbles").Build();
            instructionSet.AddLine("TESTLINE01");
            instructionSet.AddLine("TESTLINE02");
            instructionSet.AddLine("TESTLINE03");

            new Action(() => instructionSet.InsertLine(lineNumber, "test"))
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage($"Can not insert line, there is no line {lineNumber}!");
        }            

        [Fact]
        public void UpdateLineAtPosition()
        {
            _instruction.Lines[1].Should().NotBe("UPDATEDLINE");
            _instruction.UpdateLine(1, "UPDATEDLINE");
            _instruction.Lines[1].Should().Be("UPDATEDLINE");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(30)]
        public void ThrowWhenUpdatingNonExistingLine(int lineNumber)
        {
            var instructionSet = Instruction.Builder().WithName("GrabMarbles").Build();
            instructionSet.AddLine("TESTLINE01");
            instructionSet.AddLine("TESTLINE02");
            instructionSet.AddLine("TESTLINE03");

            new Action(() => instructionSet.UpdateLine(lineNumber, "test"))
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage($"Can not update line, there is no line {lineNumber}!");
        }

        [Fact]
        public void DeleteLineAtPostion()
        {
            var instructionSet = Instruction.Builder().WithName("GrabMarbles").Build();
            instructionSet.AddLine("TESTLINE01");
            instructionSet.AddLine("TESTLINE02");
            instructionSet.AddLine("TESTLINE03");

            instructionSet.DeleteLine(1);
            instructionSet.Lines.Count.Should().Be(2);
            instructionSet.Lines.Should().NotContain("TESTLINE02");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(30)]
        public void ThrowWhenDeletingNonExistingLine(int lineNumber)
        {
            var instructionSet = Instruction.Builder().WithName("GrabMarbles").Build();
            instructionSet.AddLine("TESTLINE01");
            instructionSet.AddLine("TESTLINE02");
            instructionSet.AddLine("TESTLINE03");

            new Action(() => instructionSet.DeleteLine(lineNumber))
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage($"Can not delete line, there is no line {lineNumber}!");
        }
    }
}
