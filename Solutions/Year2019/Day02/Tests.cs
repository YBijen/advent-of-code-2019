using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day02_Tests
    {
        private readonly Day02 _day02;

        public Day02_Tests()
        {
            _day02 = new Day02();
        }

        [Theory]
        [InlineData(new int[] { 1, 0, 0, 0, 99 }, new int[] { 2, 0, 0, 0, 99 })]
        public void AoCExamples_Part1(IEnumerable<int> input, IEnumerable<int> expected)
        {
            Assert.True(_day02.PerformSteps(input.ToList()).SequenceEqual(expected));
        }
    }
}
