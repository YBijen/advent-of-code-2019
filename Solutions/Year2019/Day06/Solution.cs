using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019 {

    class Day06 : ASolution
    {
        public Day06() : base(6, 2019, "") { }

        protected override string SolvePartOne()
        {
            var objectsInOrbit = ConvertInputToObjectsInOrbit();
            return CountOrbits(objectsInOrbit).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        public List<Orbit> ConvertInputToObjectsInOrbit()
        {
            var result = new List<Orbit>();
            foreach(var data in Input.SplitByNewline(true))
            {
                var parentOrbitKey = data.Split(")")[0];
                var parentOrbit = result.Find(r => r.Key == parentOrbitKey);
                if(parentOrbit == null)
                {
                    parentOrbit = new Orbit(parentOrbitKey);
                    result.Add(parentOrbit);
                }

                var childOrbitKey = data.Split(")")[1];
                var childOrbit = result.Find(r => r.Key == childOrbitKey);
                if (childOrbit == null)
                {
                    childOrbit = new Orbit(childOrbitKey, parentOrbit);
                    result.Add(childOrbit);
                }
                else
                {
                    if(childOrbit.Parent != null)
                    {
                        throw new Exception("It should not happen that an orbit already has a parent");
                    }
                    childOrbit.Parent = parentOrbit;
                }
            }
            return result;
        }

        public int CountOrbits(List<Orbit> objectsInOrbit)
        {
            var amountOfOrbits = 0;
            foreach (var currentObject in objectsInOrbit)
            {
                var parentObject = currentObject.Parent;
                while (parentObject != null)
                {
                    amountOfOrbits += 1;
                    parentObject = parentObject.Parent;
                }
            }
            return amountOfOrbits;
        }
    }
}
