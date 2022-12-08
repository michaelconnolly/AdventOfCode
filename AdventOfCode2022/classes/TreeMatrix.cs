using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2022 {

    internal class TreeMatrix {

        int[,] treeMatrix;
        bool[,] visible;
        int[,] scenicScore;
        int width;
        int height;

        public TreeMatrix(string[] lines) {

            this.width = lines[0].Length; ;
            this.height = lines.Length;
            this.treeMatrix = new int[width, height];
            this.visible = new bool[width, height];
            this.scenicScore = new int[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    char c = lines[y][x];

                    treeMatrix[x, y] = Convert.ToInt32(Char.GetNumericValue(c));
                }
            }

            this.CheckVisibility();
        }

        public int CheckScenicScore() {

            int bestScore = 0;
            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    int score = this.CheckScenicSore(row, col);
                    this.scenicScore[col, row] = score;
                    if (score > bestScore) bestScore = score;
                }
            }

            return bestScore;
        }

        private int CheckScenicSore(int row, int col) {

            // borders are always non-scenic, because one of the values will be zero.
            if (row == 0 || col == 0 || row == (width - 1) || col == (height - 1)) {
                return 0;
            }

            int currentHeight = this.treeMatrix[col, row];
            int topCount = 0;
            int bottomCount = 0;
            int leftCount = 0;
            int rightCount = 0;

            // Check up.
            for (int y = (row - 1); y >= 0; y--) {
                topCount++;
                if (this.treeMatrix[col, y] >= currentHeight) {
                    break;
                }
            }
  
            // Check down.
            for (int y = (row + 1); (y <= this.height - 1); y++) {
                bottomCount++;
                if (this.treeMatrix[col, y] >= currentHeight) {
                    break;
                }
            }

            // Check left.
            for (int x = (col - 1); x >= 0; x--) {
                leftCount++;
                if (this.treeMatrix[x, row] >= currentHeight) {
                    break;
                }
            }

            // Check right.
            for (int x = (col + 1); (x <= this.width - 1); x++) {
                rightCount++;
                if (this.treeMatrix[x, row] >= currentHeight) {
                    break;
                }
            }

            return (topCount * bottomCount * leftCount * rightCount);
        }

        private void CheckVisibility() {

            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    this.visible[col, row] = this.CheckVisibility(row, col);
                }
            }
        }

        private bool CheckVisibility(int row, int col) {

            // borders are always visible.
            if (row == 0 || col == 0 || row == (width - 1) || col == (height - 1)) {
                return true;
            }

            int currentHeight = this.treeMatrix[col, row];

            // Check up.
            bool visible = true;
            for (int y = (row - 1); y >= 0; y--) {
                if (this.treeMatrix[col, y] >= currentHeight) {
                    visible = false;
                    break;
                }
            }
            if (visible) {return true;}
         
            // Check down.
            visible = true;
            for (int y = (row + 1); (y <= this.height - 1); y++) {
                if (this.treeMatrix[col, y] >= currentHeight) {
                    visible = false;
                    break;
                }
            }
            if (visible) { return true; }


            // Check left.
            visible = true;
            for (int x = (col-1); x >= 0; x--) {
                if (this.treeMatrix[x, row] >= currentHeight) {
                    visible = false;
                    break;

                }
            }
            if (visible) { return true; }

            // Check right.
            visible = true;
            for (int x = (col+1); (x <= this.width - 1); x++) {
                if (this.treeMatrix[x, row] >= currentHeight) {
                    visible = false;
                    break;
                }
            }
            if (visible) { return true; }

            return false;
        }
    
        public int VisibleCount() {

            int count = 0;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (this.visible[x, y]) count++;
                }
            }

            return count;
        }

        public void Print() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(treeMatrix[x, y]);
                }
                Console.WriteLine();
            }

        }
    }
}
