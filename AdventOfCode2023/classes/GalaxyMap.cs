using System;
using System.Collections.ObjectModel;

namespace AdventOfCode2023 {
    internal class GalaxyMap {

        long width;
        long height;
        long newWidth;
        long newHeight;
        char[,] map;
        Collection<GalaxyMapLocation> galaxies = new Collection<GalaxyMapLocation>();
        Collection<GalaxyMapPair> galaxyDistances = new Collection<GalaxyMapPair>();
        int countOfReplacementBlankRows;
        Collection<long> blankRows = new Collection<long>();
        Collection<long> blankCols = new Collection<long>();

        public GalaxyMap(string[] lines, int countOfReplacementBlankRows) {

            this.countOfReplacementBlankRows = countOfReplacementBlankRows;
            this.height = lines.Length;
            this.width = lines[0].Length;

            // Find blank rows.
            for (int y = 0; y < height; y++) {
                bool blank = true;
                for (int x = 0; x < width; x++) {
                    if (lines[y][x] != '.') {
                        blank = false;
                        break;
                    }
                }
                if (blank) { blankRows.Add(y); };
            }

            // Find the blank columns.
            for (int x = 0; x < width; x++) {
                bool blank = true;
                for (int y = 0; y < height; y++) {
                    if (lines[y][x] != '.') {
                        blank = false;
                        break;
                    }
                }
                if (blank) { blankCols.Add(x); };
            }

            // Generate the expanded map.
            //this.newWidth = width + (blankCols.Count * (this.countOfReplacementBlankRows - 1));
            //this.newHeight = height + (blankRows.Count * (this.countOfReplacementBlankRows - 1));
            this.map = new char[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    this.map[x, y] = lines[y][x];
                }
            }
            print2();

            //// Generate the expanded map.
            //this.newWidth = width + (blankCols.Count * (this.countOfReplacementBlankRows - 1));
            //this.newHeight = height + (blankRows.Count * (this.countOfReplacementBlankRows - 1));
            //this.map = new char[newWidth, newHeight];
            //for (int x = 0; x < newWidth; x++) {
            //    for (int y = 0; y < newHeight; y++) {
            //        this.map[x, y] = 'X';
            //    }
            //}

            //// throw in the duplicate columns.
            ////this.print();
            //for (int y = 0; y < height; y++) {

            //    int currentColToWrite = 0;

            //    for (int x = 0; x < width; x++) {

            //        // do we have to write once or twice?
            //        int writeColCount = (blankCols.Contains(x) ? this.countOfReplacementBlankRows : 1);


            //        for (int writePass = 0; writePass < writeColCount; writePass++) {
            //            this.map[currentColToWrite++, y] = lines[y][x];
            //            //this.print();
            //        }
            //    }
            //}
            ////this.print();

            //// for each blank row
            ////      start from the bottom 
            //long yRead = (this.height - 1);
            //for (long yWrite = (newHeight - 1); yWrite >= 0; yWrite--) {

            //    if (blankRows.Contains(yRead)) {

            //        for (int copies = 0; copies < this.countOfReplacementBlankRows; copies++) {
            //            for (int x = 0; x < newWidth; x++) {
            //                this.map[x, yWrite] = this.map[x, yRead];
            //            }
            //            yWrite--;
            //        }
            //        yRead--;
            //        yWrite++; // not proud of this hack.


            //    }


            //    else {    

            //        for (int x = 0; x < newWidth; x++) {
            //            this.map[x, yWrite] = this.map[x, yRead];
            //        }
            //        yRead--;


            //    }

            //   // print();

            //    //     for (int y = (newHeight - 1); y >=0; y--) {


            //    //}

            //    //// Throw in our duplicate rows.
            //    //foreach (int rowToDuplicate in blankRows.Reverse()) {
            //    //    for (int y = (newHeight - 1); y > (rowToDuplicate); y--) {
            //    //    //for (int y = (newHeight - 1); y > (rowToDuplicate + this.countOfReplacementBlankRows); y--) {
            //    //    //for (int y = (newHeight - (this.countOfReplacementBlankRows -1)); y > rowToDuplicate; y--) {



            //    //            for (int x=0; x< newWidth; x++) {



            //    //            //if (this.map[x, y - 1] == 'X') // i am not proud of this hack.
            //    //            //    this.map[x, y] = '.';
            //    //            //else {
            //    //               // this.map[x, y] = this.map[x, y - (this.countOfReplacementBlankRows - 1)];
            //    //            this.map[x, y] = this.map[x, y - (this.countOfReplacementBlankRows - 1)];
            //    //            this.print();
            //    //            //}
            //    //        }
            //}



            // create the list of galaxies.
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (this.map[x, y] == '#') {
                        this.galaxies.Add(new GalaxyMapLocation(x, y));
                    }
                }
            }


            //// create the list of galaxies.
            //for (int y = 0; y < newHeight; y++) {
            //    for (int x = 0; x < newWidth; x++) {
            //        if (this.map[x,y] == '#') {
            //            this.galaxies.Add(new GalaxyMapLocation(x,y));
            //        }
            //    }
            //}




            // Generate the set of all pairs.
            for (int index = 0; index < (this.galaxies.Count - 1); index++) {
                for (int index2 = index+1; index2 < this.galaxies.Count; index2++) {
                    this.galaxyDistances.Add(new GalaxyMapPair(this.galaxies[index], this.galaxies[index2]));
                }
            }
        }

        public void print2() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Galaxy count: " + this.galaxies.Count);
            Console.WriteLine("**************************");
        }

        public void print() {

            for (int y=0; y < newHeight; y++) {
                for (int x=0; x < newWidth; x++) {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Galaxy count: " + this.galaxies.Count);
            Console.WriteLine("**************************");
        }

        public long GetSumOfAllDistanceLengths() {

            long sum = 0;

            foreach (GalaxyMapPair pair in this.galaxyDistances) {
                sum += pair.distancePart2(blankRows, blankCols, this.countOfReplacementBlankRows);
            }

            return sum;
        }
    }
}
