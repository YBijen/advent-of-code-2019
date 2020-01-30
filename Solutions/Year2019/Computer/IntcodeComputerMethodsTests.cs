using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions.Year2019.Computer
{
    public class IntcodeComputerMethodsTests
    {
        private readonly IntcodeComputerMethods _methods;

        public IntcodeComputerMethodsTests()
        {
            _methods = new IntcodeComputerMethods();
        }

        [Fact]
        public void Test_HandleOpcodeAdd_Position()
        {
            var program = new List<long> { 1, 2, 3, 0 };

            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Position
            };

            var output = _methods.HandleOpcodeAdd(program, modes, 0);

            var expectedOutput = new List<long> { 3, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeAdd_Immediate()
        {
            var program = new List<long> { 1, 2, 3, 0 };

            var modes = new List<ParameterMode>
            {
                ParameterMode.Immediate,
                ParameterMode.Immediate
            };

            var output = _methods.HandleOpcodeAdd(program, modes, 0);

            var expectedOutput = new List<long> { 5, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeAdd_Combination()
        {
            var program = new List<long> { 1, 2, 3, 0 };

            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Immediate
            };

            var output = _methods.HandleOpcodeAdd(program, modes, 0);

            var expectedOutput = new List<long> { 6, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeMultiply_Position()
        {
            var program = new List<long> { 2, 2, 3, 0 };

            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Position
            };

            var output = _methods.HandleOpcodeMultiply(program, modes, 0);

            var expectedOutput = new List<long> { 0, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeMultiply_Immediate()
        {
            var program = new List<long> { 2, 2, 3, 0 };

            var modes = new List<ParameterMode>
            {
                ParameterMode.Immediate,
                ParameterMode.Immediate
            };

            var output = _methods.HandleOpcodeMultiply(program, modes, 0);

            var expectedOutput = new List<long> { 6, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_HandleOpcodeMultiply_Combination()
        {
            var program = new List<long> { 2, 2, 3, 0 };

            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Immediate
            };

            var output = _methods.HandleOpcodeMultiply(program, modes, 0);

            var expectedOutput = new List<long> { 9, 2, 3, 0 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Theory]
        [InlineData(new long[] { 7, 3, 0, 0 }, new long[] { 1, 3, 0, 0 })]
        [InlineData(new long[] { 7, 0, 3, 0 }, new long[] { 0, 0, 3, 0 })]
        public void Test_HandleOpcodeLessThan_Position(IEnumerable<long> program, IEnumerable<long> expected)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Position
            };

            var output = _methods.HandleLessThan(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new long[] { 7, 3, 0, 0 }, new long[] { 0, 3, 0, 0 })]
        [InlineData(new long[] { 7, 0, 3, 0 }, new long[] { 1, 0, 3, 0 })]
        public void Test_HandleOpcodeLessThan_Immediate(IEnumerable<long> program, IEnumerable<long> expected)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Immediate,
                ParameterMode.Immediate
            };

            var output = _methods.HandleLessThan(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new long[] { 7, 3, 1, 0 }, new long[] { 1, 3, 1, 0 })]
        [InlineData(new long[] { 7, 1, 0, 0 }, new long[] { 0, 1, 0, 0 })]
        public void Test_HandleOpcodeLessThan_Combination(IEnumerable<long> program, IEnumerable<long> expected)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Immediate
            };

            var output = _methods.HandleLessThan(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new long[] { 7, 3, 3, 0 }, new long[] { 1, 3, 3, 0 })]
        [InlineData(new long[] { 7, 0, 3, 0 }, new long[] { 0, 0, 3, 0 })]
        public void Test_HandleOpcodeEquals_Position(IEnumerable<long> program, IEnumerable<long> expected)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Position
            };

            var output = _methods.HandleEquals(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new long[] { 7, 2, 2, 0 }, new long[] { 1, 2, 2, 0 })]
        [InlineData(new long[] { 7, 2, 3, 0 }, new long[] { 0, 2, 3, 0 })]
        public void Test_HandleOpcodeEquals_Immediate(IEnumerable<long> program, IEnumerable<long> expected)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Immediate,
                ParameterMode.Immediate
            };

            var output = _methods.HandleEquals(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new long[] { 7, 3, 0, 0 }, new long[] { 1, 3, 0, 0 })]
        [InlineData(new long[] { 7, 1, 0, 0 }, new long[] { 0, 1, 0, 0 })]
        public void Test_HandleOpcodeEquals_Combination(IEnumerable<long> program, IEnumerable<long> expected)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Immediate
            };

            var output = _methods.HandleEquals(program.ToList(), modes, 0);
            Assert.True(output.SequenceEqual(expected));
        }

        [Theory]
        [InlineData(new long[] { 7, 1, 0 }, 7)]
        [InlineData(new long[] { 7, 2, 0 }, 3)]
        public void Test_HandleOpcodeJumpIfTrue_Position(IEnumerable<long> program, int expectedIndexIncrement)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Position
            };

            Assert.Equal(_methods.FindIncrementForJumpIfTrue(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Theory]
        [InlineData(new long[] { 7, 1, 0 }, 0)]
        [InlineData(new long[] { 7, 0, 0 }, 3)]
        public void Test_HandleOpcodeJumpIfTrue_Immediate(IEnumerable<long> program, int expectedIndexIncrement)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Immediate,
                ParameterMode.Immediate
            };

            Assert.Equal(_methods.FindIncrementForJumpIfTrue(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Theory]
        [InlineData(new long[] { 7, 1, 0 }, 3)]
        [InlineData(new long[] { 7, 2, 0 }, 7)]
        public void Test_HandleOpcodeJumpIfFalse_Position(IEnumerable<long> program, int expectedIndexIncrement)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Position,
                ParameterMode.Position
            };

            Assert.Equal(_methods.FindIncrementForJumpIfFalse(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Theory]
        [InlineData(new long[] { 7, 1, 0 }, 3)]
        [InlineData(new long[] { 7, 0, 0 }, 0)]
        public void Test_HandleOpcodeJumpIfFalse_Immediate(IEnumerable<long> program, int expectedIndexIncrement)
        {
            var modes = new List<ParameterMode>
            {
                ParameterMode.Immediate,
                ParameterMode.Immediate
            };

            Assert.Equal(_methods.FindIncrementForJumpIfFalse(program.ToList(), modes, 0), expectedIndexIncrement);
        }

        [Fact]
        public void Test_HandleInput()
        {
            var program = new List<long> { 3, 2, 0 };

            var output = _methods.HandleProcessInput(program, 0, 1);

            var expectedOutput = new List<long> { 3, 2, 1 };
            Assert.True(output.SequenceEqual(expectedOutput));
        }

        [Fact]
        public void Test_GetModesForOpcode()
        {
            var modes = _methods.GetModesForOpcode(Opcode.Add);
            //Assert.True(modes.Count == 3);
            Assert.True(modes.All(m => m == ParameterMode.Position));
        }

        [Fact]
        public void Test_GetModesForParameterOpcode()
        {
            var modes = _methods.GetModesForOpcode((Opcode)101); // Will become: "00101"
            //Assert.True(modes.Count == 3);
            Assert.True(modes[0] == ParameterMode.Immediate);
            Assert.True(modes[1] == ParameterMode.Position);
            Assert.True(modes[2] == ParameterMode.Position);
        }

        [Fact]
        public void Test_GetOpcodeFromParameter()
        {
            Assert.Equal(Opcode.Add, _methods.GetOpcodeFromParameter((Opcode)101));
            Assert.Equal(Opcode.Multiply, _methods.GetOpcodeFromParameter((Opcode)1102));
            Assert.Equal(Opcode.LessThan, _methods.GetOpcodeFromParameter((Opcode)10107));
        }

        [Fact]
        public void Test_IsParameterMode()
        {
            Assert.True(_methods.IsParameterMode((Opcode)101));
            Assert.True(_methods.IsParameterMode((Opcode)1101));
            Assert.True(_methods.IsParameterMode((Opcode)11101));
            Assert.True(_methods.IsParameterMode((Opcode)10101));
            Assert.False(_methods.IsParameterMode((Opcode)1));
            Assert.False(_methods.IsParameterMode((Opcode)7));
        }

        [Theory]
        [InlineData(new long[] { 4, 2, 0, 1 }, 0)]
        [InlineData(new long[] { 4, 2, 1, 1 }, 1)]
        public void Test_HandleOpcodeOutput_Position(IEnumerable<long> program, int expectedOutput)
        {
            var modes = new List<ParameterMode> { ParameterMode.Position };
            Assert.Equal(expectedOutput, _methods.HandleProcessOutput(program.ToList(), modes, 0));
        }

        [Theory]
        [InlineData(new long[] { 4, 2, 0, 1 }, 2)]
        [InlineData(new long[] { 4, 3, 1, 1 }, 3)]
        public void Test_HandleOpcodeOutput_Immediate(IEnumerable<long> program, int expectedOutput)
        {
            var modes = new List<ParameterMode> { ParameterMode.Immediate };
            Assert.Equal(expectedOutput, _methods.HandleProcessOutput(program.ToList(), modes, 0));
        }

        [Fact]
        public void Test_GetValueForParameter_Position()
        {
            var program = new List<long> { 3, 2, 1, 0 };
            Assert.Equal(0, _methods.GetValueForParameter(program, 0, ParameterMode.Position));
        }

        [Fact]
        public void Test_GetValueForParameter_Immediate()
        {
            var program = new List<long> { 3, 2, 1, 0 };
            Assert.Equal(3, _methods.GetValueForParameter(program, 0, ParameterMode.Immediate));
        }

        [Fact]
        public void Test_GetValueForParameter_Relative()
        {
            var program = new List<long> { 3, 2, 1, 0 };
            _methods.RelativeBase = -1;
            Assert.Equal(1, _methods.GetValueForParameter(program, 0, ParameterMode.Relative));
        }

        [Fact]
        public void Test_FetchRelativeBaseModifier_Position()
        {
            var modes = new List<ParameterMode> { ParameterMode.Position };

            var program = new List<long> { 2, 2, 1, 0, 4, 6, 2 };
            Assert.Equal(1, _methods.FetchRelativeBaseModifier(program, modes, 0));
        }

        [Fact]
        public void Test_FetchRelativeBaseModifier_Immediate()
        {
            var modes = new List<ParameterMode> { ParameterMode.Immediate };

            var program = new List<long> { 2, 2, 1, 0, 4, 6, 2 };
            Assert.Equal(2, _methods.FetchRelativeBaseModifier(program, modes, 0));
        }

        [Fact]
        public void Test_FetchRelativeBaseModifier_Relative()
        {
            _methods.RelativeBase = 4;

            var modes = new List<ParameterMode> { ParameterMode.Relative };

            var program = new List<long> { 2, 2, 1, 0, 4, 6, 7, 8 };
            Assert.Equal(7, _methods.FetchRelativeBaseModifier(program, modes, 0));
        }
    }
}
