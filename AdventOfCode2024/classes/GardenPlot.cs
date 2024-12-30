using AdventOfCode2024.classes;


namespace AdventOfCode2024 {

 
    internal class GardenPlot {

        public char vegetable;
        public bool edgeUp = true;
        public bool edgeDown = true;
        public bool edgeLeft = true;
        public bool edgeRight = true;
        public GardenRegion region = null;
        public Coordinate coordinate;

        public GardenPlot(Coordinate coordinate, char vegetable) {
            this.vegetable = vegetable;
            this.coordinate = coordinate;
        }

        public int CountOfFenceEdges() {

            int count = 0;
            if (edgeUp) count++;
            if (edgeDown) count++;
            if (edgeLeft) count++;
            if (edgeRight) count++;
            return count;
        }
    }
}
