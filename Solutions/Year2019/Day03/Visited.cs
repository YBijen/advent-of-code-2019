namespace AdventOfCode.Solutions.Year2019
{
    public class Visited
    {
        public Visited(bool lineOne = false, bool lineTwo = false)
        {
            LineOne = lineOne;
            LineTwo = lineTwo;
        }

        public bool LineOne { get; set; }
        public bool LineTwo { get; set; }
    }
}
