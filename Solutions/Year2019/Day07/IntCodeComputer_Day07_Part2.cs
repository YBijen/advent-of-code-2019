using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    /// <summary>
    /// Copy of the program from Day 5 with a slight modification to the input parameter
    /// It is now a queue with integers instead of a single integer
    /// </summary>
    public class IntCodeComputer_Day07_Part2
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

        public IntCodeComputer_Day07_Part2(string programInput, int amplifier)
        {
            Program = GetProgramFromInput(programInput);
            Input.Enqueue(amplifier);
        }

        private List<int> Program;

        private int Output;

        private List<int> GetProgramFromInput(string input) => input.Split(",").Select(v => int.Parse(v)).ToList();

        public Queue<int> Input = new Queue<int>();

        public int CurrentIndex = 0;

        public bool IsStopped = false;


        public int RunProgram()
        {
            while (true)
            {
                var opcode = Program[CurrentIndex];
                if (opcode == OPCODE_STOP)
                {
                    IsStopped = true;
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

                Program = opcode switch
                {
                    OPCODE_ADD => HandleOpcodeAdd(Program, modes, CurrentIndex),
                    OPCODE_MULTIPLY => HandleOpcodeMultiply(Program, modes, CurrentIndex),
                    OPCODE_PROCESS_INPUT => HandleProcessInput(Program, CurrentIndex, Input.Dequeue()),
                    OPCODE_PROCESS_OUTPUT => HandleProcessOutput(Program, modes, CurrentIndex),
                    OPCODE_JUMP_IF_TRUE => Program, // Do nothing
                    OPCODE_JUMP_IF_FALSE => Program, // Do nothing
                    OPCODE_LESS_THAN => HandleLessThan(Program, modes, CurrentIndex),
                    OPCODE_EQUALS => HandleEquals(Program, modes, CurrentIndex),
                    _ => throw new Exception($"Opcode {opcode} is not implemented")
                };

                // Find the increment for the current opcode
                CurrentIndex = opcode switch
                {
                    OPCODE_ADD => CurrentIndex += 4,
                    OPCODE_MULTIPLY => CurrentIndex += 4,
                    OPCODE_PROCESS_INPUT => CurrentIndex += 2,
                    OPCODE_PROCESS_OUTPUT => CurrentIndex += 2,
                    OPCODE_JUMP_IF_TRUE => FindIncrementForJumpIfTrue(Program, modes, CurrentIndex),
                    OPCODE_JUMP_IF_FALSE => FindIncrementForJumpIfFalse(Program, modes, CurrentIndex),
                    OPCODE_LESS_THAN => CurrentIndex += 4,
                    OPCODE_EQUALS => CurrentIndex += 4,
                    _ => throw new Exception($"Opcode {opcode} is not implemented")
                };

                if(opcode == OPCODE_PROCESS_OUTPUT)
                {
                    break;
                }
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

        private List<int> HandleOpcodeAdd(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[updateIndex] = value1 + value2;
            return program;
        }

        private List<int> HandleOpcodeMultiply(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            var updateIndex = program[currentIndex + 3];
            program[updateIndex] = value1 * value2;
            return program;
        }

        private List<int> HandleProcessInput(List<int> program, int currentIndex, int inputModifier)
        {
            program[program[currentIndex + 1]] = inputModifier;
            return program;
        }

        private List<int> HandleProcessOutput(List<int> program, List<Mode> modes, int currentIndex)
        {
            Output = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            return program;
        }

        private List<int> HandleLessThan(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            program[program[currentIndex + 3]] = value1 < value2 ? 1 : 0;
            return program;
        }

        private List<int> HandleEquals(List<int> program, List<Mode> modes, int currentIndex)
        {
            var value1 = GetValueForParameter(ref program, currentIndex + 1, modes[0]);
            var value2 = GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            program[program[currentIndex + 3]] = value1 == value2 ? 1 : 0;
            return program;
        }

        private int GetValueForParameter(ref List<int> program, int index, Mode mode) => mode switch
        {
            Mode.Position => program[program[index]],
            Mode.Immediate => program[index],
            _ => throw new Exception($"Missing mode: {mode}")
        };

        private int FindIncrementForJumpIfTrue(List<int> program, List<Mode> modes, int currentIndex)
        {
            if (GetValueForParameter(ref program, currentIndex + 1, modes[0]) != 0)
            {
                return GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }

        private int FindIncrementForJumpIfFalse(List<int> program, List<Mode> modes, int currentIndex)
        {
            if (GetValueForParameter(ref program, currentIndex + 1, modes[0]) == 0)
            {
                return GetValueForParameter(ref program, currentIndex + 2, modes[1]);
            }
            return currentIndex += 3;
        }
    }
}
