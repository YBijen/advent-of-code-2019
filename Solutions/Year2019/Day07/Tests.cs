﻿using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day07_Tests
    {
        private readonly Day07 _day07;

        public Day07_Tests()
        {
            _day07 = new Day07();
        }

        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 43210)]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", 54321)]
        [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 65210)]
        public void Test_AoCExample_Part1(string input, int expected)
        {
            Assert.Equal(_day07.RunProgramForGivenInput(input), expected);
        }

        [Theory]
        [InlineData("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", 139629729)]
        [InlineData("3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", 18216)]
        public void Test_AoCExample_Part2(string input, int expected)
        {
            Assert.Equal(_day07.RunProgramForGivenInputWithFeedbackloop(input), expected);
        }

        [Fact]
        public void Test_Answer_Part1()
        {
            Assert.Equal("844468", _day07.Part1);
        }

        [Fact]
        public void Test_Answer_Part2()
        {
            Assert.Equal("4215746", _day07.Part2);
        }
    }
}
