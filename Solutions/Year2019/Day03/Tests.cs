using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day03_Tests
    {
        private readonly Day03 _day03;

        public Day03_Tests()
        {
            _day03 = new Day03();
        }

        [Fact]
        public void AoCExamples_1()
        {
            var lines = new List<string>
            {
                "R8,U5,L5,D3",
                "U7,R6,D4,L4"
            };

            _day03.FillNodesWithLineInstructions(lines);
            var collisions = _day03.FindCollisions();
            var distanceToClosestCollision = _day03.FindClosestCollision(collisions);
            Assert.Equal(6, distanceToClosestCollision);

            var collisionsWithDistanceDict = _day03.CalculateDistanceToCollisions(collisions);
            var shortestPathCollision = _day03.DistanceToShortestPathCollision(collisionsWithDistanceDict);
            Assert.Equal(30, shortestPathCollision);
        }

        [Fact]
        public void AoCExamples_2()
        {
            var lines = new List<string>
            {
                "R75,D30,R83,U83,L12,D49,R71,U7,L72",
                "U62,R66,U55,R34,D71,R55,D58,R83"
            };

            _day03.FillNodesWithLineInstructions(lines);
            var collisions = _day03.FindCollisions();
            var distanceToClosestCollision = _day03.FindClosestCollision(collisions);
            Assert.Equal(159, distanceToClosestCollision);

            var collisionsWithDistanceDict = _day03.CalculateDistanceToCollisions(collisions);
            var shortestPathCollision = _day03.DistanceToShortestPathCollision(collisionsWithDistanceDict);
            Assert.Equal(610, shortestPathCollision);
        }

        [Fact]
        public void AoCExamples_3()
        {
            var lines = new List<string>
            {
                "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
                "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"
            };

            _day03.FillNodesWithLineInstructions(lines);
            var collisions = _day03.FindCollisions();
            var distanceToClosestCollision = _day03.FindClosestCollision(collisions);
            Assert.Equal(135, distanceToClosestCollision);

            var collisionsWithDistanceDict = _day03.CalculateDistanceToCollisions(collisions);
            var shortestPathCollision = _day03.DistanceToShortestPathCollision(collisionsWithDistanceDict);
            Assert.Equal(410, shortestPathCollision);
        }

        [Fact]
        public void DrawLineTest()
        {
            _day03.Nodes = new Dictionary<Tuple<int, int>, Visited>();

            var pos = new Position(0, 0);
            pos = _day03.DrawLine(pos, Direction.Right, 8, 0);

            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(1, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(2, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(3, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(4, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(5, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(6, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(7, 0)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(8, 0)));
            Assert.Equal(new Position(8, 0), pos);

            pos = _day03.DrawLine(pos, Direction.Up, 5, 0);
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(8, 1)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(8, 2)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(8, 3)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(8, 4)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(8, 5)));
            Assert.Equal(new Position(8, 5), pos);

            pos = _day03.DrawLine(pos, Direction.Left, 5, 0);
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(7, 5)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(6, 5)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(5, 5)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(4, 5)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(3, 5)));
            Assert.Equal(new Position(3, 5), pos);

            pos = _day03.DrawLine(pos, Direction.Down, 3, 0);
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(3, 4)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(3, 3)));
            Assert.True(_day03.Nodes.ContainsKey(new Tuple<int, int>(3, 2)));
            Assert.Equal(new Position(3, 2), pos);

            Assert.Equal(21, _day03.Nodes.Count); // not counting 0,0
        }

        [Fact]
        public void CollisionCalculateTest()
        {
            _day03.Nodes = new Dictionary<Tuple<int, int>, Visited>();

            var posLine1 = new Position(0, 0);
            posLine1 = _day03.DrawLine(posLine1, Direction.Right, 5, 0);

            var posLine2 = new Position(0, 0);
            posLine2 = _day03.DrawLine(posLine2, Direction.Down, 1, 1);
            posLine2 = _day03.DrawLine(posLine2, Direction.Right, 2, 1);
            posLine2 = _day03.DrawLine(posLine2, Direction.Up, 3, 1);
            posLine2 = _day03.DrawLine(posLine2, Direction.Right, 2, 1);
            posLine2 = _day03.DrawLine(posLine2, Direction.Down, 3, 1);

            var collisions = _day03.FindCollisions();

            Assert.Equal(2, collisions.Count);
            Assert.Equal(new Tuple<int, int>(2, 0), collisions[0]);
            Assert.Equal(new Tuple<int, int>(4, 0), collisions[1]);

            Assert.Equal(2, _day03.CalculateManhattanDistance(collisions[0]));
            Assert.Equal(4, _day03.CalculateManhattanDistance(collisions[1]));
        }
    }
}
