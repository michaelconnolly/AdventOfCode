using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {

    public class Asteroid {

        public int row;
        public int col;
        public AsteroidMap map;

        public Asteroid(int row, int col, AsteroidMap map) {

            this.row = row;
            this.col = col;
            this.map = map;
        }

        public double? slope {
            get {
                if (this.colDiff == 0) return null;

                return (this.rowDiff / this.colDiff);
            }
        }

        public int rowDiff {
            get {
                return (map.station.row - this.row);
            }
        }

        public int colDiff {
            get {
                return map.station.col - this.col;
            }
        }

        public bool IsAt12() {

            return (this.colDiff == 0) && (this.rowDiff > 0);
        }

        public bool IsAt6() {
            return (this.colDiff == 0 && (this.rowDiff < 0));
        }

        public bool IsBetween12and6() {
            return (this.colDiff < 0);
        }

        public bool IsBetween6and12() {
            return (this.colDiff > 0);
        }
    }
}
