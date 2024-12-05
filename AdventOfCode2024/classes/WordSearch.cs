using System;


namespace AdventOfCode2024 {

    internal class WordSearch {

        string[] puzzle;

        public WordSearch(string[] input) {

            this.puzzle = input;
        }

        public int FindX() {

            int count = 0;
            char charToFind = 'A';

            for (int row = 0; row < puzzle.Length; row++) {
                for (int col = 0; col < puzzle[row].Length; col++) {

                    char c = puzzle[row][col];
                    if (c == charToFind) {
                        count += (this.IsFullXHere(row, col));
                    }
                }
            }

            return count;
        }


        public int FindWord(string word) {

            int count = 0;

            for (int row = 0; row < puzzle.Length; row++) {
                for (int col = 0; col < puzzle[row].Length; col++) {

                    char c = puzzle[row][col];
                    if (c == word[0]) {
                        count += (this.IsFullWordHere(row, col, word));
                    }
                }
            }

            return count;
        }


        public bool IsCellLegal(int row, int col) {

            if (row >= 0 && (row < this.puzzle.Length) && col >= 0 && (col < this.puzzle[0].Length)) {
                return true;
            }

            return false;
        }

        private bool IsFullWordHereDirection(int row, int col, string word, int _rowMod, int _colMod) {

            bool found = true;
            for (int i = 0; i < word.Length; i++) {
                int rowMod = _rowMod * i;
                int colMod = _colMod * i;
                if (!(this.IsCellLegal(row + rowMod, col + colMod)) ||
                    (puzzle[row + rowMod][col + colMod] != word[i])) {
                    found = false;
                    break;
                }
            }
            return found;
        }

        public int IsFullWordHere(int row, int col, string word) {

            int count = 0;

            // Downwards;
            if (this.IsFullWordHereDirection(row, col, word, 1, 0)) { count++; }

            // Upwards;
            if (this.IsFullWordHereDirection(row, col, word, -1, 0)) { count++; }

            // Rightwards;
            if (this.IsFullWordHereDirection(row, col, word, 0, 1)) { count++; }

            // Leftwards;
            if (this.IsFullWordHereDirection(row, col, word, 0, -1)) { count++; }

            // Diagonal
            if (this.IsFullWordHereDirection(row, col, word, 1, 1)) { count++; }
            if (this.IsFullWordHereDirection(row, col, word, -1, -1)) { count++; }
            if (this.IsFullWordHereDirection(row, col, word, 1, -1)) { count++; }
            if (this.IsFullWordHereDirection(row, col, word, -1, 1)) { count++; }

            return count;
        }


        public int IsFullXHere(int row, int col) {

            //int count = 0;

            // Assumptions:
            // i have been handed a cell that we found the middle char, 'A'.
            // Figure out if the left-to-right diagonal is MAS or SAM
            // Figure out if the right-to-left diagonal is MAS or SAM

            if (this.puzzle[row][col] != 'A') { return 0; }

            // If we are not at least one row in and one column in from edge, bail.
            if (col < 1 || col > this.puzzle[0].Length - 2) { return 0; }
            if (row < 1 || row > this.puzzle.Length - 2) { return 0; }

            bool passedLeft = false;
            if (((this.puzzle[row - 1][col - 1] == 'M') &&
                (this.puzzle[row + 1][col + 1] == 'S')) ||
                ((this.puzzle[row - 1][col - 1] == 'S') &&
                (this.puzzle[row + 1][col + 1] == 'M'))) {
                passedLeft = true;
            }

            bool passedRight = false;
            if (((this.puzzle[row - 1][col + 1] == 'M') &&
                (this.puzzle[row + 1][col - 1] == 'S')) ||
                ((this.puzzle[row - 1][col + 1] == 'S') &&
                (this.puzzle[row + 1][col - 1] == 'M'))) {
                passedRight = true;
            }

            return (passedLeft && passedRight ? 1 : 0);
        }
    }
}