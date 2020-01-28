using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day05_Tests
    {
        private readonly Day05 _day05;

        public Day05_Tests()
        {
            _day05 = new Day05();
        }

        [Fact]
        public void Test_HandleOpcodeAdd_Position()
        {
            var program = new List<int> { 1, 2, 3, 0 };

            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Position
            };

            var output = _day05.HandleOpcodeAdd(program, modes, 0);

            var expectedOutput = new List<int> { 3, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeAdd_Immediate()
        {
            var program = new List<int> { 1, 2, 3, 0 };

            var modes = new List<Mode>
            {
                Mode.Immediate,
                Mode.Immediate
            };

            var output = _day05.HandleOpcodeAdd(program, modes, 0);

            var expectedOutput = new List<int> { 5, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeAdd_Combination()
        {
            var program = new List<int> { 1, 2, 3, 0 };

            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Immediate
            };

            var output = _day05.HandleOpcodeAdd(program, modes, 0);

            var expectedOutput = new List<int> { 6, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeMultiply_Position()
        {
            var program = new List<int> { 2, 2, 3, 0 };

            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Position
            };

            var output = _day05.HandleOpcodeMultiply(program, modes, 0);

            var expectedOutput = new List<int> { 0, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeMultiply_Immediate()
        {
            var program = new List<int> { 2, 2, 3, 0 };

            var modes = new List<Mode>
            {
                Mode.Immediate,
                Mode.Immediate
            };

            var output = _day05.HandleOpcodeMultiply(program, modes, 0);

            var expectedOutput = new List<int> { 6, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeMultiply_Combination()
        {
            var program = new List<int> { 2, 2, 3, 0 };

            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Immediate
            };

            var output = _day05.HandleOpcodeMultiply(program, modes, 0);

            var expectedOutput = new List<int> { 9, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleInput()
        {
            var program = new List<int> { 3, 2, 0 };
            var input = 1;

            var output = _day05.HandleProcessInput(program, 0, input);

            var expectedOutput = new List<int> { 3, 2, 1 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_FindLastOutput()
        {
            var program = new List<int> { 3, 2, 0, 4, 2, 1, 5, 2, 6, 4, 1, 99 };
            Assert.Equal(2, _day05.GetFinalOutputForProgram(program));
        }

        [Fact]
        public void Test_Part1()
        {
            Assert.Equal("16434972", _day05.Part1);
        }
    }
}
