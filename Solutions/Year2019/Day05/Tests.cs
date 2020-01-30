using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day05_Tests
    {
        private readonly Day05 _day05;

        public Day05_Tests()
        {
            _day05 = new Day05();
        }

        [Fact]
        public void Test_Answer_Part1()
        {
            Assert.Equal("16434972", _day05.Part1);
        }

        [Fact]
        public void Test_Answer_Part2()
        {
            Assert.Equal("16694270", _day05.Part2);
        }

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 7, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 7, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 10, 1)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 10, 1)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 5, 999)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 8, 1000)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 13, 1001)]
        public void Test_Part2_AoCExamples(string programInput, int inputModifier, int expectedOutput)
        {
            Assert.Equal(expectedOutput, _day05.RunComputer(programInput, inputModifier));
        }
    }
}
