using System.Collections.ObjectModel;

namespace AdventOfCode2023 {
    internal class EngineGear {

        public int row;
        public int col;
        public Collection<int> neighbors = new Collection<int> ();

        public int CalculateGearRatio() {

            return neighbors[0] * neighbors[1];
        }

        public EngineGear(int row, int col) {
            this.row = row;
            this.col = col;
        }
    }
}
