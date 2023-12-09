using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023 {

    internal class DesertMap {

        public string steps;
        public Dictionary<string, DesertMapNode> nodes = new Dictionary<string, DesertMapNode>();

        public DesertMap(string[] lines) {

            this.steps = lines[0];

            for (int i = 2; i < lines.Length; i++) {

                string[] parts = lines[i].Split(" = ");
                string nodeName = parts[0];
                string nodeOther = parts[1];

                nodeOther = nodeOther.Substring(1, nodeOther.Length - 2);
                parts = nodeOther.Split(", ");
                string nodeLeft = parts[0];
                string nodeRight = parts[1];
                this.nodes[nodeName] = new DesertMapNode(nodeName, nodeLeft, nodeRight);
            }
        
        }


        public long StepCount() {

            int count = 0;
            bool keepGoing = true;
            int index = 0;
            DesertMapNode currentNode;

            try {

                currentNode = this.nodes["AAA"];
            }
            catch {
                Console.WriteLine("error, this feed has no AAA.");
                return -1;
            }

            while (keepGoing) {

                char currentDirection = this.steps[index];
                count++;

                if (currentDirection == 'L') {
                    currentNode = this.nodes[currentNode.nodeLeft];
                }
                else if (currentDirection == 'R') {
                    currentNode = this.nodes[currentNode.nodeRight];
                }
                else {
                    Console.WriteLine("ERROR! Bad direction code.");
                    throw new Exception();
                }

                if (currentNode.nodeName == "ZZZ") {
                    keepGoing = false;
                }

                index++;
                if (index >= this.steps.Length) {
                    index = 0;
                }
               
            }


            return count;
        }


        public long StepCountPartTwo() {

            long count = 0;
            bool keepGoing = true;
            int index = 0;
            Collection<DesertMapNode> currentNodes = new Collection<DesertMapNode>(); // = this.nodes["AAA"];

            // Populate currentNodes;
            foreach (string nodeName in this.nodes.Keys) {
                if (nodeName[2] == 'A') {
                    currentNodes.Add(this.nodes[nodeName]);
                }
            }

            while (keepGoing) {

                // Figure out the next direction.  And, if we got here, this counts as a step.
                char currentDirection = this.steps[index];
                count++;

                // Assume we don't have to continue after the first hop.
                bool continueHopping = false;


                // Loop through all seed nodes.
                int initialNodeCount = currentNodes.Count;
                //for (int i = 0; i < 4; i++) { //  currentNodes.Count; i++) {
                 for (int i = 0; i < initialNodeCount; i++) { //  currentNodes.Count; i++) {

                        DesertMapNode currentNode = currentNodes[i];

                 

                    // Reset the node to be in the correct next direction.
                    if (currentDirection == 'L') {
                        currentNodes[i] = this.nodes[currentNode.nodeLeft];
                    }
                    else if (currentDirection == 'R') {
                        currentNodes[i] = this.nodes[currentNode.nodeRight];
                    }
                    else {
                        Console.WriteLine("ERROR! Bad direction code.");
                        throw new Exception();
                    }

                    currentNode = currentNodes[i];


                    // If the last character doesn't have a Z, we have to continue, flip the switch.
                    if (currentNode.nodeName[2] != 'Z') {
                        continueHopping = true;
                    } 
                }

                // Did any node trigger us to have to keep hopping?
                if (!continueHopping) 
                    keepGoing=false; 

                   // if (node.nodeName[2] != 'Z') {
                  //      match = false;
                  //      break;
                    //}
                //}
                //if (match) {
                 //   keepGoing = false;
                //}

                index++;
                if (index >= this.steps.Length) {
                    index = 0;
                }
            }

            return count;
        }


        public void print() {

            Console.WriteLine("steps: " + this.steps);
            foreach (string key in this.nodes.Keys) {
                this.nodes[key].print();
            }
        }
    }
}
