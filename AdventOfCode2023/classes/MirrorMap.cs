using System;

namespace AdventOfCode2023 {

    internal enum reflectionPlane {
        unknown = 0,
        horiztonal,
        vertical
    }

    internal class MirrorMap {

        private char[,] map;
        int height;
        int width;
        reflectionPlane reflectionPlane = reflectionPlane.unknown;
        int significantRowCol = -1;
        reflectionPlane reflectionPlane2 = reflectionPlane.unknown;
        int significantRowCol2 = -1;
     
        public MirrorMap(string[] lines) {

            this.height = lines.Length;
            this.width = lines[0].Length;

            // want to reference as [x,y]
            this.map = new char[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    this.map[x, y] = lines[y][x];
                }
            }

            this.FindReflectionPlane();
            this.FindReflectionPlane2();
        }


        private bool IsThisTheRowPlane(int initialRow2, int mistakesAllowed=0) {

            int row1 = initialRow2 - 1;
            int row2 = initialRow2;
            int passCount = Math.Min(row1+1, (height - row2));
            bool isMatch = true;
            int mistakesFound = 0;

            for (int pass = 0; pass < passCount; pass++) {

                for (int x=0; x<width; x++) {
                    if (this.map[x, row1-pass] != this.map[x,row2+pass]) {
                        mistakesFound++;
                        if (mistakesFound > mistakesAllowed) {
                            isMatch = false;
                            break;
                        }
                    }
                }
               
                if (!isMatch) {
                    break;
                }
            }

            if (isMatch && (mistakesFound == mistakesAllowed)) {
                return true;
            }

            return false;
        }


        private bool IsThisTheColPlane(int initialCol2, int mistakesAllowed=0) {

            int col1 = initialCol2 - 1;
            int col2 = initialCol2;
            int passCount = Math.Min(col1 + 1, (width - col2));
            bool isMatch = true;
            int mistakesFound = 0;

            for (int pass = 0; pass < passCount; pass++) {

                for (int y = 0; y < height; y++) {
                    if (this.map[col1-pass, y] != this.map[col2+pass, y]) {
                        mistakesFound++;
                        if (mistakesFound > mistakesAllowed) {
                            isMatch = false;
                            break;
                        }
                    }
                }

                if (!isMatch) {
                    break;
                }
            }

            // we need to double check there was at least one smudge.
            if (isMatch && (mistakesFound == mistakesAllowed)) {
                return true;
            }

            return false;
        }

        private void FindReflectionPlane() {

            // this code will act wonky if the map is not at least two rows high.

            // First, let's look for it horizontally.
            for (int y = 1; y < height; y++) {

                if (this.IsThisTheRowPlane(y)) {
                    this.reflectionPlane = reflectionPlane.horiztonal;
                    this.significantRowCol = y;
                    return;
                }
            }

            // How about vertically?
            for (int x=1; x < width; x++) {

                if (this.IsThisTheColPlane(x)) {
                    this.reflectionPlane = reflectionPlane.vertical;
                    this.significantRowCol = x;
                    return;
                }
            }

            Console.WriteLine("ERROR in FindReflectionPlane()!");
            return;
        }


        private void FindReflectionPlane2() {

            // First, let's look for it horizontally.
            for (int y = 1; y < height; y++) {

                if (this.IsThisTheRowPlane(y, 1)) {
                    this.reflectionPlane2 = reflectionPlane.horiztonal;
                    this.significantRowCol2 = y;
                    return;
                }
            }

            // How about vertically?
            for (int x = 1; x < width; x++) {

                if (this.IsThisTheColPlane(x, 1)) {
                    this.reflectionPlane2 = reflectionPlane.vertical;
                    this.significantRowCol2 = x;
                    return;
                }
            }

            Console.WriteLine("ERROR in FindReflectionPlane2()!");
            throw new Exception();
            return;
        }

        public void print() { 

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("points: " + this.GetPoints());
            Console.WriteLine("plane: " + this.reflectionPlane.ToString());
            Console.WriteLine("signficant: " + this.significantRowCol.ToString());
            Console.WriteLine("points2: " + this.GetPoints2());
            Console.WriteLine("plane2: " + this.reflectionPlane2.ToString());
            Console.WriteLine("signficant2: " + this.significantRowCol2.ToString());

            Console.WriteLine();

        }

        public long GetPoints() {

            long value = 0;

            switch (this.reflectionPlane) {

                case reflectionPlane.unknown:
                    
                    value = 0;
                    break;

                case reflectionPlane.horiztonal:

                    // this should be the row id to the bottom of the reflection plane.
                    value = (100 * this.significantRowCol);
                    break;

                case reflectionPlane.vertical:

                    // this should be the column id to the right of the reflection plane.
                    value = this.significantRowCol;
                    break;
            }


            return value;
        }

        public long GetPoints2() {

            long value = 0;

            switch (this.reflectionPlane2) {

                case reflectionPlane.unknown:

                    throw new Exception();
                    value = 0;
                    break;

                case reflectionPlane.horiztonal:

                    // this should be the row id to the bottom of the reflection plane.
                    value = (100 * this.significantRowCol2);
                    break;

                case reflectionPlane.vertical:

                    // this should be the column id to the right of the reflection plane.
                    value = this.significantRowCol2;
                    break;
            }

            return value;
        }

    }
}
