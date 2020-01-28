using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day06_Tests
    {
        private readonly Day06 _day06;

        public Day06_Tests()
        {
            _day06 = new Day06();
        }

        [Fact]
        public void Test_AoCExample_Part1()
        {
            const string EXPECTED_AMOUNT_OF_ORBITS = "42";
            _day06.DebugInput = "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L";

            Assert.Equal(_day06.Part1, EXPECTED_AMOUNT_OF_ORBITS);
        }

        [Fact]
        public void Test_AoCExample_Part2()
        {
            const string EXPECTED_AMOUNT_OF_ORBITS = "4";
            _day06.DebugInput = "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN";

            Assert.Equal(_day06.Part2, EXPECTED_AMOUNT_OF_ORBITS);
        }
    }
}
