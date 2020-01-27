using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day04_Tests
    {
        private readonly Day04 _day04;

        public Day04_Tests()
        {
            _day04 = new Day04();
        }

        [Fact]
        public void Test_ConvertInputToRange()
        {
            var result = _day04.ConvertInputToRange("120-130");
            Assert.True(result.Count() == 11);
            Assert.True(result.First() == 120);
            Assert.True(result.Last() == 130);
        }

        [Theory]
        [InlineData(1234)]
        [InlineData(1222)]
        [InlineData(12)]
        [InlineData(1111)]
        [InlineData(133)]
        public void Test_ContainsIncrementingDigits_True(int number)
        {
            Assert.True(_day04.ContainsIncrementingDigits(number));
        }

        [Theory]
        [InlineData(1243)]
        [InlineData(1221)]
        [InlineData(21)]
        [InlineData(1230)]
        [InlineData(1001)]
        public void Test_ContainsIncrementingDigits_False(int number)
        {
            Assert.False(_day04.ContainsIncrementingDigits(number));
        }

        [Theory]
        [InlineData(1223)]
        [InlineData(1222)]
        [InlineData(1111)]
        [InlineData(1124)]
        [InlineData(133)]
        public void Test_ContainsDoubleDigits_True(int number)
        {
            Assert.True(_day04.ContainsDoubleDigits(number));
        }

        [Theory]
        [InlineData(1245)]
        [InlineData(1212)]
        [InlineData(21)]
        [InlineData(1230)]
        [InlineData(1021)]
        public void Test_ContainsDoubleDigits_False(int number)
        {
            Assert.False(_day04.ContainsDoubleDigits(number));
        }

        [Fact]
        public void Test_ValidRange()
        {
            var input = "132-200";
            var expectedOutput = 7;

            var range = _day04.ConvertInputToRange(input);
            var actual = range.Count(number => _day04.ContainsIncrementingDigits(number) && _day04.ContainsDoubleDigits(number));

            Assert.Equal(expectedOutput, actual);
        }

        [Theory]
        [InlineData(111111)]
        public void AoCExamples_Part1_True(int number)
        {
            Assert.True(_day04.ContainsIncrementingDigits(number) && _day04.ContainsDoubleDigits(number));
        }

        [Theory]
        [InlineData(223450)]
        [InlineData(123789)]
        public void AoCExamples_Part1_False(int number)
        {
            Assert.False(_day04.ContainsIncrementingDigits(number) && _day04.ContainsDoubleDigits(number));
        }
    }
}
