using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day01 : ASolution {
        
        private const int DIVIDE_BY = 3;
        private const int REDUCE_BY = 2;

        public Day01() : base(1, 2019, "") { }

        protected override string SolvePartOne() =>
            Input.SplitByNewline().Select(v => double.Parse(v)).Sum(CalculateRequiredFuel).ToString();

        protected override string SolvePartTwo() =>
            Input.SplitByNewline().Select(v => double.Parse(v)).Sum(CalculateAllRequiredFuel).ToString();

        public double CalculateRequiredFuel(double mass)
        {
            var fuelNeeded = Math.Floor(mass / DIVIDE_BY);
            if (fuelNeeded >= REDUCE_BY)
            {
                return fuelNeeded - REDUCE_BY;
            }

            return 0;
        }

        public double CalculateAllRequiredFuel(double mass)
        {
            double requiredFuel = 0;
            while (mass > 0)
            {
                var fuelNeeded = CalculateRequiredFuel(mass);
                requiredFuel += fuelNeeded;
                mass = fuelNeeded;
            }
            return requiredFuel;
        }
    }
}
