namespace AdventOfCode.Solutions.Year2019
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Position pos)
            {
                return pos.X == X && pos.Y == Y;
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
