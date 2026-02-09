using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    public class TachyonBeam {

        char[,] map;
        public int row;
        public int col;
        public bool finished = false;

        public TachyonBeam(char[,] map, int row, int col) {

            this.map = map;
            this.row = row;
            this.col = col;
        }
    }
}
