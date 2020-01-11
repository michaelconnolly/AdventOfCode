using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {
    public class RepairDroidMap {

        //int maximumX, maximumY;
        //int minimumX, minimumY;
        List<RepairDroidCoordinate> coordinates = new List<RepairDroidCoordinate>();
        public RepairDroidCoordinate start = null;
        public RepairDroidCoordinate end = null;

        public RepairDroidCoordinate TeleportToBetterSpot() {

            foreach (RepairDroidCoordinate coordinate in coordinates) {

                int direction = PickDirection(coordinate);
                if (direction != -99) {
                    return coordinate;
                }
            }

            return null;
        }

        private int RotateDirection(int startDirection) {

            int newDirection = startDirection + 1;
            if (newDirection == 5) newDirection = 1;

            return newDirection;
        }

        public int PickDirection(RepairDroidCoordinate currentCoordinate) { //, int currentFailedDirection) { //, int cameFromDirection) {

            //if (newDirection == 5) newDirection = 1;

            int newDirection = RepairDroid.NORTH;
            int attemptCount = 1;

            while (attemptCount <= 4) {

              
                // check to see if we have already gone that route.  Only do that if all other paths ...
                RepairDroidCoordinate newCoordinate = currentCoordinate.FindNeighbor(newDirection, false);
                if (newCoordinate == null) {
                    return newDirection;
                }

                //if (newDirection == cameFromDirection) {
                //    // do nothing;
                //}
                //else {
                //    // do nothing
                //}

                newDirection = this.RotateDirection(newDirection);


                attemptCount++;
            }

            return -99;

            //Console.WriteLine("HELP!  STUCK!");
            //  throw new Exception() ;
            ////
            //return cameFromDirection;
        }

        public RepairDroidCoordinate CreateCoordinate(int x, int y, char item) {

            RepairDroidCoordinate coordinate = new RepairDroidCoordinate(x, y, item, this);
            this.coordinates.Add(coordinate);

            return coordinate;
        }

        public RepairDroidCoordinate FindCoordinate(int x, int y) {

            foreach (RepairDroidCoordinate coordinate in coordinates) {

                if (coordinate.x == x && coordinate.y == y) {
                    return coordinate;
                }
            }

            return null;
        }

        public void Print() {

            // Calculate screen size.
            int maximumX = int.MinValue;
            int maximumY = int.MinValue;
            int minimumX = int.MaxValue;
            int minimumY = int.MaxValue;
            foreach (RepairDroidCoordinate coordinate in this.coordinates) {
                if (coordinate.x > maximumX) maximumX = coordinate.x;
                if (coordinate.y > maximumY) maximumY = coordinate.y;
                if (coordinate.x < minimumX) minimumX = coordinate.x;
                if (coordinate.y < minimumY) minimumY = coordinate.y;
            }

            // why do i do this? I forget.
            maximumX++;
            maximumY++;

            Console.WriteLine("\n");

            // Remap all the relative values i have to something 0 based so i can easily print out.
            foreach (RepairDroidCoordinate coordinate in this.coordinates) { 

                coordinate.xFormatted = coordinate.x + (-(minimumX));
                coordinate.yFormatted = coordinate.y + (-(minimumY));
            }
            int realMaximumX = maximumX + (-(minimumX));
            int realMaximumY = maximumY + (-(minimumY));

            // Construct some big dumb arrays to store our printable map.
            char[][] rows = new char[(realMaximumY)][];
            for (int row = 0; row < realMaximumY; row++) {
                rows[row] = new char[realMaximumX];
            }

           // Map the characters we have stored for each coordinate to the big dumb array we are using to print to the screen. 
           foreach (RepairDroidCoordinate coordinate in this.coordinates) { 

                // Special case: print the starting spot differently.
                if (coordinate == this.start) {
                    coordinate.item = 'X';
                }
                rows[coordinate.yFormatted][coordinate.xFormatted] = coordinate.item; 
            }

            // Dump the big dumb arrays of characters to the screen. 
            foreach (char[] row in rows) {

                string rowString = "";
                foreach (char currentChar in row) {
                    rowString += currentChar;
                }

                Console.WriteLine(rowString);
            }

            return;
        }
    }
}
