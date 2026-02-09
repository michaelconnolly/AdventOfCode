using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {
    internal class Tile {

        public int x;
        public int y;

        public Tile(int x, int y) {

            this.x = x;
            this.y = y;
        }

        public void Print() {
            Console.WriteLine(this.x + "," + this.y);
        }
    }
}
