using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019 {

    class Day06 : ASolution
    {
        private const string OBJECT_YOU = "YOU";
        private const string OBJECT_SANTA = "SAN";

        public Day06() : base(6, 2019, "") { }

        protected override string SolvePartOne()
        {
            var objectsInOrbit = ConvertInputToObjectsInOrbit();
            return CountOrbits(objectsInOrbit).ToString();
        }

        protected override string SolvePartTwo()
        {
            var objectsInOrbit = ConvertInputToObjectsInOrbit();
            return CountRequiredTransfersBetweenMeAndSanta(objectsInOrbit).ToString();
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

        private int CountRequiredTransfersBetweenMeAndSanta(List<Orbit> objectsInOrbit)
        {
            var myOrbit = objectsInOrbit.Find(o => o.Key == OBJECT_YOU);
            var myOrbitPath = CreatePathFromOrbit(myOrbit);

            var santaOrbit = objectsInOrbit.Find(o => o.Key == OBJECT_SANTA);
            var santaOrbitPath = CreatePathFromOrbit(santaOrbit);

            Orbit orbitItemToFind = null;
            var requiredTransfers = 0;

            // Continue transfering in my orbits until we are at a path which is shared with Santa
            foreach(var orbit in myOrbitPath)
            {
                orbitItemToFind = santaOrbitPath.FirstOrDefault(so => so.Key == orbit.Key);
                if (orbitItemToFind != null)
                {
                    break;
                }
                requiredTransfers++;
            }

            // Find the amount of orbits between Santa and the Orbit item which is found in both orbit paths
            requiredTransfers += santaOrbitPath.ToList().IndexOf(orbitItemToFind);

            return requiredTransfers;
        }

        private IEnumerable<Orbit> CreatePathFromOrbit(Orbit orbit)
        {
            var parentOrbit = orbit.Parent;
            while(parentOrbit != null)
            {
                yield return parentOrbit;
                parentOrbit = parentOrbit.Parent;
            }
        }
    }
}
