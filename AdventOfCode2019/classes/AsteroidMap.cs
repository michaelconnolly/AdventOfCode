using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {


    public class AsteroidMap {

        // Constants.
        char ASTEROID = '#';
        public static char EMPTY = '.';
        public static char VISIBLE_ASTEROID = 'O';
        char INVISIBLE_ASTEROID = 'X';
        public int mapWidth;
        public int mapHeight;
        public Asteroid station;
        string[] rows;

        public AsteroidMap(string[] rows, Asteroid station) {

            this.rows = rows;
            this.mapHeight = this.rows.Length;
            this.mapWidth = this.rows[0].Length;
            this.station = station;
        }

        public void DestroyVisibileAndReset() {

            for (int row = 0; row < this.mapHeight; row++) {
                for (int col = 0; col < this.mapWidth; col++) {

                    char c = this.GetCell(row, col);
                    //if (this.GetCell(row, col) == c) count++;
                    if (c == VISIBLE_ASTEROID) {
                        this.SetCellEmpty(row, col);
                    }
                    else if (c == INVISIBLE_ASTEROID) {
                        this.SetCell(row, col, ASTEROID);
                    }
                }
            }
        }

        public void PrintOut() {

            Console.WriteLine("Asteroid Map:");
            foreach (string row in this.rows) {
                Console.WriteLine("\t" + row);
            }
        }

        public char GetCell(int row, int col) {
            return this.rows[row].ToCharArray()[col];
        }

        void SetCell(int row, int col, char value) {

            char[] chars = this.rows[row].ToCharArray();
            chars[col] = value;
            string output = "";
            foreach (char c in chars) {
                output += c;
            }
            //this.rows[row] = chars.
            this.rows[row] = output;
        }

        //void Reduce(int x, int y, out int xOut, out int yOut) {


        //}

        void SetCellVisible(int asteroidRow, int asteroidCol, int stationRow, int stationCol) {

            // Mark the current asteroid as visible.
            this.SetCell(asteroidRow, asteroidCol, VISIBLE_ASTEROID);

            int rowDiff = asteroidRow - stationRow;
            int colDiff = asteroidCol - stationCol;
            int reducer = Program.gcd(rowDiff, colDiff);
            rowDiff = rowDiff / reducer;
            colDiff = colDiff / reducer;

            //int newRowDiff = rowDiff;
            //int newColDiff = colDiff;

            //// Reduce the ratio to something even and base 1.  For example, 3/3 needs to be reduced to 1/1.
            //bool modifyNegative = false;
            //if (colDiff != 0 && rowDiff != 0) {
            //    if (colDiff != 0 && (rowDiff % colDiff == 0)) {
            //        if (rowDiff < 0 && colDiff < 0) modifyNegative = true;
            //        newRowDiff = rowDiff / colDiff;
            //        newColDiff = colDiff / colDiff;
            //        if (modifyNegative) { newRowDiff = -newRowDiff; newColDiff = -newColDiff; modifyNegative = false; }
            //    }
            //    else if (rowDiff != 0 && (colDiff % rowDiff == 0)) {
            //        if (rowDiff < 0 && colDiff < 0) modifyNegative = true;
            //        newRowDiff = rowDiff / rowDiff;
            //        newColDiff = colDiff / rowDiff;
            //        if (modifyNegative) { newRowDiff = -newRowDiff; newColDiff = -newColDiff; modifyNegative = false; }
            //    }
            //}
            //rowDiff = newRowDiff;
            //colDiff = newColDiff;

            // Make anything directly behind items on the same row invisible.
            if (rowDiff == 0) {
                if (colDiff < 0) {
                    for (int i = 0; i < stationCol; i++) {
                        if (this.GetCell(stationRow, i) == ASTEROID) {
                            this.SetCellInvisible(stationRow, i);
                        }
                    }
                }
                else {
                    for (int i = (stationCol + 1); i < this.mapWidth; i++) {
                        if (this.GetCell(stationRow, i) == ASTEROID) {
                            this.SetCellInvisible(stationRow, i);
                        }
                    }
                }
            }

            // Make anything directly behind items on the same column invisible.
            else if (colDiff == 0) {
                if (rowDiff < 0) {
                    for (int i = 0; i < stationRow; i++) {
                        if (this.GetCell(i, stationCol) == ASTEROID) {
                            this.SetCellInvisible(i, stationCol);
                        }
                    }
                }
                else {
                    for (int i = (stationRow + 1); i < this.mapHeight; i++) {
                        if (this.GetCell(i, stationCol) == ASTEROID) {
                            this.SetCellInvisible(i, stationCol);
                        }
                    }
                }
            }

            else { // if (rowDiff < 0) {
                int col = asteroidCol;
                for (int row = (asteroidRow + rowDiff); (row < this.mapHeight && row >= 0 && rowDiff != 0); row += rowDiff) {
                    col += colDiff;
                    if ((col < this.mapWidth && col >=0 ) && (this.GetCell(row, col) == ASTEROID)) {
                        //for (int col = (asteroidCol + colDiff); (col < this.mapHeight && col >= 0 && colDiff != 0); col += colDiff) {
                        //(this.GetCell(row, col) == ASTEROID) {
                        this.SetCellInvisible(row, col);
                    }

                    //}
                }
            }
        }


        public int AsteroidCount() {

            return this.CountOf(ASTEROID);
        }

        public int VisibileAsteroidCount() {

            return this.CountOf(VISIBLE_ASTEROID);
        }

        private int CountOf(char c) {

            int count = 0;

            for (int row = 0; row < this.mapHeight; row++) {
                for (int col = 0; col < this.mapWidth; col++) {
                    if (this.GetCell(row, col) == c) count++;
                }
            }

            return count;
        }

        public void BestLocationForStation(out int outputRow, out int outputCol) {

            //AsteroidMap map = new AsteroidMap((string[])this.rows.Clone());
            int bestCountOfVisibileAsteroids = int.MinValue;
            int bestRow = int.MinValue;
            int bestCol = int.MaxValue;

            for (int row = 0; row < this.mapHeight; row++) {
                for (int col = 0; col < this.mapWidth; col++) {

                    if (this.GetCell(row, col) == ASTEROID) {
                        AsteroidMap visibilityMap = this.VisibilityMap(row, col);

                        int visibilityCount = visibilityMap.VisibileAsteroidCount();
                        // map.SetCell(row, col, visibilityCount.ToString());

                        if (visibilityCount > bestCountOfVisibileAsteroids) {
                            bestCountOfVisibileAsteroids = visibilityCount;
                            bestRow = row;
                            bestCol = col;
                        }
                    }
                }
            }

            Console.WriteLine("Best visibility: " + bestCountOfVisibileAsteroids + " asteroids if station is at " + bestCol + "," + bestRow);
            outputRow = bestRow;
            outputCol = bestCol;
            return;
        }

        public void SetCellInvisible(int asteroidRow, int asteroidCol) {

            this.SetCell(asteroidRow, asteroidCol, INVISIBLE_ASTEROID);
        }

        public void SetCellEmpty(int asteroidRow, int asteroidCol) {

            this.SetCell(asteroidRow, asteroidCol, EMPTY);
        }

        //public void ListOfVisibleAsteroids(out List<Asteroid> asteroids12, out List<Asteroid> asteroids12to6, 
          //  out List<Asteroid> asteroids6, out List<Asteroid> asteroids6to12) {
            public List<Asteroid> ListOfVisibleAsteroids() {

            List<Asteroid> asteroids = new List<Asteroid>();

                List<Asteroid> asteroids12 = new List<Asteroid>();
            List<Asteroid> asteroids12to6 = new List<Asteroid>();
            List<Asteroid> asteroids6 = new List<Asteroid>();
            List<Asteroid> asteroids6to12 = new List<Asteroid>();
            //asteroids12 = new List<Asteroid>();
            //asteroids12to6 = new List<Asteroid>();
            //asteroids6 = new List<Asteroid>();
            //asteroids6to12 = new List<Asteroid>();

            for (int row=0; row<this.mapHeight; row++) {
                for (int col=0; col<this.mapWidth; col++) {
                    if (this.GetCell(row, col) == VISIBLE_ASTEROID) {

                        Asteroid asteroid = new Asteroid(row, col, this);

                        if (asteroid.IsAt12()) {
                            asteroids12.Add(asteroid);
                        }
                        else if (asteroid.IsBetween12and6()) {
                            asteroids12to6.Add(asteroid);
                        }
                        else if (asteroid.IsAt6()) {
                            asteroids6.Add(asteroid);
                        }
                        else if (asteroid.IsBetween6and12()) {
                            asteroids6to12.Add(asteroid);
                        }
                        else {
                            Console.WriteLine("ERROR: Asteroid is uncharted territory!!!!");
                        }

                        //asteroids.Add(new Asteroid(row, col, this));
                    }
                }
            }

            // Make sure these are sorted!
            asteroids12to6 = SortAsteroidList(asteroids12to6);
            asteroids6to12 = SortAsteroidList(asteroids6to12);

            // Take our four lists and combine to one.
            asteroids.AddRange(asteroids12);
            asteroids.AddRange(asteroids12to6);
            asteroids.AddRange(asteroids6);
            asteroids.AddRange(asteroids6to12);

            return asteroids;
        }

        private List<Asteroid> SortAsteroidList(List<Asteroid> asteroidsInput) {

            List<Asteroid> asteroidsOutput = new List<Asteroid>();

            while (asteroidsInput.Count > 0) {


                double lowestValue = int.MaxValue;
                Asteroid asteroidLowest = null;


                foreach (Asteroid asteroid in asteroidsInput) {
                    double? slope = asteroid.slope;
                    if (slope == null) {
                        Console.WriteLine("ERROR! Unexpected NULL for asteroid.slope in SortAsteroidList()");
                    }
                    else  {
                        if (slope < lowestValue) {
                            lowestValue = (double) slope;
                            asteroidLowest = asteroid;
                        }
                    }
                }

                asteroidsOutput.Add(asteroidLowest);
                bool successfullyRemove = asteroidsInput.Remove(asteroidLowest);
                //asteroidsInput.r
            }

            return asteroidsOutput;
        }


        public AsteroidMap VisibilityMap(int stationRow, int stationCol) {

            // Create a map of what other asteroids are visible if i put a station on the inputted asteroid.
            Asteroid station = new Asteroid(stationRow, stationCol, null);
            AsteroidMap map = new AsteroidMap((string[])this.rows.Clone(), station);
            station.map = map;

            // If there is no asteroid in the inputted area, return null;
            if (map.GetCell(stationRow, stationCol) != ASTEROID) {
                Console.WriteLine("ERROR! You asked for a visibility map for a location that has no asteroid.");
                return null;
            }

            // Logic:
            // Look directly to the left of me, find the first asteroid, and consider everything else blocked.
            // Look directly to the right of me, find the first asteroid, and consider everything else blocked.
            // Look at the entire row above me.  Find asteroids, and block everything behind it.
            // And then move up a row.
            // Look at the entire row below me.  Find any asteroids, and block everything behind it.


            for (int columnId = (stationCol - 1); columnId >= 0; columnId--) {
                int rowId = stationRow;
                if (map.GetCell(rowId, columnId) == ASTEROID) {
                    map.SetCellVisible(rowId, columnId, stationRow, stationCol);
                    // break;
                }
            }

            for (int columnId = (stationCol + 1); columnId < this.mapWidth; columnId++) {
                int rowId = stationRow;
                if (map.GetCell(rowId, columnId) == ASTEROID) {
                    map.SetCellVisible(rowId, columnId, stationRow, stationCol);
                    //break;
                }
            }

            for (int rowId = (stationRow - 1); rowId >= 0; rowId--) {
                for (int colId = 0; colId < this.mapWidth; colId++) {
                    if (map.GetCell(rowId, colId) == ASTEROID) {
                        map.SetCellVisible(rowId, colId, stationRow, stationCol);
                        //break;
                    }
                }
            }

            for (int rowId = (stationRow + 1); rowId < this.mapHeight; rowId++) {
                for (int colId = 0; colId < this.mapWidth; colId++) {
                    if (map.GetCell(rowId, colId) == ASTEROID) {
                        map.SetCellVisible(rowId, colId, stationRow, stationCol);
                        //break;
                    }
                }
            }

            return map;
        }


        public void SpitOutVisibilityMapInfo(int row, int col) {

            AsteroidMap visibilityMap = this.VisibilityMap(row, col);
            if (visibilityMap == null) {
                Console.WriteLine("No asteroid at " + col + "," + row + "!");
                return;
            }

            visibilityMap.PrintOut();
            Console.WriteLine("Total asteroids: " + this.AsteroidCount());
            Console.WriteLine("Total visible asteroids at " + col + "," + row + ": " + visibilityMap.VisibileAsteroidCount());
        }
    }
}
