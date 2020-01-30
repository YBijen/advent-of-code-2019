using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019.Computer
{
    /// <summary>
    /// Extending this class in the computers is not the best solution, however it allows me to pass a global value
    /// </summary>
    public class IntcodeComputerMethods
    {
        public int RelativeBase { get; set; } = 0;

        private const int MAX_PARAMETERS = 3;

        public List<long> ConvertProgramInputToProgram(string programInput) => programInput.Split(",").Select(v => long.Parse(v)).ToList();

        public List<ParameterMode> GetModesForOpcode(Opcode opcode)
        {
            if(!this.IsParameterMode(opcode))
            {
                return Enumerable.Range(0, MAX_PARAMETERS).Select(x => ParameterMode.Position).ToList();
            }

            var result = new List<ParameterMode>();
            var opcodeString = ((int)opcode).ToString().PadLeft(5, '0');
            for(var i = 1; i <= MAX_PARAMETERS; i++)
            {
                result.Add(opcodeString[MAX_PARAMETERS - i] == '0' ? ParameterMode.Position : ParameterMode.Immediate);
            }
            return result;
        }

        public Opcode GetOpcodeFromParameter(Opcode opcode) => (Opcode)Convert.ToInt32(((int)opcode).ToString().Substring(((int)opcode).ToString().Length - 2));

        public bool IsParameterMode(Opcode opcode) => !Enum.IsDefined(typeof(Opcode), opcode);

        public List<long> HandleOpcodeAdd(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[(int)updateIndex] = value1 + value2;
            return program;
        }

        public List<long> HandleOpcodeMultiply(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[(int)updateIndex] = value1 * value2;
            return program;
        }

        public List<long> HandleProcessInput(List<long> program, int currentIndex, int inputModifier)
        {
            program[(int)program[(int)currentIndex + 1]] = inputModifier;
            return program;
        }

        public int HandleProcessOutput(List<long> program, List<ParameterMode> modes, int currentIndex) =>
             GetValueForParameter(program, currentIndex + 1, modes[0]);

        public List<long> HandleLessThan(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            program[(int)program[currentIndex + 3]] = value1 < value2 ? 1 : 0;
            return program;
        }

        public List<long> HandleEquals(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            program[(int)program[currentIndex + 3]] = value1 == value2 ? 1 : 0;
            return program;
        }

        public int FetchRelativeBaseModifier(List<long> program, List<ParameterMode> modes, int currentIndex) =>
            GetValueForParameter(program, currentIndex + 1, modes[0]);

        public int GetValueForParameter(List<long> program, int index, ParameterMode mode) => mode switch
        {
            ParameterMode.Position => (int)program[(int)program[index]],
            ParameterMode.Immediate => (int)program[index],
            ParameterMode.Relative => (int)program[(int)program[index] + RelativeBase],
            _ => throw new Exception($"Missing mode: {mode}")
        };

        public int FindIncrementForJumpIfTrue(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            if (GetValueForParameter(program, currentIndex + 1, modes[0]) != 0)
            {
                return GetValueForParameter(program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }

        public int FindIncrementForJumpIfFalse(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            if (GetValueForParameter(program, currentIndex + 1, modes[0]) == 0)
            {
                return GetValueForParameter(program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }
    }
}
