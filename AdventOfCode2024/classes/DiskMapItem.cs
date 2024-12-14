using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    
    internal class DiskMapItem {

        public int id;
        public int startPos;
        public int length;
        public List<int> finalPositions = new List<int>();

        public DiskMapItem(int id, int startPos, int length) {
            this.id = id;
            this.startPos = startPos;
            this.length = length;
        }
    }
}
