using AdventOfCode.Solutions.Year2019.Computer;

namespace AdventOfCode.Solutions.Year2019
{
    class Day09 : ASolution
    {
        private readonly IntcodeComputer _intcodeComputer = new IntcodeComputer();

        public Day09() : base(9, 2019, "") { }

        protected override string SolvePartOne() => RunComputer(Input, 1).ToString();
        protected override string SolvePartTwo() => null; // RunComputer(Input, 5).ToString();

        public long RunComputer(string programInput, int input) => _intcodeComputer.Run(programInput, input);
    }
}
