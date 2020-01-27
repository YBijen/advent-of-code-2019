using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day05 : ASolution
    {
        private const int OPCODE_ADD = 1;
        private const int OPCODE_MULTIPLY = 2;
        private const int OPCODE_PROCESS_INPUT = 3;
        private const int OPCODE_PROCESS_OUTPUT = 4;
        private const int OPCODE_STOP = 99;
        private const int INDEX_INCREMENT = 4;

        public Day05() : base(5, 2019, "") { }

        protected override string SolvePartOne() {
            var input = Input.Split(",").Select(v => int.Parse(v)).ToList();
            return RunProgram(input, 1)[0].ToString();
        }

        protected override string SolvePartTwo() {
            return null;
        }

        public List<int> RunProgram(List<int> program, int input)
        {
            var currentIndex = 0;
            while (true)
            {
                var opcode = program[currentIndex];
                if (opcode == OPCODE_STOP)
                {
                    break;
                }

                var modes = new List<Mode>
                {
                    Mode.Position,
                    Mode.Position,
                    Mode.Position // Adding the 3rd parameter, but it is not used in this version (yet)
                };

                if(IsParameterMode(opcode))
                {
                    modes = UpdateParameterModes(modes, opcode);
                    opcode = GetOpcodeFromParameter(opcode);
                }

                program = opcode switch
                {
                    OPCODE_ADD => HandleOpcodeAdd(program, modes, currentIndex),
                    OPCODE_MULTIPLY => HandleOpcodeAdd(program, modes, currentIndex),
                    OPCODE_PROCESS_INPUT => throw new NotImplementedException(),
                    OPCODE_PROCESS_OUTPUT => throw new NotImplementedException(),
                    _ => throw new Exception($"Opcode {opcode} is not implemented")
                };

                currentIndex += INDEX_INCREMENT;
            }

            return program;
        }

        private bool IsParameterMode(int opcode) => opcode.ToString().Length >= 2;

        private int GetOpcodeFromParameter(int parameter) => Convert.ToInt32(parameter.ToString().Substring(parameter.ToString().Length - 2));

        private List<Mode> UpdateParameterModes(List<Mode> currentModes, int parameter)
        {
            var str = parameter.ToString().PadLeft(5, '0');
            for(var i = 0; i < currentModes.Count; i++)
            {
                currentModes[i] = str[i] == '0' ? Mode.Position : Mode.Immediate;
            }
            return currentModes;
        }

        public List<int> HandleOpcodeAdd(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[updateIndex] = value1 + value2;
            return program;
        }

        public List<int> HandleOpcodeMultiply(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[updateIndex] = value1 * value2;
            return program;
        }

        private int GetValueForParameter(ref List<int> program, int index, Mode mode) => mode switch
        {
            Mode.Position => GetValueOfGivenIndex(program, index),
            Mode.Immediate => program[index],
            _ => throw new Exception($"Missing mode: {mode}")
        };

        private int GetValueOfGivenIndex(List<int> input, int index) => input[input[index]];
    }
}
