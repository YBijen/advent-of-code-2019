using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day04 : ASolution
    {
        public Day04() : base(4, 2019, "") { }

        protected override string SolvePartOne() {
            var range = ConvertInputToRange(Input);
            return range.Count(number => ContainsIncrementingDigits(number) && ContainsDoubleDigits(number)).ToString();
        }

        protected override string SolvePartTwo() {
            var range = ConvertInputToRange(Input);
            return range.Count(number => ContainsIncrementingDigits(number) && ContainsSpecificDoubleDigits(number)).ToString();
        }

        public bool ContainsIncrementingDigits(int number)
        {
            while(number > 0)
            {
                // Get the last digit
                var lastDigit = GetLastDigit(number);

                // Remove the last digit from the number
                number /= 10;

                // Get the last digit again to compare
                var compareDigit = GetLastDigit(number);

                if (lastDigit < compareDigit)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetLastDigit(int number) => number % 10;

        public bool ContainsDoubleDigits(int number)
        {
            var stringNumber = number.ToString();
            for(var i = 0; i < stringNumber.Length - 1; i++)
            {
                if(stringNumber[i] == stringNumber[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsSpecificDoubleDigits(int number) =>
            number.ToString().GroupBy(number => number).Any(v => v.Count() == 2);

        public IEnumerable<int> ConvertInputToRange(string input)
        {
            var values = input.Split("-").Select(v => Convert.ToInt32(v));
            return Enumerable.Range(values.ElementAt(0), (values.ElementAt(1) - values.ElementAt(0)) + 1);
        }
    }
}
