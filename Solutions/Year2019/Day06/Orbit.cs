namespace AdventOfCode.Solutions.Year2019
{
    public class Orbit
    {
        public Orbit(string key)
        {
            Key = key;
        }

        public Orbit(string key, Orbit parent)
        {
            Key = key;
            Parent = parent;
        }

        public string Key { get; set; }
        public Orbit Parent { get; set; }
    }
}
