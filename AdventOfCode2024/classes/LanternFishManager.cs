using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.classes {

    internal class LanternFishManager {

        char[,] map;
        char[,] mapLarge;
        string moveList = "";
        Coordinate robot;
        Coordinate robotLarge;

        public LanternFishManager(string[] input) {

            // ########
            // #..O.O.#
            // ##@.O..#
            // #...O..#
            // #.#.O..#
            // #...O..#
            // #......#
            // ########
            //
            // <^^>>>vv<v>>v<<

            // Figure out where the gap line is. 
            int gapLine = -1;
            for (int i=0; i<input.Length; i++) {
                if (input[i] == "") {
                    gapLine = i;
                    break;
                }
            }

            // The map is everything above the gap line.  
            string[] mapRaw = new string[gapLine];
            for (int i = 0; i < gapLine; i++) {
                mapRaw[i] = input[i];
            }
            this.ParseMap(mapRaw);

            // the move list is everything below the gap line. 
            for (int i = gapLine + 1; i < input.Length; i++) {
                moveList += input[i];
            }
        }

        private void ParseMap(string[] input) {

            this.map = new char[input[0].Length, input.Length];
            this.mapLarge = new char[input[0].Length, input.Length * 2];

            // small map.
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {
                    if (input[i][j] == '@') {
                        this.robot = new Coordinate(i, j);
                        this.map[i, j] = '.';
                    }
                    else {
                        this.map[i, j] = input[i][j];
                    }
                }
            }

            // large map.
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {

                    switch (input[i][j]) {

                        case '@':
                            this.robotLarge = new Coordinate(i, j*2);
                            this.mapLarge[i, j*2] = '.';
                            this.mapLarge[i, j*2+1] = '.';
                            break;

                        case '#':
                            this.mapLarge[i, j * 2] = '#';
                            this.mapLarge[i, j * 2+ 1] = '#';
                            break;

                        case '.':
                            this.mapLarge[i, j * 2] = '.';
                            this.mapLarge[i, j * 2+1] = '.';
                            break;

                        case 'O':
                            this.mapLarge[i, j * 2] = '[';
                            this.mapLarge[i, j * 2+1] = ']';
                            break;
                    }
                }
            }
        }


        public void PrintMapLarge() {

            for (int i = 0; i < this.mapLarge.GetLength(0); i++) {
                for (int j = 0; j < this.mapLarge.GetLength(1); j++) {

                    if (this.robotLarge.col == j && this.robotLarge.row == i) {
                        Console.Write('@');
                    }
                    else {
                        Console.Write(this.mapLarge[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }


        public void PrintMap() {

            for (int i = 0; i < this.map.GetLength(0); i++) {
                for (int j = 0; j < this.map.GetLength(1); j++) {

                    if (this.robot.col == j && this.robot.row == i) {
                        Console.Write('@');
                    }
                    else {
                        Console.Write(this.map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        private Coordinate GetNeighbor(Coordinate start, HikingDirection direction) {

            int rowDelta = 0;
            int colDelta = 0;

            switch (direction) {
                case HikingDirection.Up:
                    rowDelta = -1;
                    break;
                case HikingDirection.Down:
                    rowDelta = 1;
                    break;
                case HikingDirection.Left:
                    colDelta = -1;
                    break;
                case HikingDirection.Right:
                    colDelta = 1;
                    break;
            }

            return new Coordinate(start.row + rowDelta, start.col + colDelta);
        }


        private bool TryPushBox(HikingDirection direction, Coordinate box) {

            // boxes need to also be pushed, when appropriate, within this function.

            Coordinate neighbor = GetNeighbor(box, direction);
            char neighborItem = this.map[neighbor.row, neighbor.col];

            switch (neighborItem) {
                case '.':
                    // move the box.
                    //this.map[neighbor.row, neighbor.col] = 'O';
                    //this.map[box.row, box.col] = '.';
                    //return true;
                    break;

                case '#':
                    return false;

                case 'O':
                    if (!(TryPushBox(direction, neighbor))) {
                        return false;
                    }
                    break;

                default:
                    Debug.Assert(false);
                    return false;
            }

            this.map[neighbor.row, neighbor.col] = 'O';
            this.map[box.row, box.col] = '.';
            return true;
        }
        

        public void RunTheRobot() {

            int index = 0;

            foreach (char move in moveList) {

                // What direction do we need to move into?
                HikingDirection direction;
                switch (move) {
                    case '^':
                        direction = HikingDirection.Up;
                        break;
                    case 'v':
                        direction = HikingDirection.Down;
                        break;
                    case '<':
                        direction = HikingDirection.Left;
                        break;
                    case '>':
                        direction = HikingDirection.Right;
                        break;
                    default:
                        Debug.Assert(false);
                        direction = HikingDirection.Up;
                        return;
                }

                // Figure out the space i need to move into.
                Coordinate nextSpace = this.GetNeighbor(this.robot, direction);
                char nextSpaceItem = this.map[nextSpace.row, nextSpace.col];

                switch (nextSpaceItem) {

                    // if it's a wall, do nothing.
                    case '#':
                        // do nothing.
                        break;

                    // if: it's empty, move into it.
                    case '.':
                        this.robot = nextSpace;
                        break;

                    // if: it's a box, attempt to shove the box in that direction.
                    case 'O':
                        if (this.TryPushBox(direction, nextSpace)) {
                            this.robot = nextSpace;
                        }
                        break;


                }

                // Debug purposes.
                //Console.WriteLine();
                //Console.WriteLine("move " + index++ + ": " + move);
                //this.PrintMap();
            }
        }

        public int CalculateGpsValue(int row, int col) {

            return (100 * row) + col;
        }


        public int SumOfAllBoxCoordinates() {

            int sum = 0;

            for (int i = 0; i < this.map.GetLength(0); i++) {
                for (int j = 0; j < this.map.GetLength(1); j++) {
                    if (this.map[i,j] == 'O') {

                        sum += CalculateGpsValue(i, j);

                    }
                   
                }
                Console.WriteLine();
            }



            return sum;
        }
    }
}
