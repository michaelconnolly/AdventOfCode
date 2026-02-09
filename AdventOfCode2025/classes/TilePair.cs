using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {


    internal class TilePair {

        public Tile tile1;
        public Tile tile2;

        public TilePair(Tile tile1, Tile tile2) {
            this.tile1 = tile1;
            this.tile2 = tile2;
        }

        public long Area() {

            long length = Math.Abs(this.tile1.x - this.tile2.x) + 1;
            long width = Math.Abs(this.tile1.y - this.tile2.y) + 1;
            return length * width;
        }

       // public bool WithinContiguousArea(char[.] map) {
         public bool WithinContiguousArea(List<char[]> map) {

            Tile tile3 = new Tile(this.tile1.x, this.tile2.y);
            Tile tile4 = new Tile(this.tile2.x, this.tile1.y);

            int leftMost = Math.Min(this.tile1.x, this.tile2.x);
            int rightMost = Math.Max(this.tile1.x, this.tile2.x);
            int topMost = Math.Min(this.tile1.y, this.tile2.y);
            int bottomMost = Math.Max(this.tile1.y, this.tile2.y);

            if ((map[leftMost][ topMost] == '.') ||
                (map[rightMost][ topMost] == '.') ||
                (map[leftMost][ bottomMost] == '.') ||
                (map[rightMost][ bottomMost] == '.')) {

                return false;
            }

            return true;
        }
    }
}
