using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day03 : ASolution
    {
        public Dictionary<Tuple<int, int>, Visited> Nodes;
        public List<List<Tuple<int, int>>> PathOfLines;

        public Day03() : base(3, 2019, "") { }

        protected override string SolvePartOne()
        {
            var collisions = FindAllCollisions();
            return FindClosestCollision(collisions).ToString();
        }

        protected override string SolvePartTwo()
        {
            var collisions = FindAllCollisions();
            var collisionsWithDistanceDict = CalculateDistanceToCollisions(collisions);
            return DistanceToShortestPathCollision(collisionsWithDistanceDict).ToString();
        }

        private List<Tuple<int, int>> FindAllCollisions()
        {
            var lines = Input.SplitByNewline().ToList();
            FillNodesWithLineInstructions(lines);
            return FindCollisions();
        }

        public int DistanceToShortestPathCollision(Dictionary<Tuple<int, int>, int> collisionsWithDistance) =>
            collisionsWithDistance.Select(cwd => cwd.Value).Min();

        public Dictionary<Tuple<int, int>, int> CalculateDistanceToCollisions(List<Tuple<int, int>> collisions)
        {
            var result = new Dictionary<Tuple<int, int>, int>();
            foreach (var collision in collisions)
            {
                result.Add(collision, PathOfLines[0].IndexOf(collision) + PathOfLines[1].IndexOf(collision));
            }
            return result;
        }

        public int FindClosestCollision(List<Tuple<int, int>> collisionPositions) =>
            collisionPositions.Min(CalculateManhattanDistance);

        public void FillNodesWithLineInstructions(List<string> lines)
        {
            Nodes = new Dictionary<Tuple<int, int>, Visited>();
            PathOfLines = new List<List<Tuple<int, int>>>();
            for (var lineIndex = 0; lineIndex < lines.Count; lineIndex++)
            {
                PathOfLines.Add(new List<Tuple<int, int>> { new Tuple<int, int>(0, 0) }); // Add starting position
                var pos = new Position(0, 0);

                foreach (var lineInstruction in lines[lineIndex].Split(","))
                {
                    var direction = GetDirectionForChar(lineInstruction[0]);
                    var length = int.Parse(lineInstruction.Substring(1));

                    pos = DrawLine(pos, direction, length, lineIndex);
                }
            }
        }

        public Direction GetDirectionForChar(char c)
        {
            return c switch
            {
                'R' => Direction.Right,
                'D' => Direction.Down,
                'U' => Direction.Up,
                'L' => Direction.Left,
                _ => throw new Exception("Input failure"),
            };
        }

        public List<Tuple<int, int>> FindCollisions()
        {
            return Nodes.Where(n => n.Value.LineOne && n.Value.LineTwo).Select(kvp => kvp.Key).ToList();
        }

        public int CalculateManhattanDistance(Tuple<int, int> pos) => Math.Abs(pos.Item1) + Math.Abs(pos.Item2);

        public Position DrawLine(Position currentPosition, Direction direction, int length, int lineIndex)
        {
            IsFirst = true;
            var range = Enumerable.Range(0, length + 1).ToList();
            var newPosition = new Position(currentPosition.X, currentPosition.Y);
            switch (direction)
            {
                case Direction.Up:
                    range.ForEach(value => TryAddToLines(lineIndex, new Position(currentPosition.X, currentPosition.Y + value)));
                    newPosition.Y += length;
                    break;
                case Direction.Down:
                    range.ForEach(value => TryAddToLines(lineIndex, new Position(currentPosition.X, currentPosition.Y - value)));
                    newPosition.Y -= length;
                    break;
                case Direction.Left:
                    range.ForEach(value => TryAddToLines(lineIndex, new Position(currentPosition.X - value, currentPosition.Y)));
                    newPosition.X -= length;
                    break;
                case Direction.Right:
                    range.ForEach(value => TryAddToLines(lineIndex, new Position(currentPosition.X + value, currentPosition.Y)));
                    newPosition.X += length;
                    break;
            }
            return newPosition;
        }

        private bool IsFirst;

        private void TryAddToLines(int lineIndex, Position pos)
        {
            var posTuple = new Tuple<int, int>(pos.X, pos.Y);

            // Keep track of each line's path for part 2 of the puzzle
            if (!IsFirst)
            {
                PathOfLines[lineIndex].Add(posTuple);
            }
            IsFirst = false;

            // Don't add the 0,0 position to the Nodes
            if (pos.X == 0 && pos.Y == 0)
            {
                return;
            }

            if (lineIndex == 0 && !Nodes.ContainsKey(posTuple))
            {
                Nodes.Add(posTuple, new Visited(lineOne: true));
            }
            else if (lineIndex == 1 && Nodes.ContainsKey(posTuple))
            {
                Nodes[posTuple].LineTwo = true;
            }
        }

    }
}
