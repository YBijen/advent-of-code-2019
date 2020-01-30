using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019.Computer
{
    public class StatefulIntcodeComputer
    {
        private readonly IntcodeComputerMethods _intcodeComputerMethods = new IntcodeComputerMethods();

        private int ProgramOutput = 0;
        private int CurrentIndex = 0;

        private List<int> Program;
        public readonly Queue<int> Input = new Queue<int>();

        public bool IsRunning = true;

        public StatefulIntcodeComputer(string programInput, int input)
        {
            this.Program = _intcodeComputerMethods.ConvertProgramInputToProgram(programInput);
            this.Input.Enqueue(input);
        }

        public StatefulIntcodeComputer(string programInput, Queue<int> input)
        {
            this.Program = _intcodeComputerMethods.ConvertProgramInputToProgram(programInput);
            this.Input = input;
        }

        public int Run(bool returnOnOutput = true)
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
                var modes = _intcodeComputerMethods.GetModesForOpcode(opcode);
                if (_intcodeComputerMethods.IsParameterMode(opcode))
                {
                    opcode = _intcodeComputerMethods.GetOpcodeFromParameter(opcode);
                }

                // Handle Opcode Logic
                if(opcode == Opcode.ProcessOutput)
                {
                    this.ProgramOutput = _intcodeComputerMethods.HandleProcessOutput(Program, modes, CurrentIndex);
                }
                else if(opcode == Opcode.ProcessInput)
                {
                    Program = _intcodeComputerMethods.HandleProcessInput(Program, CurrentIndex, Input.Dequeue());
                }
                else
                {
                    Program = opcode switch
                    {
                        Opcode.Add => _intcodeComputerMethods.HandleOpcodeAdd(Program, modes, CurrentIndex),
                        Opcode.Multiply => _intcodeComputerMethods.HandleOpcodeMultiply(Program, modes, CurrentIndex),
                        Opcode.LessThan => _intcodeComputerMethods.HandleLessThan(Program, modes, CurrentIndex),
                        Opcode.Equals => _intcodeComputerMethods.HandleEquals(Program, modes, CurrentIndex),
                        Opcode.JumpIfTrue => Program,
                        Opcode.JumpIfFalse => Program,
                        _ => throw new Exception($"The given opcode: {opcode} is not simple.")
                    };
                }

                // Find the index increment for the given opcode
                if (opcode == Opcode.JumpIfFalse)
                {
                    CurrentIndex = _intcodeComputerMethods.FindIncrementForJumpIfFalse(Program, modes, CurrentIndex);
                }
                else if (opcode == Opcode.JumpIfTrue)
                {
                    CurrentIndex = _intcodeComputerMethods.FindIncrementForJumpIfTrue(Program, modes, CurrentIndex);
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

                if(returnOnOutput && opcode ==Opcode.ProcessOutput)
                {
                    return this.ProgramOutput;
                }
            }

            return this.ProgramOutput;
        }
    }
}
