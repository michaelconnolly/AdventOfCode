using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {
   
    internal enum Direction {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
    
    internal class RollingRockMap {

        int height;
        int width;
        char[,] map;

        public RollingRockMap(string[] lines) {

            this.height = lines.Length;
            this.width = lines[0].Length;
            this.map = new char[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    this.map[x, y] = lines[y][x];
                }
            }
        }

        public void print() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(this.map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Load: " + this.CalculateLoad());
            Console.WriteLine();
        }

        //public void TiltNorth() {

        //    // Let's go column by column.
        //    for (int x=0; x<width; x++) {

        //        int nextFullStop = -1;
 
        //        // From top to bottom, let's have things that roll, roll.
        //        for (int y = 0; y<height; y++) {

        //            switch (this.map[x, y]) {

        //                case '.':
        //                    break;

        //                case 'O':

        //                    // is there a space between me and the last full stop?
        //                    if (y > (nextFullStop + 1)) {

        //                        // then teleport it.
        //                        this.map[x, (nextFullStop + 1)] = 'O';
        //                        this.map[x, y] = '.';
        //                        nextFullStop = nextFullStop + 1;
        //                    }
        //                    else {
        //                        nextFullStop = y;
        //                    }
        //                    break;

        //                case '#':

        //                    nextFullStop = y;
        //                    break;

        //                default:

        //                    throw new Exception();
        //            }
        //        }
        //    }

        //    return;
        //}

        public void TiltNorthOrSouth(Direction direction=Direction.North) {

            // Let's go column by column.
            for (int x = 0; x < width; x++) {

                int nextFullStop, nextRestingStop, nextRestingStopIncrement;

                switch (direction) {
                    case Direction.North:

                        nextFullStop = -1;
                        nextRestingStopIncrement = 1;
                        //nextRestingStop = nextFullStop + nextRestingStopIncrement;
                        break;

                    case Direction.South:
                        nextFullStop = this.height; // -1;
                        nextRestingStopIncrement = -1;
                        //nextRestingStop = nextFullStop - 1;
                        break;

                    default:
                        throw new Exception();
                }

                nextRestingStop = nextFullStop + nextRestingStopIncrement;


                // From top to bottom, let's have things that roll, roll.
                for (int y = 0; y < height; y++) {

                    int currentRow = y;
                    bool test = currentRow > nextRestingStop;
                    if (direction == Direction.South) {
                        currentRow = height - 1 - y;
                        test = currentRow < nextRestingStop;
                    }

                    switch (this.map[x, currentRow]) {

                        case '.':
                            break;

                        case 'O':

                            // is there a space between me and the last full stop?
                           //if (y > (nextFullStop + 1)) {
                               // if (currentRow > (nextRestingStop)) {
                               if (test) { 

                                    // then teleport it.
                                 //   this.map[x, (nextFullStop + 1)] = 'O';
                                this.map[x, nextRestingStop] = 'O';
                                this.map[x, currentRow] = '.';
                                //nextFullStop = nextFullStop + 1;
                                
                                nextFullStop = nextRestingStop;
                                nextRestingStop = nextFullStop + nextRestingStopIncrement;
                            }
                            else {
                                nextFullStop = currentRow;
                                nextRestingStop = nextFullStop + nextRestingStopIncrement;
                            }
                            break;

                        case '#':

                            nextFullStop = currentRow;
                            nextRestingStop = nextFullStop + nextRestingStopIncrement;
                            break;

                        default:

                            throw new Exception();
                    }
                }
            }

            return;
        }


        public void TiltWestOrEast(Direction direction = Direction.West) {

            // Let's go row by row.
            for (int y = 0; y < height; y++) {

                int nextFullStop, nextRestingStop, nextRestingStopIncrement;

                switch (direction) {
                    case Direction.West:

                        nextFullStop = -1;
                        nextRestingStopIncrement = 1;
                        break;

                    case Direction.East:
                        nextFullStop = this.height;
                        nextRestingStopIncrement = -1;
                        break;

                    default:
                        throw new Exception();
                }

                nextRestingStop = nextFullStop + nextRestingStopIncrement;


                // From left to right, let's have things that roll, roll.
                for (int x = 0; x < width; x++) {

                    int currentCol = x;
                    bool test = currentCol > nextRestingStop;
                    if (direction == Direction.East) {
                        currentCol = width - 1 - x;
                        test = currentCol < nextRestingStop;
                    }

                    switch (this.map[currentCol, y]) {

                        case '.':
                            break;

                        case 'O':

                            // is there a space between me and the last full stop?
                            //if (y > (nextFullStop + 1)) {
                            //if (currentRow > (nextRestingStop)) {
                            if (test) {

                                // then teleport it.
                                //   this.map[x, (nextFullStop + 1)] = 'O';
                                //this.map[x, nextRestingStop] = 'O';
                                //this.map[x, currentRow] = '.';
                                ////nextFullStop = nextFullStop + 1;
                                this.map[nextRestingStop, y] = 'O';
                                this.map[currentCol, y] = '.';


                                nextFullStop = nextRestingStop;
                                nextRestingStop = nextFullStop + nextRestingStopIncrement;
                            }
                            else {
                                nextFullStop = currentCol;
                                nextRestingStop = nextFullStop + nextRestingStopIncrement;
                            }
                            break;

                        case '#':

                            nextFullStop = currentCol;
                            nextRestingStop = nextFullStop + nextRestingStopIncrement;
                            break;

                        default:

                            throw new Exception();
                    }
                }
            }

            return;
        }


        public long CalculateLoad() {
            
            long sum = 0;

            for (int y=0; y<height; y++) {

                int currentLoadOfRock = height - y;

                for (int x=0; x<width; x++) {
                    if (this.map[x,y] == 'O') {
                        sum += currentLoadOfRock;
                    }
                }
            }

            return sum;
        }
    }
}
