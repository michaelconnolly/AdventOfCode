using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {
    internal class PipeMap {

        char[,] map;
        char[,] mapClear;
        char[,] mapEnclosed;
        int width;
        int height;
        int startX;
        int startY;
        int countOfSteps;
        int countOfEnclosedTiles;

        public PipeMap(string[] lines) {

            // initialize our maps.
            this.width = lines[0].Length;
            this.height = lines.Length;
            this.map = new char[width, height];
            this.mapClear = new char[width, height]; 
            this.mapEnclosed = new char[width, height];
     
            // Populate first map.
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    this.mapClear[x, y] = '.';
                    this.map[x, y] = lines[y].ToCharArray()[x];
                    // while we are here, find the 'S'.
                    if (this.map[x, y] == 'S') {
                        this.startX = x;
                        this.startY = y;
                    }
                }
            }

            this.countOfSteps = this.CalculateOfficialLoop();

            // initialize our third map from our second map.
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    this.mapEnclosed[x, y] = this.mapClear[x, y];
                }
            }

            // pre-calculate answer to #2.
            this.countOfEnclosedTiles = this.CalculateEnclosedTiles();
        }

        private PipeMapLocation FindOtherConnection(PipeMapLocation center, PipeMapLocation end1) {

            Collection<PipeMapLocation> pipeMapLocations = this.CalculateConnectedNeighbors(center);

            foreach (PipeMapLocation location in pipeMapLocations) {
                if ((location.x != end1.x) || (location.y != end1.y)) return location;
            }

            Console.WriteLine("ERROR!");
            throw new Exception();
        }

        private Collection<PipeMapLocation> CalculateConnectedNeighbors(PipeMapLocation location) {

            Collection<PipeMapLocation> pair = new Collection<PipeMapLocation>();
            char c = this.map[location.x, location.y];
            int x = location.x;
            int y = location.y;

            // above?
            if (location.y > 0) {
                char up = this.map[x, y - 1];
                if ((up == '7' || up == 'F' || up == '|' || up == 'S') &&
                    (c == 'S' || c == '|' || c == 'L' || c == 'J')) {
                    pair.Add(new PipeMapLocation(x, y - 1));
                }
            }

            // below?
            if (location.y < this.height - 1) {
                char down = this.map[x, y + 1];

                if ((down == 'L' || down == 'J' || down == '|' || down == 'S') &&
                (this.map[x, y] == 'S' || this.map[x, y] == '|' || this.map[x, y] == '7' || this.map[x, y] == 'F')) {
                    pair.Add(new PipeMapLocation(x, y + 1));
                }
            }

            // left?
            if (location.x > 0) {
                char left = this.map[x - 1, y];

                if ((left == 'F' || left == 'L' || left == '-' || left == 'S') &&
                (this.map[x, y] == 'S' || this.map[x, y] == '-' || this.map[x, y] == '7' || this.map[x, y] == 'J')) {
                    pair.Add(new PipeMapLocation(x - 1, y));
                }
            }


            // right?
            if (location.x < this.width - 1) {
                char right = this.map[x + 1, y];

                if ((right == 'J' || right == '7' || right == '-' || right == 'S') &&
                 (this.map[x, y] == 'S' || this.map[x, y] == '-' || this.map[x, y] == 'L' || this.map[x, y] == 'F')) {
                    pair.Add(new PipeMapLocation(x + 1, y));
                }
            }

            if (pair.Count != 2) {
                Console.WriteLine("ERROR!");
                throw new Exception();
            }

            return pair;
        }
    


        private void MarkOnMap(PipeMapLocation location) {
            this.mapClear[location.x, location.y] = this.map[location.x, location.y];
        }

        public int CalculateOfficialLoop() {

            // Find S, and find two initial connections.
            int count = 2;
            PipeMapLocation start = new PipeMapLocation(this.startX, this.startY);
            Collection<PipeMapLocation> startConnections = this.CalculateConnectedNeighbors(start);
            PipeMapLocation connection1 = startConnections[0];
            PipeMapLocation connection2 = startConnections[1];
            this.MarkOnMap(start);
            this.MarkOnMap(connection1);
            this.MarkOnMap(connection2);

            // Pic one, and start looping.
            PipeMapLocation lastLocation = start;
            PipeMapLocation currentLocation = connection1;

            while ((currentLocation.x != connection2.x) || (currentLocation.y != connection2.y)) {

                PipeMapLocation newLocation = this.FindOtherConnection(currentLocation, lastLocation);
                this.MarkOnMap(newLocation);
                lastLocation = currentLocation;
                currentLocation = newLocation;
                count++;
            }

            return count;
        }


        private bool TestAnyNeighbor(PipeMapLocation location, char charToTest) {

            int x = location.x;
            int y = location.y;

            if (this.mapEnclosed[x - 1, y - 1] == charToTest ||
                this.mapEnclosed[x, y - 1] == charToTest ||
                this.mapEnclosed[x + 1, y - 1] == charToTest ||
                this.mapEnclosed[x - 1, y] == charToTest ||
                this.mapEnclosed[x + 1, y] == charToTest ||
                this.mapEnclosed[x - 1, y + 1] == charToTest ||
                this.mapEnclosed[x, y + 1] == charToTest ||
                this.mapEnclosed[x + 1, y + 1] == charToTest) {

                //this.mapEnclosed[x, y] = charToTest;
                return true;
            }

            return false;
        }


        public bool TestIfFound(int x, int y, char c) {

            return (this.map[x, y] == c || this.map[x, y] == 'S' || this.map[x, y] == '?');
        }


        public bool TestForEnclosed(int x, int y) {

            // Anything on the edges is definitely not inside the loop.
            if (x == 0 || x == (width - 1) || y == 0 || y == (height - 1) ) {
                this.mapEnclosed[x, y] = 'O';
                return false;
            }

            // If any cell around it was deemed outside, it's outside.
            //if (this.mapEnclosed[x-1, y-1] == 'O' ||
            //    this.mapEnclosed[x , y - 1] == 'O' ||
            //    this.mapEnclosed[x + 1, y - 1] == 'O' ||
            //    this.mapEnclosed[x-1, y] == 'O' ||
            //    this.mapEnclosed[x + 1, y] == 'O' ||
            //    this.mapEnclosed[x - 1, y + 1] == 'O' ||
            //    this.mapEnclosed[x, y + 1] == 'O' ||
            //    this.mapEnclosed[x + 1, y + 1] == 'O') {
            //    this.mapEnclosed[x, y] = 'O';
            //    return false;
            //}
            if (this.TestAnyNeighbor(new PipeMapLocation(x,y), 'O')) {
                this.mapEnclosed[x, y] = 'O';
                return false;
            }

            // If any cell around it was deemed inside, it's inside.
            if (this.TestAnyNeighbor(new PipeMapLocation(x, y), 'I')) {
                this.mapEnclosed[x, y] = 'I';
                return true;
            }

            // If you are surrounded by the right tiles, you are enclosed.
            //if ((this.map[x, y - 1] == '-' || this.map[x, y - 1] == 'S' || this.map[x, y - 1] == '?') &&
            //   (this.map[x, y + 1] == '-' || this.map[x, y + 1] == 'S' || this.map[x, y + 1] == '?') &&
            //   (this.map[x - 1, y] == '|' || this.map[x - 1, y] == 'S' || this.map[x - 1, y] == '?') &&
            //   (this.map[x + 1, y] == '|' || this.map[x + 1, y] == 'S' || this.map[x + 1, y] == '?')) {
            //    this.mapEnclosed[x, y] = 'I';
            //    return true;

            bool upEnclosed = (this.TestIfFound(x, y - 1, '-'));
            bool downEnclosed = this.TestIfFound(x, y + 1, '-');
            bool leftEnclosed = this.TestIfFound(x - 1, y, '|');
            bool rightEnclosed = this.TestIfFound(x + 1, y, '|');

            if (upEnclosed && downEnclosed && leftEnclosed && rightEnclosed) {
                this.mapEnclosed[x, y] = 'I';
                return true;
            }

            // OK we need to go deeper.
            this.mapEnclosed[x, y] = '?';

            if (!upEnclosed) {
                upEnclosed = this.TestForEnclosed(x, y - 1);
            }


            // if ((this.map[x, y - 1] == '-' || this.map[x, y - 1] == 'S') &&
            //    (this.map[x, y + 1] == '-' || this.map[x, y + 1] == 'S') &&
            //    (this.map[x-1, y] == '|' || this.map[x-1, y] == 'S') &&
            //    (this.map[x+1, y] == '|' || this.map[x+1, y] == 'S')) {
            //    this.mapEnclosed[x, y] = 'I';
            //    return true;
            //}

            //// recursively ask if my neighbors are well understood.
            //this.mapEnclosed[x, y] = '?';
            //bool upEnclosed = true;
            //bool downEnclosed = true;
            //bool leftEnclosed = true;
            //bool rightEnclosed = true;

            //if(y>0) upEnclosed = this.TestForEnclosed(x, y - 1);
            //if (y < height - 1) downEnclosed = this.TestForEnclosed(x, y + 1);
            //if (x > 0) leftEnclosed = this.TestForEnclosed(x - 1, y);
            //if (x < width - 1) rightEnclosed = this.TestForEnclosed(x+1, y);

            //if (upEnclosed || downEnclosed || leftEnclosed || rightEnclosed) {
            //    this.mapEnclosed[x, y] = 'I';
            //    return true;
            //}



            return false;
        }

        public int CalculateEnclosedTiles() {

            // Find S, and find two initial connections.
            int count = 0;
            //PipeMapLocation start = new PipeMapLocation(this.startX, this.startY);
            //Collection<PipeMapLocation> startConnections = this.CalculateConnectedNeighbors(start);
            //PipeMapLocation connection1 = startConnections[0];
            //PipeMapLocation connection2 = startConnections[1];
            // this.MarkOnMap(start);
            // this.MarkOnMap(connection1);
            //this.MarkOnMap(connection2);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {

                    char c = this.mapClear[x, y];

                    switch (c) {

                        case 'S':
                        case 'F':
                        case 'J':
                        case '-':
                        case '|':
                        case '7':
                        case 'L':
                            this.mapEnclosed[x, y] = 'X';
                            break;

                        default:
                            if (this.TestForEnclosed(x, y)) {
                                count++;
                            }
                            break;
                    }

                }
            }

            return count;
        }



        public void print() {

            for (int y = 0; y < height; y++) {

                for (int x = 0; x < width; x++) {

                    Console.Write(this.map[x, y]);

                }

                Console.WriteLine();
            }

            Console.WriteLine();


            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(this.mapClear[x, y]);
                }
                Console.WriteLine();
            }

            // Map #3.
            Console.WriteLine();
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(this.mapEnclosed[x, y]);
                }
                Console.WriteLine();
            }
        }

        public int GetStepCountOfFurthestPointFromStart() {

            
            return (this.countOfSteps / 2);

        }

        public int GetCountOfEnclosedTiles() {

           return this.countOfEnclosedTiles;

        }
    }
}
