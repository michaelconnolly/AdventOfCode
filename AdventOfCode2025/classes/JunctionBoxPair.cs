using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {
    internal class JunctionBoxPair {

        public JunctionBox box1;
        public JunctionBox box2;
        private JunctionBoxManager boxManager;

        public JunctionBoxPair(JunctionBox box1, JunctionBox box2, JunctionBoxManager boxManager) {
            this.box1 = box1;
            this.box2 = box2;
            this.boxManager = boxManager;
        }

        public double distance {

            get {

                return this.boxManager.DistanceBetweenTwoBoxes(this.box1, this.box2);
            }
        }
    }
}
