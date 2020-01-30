using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019.Computer
{
    public class IntcodeComputerMethods
    {
        private const int MAX_PARAMETERS = 3;

        public List<int> ConvertProgramInputToProgram(string programInput) => programInput.Split(",").Select(v => int.Parse(v)).ToList();

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

        public List<int> HandleOpcodeAdd(List<int> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[updateIndex] = value1 + value2;
            return program;
        }

        public List<int> HandleOpcodeMultiply(List<int> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[updateIndex] = value1 * value2;
            return program;
        }

        public List<int> HandleProcessInput(List<int> program, int currentIndex, int inputModifier)
        {
            program[program[currentIndex + 1]] = inputModifier;
            return program;
        }

        public int HandleProcessOutput(List<int> program, List<ParameterMode> modes, int currentIndex) =>
             GetValueForParameter(program, currentIndex + 1, modes[0]);

        public List<int> HandleLessThan(List<int> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            program[program[currentIndex + 3]] = value1 < value2 ? 1 : 0;
            return program;
        }

        public List<int> HandleEquals(List<int> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            program[program[currentIndex + 3]] = value1 == value2 ? 1 : 0;
            return program;
        }

        public int GetValueForParameter(List<int> program, int index, ParameterMode mode) => mode switch
        {
            ParameterMode.Position => program[program[index]],
            ParameterMode.Immediate => program[index],
            _ => throw new Exception($"Missing mode: {mode}")
        };

        public int FindIncrementForJumpIfTrue(List<int> program, List<ParameterMode> modes, int currentIndex)
        {
            if (GetValueForParameter(program, currentIndex + 1, modes[0]) != 0)
            {
                return GetValueForParameter(program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }

        public int FindIncrementForJumpIfFalse(List<int> program, List<ParameterMode> modes, int currentIndex)
        {
            if (GetValueForParameter(program, currentIndex + 1, modes[0]) == 0)
            {
                return GetValueForParameter(program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }
    }
}
