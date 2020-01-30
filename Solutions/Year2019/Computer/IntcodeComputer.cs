using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019.Computer
{
    public class IntcodeComputer
    {
        private int ProgramOutput = 0;

        private readonly IntcodeComputerMethods _intcodeComputerMethods = new IntcodeComputerMethods();

        public int Run(string programInput, int input)
        {
            var queue = new Queue<int>();
            queue.Enqueue(input);
            return Run(programInput, queue);
        }

        public int Run(string programInput, Queue<int> input)
        {
            var program = _intcodeComputerMethods.ConvertProgramInputToProgram(programInput);

            var currentIndex = 0;
            while (true)
            {
                var opcode = (Opcode)program[currentIndex];
                if (opcode == Opcode.Stop)
                {
                    break;
                }

                // Handle the Opcode Modes and possible parameters
                var modes = _intcodeComputerMethods.GetModesForOpcode(opcode);
                if (_intcodeComputerMethods.IsParameterMode(opcode))
                {
                    opcode = _intcodeComputerMethods.GetOpcodeFromParameter(opcode);
                }

                // Handle Opcode Logic
                if(opcode == Opcode.ProcessOutput)
                {
                    this.ProgramOutput = _intcodeComputerMethods.HandleProcessOutput(program, modes, currentIndex);
                }
                else if(opcode == Opcode.ProcessInput)
                {
                    program = _intcodeComputerMethods.HandleProcessInput(program, currentIndex, input.Dequeue());
                }
                else
                {
                    program = opcode switch
                    {
                        Opcode.Add => _intcodeComputerMethods.HandleOpcodeAdd(program, modes, currentIndex),
                        Opcode.Multiply => _intcodeComputerMethods.HandleOpcodeMultiply(program, modes, currentIndex),
                        Opcode.LessThan => _intcodeComputerMethods.HandleLessThan(program, modes, currentIndex),
                        Opcode.Equals => _intcodeComputerMethods.HandleEquals(program, modes, currentIndex),
                        Opcode.JumpIfTrue => program,
                        Opcode.JumpIfFalse => program,
                        _ => throw new Exception($"The given opcode: {opcode} is not simple.")
                    };
                }

                // Find the index increment for the given opcode
                if (opcode == Opcode.JumpIfFalse)
                {
                    currentIndex = _intcodeComputerMethods.FindIncrementForJumpIfFalse(program, modes, currentIndex);
                }
                else if (opcode == Opcode.JumpIfTrue)
                {
                    currentIndex = _intcodeComputerMethods.FindIncrementForJumpIfTrue(program, modes, currentIndex);
                }
                else
                {
                    currentIndex += opcode switch
                    {
                        Opcode.Add => 4,
                        Opcode.Multiply => 4,
                        Opcode.ProcessInput => 2,
                        Opcode.ProcessOutput => 2,
                        Opcode.LessThan => 4,
                        Opcode.Equals => 4,
                        _ => throw new Exception($"An index increment was not given for the opcode: {opcode}.")
                    };
                }
            }

            return ProgramOutput;
        }
    }
}
