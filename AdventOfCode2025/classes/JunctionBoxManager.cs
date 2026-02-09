using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Advanced;

namespace AdventOfCode2025 {

    internal class JunctionBoxManager {

        List<JunctionBox> junctionBoxes = new List<JunctionBox>();
        List<JunctionBoxCircuit> junctionBoxCircuits = new List<JunctionBoxCircuit>();
        List<JunctionBoxPair> junctionBoxPairs = new List<JunctionBoxPair>();

        public JunctionBoxManager(string[] input) {

            foreach (string line in input) {

                string[] parts = line.Split(',');
                if (parts.Length != 3) { Debug.Assert(false); }

                this.junctionBoxes.Add(new JunctionBox(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }

            //this.junctionBoxPairs = this.SortPairs();

            //// Debugging:
            //   for (int i=0;i < junctionBoxPairs.Count; i++) { 
                
            //    JunctionBoxPair pair = junctionBoxPairs[i];

            //    Console.Write(i + " - ");
            //    pair.box1.Print();
            //    Console.Write(" - ");
            //    pair.box2.Print();
 
            //    Console.WriteLine();
            //}


        }


        private List<JunctionBoxPair> CreateAndSortPairs() {

            List<JunctionBoxPair> junctionBoxPairs = new List<JunctionBoxPair>();
            List<JunctionBoxPair> junctionBoxPairsSorted = new List<JunctionBoxPair>();


            for (int i = 0; i < this.junctionBoxes.Count; i++) {
                for (int j = i + 1; j < this.junctionBoxes.Count; j++) {
                    junctionBoxPairs.Add(new JunctionBoxPair(this.junctionBoxes[i], this.junctionBoxes[j], this));
                }
            }

            

            foreach (JunctionBoxPair boxPair in junctionBoxPairs) {

                bool placed = false;

                for (int i = 0; i < junctionBoxPairsSorted.Count; i++) {

                    if (boxPair.distance < junctionBoxPairsSorted[i].distance) {
                        junctionBoxPairsSorted.Insert(i, boxPair);
                        placed = true;
                        break;
                    }
                }
                if (!placed) {
                    junctionBoxPairsSorted.Add(boxPair);
                }

            }

            return junctionBoxPairsSorted;

        }

        public double DistanceBetweenTwoBoxes(JunctionBox box1, JunctionBox box2) {

            // From ... https://en.wikipedia.org/wiki/Euclidean_distance#Higher_dimensions
            return Math.Sqrt(Math.Pow((box1.x - box2.x), 2) + Math.Pow((box1.y - box2.y),2) + Math.Pow((box1.z - box2.z),2) );
        }


        private bool BoxesAlreadyPaired(JunctionBox box1, JunctionBox box2) {

           
            
            if (box1.circuit == null || box2.circuit == null) return false;

           // if (box1.circuit == box2.circuit) return true;
           if (box1.connections.Contains(box2)) return true;

            return false;
        }

        private void FindShortest(int connectionsToMake) {

            int connections = 0;
         
            while (connections < connectionsToMake) {

                double shortestSoFarLength = double.MaxValue;
                JunctionBox shortestBox1 = null;
                JunctionBox shortestBox2 = null;

                for (int i = 0; i < this.junctionBoxes.Count; i++) {
                    for (int j = i + 1; j < this.junctionBoxes.Count; j++) {

                        JunctionBox box1 = this.junctionBoxes[i];
                        JunctionBox box2 = this.junctionBoxes[j];

                        if (box1 == box2) {
                            Debug.Assert(false);
                            // do nothing.
                        }
                        else if (BoxesAlreadyPaired(box1, box2)) {
                            //connections++;
                            // do nothing.
                        }
                        else {
                            double length = DistanceBetweenTwoBoxes(box1, box2);
                            if (length < shortestSoFarLength) {
                                shortestSoFarLength = length;
                                shortestBox1 = box1;
                                shortestBox2 = box2;
                            }
                        }
                    }
                }

                if (shortestBox1 == null || shortestBox2 == null) Debug.Assert(false);

              

                // Do we already have a circuit with these two boxes?
                if ((shortestBox1.circuit != null) && (shortestBox1.circuit == shortestBox2.circuit)) {
                    // do nothing much.
                    shortestBox1.connections.Add(shortestBox2);
                    shortestBox2.connections.Add(shortestBox1);
                }
                // Are they on different circuits?  Need to merge.
                else if ((shortestBox1.circuit != null) && (shortestBox2.circuit != null) && (shortestBox1.circuit != shortestBox2.circuit)) {

                    // merge circuit2 into circuit1.
                    foreach (JunctionBox box in shortestBox2.circuit.junctionBoxes) {
                        shortestBox1.circuit.junctionBoxes.Add(box);
                    }

                    // delete circuit 2.
                    this.junctionBoxCircuits.Remove(shortestBox2.circuit);

                    // add direct connection.
                    shortestBox1.connections.Add(shortestBox2);
                    shortestBox2.connections.Add(shortestBox1);
                }


                else if (shortestBox1.circuit != null) {
                    Debug.Assert(shortestBox2.circuit == null);
                    shortestBox1.circuit.junctionBoxes.Add(shortestBox2);
                    shortestBox2.circuit = shortestBox1.circuit;
                    shortestBox1.connections.Add(shortestBox2);
                    shortestBox2.connections.Add(shortestBox1);
                }
                else if (shortestBox2.circuit != null) {
                    Debug.Assert(shortestBox1.circuit == null);
                    shortestBox2.circuit.junctionBoxes.Add(shortestBox1);
                    shortestBox1.circuit = shortestBox2.circuit;
                    shortestBox1.connections.Add(shortestBox2);
                    shortestBox2.connections.Add(shortestBox1);
                }
                else {
                    JunctionBoxCircuit circuit = new JunctionBoxCircuit();
                    circuit.junctionBoxes.Add(shortestBox1);
                    circuit.junctionBoxes.Add(shortestBox2);
                    shortestBox1.circuit = circuit;
                    shortestBox2.circuit = circuit;
                    this.junctionBoxCircuits.Add(circuit);
                    shortestBox1.connections.Add(shortestBox2);
                    shortestBox2.connections.Add(shortestBox1);
                }

                connections++;

                //// Debugging:
                //Console.Write(connections + ": ");
                //shortestBox1.Print();
                //Console.Write(" - ");
                //shortestBox2.Print();
                //Console.Write(" - ");
                //Console.Write(this.junctionBoxCircuits.Count);
                //Console.WriteLine();
            }
 
            // Last pass: for any junction boxes not hooked up into a circuit, create a new circuit with just that one box.
            foreach (JunctionBox box in this.junctionBoxes) {
                if (box.circuit == null) {
                    JunctionBoxCircuit circuit = new JunctionBoxCircuit();
                    circuit.junctionBoxes.Add(box);
                    box.circuit = circuit;
                    this.junctionBoxCircuits.Add(circuit);
                }
            }
        }      

        //Your list contains many junction boxes; connect together the 1000 pairs of junction boxes
        //which are closest together. Afterward, what do you get if you multiply together the sizes
        //of the three largest circuits ?
        public long QuestionOne(int connectionCount) {

            // different logic.
            // just create the connections.
            this.junctionBoxPairs = this.CreateAndSortPairs();

            foreach (JunctionBoxPair pair in this.junctionBoxPairs) {

                // connect the dots.
                pair.box1.connections.Add(pair.box2);
                pair.box2.connections.Add(pair.box1);

                // circuit: are they both currently disconnected?
                if (pair.box1.circuit == null && pair.box2.circuit == null) {
                    JunctionBoxCircuit circuit = new JunctionBoxCircuit();
                    circuit.junctionBoxes.Add(pair.box1);
                    circuit.junctionBoxes.Add(pair.box2);
                    pair.box1.circuit = circuit;
                    pair.box2.circuit = circuit;
                    this.junctionBoxCircuits.Add(circuit);
                }

                // circuit leftover: if box1 is disconnected, add it to box 2 ciruit.
                else if (pair.box1.circuit == null) {
                    pair.box1.circuit = pair.box2.circuit;
                    pair.box2.circuit.junctionBoxes.Add(pair.box1);
                }

                // circuit leftover: if box2 is disconnected, add it to box 1 ciruit.
                else if (pair.box2.circuit == null) {
                    pair.box2.circuit = pair.box1.circuit;
                    pair.box1.circuit.junctionBoxes.Add(pair.box2);
                }

                // circuit leftover: ok, they both have circuits.  are they the same?
                else if (pair.box1.circuit == pair.box2.circuit) {
                    // do nothing.
                }

                // circuit leftover: they are on different circuits.
                else {

                    // merge circuit2 into circuit1.
                    foreach (JunctionBox box in pair.box2.circuit.junctionBoxes) {
                        pair.box1.circuit.junctionBoxes.Add(box);
                        box.circuit = pair.box1.circuit;
                    }

                    // delete circuit 2.
                    this.junctionBoxCircuits.Remove(pair.box2.circuit);
                }
            }


     
                //  this.FindShortest(connectionCount);

                int firstPlace = int.MinValue;
            int secondPlace = int.MinValue;
            int thirdPlace = int.MinValue;

            foreach (JunctionBoxCircuit circuit in this.junctionBoxCircuits) {

                int size = circuit.junctionBoxes.Count;

                if (size > firstPlace) {
                    thirdPlace = secondPlace;
                    secondPlace = firstPlace;
                    firstPlace = size;
                }
                else if (size > secondPlace) {
                    thirdPlace = secondPlace;
                    secondPlace = size;
                }
                else if (size > thirdPlace) {
                    thirdPlace = size;
                }
            }

            return (firstPlace * secondPlace * thirdPlace);
        }
    }

    

}
