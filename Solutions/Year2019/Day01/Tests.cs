using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Tests
    {
        private readonly Day01 _day01;

        public Tests()
        {
            _day01 = new Day01();
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void RunExamplesPart1(int mass, int requiredFuel)
        {
            Assert.Equal(_day01.CalculateRequiredFuel(mass), requiredFuel);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void RunExamplesPart2(int mass, int requiredFuel)
        {
            Assert.Equal(_day01.CalculateAllRequiredFuel(mass), requiredFuel);
        }
    }
}
