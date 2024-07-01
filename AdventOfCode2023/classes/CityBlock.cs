using System.Collections.Generic;

namespace AdventOfCode2023.classes {
    internal class CityBlock {

        private static int nextId = 0;
        public int id, x, y, heatLoss;
        //public Dictionary<CityBlockFacing, int> bestValues = new Dictionary<CityBlockFacing, int>();
        //public Dictionary<CityBlockFacing, int> bestValueStraightCount = new Dictionary<CityBlockFacing, int>();
        public Dictionary<string, int> bestValues = new Dictionary<string, int>();
        
        public CityBlock(int x, int y, int heatLoss) {

            this.id = nextId++;
            this.x = x;
            this.y = y;
            this.heatLoss = heatLoss;
        }
    }
}
