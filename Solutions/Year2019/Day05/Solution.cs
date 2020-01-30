using AdventOfCode.Solutions.Year2019.Computer;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019
{
    class Day05 : ASolution
    {
        private readonly IntcodeComputer _intcodeComputer = new IntcodeComputer();

        public Day05() : base(5, 2019, "") { }

        protected override string SolvePartOne() => RunComputer(Input, 1).ToString();
        protected override string SolvePartTwo() => RunComputer(Input, 5).ToString();

        public int RunComputer(string programInput, int input) => _intcodeComputer.Run(programInput, input);
    }
}
