using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    /// <summary>
    /// Copy of the program from Day 5 with a slight modification to the input parameter
    /// It is now a queue with integers instead of a single integer
    /// </summary>
    public class IntCodeComputer_Day09
    {
        private const int OPCODE_ADD = 1;
        private const int OPCODE_MULTIPLY = 2;
        private const int OPCODE_PROCESS_INPUT = 3;
        private const int OPCODE_PROCESS_OUTPUT = 4;
        private const int OPCODE_JUMP_IF_TRUE = 5;
        private const int OPCODE_JUMP_IF_FALSE = 6;
        private const int OPCODE_LESS_THAN = 7;
        private const int OPCODE_EQUALS = 8;
        private const int OPCODE_STOP = 99;

        /// <summary>
        /// The output of the program
        /// </summary>
        private int Output = 0;

        private List<int> GetProgramFromInput(string input) => input.Split(",").Select(v => int.Parse(v)).ToList();

        public int RunProgram(string programInput, Queue<int> input)
        {
            var program = GetProgramFromInput(programInput);
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
                    Mode.Position // Adding the third variable even though I don't expect it to be in Immediate mode ever
                };

                if (IsParameterMode(opcode))
                {
                    modes = UpdateParameterModes(modes, opcode);
                    opcode = GetOpcodeFromParameter(opcode);
                }

                program = opcode switch
                {
                    OPCODE_ADD => HandleOpcodeAdd(program, modes, currentIndex),
                    OPCODE_MULTIPLY => HandleOpcodeMultiply(program, modes, currentIndex),
                    OPCODE_PROCESS_INPUT => HandleProcessInput(program, currentIndex, input.Dequeue()),
                    OPCODE_PROCESS_OUTPUT => HandleProcessOutput(program, modes, currentIndex),
                    OPCODE_JUMP_IF_TRUE => program, // Do nothing
                    OPCODE_JUMP_IF_FALSE => program, // Do nothing
                    OPCODE_LESS_THAN => HandleLessThan(program, modes, currentIndex),
                    OPCODE_EQUALS => HandleEquals(program, modes, currentIndex),
                    _ => throw new Exception($"Opcode {opcode} is not implemented")
                };

                // Find the increment for the current opcode
                currentIndex = opcode switch
                {
                    OPCODE_ADD => currentIndex += 4,
                    OPCODE_MULTIPLY => currentIndex += 4,
                    OPCODE_PROCESS_INPUT => currentIndex += 2,
                    OPCODE_PROCESS_OUTPUT => currentIndex += 2,
                    OPCODE_JUMP_IF_TRUE => FindIncrementForJumpIfTrue(program, modes, currentIndex),
                    OPCODE_JUMP_IF_FALSE => FindIncrementForJumpIfFalse(program, modes, currentIndex),
                    OPCODE_LESS_THAN => currentIndex += 4,
                    OPCODE_EQUALS => currentIndex += 4,
                    _ => throw new Exception($"Opcode {opcode} is not implemented")
                };
            }

            return Output;
        }

        private bool IsParameterMode(int opcode) => opcode.ToString().Length >= 2;

        private int GetOpcodeFromParameter(int parameter) => Convert.ToInt32(parameter.ToString().Substring(parameter.ToString().Length - 2));

        private List<Mode> UpdateParameterModes(List<Mode> currentModes, int parameter)
        {
            var str = parameter.ToString().PadLeft(5, '0');

            // Simple check to make sure that the logic is working.
            // For now I don't expect the input to require the 3rd parameter to support Immediate mode
            if (str[0] != '0')
            {
                throw new Exception("The unexpected happened!");
            }

            for (var i = currentModes.Count - 1; i >= 0; i--)
            {
                currentModes[(currentModes.Count - 1) - i] = str[i] == '0' ? Mode.Position : Mode.Immediate;
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

        public List<int> HandleProcessInput(List<int> program, int currentIndex, int inputModifier)
        {
            program[program[currentIndex + 1]] = inputModifier;
            return program;
        }

        private List<int> HandleProcessOutput(List<int> program, List<Mode> modes, int currentIndex)
        {
            Output = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            return program;
        }

        public List<int> HandleLessThan(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            program[program[currentIndex + 3]] = value1 < value2 ? 1 : 0;
            return program;
        }

        public List<int> HandleEquals(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            program[program[currentIndex + 3]] = value1 == value2 ? 1 : 0;
            return program;
        }

        public int GetValueForParameter(ref List<int> program, int index, Mode mode) => mode switch
        {
            Mode.Position => program[program[index]],
            Mode.Immediate => program[index],
            _ => throw new Exception($"Missing mode: {mode}")
        };

        public int FindIncrementForJumpIfTrue(List<int> program, List<Mode> modes, int currentIndex)
        {
            if (GetValueForParameter(ref program, currentIndex + 1, modes[0]) != 0)
            {
                return GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }

        public int FindIncrementForJumpIfFalse(List<int> program, List<Mode> modes, int currentIndex)
        {
            if (GetValueForParameter(ref program, currentIndex + 1, modes[0]) == 0)
            {
                return GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }
    }
}
