using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day02 : ASolution
    {
        private const int OPCODE_ADD = 1;
        private const int OPCODE_MULTIPLY = 2;
        private const int OPCODE_STOP = 99;
        private const int INDEX_INCREMENT = 4;

        public Day02() : base(2, 2019, "") { }

        protected override string SolvePartOne() {
            var input = Input.Split(",").Select(v => int.Parse(v)).ToList();

            // Manual override for part 1
            input[1] = 1;
            input[2] = 0;

            return PerformSteps(input)[0].ToString();
        }
        protected override string SolvePartTwo()
        {
            var input = Input.Split(",").Select(v => int.Parse(v)).ToList();

            return FindNounAndVerbPartTwo(input, 19690720).ToString();
        }

        public List<int> PerformSteps(List<int> input)
        {
            var currentIndex = 0;
            while (true)
            {
                var opcode = input[currentIndex];
                if (opcode == OPCODE_STOP)
                {
                    break;
                }

                var value1 = GetValueOfGivenIndex(input, currentIndex + 1);
                var value2 = GetValueOfGivenIndex(input, currentIndex + 2);
                var updateIndex = input[currentIndex + 3];

                if (opcode == OPCODE_ADD)
                {
                    input[updateIndex] = value1 + value2;
                }
                else if (opcode == OPCODE_MULTIPLY)
                {
                    input[updateIndex] = value1 * value2;
                }

                currentIndex += INDEX_INCREMENT;
            }

            return input;
        }

        private int GetValueOfGivenIndex(List<int> input, int index) => input[input[index]];

        private int FindNounAndVerbPartTwo(List<int> input, int valueToFind)
        {
            // Keep a copy of the original input
            var originalInput = input.ToArray();

            var currentNoun = 0;
            while (true)
            {
                input[1] = currentNoun;
                if (PerformSteps(input)[0] > valueToFind)
                {
                    currentNoun--;

                    // Obtain the value of the current Noun (the previous in the loop)
                    input = originalInput.ToList();
                    input[1] = currentNoun;

                    // Calculate the verb
                    var verb = valueToFind - PerformSteps(input)[0];

                    var answer = 100 * currentNoun + verb;
                    return answer;
                }

                currentNoun++;
                input = originalInput.ToList();
            }
        }
    }
}
