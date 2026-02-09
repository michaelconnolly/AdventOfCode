using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {
    internal class TileFloor {

        public List<Tile> redTiles = new List<Tile>();
        public List<TilePair> tilePairs = new List<TilePair>();
        //public char[,] tileMap;
        public List<char[]> tileMap = new List<char[]>();

        public TileFloor(string[] input) {

            int largestRow = 0;
            int largestCol = 0;

            foreach (string line in input) {
                string[] parts = line.Split(',');
                int col = int.Parse(parts[0]);
                int row = int.Parse(parts[1]);
                this.redTiles.Add(new Tile(row, col));
                if (row > largestRow) largestRow = row;
                if (col > largestCol) largestCol = col;
            }

            // Map input to a 2D array of chars;
            // Need this for part two.

            // this.tileMap = new char[largestRow+1, largestCol+1];
            

            for (int r = 0; r < largestRow+1; r++) {

                char[] rowData = new char[largestCol + 1];
                for (int c = 0; c < largestCol+1; c++) {
                    //this.tileMap[r, c] = '.';
                    rowData[c] = '.';
                }
                this.tileMap.Add(rowData);
            }

            //this.PrintMap();

            foreach (Tile redTile in this.redTiles) {
                //this.tileMap[redTile.x, redTile.y] = '#';
                this.tileMap[redTile.x][redTile.y] = '#';

                //this.PrintMap();
            }



        }

        public void Print() {

            foreach (Tile tile in this.redTiles) {
                tile.Print();
            }
        }


        public void PrintMap() {

            //int rows = this.tileMap.GetLength(0);
            //int cols = this.tileMap.GetLength(1);
            //for (int r = 0; r < rows; r++) {
            //    for (int c = 0; c < cols; c++) {
            //        Console.Write(this.tileMap[r, c]);
            //    }
            //    Console.WriteLine();
            //}


            //Console.WriteLine();


            int rows = this.tileMap.Count;
            int cols = this.tileMap[0].Length;
            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    Console.Write(this.tileMap[r][ c]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public void FillInAreas() {

            //for (int row = 0; row < this.tileMap.GetLength(0); row++) {
                for (int row = 0; row < this.tileMap.Count; row++) {

                    bool inArea = false;
                int startCol = -1;
                //int endCol = -1;    

              //  for (int col = 0; col < this.tileMap.GetLength(1); col++) {
                    for (int col = 0; col < this.tileMap[0].Length; col++) {

                        if (this.tileMap[row][ col] != '.') {

                        if (inArea) {
                            //inArea = false;

                            for (int fillCol = startCol + 1; fillCol < col; fillCol++) {
                                this.tileMap[row][ fillCol] = 'X';
                            }

                            startCol = col;
                        }
                        else { // inArea == false;

                            inArea = true;
                            startCol = col;

                        }
                    }
                }
            }

        }


        public void DrawConnectingLines() {

            foreach (TilePair tilePair in this.tilePairs) {

                // Are these guys are in the same row?
                if (tilePair.tile1.x == tilePair.tile2.x) {

                    int x = tilePair.tile1.x;
                    int startY = (Math.Min(tilePair.tile1.y, tilePair.tile2.y));
                    int endY = (Math.Max(tilePair.tile1.y, tilePair.tile2.y));

                    for (int y = startY + 1; y < endY; y++) {
                        if (this.tileMap[x][ y] == '.') {
                            this.tileMap[x][ y] = 'X';
                        }
                        else {
                            break;
                        }
                    }
                }

                // Are these guys in the same column?
                if (tilePair.tile1.y == tilePair.tile2.y) {

                    int y = tilePair.tile1.y;
                    int startX = (Math.Min(tilePair.tile1.x, tilePair.tile2.x));
                    int endX = (Math.Max(tilePair.tile1.x, tilePair.tile2.x));

                    for (int x = startX + 1; x < endX; x++) {
                        if (this.tileMap[x][ y] == '.') {
                            this.tileMap[x][ y] = 'X';
                        }
                        else {
                            break;
                        }
                    }
                }
            }

        }


        public long LargestArea() {

            long largestArea = 0;
            for (int i = 0; i < this.redTiles.Count; i++) {
                for (int j = i + 1; j < this.redTiles.Count; j++) {
                    TilePair tilePair = new TilePair(this.redTiles[i], this.redTiles[j]);
                    this.tilePairs.Add(tilePair);
                    long area = tilePair.Area();
                    if (area > largestArea) {
                        largestArea = area;
                    }
                }
            }
            return largestArea;

        }

        public long LargestAreaWithinContiguousSpace() {

            long largestArea = 0;
            foreach(TilePair tilePair in this.tilePairs) {
                if (tilePair.WithinContiguousArea(this.tileMap)) {
                    long area = tilePair.Area();
                    if (area > largestArea) {
                        largestArea = area;
                    }
                }
            }
           
            return largestArea;

        }
    }
}
