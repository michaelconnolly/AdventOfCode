using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    internal class TachyonManifold {

        public char[,] map;
        public List<TachyonBeam> beams = new List<TachyonBeam>();


        public TachyonManifold(string[] input) {

            // initialize map.
            this.map = new char[input.Length, input[0].Length];
            for (int row = 0; row < input.Length; row++) {
                for (int col = 0; col < input[row].Length; col++) {
                 
                    if (input[row][col] == 'S') {
                        this.beams.Add(new TachyonBeam(this.map, row, col));
                        this.map[row, col] = '|';
                    }
                    else {
                        this.map[row, col] = input[row][col];
                    }
                }
            }

            this.Print();

        }

        public int Tick() {

            bool fSomethingHappened = true;
            int splitCount = 0;

            while (fSomethingHappened) {

                fSomethingHappened = false;
                List<TachyonBeam> beamsCopy = new List<TachyonBeam>(this.beams);

                foreach (TachyonBeam beam in beamsCopy) {

                    //char currentChar = this.map[beam.row, beam.col];
                    Debug.Assert(this.map[beam.row, beam.col] == '|');

                    if (beam.finished) {
                        // do nothing.
                    }
                    else if (beam.row == (this.map.GetLength(0) - 1)) {
                        beam.finished = true;
                    }
                    else if (this.map[beam.row + 1, beam.col] == '|') {
                        // undefined.
                        //Debug.Assert(false);
                        beam.finished = true;
                    }
                    else if (this.map[beam.row + 1, beam.col] == '.') {

                        fSomethingHappened = true;
                        beam.row = beam.row + 1;
                        this.map[beam.row, beam.col] = '|';
                    }
                    else if (this.map[beam.row + 1, beam.col] == '^') {

                        fSomethingHappened = true;
                        splitCount++;
                        beam.finished = true;

                        // split to the left.
                        if (((beam.col - 1) >= 0) && (beam.row + 1 <= this.map.GetLength(0)) 
                            && (this.map[beam.row + 1, beam.col - 1] == '.')) {

                            TachyonBeam beam1 = new TachyonBeam(this.map, beam.row + 1, beam.col - 1);
                            this.beams.Add(beam1);
                            this.map[beam1.row, beam1.col] = '|';
                        }

                        // split to the right.
                        if (((beam.col + 1) <= this.map.GetLength(1)) && (beam.row + 1 <= this.map.GetLength(0)) 
                            && (this.map[beam.row + 1, beam.col + 1] == '.')) {

                            TachyonBeam beam2 = new TachyonBeam(this.map, beam.row + 1, beam.col + 1);
                            this.beams.Add(beam2);
                            this.map[beam2.row, beam2.col] = '|';
                        }
                    }

                    //this.Print();
                }
            }
            
            return splitCount;
        }


    

        public void Print() {

            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {

                    Console.Write(this.map[row, col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
