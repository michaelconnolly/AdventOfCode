using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {
    public class RepairDroidMap {

        int maximumX, maximumY;
        int minimumX, minimumY;
        List<RepairDroidCoordinate> coordinates = new List<RepairDroidCoordinate>();

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

        public void CalculateScreenSize() {

            this.maximumX = int.MinValue;
            this.maximumY = int.MinValue;
            this.minimumX = int.MaxValue;
            this.minimumY = int.MaxValue;

            //foreach (string key in robotCommands.Keys) {
            foreach (RepairDroidCoordinate coordinate in this.coordinates) { 

                //string[] coordinates = key.Split(',');
                //int x = Convert.ToInt32(coordinates[0]);
                //int y = Convert.ToInt32(coordinates[1]);
                if (coordinate.x > maximumX) maximumX = coordinate.x;
                if (coordinate.y > maximumY) maximumY = coordinate.y;
                if (coordinate.x < minimumX) minimumX = coordinate.x;
                if (coordinate.y < minimumY) minimumY = coordinate.y;
            }

            // why do i do this?
            this.maximumX++;
            this.maximumY++;
        }


        public void Print() {

            this.CalculateScreenSize();

            Console.WriteLine("\n");

            //Dictionary<string, string> robotCommandsFormatted = new Dictionary<string, string>();
            //foreach (string key in robotCommands.Keys) {
            foreach (RepairDroidCoordinate coordinate in this.coordinates) { 
                //string[] coordinates = key.Split(',');
                //coordinate.xFormatted = Convert.ToInt32(coordinates[0]) + (-(this.minimumX));
                //coordinate.yFormatted = Convert.ToInt32(coordinates[1]) + (-(this.minimumY));

                coordinate.xFormatted = coordinate.x + (-(this.minimumX));
                coordinate.yFormatted = coordinate.y + (-(this.minimumY));


                //string newKey = x.ToString() + "," + y.ToString();
                //robotCommandsFormatted[newKey] = robotCommands[key];
            }

            int realMaximumX = this.maximumX + (-(this.minimumX));
            int realMaximumY = this.maximumY + (-(this.minimumY));

            // Console.WriteLine("SCORE: " + this.CurrentScore);

            char[][] rows = new char[(realMaximumY)][];

            for (int row = 0; row < realMaximumY; row++) {

                char[] rowData = new char[realMaximumX];
                rows[row] = rowData;
            }

           // foreach (string key in robotCommandsFormatted.Keys) {
           foreach (RepairDroidCoordinate coordinate in this.coordinates) { 

                //string[] coordinates = key.Split(',');
                //int x = Convert.ToInt32(coordinates[0]);
                //int y = Convert.ToInt32(coordinates[1]);
                //string rawChar = (string)robotCommandsFormatted[key];
                //char actualChar = rawChar.ToCharArray()[0];
                rows[coordinate.yFormatted][coordinate.xFormatted] = coordinate.item; // this.DrawThisTile(actualChar);

                //// Where is the paddle and ball?
                //if (actualChar == '3') {
                //    this.currentColPaddle = x;
                //}
                //else if  (actualChar == '4') {
                //    this.currentColBall = x;
                //}
            }

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
