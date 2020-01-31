using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day09_Tests
    {
        private readonly Day09 _day09;

        public Day09_Tests()
        {
            _day09 = new Day09();
        }

        [Theory]
        [InlineData("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", 0)]
        [InlineData("1102,34915192,34915192,7,4,7,99,0", 1219070632396864L)]
        [InlineData("104,1125899906842624,99", 1125899906842624L)]
        public void Test_AoCExample_Part1(string input, long expected)
        {
            Assert.Equal(expected, _day09.RunComputer(input, 0));
        }

        [Theory]
        //[InlineData("109,1,203,11,209,8,204,1,99,10,0,42,0", 5)]
        //[InlineData("109,1,203,11,209,8,204,1,99,10,0,42,0", 10)]
        //[InlineData("109,1,203,11,209,8,204,1,99,10,0,42,0", 25)]
        //[InlineData("109,1,203,11,209,8,204,1,99,10,0,42,0", 5000)]
        [InlineData("109,-1,4,1,99", -1)]
        [InlineData("109,-1,104,1,99", 1)]
        [InlineData("109, -1, 204, 1, 99", 109)]
        [InlineData("109,1,9,2,204,-6,99", 204)]
        [InlineData("109,1,109,9,204,-6,99", 204)]
        [InlineData("109,1,209,-1,204,-106,99", 204)]
        [InlineData("109,1,3,3,204,2,99", 25)]
        [InlineData("109,1,203,2,204,2,99", 25)]
        public void Test_RedditExamples(string input, long expected)
        {
            Assert.Equal(expected, _day09.RunComputer(input, (int)expected));
        }



        //[Theory]
        //[InlineData("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", 139629729)]
        //[InlineData("3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", 18216)]
        //public void Test_AoCExample_Part2(string input, int expected)
        //{
        //    Assert.Equal(_day07.RunProgramForGivenInputWithFeedbackloop(input), expected);
        //}

        //[Fact]
        //public void Test_Answer_Part1()
        //{
        //    Assert.Equal("844468", _day07.Part1);
        //}

        //[Fact]
        //public void Test_Answer_Part2()
        //{
        //    Assert.Equal("4215746", _day07.Part2);
        //}
    }
}
