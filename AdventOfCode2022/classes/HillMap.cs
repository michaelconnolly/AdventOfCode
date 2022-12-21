using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode2022 {

    public class HillMap {

        string[] hillMap;
        int width;
        int height;
        public HillCoordinate start;
        HillCoordinate end;
        //int[,] preCalcBestPath;

        public HillMap(string[] lines) {

            this.hillMap = lines;
            this.width = this.hillMap[0].Length;
            this.height = this.hillMap.Length;
            this.start = this.FindOnMap('S');
            this.end = this.FindOnMap('E');

            //    this.preCalcBestPath = new int[this.height, this.width];
            //    for (int row = 0; row < height; row++) {
            //        for (int col = 0; col < width; col++) {
            //            this.preCalcBestPath[row, col] = -1;
            //        }
            //    }
            //}
        }

        public int[,] CreatePreCalcBestPath(int[,] copyMe) {

            int[,] preCalcBestPath = new int[this.height, this.width];
         
            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {

                    if (copyMe == null) preCalcBestPath[row, col] = -1;
                    else preCalcBestPath[row, col] = copyMe[row, col];
                }
            }

            return preCalcBestPath;
        }

        private HillCoordinate FindOnMap(char c) {

            for (int col = 0; col < width; col++) {
                for (int row = 0; row < height; row++) {
                    if (c == this.hillMap[row][col]) {
                        return new HillCoordinate(row, col);
                    }
                }
            }

            throw new Exception();
        }

        public char[,] CreateHistoryMap() {

            char[,] historyMap = new char[height, width];
            for (int col = 0; col < width; col++) {
                for (int row = 0; row < height; row++) {
                    historyMap[row, col] = '.';
                }
            }

            return historyMap;
        }

        public char[,] CreateHistoryMap(char[,] startMap) {

            char[,] historyMap = new char[height, width];
            for (int col = 0; col < width; col++) {
                for (int row = 0; row < height; row++) {
                    historyMap[row, col] = startMap[row,col];
                }
            }

            return historyMap;
        }

        public void markHistoryMap(char[,]historyMap, HillCoordinate location, int stepsSoFar) {

            char c = (stepsSoFar % 10).ToString()[0];
            historyMap[location.row, location.col] = c;
        }

        public void Print() {

            Console.WriteLine("height: " + this.height + ", width: " + this.width + ", start loc: " +
                this.start.Print() + " , end loc: " + this.end.Print());
        }

        public void PrintHistoryMap(char[,] historyMap) {

            for (int row = 0; row < this.height; row++) {
                for (int col = 0; col < this.width; col++) {
                    Console.Write(historyMap[row, col]);
                    //if (historyMap[row, col]) Console.Write("X");
                    //else Console.Write(".");


                }
                Console.WriteLine("");
      
            }

            Console.WriteLine("");

        }

        private int GetValue(HillCoordinate location) {

            char currentChar = this.hillMap[location.row][ location.col];

            if (currentChar == 'S') return 0;
            if (currentChar == 'E') return ((int)'z') - 96;

            return ((int)currentChar) - 96;
        }

        public bool IsLegalMove(HillCoordinate location, HillCoordinate nextLocation, char[,] historyMap) {

            // if going outside the bounds of the map, return false;
            if (!(IsOnTheBoard(nextLocation))) return false;

            // Have we already been here?
            if (historyMap[nextLocation.row, nextLocation.col] != '.') return false;

            int currentValue = this.GetValue(location);
            int nextValue = this.GetValue(nextLocation);

            if (nextValue == 999) return true;

            if (nextValue < currentValue) return true;

            if (nextValue <= currentValue + 1) return true;

            return false;
        }

        public bool IsOnTheBoard(HillCoordinate location) {

            // if going outside the bounds of the map, return false;
            if (location.row < 0 || location.row >= height || location.col < 0 || location.col >= width) return false;

            return true;
        }


        private int FindShortestPathOneDirection(HillCoordinate startLocation, HillCoordinate nextLocation, 
            char[,] historyMap, int stepsSoFar, int[,]preCalcBestPath) {


            // bail if this is an illegal move.
            if (!(this.IsLegalMove(startLocation, nextLocation, historyMap))) return int.MaxValue; // previousBest;

            char[,] newHistoryMap = this.CreateHistoryMap(historyMap);
            //int[,] newPreCalcBestPath = this.CreatePreCalcBestPath(preCalcBestPath);


            int shortestPath = this.FindShortestPath(stepsSoFar + 1, nextLocation,
                newHistoryMap, preCalcBestPath); // newPreCalcBestPath);
   
            return shortestPath;
        }

        public int FindShortestPath(int stepsSoFar, HillCoordinate location, char[,] historyMap, int[,] preCalcBestPath) {

            // Did we find a cached value from a previous run?
            // if it's int.MaxValue, that is our hint that there is no way to go from here.
            int cachedValue = preCalcBestPath[location.row, location.col];
            if (cachedValue == int.MaxValue) {
                return int.MaxValue; // previousBest;
            }
            else if (cachedValue != -1) {
                return( stepsSoFar + cachedValue);
                //if (fullDistance < previousBest) return (stepsSoFar + cachedValue);
                //else return previousBest;
            }

            // Did we find it? 
            if (this.IsOnTheBoard(location) && this.hillMap[location.row][location.col] == 'E') {
               // if (previousBest < (stepsSoFar )) return previousBest;
                this.PrintHistoryMap(historyMap);
                return stepsSoFar;
            }
          
            this.markHistoryMap(historyMap, location, stepsSoFar);
            //bool[,] newHistoryMap = this.CreateHistoryMap(historyMap);

            // up.
            int up = this.FindShortestPathOneDirection(location, new HillCoordinate(location.row - 1, location.col),
                historyMap, stepsSoFar, preCalcBestPath);
            // down
            int down = this.FindShortestPathOneDirection(location, new HillCoordinate(location.row + 1, location.col),
              historyMap, stepsSoFar, preCalcBestPath);
            // left
            int left = this.FindShortestPathOneDirection(location, new HillCoordinate(location.row, location.col - 1),
              historyMap, stepsSoFar, preCalcBestPath);
            // right
            int right = this.FindShortestPathOneDirection(location, new HillCoordinate(location.row, location.col + 1),
              historyMap, stepsSoFar, preCalcBestPath);


            int newBest1 = (up < down ? up : down);
            int newBest2 = (left < right ? left : right);
            int newBest = (newBest1 < newBest2 ? newBest1 : newBest2);
         
            // Cache a value right here.
            if (newBest != int.MaxValue) {
                if (newBest > 100000) { int x = 5; }
                int stepsFromHere = (newBest - stepsSoFar);

                preCalcBestPath[location.row, location.col] = stepsFromHere;
            }
            else {
                preCalcBestPath[location.row, location.col] = int.MaxValue;
            }

            return newBest;
        }
    }
}
