using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {
    internal class TachyonWorld {

        char[,] map;
        TachyonBeam beam;

        public TachyonWorld(string[] input, TachyonBeam beam=null, char[,] map=null) {

            // initialize map.
            if (map != null) {
                this.map = map;
            }
            else {


                this.map = new char[input.Length, input[0].Length];
                for (int row = 0; row < input.Length; row++) {
                    for (int col = 0; col < input[row].Length; col++) {

                        if (input[row][col] == 'S') {
                            if (this.beam != null) Debug.Assert(false);
                            this.beam = (new TachyonBeam(this.map, row, col));
                            //this.map[row, col] = '|';
                        }
                        else {
                            this.map[row, col] = input[row][col];
                        }
                    }
                } 
            }


            if (beam == null && this.beam == null) {
                Debug.Assert(false);
            }

            if (beam != null) {
                this.beam = beam;
                //this.map[beam.row, beam.col] = '|';
            }

            //this.Print();
        }


        public int Tick(bool newTimeline) {

            bool fSomethingHappened = true;
            //int splitCount = 0;
            int timeLines = 0;
            if (newTimeline)  timeLines = 1;
           

                while (fSomethingHappened) {

                    fSomethingHappened = false;
                    //List<TachyonBeam> beamsCopy = new List<TachyonBeam>(this.beams);

                    // foreach (TachyonBeam beam in beamsCopy) {

                    //char currentChar = this.map[beam.row, beam.col];
                    //Debug.Assert(this.map[beam.row, beam.col] == '|');

                    if (beam.finished) {

                        // do nothing.
                    }
                    else if (beam.row == (this.map.GetLength(0) - 1)) {
                        beam.finished = true;
                    }
                    //else if (this.map[beam.row + 1, beam.col] == '|') {
                    //    // undefined.
                    //    Debug.Assert(false);
                    //    //beam.finished = true;
                    //}
                    else if (this.map[beam.row + 1, beam.col] == '.') {

                        fSomethingHappened = true;
                        beam.row = beam.row + 1;
                        //this.map[beam.row, beam.col] = '|';
                    }
                    else if (this.map[beam.row + 1, beam.col] == '^') {

                        //// for observability it would be better if we can print a clearer map.
                        //char[,] newMap = new char[this.map.GetLength(0), this.map.GetLength(1)];
                        //for (int row = 0; row < map.GetLength(0); row++) {
                        //    for (int col = 0; col < map.GetLength(1); col++) {
                        //        newMap[row, col] = this.map[row, col];
                        //    }
                        //}

                        fSomethingHappened = true;
                    int nextRow = beam.row + 1;
                    //splitCount++;
                    //beam.finished = true;

                    // split to the left.
                    if (((beam.col - 1) >= 0) && (nextRow <= this.map.GetLength(0))
                        && (this.map[nextRow, beam.col - 1] == '.')) {

                        //splitCount = new TachyonWorld(null, new TachyonBeam()

                        //TachyonBeam beam1 = new TachyonBeam(newMap, beam.row + 1, beam.col - 1);
                        beam.row = nextRow;
                        beam.col -= 1;
                        TachyonWorld newWorld = new TachyonWorld(null, beam, this.map);
                        timeLines += newWorld.Tick(false);

                        //this.beams.Add(beam1);
                        //this.map[beam1.row, beam1.col] = '|';

                    }

                        // split to the right.
                        if (((beam.col + 1) <= this.map.GetLength(1)) && (nextRow <= this.map.GetLength(0))
                            && (this.map[nextRow, beam.col + 1] == '.')) {

                            // TachyonBeam beam2 = new TachyonBeam(this.map, beam.row + 1, beam.col + 1);
                            //this.beams.Add(beam2);
                            //this.map[beam2.row, beam2.col] = '|';

                            TachyonBeam beam2 = new TachyonBeam(this.map, nextRow, beam.col + 1);
                            TachyonWorld newWorld = new TachyonWorld(null, beam2, this.map);
                            timeLines += newWorld.Tick(true);
                        }
                    }

                    //this.Print();
                    // }
                }

            return timeLines;
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
