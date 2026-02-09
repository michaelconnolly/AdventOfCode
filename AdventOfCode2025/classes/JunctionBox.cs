using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {
    internal class JunctionBox {

        public long x;
        public long y;
        public long z;
        public JunctionBoxCircuit circuit = null;
        public List<JunctionBox> connections = new List<JunctionBox>();


        public JunctionBox(long x, long y, long z) {

            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Print() {

            Console.Write(this.x + "," + this.y + "," + this.z);
            return;
        }
    }
}
