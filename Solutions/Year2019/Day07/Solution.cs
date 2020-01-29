using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day07 : ASolution
    {
        private readonly IntCodeComputer_Day07 _intcodeComptuer;

        public Day07() : base(7, 2019, "")
        {
            _intcodeComptuer = new IntCodeComputer_Day07();
        }

        protected override string SolvePartOne() => RunProgramForGivenInput(Input).ToString();

        protected override string SolvePartTwo() => RunProgramForGivenInputWithFeedbackloop(Input).ToString();

        public int RunProgramForGivenInputWithFeedbackloop(string input)
        {
            var itemsToPermuate = new List<int> { 5, 6, 7, 8, 9 };
            var permutationList = GetPermutations(itemsToPermuate, itemsToPermuate.Count);

            // Keep track of the highest output
            var highestOutput = 0;

            foreach (var permutation in permutationList)
            {
                var output = 0;

                // For each value in the permutation we create a computer and supply the permutation value
                var intcodeComputers = permutation.Select(p => new IntCodeComputer_Day07_Part2(input, p)).ToList();

                // Continue running the amplifiers until they are all stopped
                while(intcodeComputers.Any(ic => !ic.IsStopped))
                {
                    foreach(var c in intcodeComputers.Where(x => !x.IsStopped))
                    {
                        c.Input.Enqueue(output);
                        output = c.RunProgram();
                    }
                }

                if (output > highestOutput)
                {
                    highestOutput = output;
                }
            }

            return highestOutput;
        }

        public int RunProgramForGivenInput(string programInput)
        {
            var itemsToPermuate = new List<int> { 0, 1, 2, 3, 4 };
            var permutationList = GetPermutations(itemsToPermuate, itemsToPermuate.Count);

            // Keep track of the highest output
            var highestOutput = 0;

            // The queue which will be used as input for the intcodecomputer
            var intCodeInput = new Queue<int>();

            foreach (var permutation in permutationList)
            {
                var output = 0;
                foreach (var ampSetting in permutation)
                {
                    intCodeInput.Enqueue(ampSetting);
                    intCodeInput.Enqueue(output);

                    output = _intcodeComptuer.RunProgram(programInput, intCodeInput);

                    // A simple check to make sure that each program runs correctly
                    if (intCodeInput.Count != 0)
                    {
                        throw new Exception("Something is left in the queue!");
                    }
                }

                if (output > highestOutput)
                {
                    highestOutput = output;
                }
            }

            return highestOutput;
        }

        /// <summary>
        /// Method "stolen" from: https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private IEnumerable<IList<T>> GetPermutations<T>(ICollection<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            // Continue reducing the list until we can create a list of lists
            return GetPermutations(list, length - 1)
                // Go through each list of values and check if the other list items are already added to these new lists
                // If a list is missing the value then we add it by concatting it
                .SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }).ToList());
        }
    }
}
