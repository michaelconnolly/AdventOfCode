using System;
using System.Collections.ObjectModel;


namespace AdventOfCode2023 {

    
    internal class GalaxyMapPair {

        public GalaxyMapLocation location1;
        public GalaxyMapLocation location2;

        public GalaxyMapPair(GalaxyMapLocation location1, GalaxyMapLocation location2) {
            this.location1 = location1;
            this.location2 = location2;
        }

        public int distance() {

            return (Math.Abs(location1.x - location2.x) + Math.Abs(location1.y - location2.y));
        }

        public long distancePart2(Collection<long> blankRows, Collection<long> blankCols, int mulitplier) {

            long distanceX = 0;
            long distanceY = 0;

            // figure out if any of the spaces inbetween need to be multiplied.
            int lowerValue, upperValue;
            if (location1.x <= location2.x) {
                lowerValue = location1.x;
                upperValue = location2.x;
            }
            else {
                lowerValue = location2.x;
                upperValue = location1.x;
            }
            for (int i = lowerValue+1; i <= upperValue; i++) {
                if (blankCols.Contains(i)) {
                    distanceX += mulitplier;
                }
                else {
                    distanceX++;
                }
            }

            // figure out if any of the spaces inbetween need to be multiplied.
           // int lowerValue, upperValue;
            if (location1.y <= location2.y) {
                lowerValue = location1.y;
                upperValue = location2.y;
            }
            else {
                lowerValue = location2.y;
                upperValue = location1.y;
            }
            for (int i = lowerValue+1; i <= upperValue; i++) {
                if (blankRows.Contains(i)) {
                    distanceY += mulitplier;
                }
                else {
                    distanceY++;
                }
            }

            long distanceOld = this.distance();
            long distanceNew = distanceX + distanceY;
            return distanceNew;
        }

    }
}
