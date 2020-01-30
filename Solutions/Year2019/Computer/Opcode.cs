namespace AdventOfCode.Solutions.Year2019.Computer
{
    public enum Opcode
    {
        Add = 1,
        Multiply = 2,
        ProcessInput = 3,
        ProcessOutput = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Stop = 99
    }
}
