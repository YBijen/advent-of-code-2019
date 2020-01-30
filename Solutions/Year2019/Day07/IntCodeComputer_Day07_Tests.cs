using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class IntCodeComputer_Day07_Tests
    {
        private readonly IntCodeComputer_Day07 _intCodeComputer;

        public IntCodeComputer_Day07_Tests()
        {
            _intCodeComputer = new IntCodeComputer_Day07();
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

            var output = _intCodeComputer.HandleOpcodeAdd(program, modes, 0);

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

            var output = _intCodeComputer.HandleOpcodeAdd(program, modes, 0);

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

            var output = _intCodeComputer.HandleOpcodeAdd(program, modes, 0);

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

            var output = _intCodeComputer.HandleOpcodeMultiply(program, modes, 0);

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

            var output = _intCodeComputer.HandleOpcodeMultiply(program, modes, 0);

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

            var output = _intCodeComputer.HandleOpcodeMultiply(program, modes, 0);

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

            var output = _intCodeComputer.HandleLessThan(program.ToList(), modes, 0);
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

            var output = _intCodeComputer.HandleLessThan(program.ToList(), modes, 0);
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

            var output = _intCodeComputer.HandleLessThan(program.ToList(), modes, 0);
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

            var output = _intCodeComputer.HandleEquals(program.ToList(), modes, 0);
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

            var output = _intCodeComputer.HandleEquals(program.ToList(), modes, 0);
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

            var output = _intCodeComputer.HandleEquals(program.ToList(), modes, 0);
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

            Assert.Equal(_intCodeComputer.FindIncrementForJumpIfTrue(program.ToList(), modes, 0), expectedIndexIncrement);
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

            Assert.Equal(_intCodeComputer.FindIncrementForJumpIfTrue(program.ToList(), modes, 0), expectedIndexIncrement);
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

            Assert.Equal(_intCodeComputer.FindIncrementForJumpIfFalse(program.ToList(), modes, 0), expectedIndexIncrement);
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

            Assert.Equal(_intCodeComputer.FindIncrementForJumpIfFalse(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Fact]
        public void Test_HandleInput()
        {
            var program = new List<int> { 3, 2, 0 };

            var output = _intCodeComputer.HandleProcessInput(program, 0, 1);

            var expectedOutput = new List<int> { 3, 2, 1 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }
    }
}
