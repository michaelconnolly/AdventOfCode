using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021 {
    
    public class OceanFloorLine {

        public int x1;
        public int x2;
        public int y1;
        public int y2;


        public bool isHorizontalOrVertical() {

            return (isHorizontal() || isVertical());
        }


        public bool isHorizontal() {

            return (x1 == x2);
        }


        public bool isVertical() {

            return (y1 == y2);
        }



        public OceanFloorLine(string x1, string y1, string x2, string y2) {

            this.x1 = Convert.ToInt32(x1);
            this.y1 = Convert.ToInt32(y1);
            this.x2 = Convert.ToInt32(x2);
            this.y2 = Convert.ToInt32(y2);
        }


        public void print() {
            Console.WriteLine("line: (" + x1 + "," + y1 + ") -> (" + x2 + "," + y2 + ")");
        }
    }
}
