using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {


    internal class DesertMapNode {

        public string nodeName;
        public string nodeLeft;
        public string nodeRight;

        public DesertMapNode(string name, string left, string right) {
            this.nodeName = name;
            this.nodeLeft = left;
            this.nodeRight = right;
        }

        public void print() {
           
            Console.WriteLine(this.nodeName + ": " + this.nodeLeft + ", " + this.nodeRight);
        }
    }
}
