using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2022 {

    public class HillCoordinate {

        public int row;
        public int col;

        public HillCoordinate(int row, int col) {
            this.row = row;
            this.col = col;
        }

        public string Print() {
            return this.row.ToString() + "," + this.col.ToString();
        }
    }
}
