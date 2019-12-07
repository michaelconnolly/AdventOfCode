using System.Collections.Generic;

namespace AdventOfCode2019 {
    class Day6_Object {

        public string name;
        public Day6_Object orbitalParent;
        public List<Day6_Object> orbitalChildren = new List<Day6_Object>();

        public Day6_Object(string name, Day6_Object orbitalParent) {

            this.name = name;
            this.AddParent(orbitalParent);
        }

        public void AddParent(Day6_Object orbitalParent) {

            this.orbitalParent = orbitalParent;

            if (this.orbitalParent != null) {
                this.orbitalParent.orbitalChildren.Add(this);
            }
        }

        public int directOrbitalChildrenCount {
            get {
                return this.orbitalChildren.Count;
            }
        }

        public int indirectOrbitalChildrenCount {
            get {
                int total = this.directOrbitalChildrenCount;
                foreach (Day6_Object orbitalChild in this.orbitalChildren) {
                    total += orbitalChild.indirectOrbitalChildrenCount;
                }
                return total;
            }
        }

        public string description {
            get {
                string parentName = "NULL";
                if (this.orbitalParent != null) {
                    parentName = this.orbitalParent.name;
                }

                return "\t" + this.name + " orbits " + parentName + ", has " + this.directOrbitalChildrenCount.ToString() + " children and " + this.indirectOrbitalChildrenCount.ToString() + " indirect children.";
            }
        }

        public int DoYouHavePathTo(Day6_Object target, Day6_Object upPath) {

            // If the target already orbits me, there are 0 hops required.
            if (target.orbitalParent == this) {
                return 0;
            }

            // If they have the same parent, the are 0 hops from each other.
            if (this.orbitalParent != null && target.orbitalParent != null && this.orbitalParent == target.orbitalParent) {
                return 0;
            }

            // If the target is orbitting my parent's parent, there are 1 hops from each other.
            if ((this.orbitalParent != null) && (this.orbitalParent.orbitalParent == target.orbitalParent)) {
                return 1;
            }

            // If the target is orbitting one of my child objects, there are 1 hops from each other.
            foreach (Day6_Object orbitalChild in this.orbitalChildren) {
                if (target.orbitalParent == orbitalChild) {
                    return 1;
                }
            }

            // If the target is orbitting one of my parent's child objects, there are 1 hops from each other.
            if (this.orbitalParent != null) {
                foreach (Day6_Object orbitalChild in this.orbitalParent.orbitalChildren) {
                    if (target.orbitalParent == orbitalChild) {
                        return 2;
                    }
                }
            }

            // Calculate path length through parent.
            int pathThroughParent = int.MinValue;

            // Figure out path to parent.
            if (this.orbitalParent != null && this.orbitalParent != null && this.orbitalParent != upPath) {
                pathThroughParent = this.orbitalParent.DoYouHavePathTo(target, this);
                if (pathThroughParent != int.MinValue) {
                    pathThroughParent++;
                }
            }

            // Calculate path length through children.
            int bestPathThroughChildren = int.MaxValue;
            foreach (Day6_Object child in this.orbitalChildren) {

                if (child != upPath) {

                    int pathThroughChild = child.DoYouHavePathTo(target, this);
                    if (pathThroughChild != int.MinValue) {
                        pathThroughChild++;
                        bestPathThroughChildren = (pathThroughChild < bestPathThroughChildren) ? pathThroughChild : bestPathThroughChildren;
                    }
                }
            }

            if (pathThroughParent == int.MinValue && bestPathThroughChildren == int.MaxValue) {
                return int.MinValue;
            }
            else if (pathThroughParent == int.MinValue) {
                return bestPathThroughChildren;
            }
            else if (bestPathThroughChildren == int.MaxValue) {
                return pathThroughParent;
            }
            else {

                int shortestPath = (pathThroughParent < bestPathThroughChildren ? pathThroughParent : bestPathThroughChildren);
                return shortestPath;
            }
        }
    }
}

