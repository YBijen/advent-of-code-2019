using AdventOfCode.Solutions.Year2019.Computer;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019
{
    public class StatefulIntcodeComputer : IntcodeComputerMethods
    {
        private long ProgramOutput = 0;
        private int CurrentIndex = 0;

        private List<long> Program;
        public readonly Queue<long> Input = new Queue<long>();

        public bool IsRunning = true;

        public StatefulIntcodeComputer(string programInput, long input)
        {
            this.Program = base.ConvertProgramInputToProgram(programInput);
            this.Input.Enqueue(input);
        }

        public StatefulIntcodeComputer(string programInput, Queue<long> input)
        {
            this.Program = base.ConvertProgramInputToProgram(programInput);
            this.Input = input;
        }

        public long Run()
        {
            while (true)
            {
                var opcode = (Opcode)Program[CurrentIndex];
                if (opcode == Opcode.Stop)
                {
                    IsRunning = false;
                    break;
                }

                // Handle the Opcode Modes and possible parameters
                var modes = base.GetModesForOpcode(opcode);
                if (base.IsParameterMode(opcode))
                {
                    opcode = base.GetOpcodeFromParameter(opcode);
                }

                // Handle Opcode Logic
                if (opcode == Opcode.ProcessOutput)
                {
                    this.ProgramOutput = base.HandleProcessOutput(Program, modes, CurrentIndex);
                }
                else if (opcode == Opcode.ProcessInput)
                {
                    Program = base.HandleProcessInput(Program, modes, CurrentIndex, Input.Dequeue());
                }
                else
                {
                    Program = opcode switch
                    {
                        Opcode.Add => base.HandleOpcodeAdd(Program, modes, CurrentIndex),
                        Opcode.Multiply => base.HandleOpcodeMultiply(Program, modes, CurrentIndex),
                        Opcode.LessThan => base.HandleLessThan(Program, modes, CurrentIndex),
                        Opcode.Equals => base.HandleEquals(Program, modes, CurrentIndex),
                        Opcode.JumpIfTrue => Program,
                        Opcode.JumpIfFalse => Program,
                        _ => throw new Exception($"The given opcode: {opcode} is not simple.")
                    };
                }

                // Find the index increment for the given opcode
                if (opcode == Opcode.JumpIfFalse)
                {
                    CurrentIndex = base.FindIncrementForJumpIfFalse(Program, modes, CurrentIndex);
                }
                else if (opcode == Opcode.JumpIfTrue)
                {
                    CurrentIndex = base.FindIncrementForJumpIfTrue(Program, modes, CurrentIndex);
                }
                else
                {
                    CurrentIndex += opcode switch
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

                if (opcode == Opcode.ProcessOutput)
                {
                    return this.ProgramOutput;
                }
            }

            return this.ProgramOutput;
        }
    }
}
