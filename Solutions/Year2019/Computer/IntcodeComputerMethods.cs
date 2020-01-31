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
                var parameterValue = opcodeString[MAX_PARAMETERS - i];
                if(parameterValue == '0')
                {
                    result.Add(ParameterMode.Position);
                }
                else if(parameterValue == '1')
                {
                    result.Add(ParameterMode.Immediate);
                }
                else if (parameterValue == '2')
                {
                    result.Add(ParameterMode.Relative);
                }
            }
            return result;
        }

        public Opcode GetOpcodeFromParameter(Opcode opcode) => (Opcode)Convert.ToInt32(((int)opcode).ToString().Substring(((int)opcode).ToString().Length - 2));

        public bool IsParameterMode(Opcode opcode) => !Enum.IsDefined(typeof(Opcode), opcode);

        public List<long> HandleOpcodeAdd(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = (int)GetValueAtIndex(program, currentIndex + 3);
            return SetValueAtIndex(program, updateIndex, value1 + value2);
        }

        public List<long> HandleOpcodeMultiply(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = (int)GetValueAtIndex(program, currentIndex + 3);
            return SetValueAtIndex(program, updateIndex, value1 * value2);
        }

        public List<long> HandleProcessInput(List<long> program, List<ParameterMode> modes, int currentIndex, long inputModifier)
        {
            var updateIndex = (int)GetValueForParameter(program, currentIndex + 1, modes[0]);
            return SetValueAtIndex(program, updateIndex, inputModifier);
        }

        public long HandleProcessOutput(List<long> program, List<ParameterMode> modes, int currentIndex) =>
             GetValueForParameter(program, currentIndex + 1, modes[0]);

        public List<long> HandleLessThan(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = (int)GetValueAtIndex(program, currentIndex + 3);
            return SetValueAtIndex(program, updateIndex, value1 < value2 ? 1 : 0);
        }

        public List<long> HandleEquals(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(program, currentIndex + 2, modes[1]);
            var updateIndex = (int)GetValueAtIndex(program, currentIndex + 3);
            return SetValueAtIndex(program, updateIndex, value1 == value2 ? 1 : 0);
        }

        public int FetchRelativeBaseModifier(List<long> program, List<ParameterMode> modes, int currentIndex) =>
            (int)GetValueForParameter(program, currentIndex + 1, modes[0]);

        public long GetValueForParameter(List<long> program, int index, ParameterMode mode) => mode switch
        {
            ParameterMode.Position => GetValueAtIndex(program, (int)GetValueAtIndex(program, index)),
            ParameterMode.Immediate => GetValueAtIndex(program, index),
            ParameterMode.Relative => GetValueAtIndex(program, (int)GetValueAtIndex(program, index) + RelativeBase),
            _ => throw new Exception($"Missing mode: {mode}")
        };

        private long GetValueAtIndex(List<long> program, int index)
        {
            if(index >= program.Count)
            {
                return 0;
            }

            return program[index];
        }

        public int FindIncrementForJumpIfTrue(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            if (GetValueForParameter(program, currentIndex + 1, modes[0]) != 0)
            {
                return (int)GetValueForParameter(program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }

        public int FindIncrementForJumpIfFalse(List<long> program, List<ParameterMode> modes, int currentIndex)
        {
            if (GetValueForParameter(program, currentIndex + 1, modes[0]) == 0)
            {
                return (int)GetValueForParameter(program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }

        private List<long> SetValueAtIndex(List<long> program, int index, long value)
        {
            if(index > program.Count)
            {
                program.AddRange(Enumerable.Range(0, (index - program.Count) + 2).Select(v => 0L));
            }

            program[index] = value;
            return program;
        }
    }
}
