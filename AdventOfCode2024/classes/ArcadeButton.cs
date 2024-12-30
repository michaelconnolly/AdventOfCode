using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    internal class ArcadeButton {

        public long x;
        public long y;

        public ArcadeButton(string x, string y) {
            this.x = long.Parse(x);
            this.y = long.Parse(y);
        }

        public ArcadeButton(long x, long y) {
            this.x = x;
            this.y = y;
        }
    }
}
