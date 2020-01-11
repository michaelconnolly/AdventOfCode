using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {

    public class RepairDroidCoordinate {

        public int x;
        public int y;
        private RepairDroidMap map;
        public char item;
        public int xFormatted;
        public int yFormatted;

        public RepairDroidCoordinate(int x, int y, char item, RepairDroidMap map) {

            this.x = x;
            this.y = y;
            this.item = item;
            this.map = map;
        }

        public string description {
            get {
                return "(" + this.x + "," + this.y.ToString() + ") - (" + this.xFormatted + "," + this.yFormatted + ")";
            }
        }

///*        public RepairDroidCoordinate FindNeighbor(int direction) */{

//            int xNeighbor = this.x;
//            int yNeighbor = this.y;

//            switch (direction) {

//                case RepairDroid.NORTH:

//                    yNeighbor--;
//                    break;

//                case RepairDroid.SOUTH:

//                    yNeighbor++;
//                    break;

//                case RepairDroid.WEST:

//                    xNeighbor--;
//                    break;

//                case RepairDroid.EAST:

//                    xNeighbor++;
//                    break;

//                default:

//                    Console.WriteLine("ERROR in FindNeighbor()!");
//                    break;
//            }

//            return map.FindCoordinate(xNeighbor, yNeighbor);
//        }


        public RepairDroidCoordinate FindNeighbor(int direction, bool createForMe) {

            int xNeighbor = this.x;
            int yNeighbor = this.y;

            switch (direction) {

                case RepairDroid.NORTH:

                    yNeighbor--;
                    break;

                case RepairDroid.SOUTH:

                    yNeighbor++;
                    break;

                case RepairDroid.WEST:

                    xNeighbor--;
                    break;

                case RepairDroid.EAST:

                    xNeighbor++;
                    break;

                default:

                    Console.WriteLine("ERROR in FindNeighbor()!");
                    break;
            }

            RepairDroidCoordinate neighbor = map.FindCoordinate(xNeighbor, yNeighbor);

            if (createForMe) {
                Program.Assert(neighbor == null, "ERROR in RepairDroidCoordinate.FindNeighbor!");
                return map.CreateCoordinate(xNeighbor, yNeighbor, '?');
            }

            return map.FindCoordinate(xNeighbor, yNeighbor);
        }

        //public int PickDirection(int currentDirection) {

        //    int newDirection = currentDirection + 1;
        //    if (newDirection == 5) newDirection = 1;


        //    // check to see if we have already gone that route.  Only do that if all other paths ...


        //    return newDirection;
        //}

    }
}
