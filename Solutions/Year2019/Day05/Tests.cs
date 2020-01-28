using System;
using System.Collections.Generic;
using System.Linq;
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

        [Theory]
        [InlineData(new int[] { 7, 3, 0, 0 }, new int[] { 1, 3, 0, 0 })]
        [InlineData(new int[] { 7, 0, 3, 0 }, new int[] { 0, 0, 3, 0 })]
        public void Test_HandleOpcodeLessThan_Position(IEnumerable<int> program, IEnumerable<int> expected)
        {
            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Position
            };

            var output = _day05.HandleLessThan(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new int[] { 7, 3, 0, 0 }, new int[] { 0, 3, 0, 0 })]
        [InlineData(new int[] { 7, 0, 3, 0 }, new int[] { 1, 0, 3, 0 })]
        public void Test_HandleOpcodeLessThan_Immediate(IEnumerable<int> program, IEnumerable<int> expected)
        {
            var modes = new List<Mode>
            {
                Mode.Immediate,
                Mode.Immediate
            };

            var output = _day05.HandleLessThan(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new int[] { 7, 3, 1, 0 }, new int[] { 1, 3, 1, 0 })]
        [InlineData(new int[] { 7, 1, 0, 0 }, new int[] { 0, 1, 0, 0 })]
        public void Test_HandleOpcodeLessThan_Combination(IEnumerable<int> program, IEnumerable<int> expected)
        {
            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Immediate
            };

            var output = _day05.HandleLessThan(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new int[] { 7, 3, 3, 0 }, new int[] { 1, 3, 3, 0 })]
        [InlineData(new int[] { 7, 0, 3, 0 }, new int[] { 0, 0, 3, 0 })]
        public void Test_HandleOpcodeEquals_Position(IEnumerable<int> program, IEnumerable<int> expected)
        {
            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Position
            };

            var output = _day05.HandleEquals(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new int[] { 7, 2, 2, 0 }, new int[] { 1, 2, 2, 0 })]
        [InlineData(new int[] { 7, 2, 3, 0 }, new int[] { 0, 2, 3, 0 })]
        public void Test_HandleOpcodeEquals_Immediate(IEnumerable<int> program, IEnumerable<int> expected)
        {
            var modes = new List<Mode>
            {
                Mode.Immediate,
                Mode.Immediate
            };

            var output = _day05.HandleEquals(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new int[] { 7, 3, 0, 0 }, new int[] { 1, 3, 0, 0 })]
        [InlineData(new int[] { 7, 1, 0, 0 }, new int[] { 0, 1, 0, 0 })]
        public void Test_HandleOpcodeEquals_Combination(IEnumerable<int> program, IEnumerable<int> expected)
        {
            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Immediate
            };

            var output = _day05.HandleEquals(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 0 }, 7)]
        [InlineData(new int[] { 7, 2, 0 }, 3)]
        public void Test_HandleOpcodeJumpIfTrue_Position(IEnumerable<int> program, int expectedIndexIncrement)
        {
            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Position
            };

            Assert.Equal(_day05.FindIncrementForJumpIfTrue(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 0 }, 0)]
        [InlineData(new int[] { 7, 0, 0 }, 3)]
        public void Test_HandleOpcodeJumpIfTrue_Immediate(IEnumerable<int> program, int expectedIndexIncrement)
        {
            var modes = new List<Mode>
            {
                Mode.Immediate,
                Mode.Immediate
            };

            Assert.Equal(_day05.FindIncrementForJumpIfTrue(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 0 }, 3)]
        [InlineData(new int[] { 7, 2, 0 }, 7)]
        public void Test_HandleOpcodeJumpIfFalse_Position(IEnumerable<int> program, int expectedIndexIncrement)
        {
            var modes = new List<Mode>
            {
                Mode.Position,
                Mode.Position
            };

            Assert.Equal(_day05.FindIncrementForJumpIfFalse(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 0 }, 3)]
        [InlineData(new int[] { 7, 0, 0 }, 0)]
        public void Test_HandleOpcodeJumpIfFalse_Immediate(IEnumerable<int> program, int expectedIndexIncrement)
        {
            var modes = new List<Mode>
            {
                Mode.Immediate,
                Mode.Immediate
            };

            Assert.Equal(_day05.FindIncrementForJumpIfFalse(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Fact]
        public void Test_HandleInput()
        {
            var program = new List<int> { 3, 2, 0 };

            var output = _day05.HandleProcessInput(program, 0, 1);

            var expectedOutput = new List<int> { 3, 2, 1 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_Part1()
        {
            Assert.Equal("16434972", _day05.Part1);
        }

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 7, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 7, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 10, 1)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 10, 1)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 5, 999)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 8, 1000)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 13, 1001)]
        public void Test_Part2_AoCExamples(string programInput, int inputModifier, int expectedOutput)
        {
            _day05.DebugInput = programInput;
            Assert.Equal(expectedOutput, _day05.RunProgram(_day05.GetProgramFromInput(), inputModifier));
        }
    }
}
