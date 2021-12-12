using System;
using System.Collections.ObjectModel;
using System.Text;


namespace AdventOfCode2021 {
    
    
    public class OceanFloorMap {

        Collection<OceanFloorLine> lines = new Collection<OceanFloorLine>();
        public int maxX = 0;
        public int maxY = 0;
        int[,] overlappingLinesMap;


        public OceanFloorMap(string[] mapData) {

            // Load line data.
            Console.WriteLine("Initializing ocean floor map with " + mapData.Length + " rows.");
            foreach (string row in mapData) {
                string[] tuple = row.Split(" -> ");
                string[] coordinate0 = tuple[0].Split(',');
                string[] coordinate1 = tuple[1].Split(',');
                lines.Add( new OceanFloorLine(coordinate0[0], coordinate0[1], 
                    coordinate1[0], coordinate1[1]));
            }

            // Figure out the highest x and y so we know how big to build our map.
            foreach (OceanFloorLine line in this.lines) {

                if (line.x1 > maxX) maxX = line.x1;
                if (line.x2 > maxX) maxX = line.x2;
                if (line.y1 > maxY) maxY = line.y1;
                if (line.y2 > maxY) maxY = line.y2;
            }
            maxX++;
            maxY++;
            overlappingLinesMap = new int[maxX, maxY];
        }
        

        public void plotStaightLinesOnly() {

            foreach (OceanFloorLine line in this.lines) {

                if (line.isHorizontalOrVertical()) {
                    plotLine(line);
                }
            }
        }


        public void plotAllLines () {

            foreach (OceanFloorLine line in this.lines) {
                plotLine(line);
            }
        }


        public void plotLine(OceanFloorLine line) {

            if (line.isHorizontal()) {

                int start = Math.Min(line.y1, line.y2);
                int end = Math.Max(line.y1, line.y2);

                for (int y = start; y <= end; y++) {
                    this.overlappingLinesMap[line.x1, y]++;
                }
            }

            else if (line.isVertical()) {

                int start = Math.Min(line.x1, line.x2);
                int end = Math.Max(line.x1, line.x2);

                for (int x = start; x <= end; x++) {
                    this.overlappingLinesMap[x, line.y1]++;
                }
            }

            else {// diagonal;  Always 45 degrees.

                int directionX = (line.x1 < line.x2 ? 1 : -1);
                int directionY = (line.y1 < line.y2 ? 1 : -1);
                int diffY = Math.Abs(line.y1 - line.y2) + 1;
                int currentX = line.x1;
                int currentY = line.y1;

                for (int y=0; y<diffY; y++) {
                    this.overlappingLinesMap[currentX, currentY]++;
                    currentX += directionX;
                    currentY += directionY;
                }
            }
        }


        public int countOfOverlaps(int thresholdAmount) {

            int overlaps = 0;

            for (int y=0; y < maxY; y++) {
                for (int x=0; x < maxX; x++) {
                    if (overlappingLinesMap[x,y] >= thresholdAmount) {
                        overlaps++;
                    }
                }
            }

            return overlaps;
        }


        public void print() {

            Console.WriteLine("");
            for (int y = 0; y < maxY; y++) {
                for (int x=0; x<maxX; x++) {
                    Console.Write(overlappingLinesMap[x, y].ToString() + ' ');
                }
                Console.WriteLine("");
            }
        }
    }
}
