using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019.Computer
{
    public class IntcodeComputer : IntcodeComputerMethods
    {
        private long ProgramOutput = 0;

        public long Run(string programInput, long input)
        {
            var queue = new Queue<long>();
            queue.Enqueue(input);
            return Run(programInput, queue);
        }

        public long Run(string programInput, Queue<long> input)
        {
            base.RelativeBase = 0;
            var program = base.ConvertProgramInputToProgram(programInput);

            var currentIndex = 0;
            while (true)
            {
                var opcode = (Opcode)program[currentIndex];
                if (opcode == Opcode.Stop)
                {
                    break;
                }

                // Handle the Opcode Modes and possible parameters
                var modes = base.GetModesForOpcode(opcode);
                if (base.IsParameterMode(opcode))
                {
                    opcode = base.GetOpcodeFromParameter(opcode);
                }

                // Handle Opcode Logic
                if(opcode == Opcode.ProcessOutput)
                {
                    this.ProgramOutput = base.HandleProcessOutput(program, modes, currentIndex);
                }
                else if(opcode == Opcode.ProcessInput)
                {
                    program = base.HandleProcessInput(program, modes, currentIndex, input.Dequeue());
                }
                else if(opcode == Opcode.UpdateRelativeBase)
                {
                    base.RelativeBase += base.FetchRelativeBaseModifier(program, modes, currentIndex);
                }
                else
                {
                    program = opcode switch
                    {
                        Opcode.Add => base.HandleOpcodeAdd(program, modes, currentIndex),
                        Opcode.Multiply => base.HandleOpcodeMultiply(program, modes, currentIndex),
                        Opcode.LessThan => base.HandleLessThan(program, modes, currentIndex),
                        Opcode.Equals => base.HandleEquals(program, modes, currentIndex),
                        Opcode.JumpIfTrue => program,
                        Opcode.JumpIfFalse => program,
                        Opcode.UpdateRelativeBase => program,
                        _ => throw new Exception($"The given opcode: {opcode} is not simple.")
                    };
                }

                // Find the index increment for the given opcode
                if (opcode == Opcode.JumpIfFalse)
                {
                    currentIndex = base.FindIncrementForJumpIfFalse(program, modes, currentIndex);
                }
                else if (opcode == Opcode.JumpIfTrue)
                {
                    currentIndex = base.FindIncrementForJumpIfTrue(program, modes, currentIndex);
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
                        Opcode.UpdateRelativeBase => 2,
                        _ => throw new Exception($"An index increment was not given for the opcode: {opcode}.")
                    };
                }

                if(opcode == Opcode.ProcessOutput)
                {
                    Console.WriteLine("Current output: " + ProgramOutput);
                }
            }

            return ProgramOutput;
        }
    }
}
