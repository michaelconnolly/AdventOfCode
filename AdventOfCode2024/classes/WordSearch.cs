using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    internal class WordSearch {

        string[] puzzle;

        public WordSearch(string[] input) {

            this.puzzle = input;
        }


        public int FindWord(string word) {

            int count = 0;

            for (int row = 0; row < puzzle.Length; row++) {
                for (int col = 0; col < puzzle[row].Length; col++) {

                    char c = puzzle[row][col];
                    if (c == word[0]) {
                        count += (this.IsFullWordHere(row, col, word));
                            //count++;
                       // }
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

            if (false) {
                // Downwards.
                bool found = true;
                for (int i = 0; i < word.Length; i++) {
                    int rowMod = i;
                    int colMod = 0;
                    if (!(this.IsCellLegal(row + rowMod, col + colMod)) ||
                        (puzzle[row + rowMod][col + colMod] != word[rowMod])) {
                        found = false;
                        break;
                    }
                }
                if (found) { count++; }

                // Upwards.
                found = true;
                for (int i = 0; i < word.Length; i++) {
                    int rowMod = -i;
                    int colMod = 0;
                    if (!(this.IsCellLegal(row + rowMod, col + colMod)) ||
                        (puzzle[row + rowMod][col + colMod] != word[i])) {
                        found = false;
                        break;
                    }
                }
                if (found) { count++; }
               }

            // Downwards;
            if (this.IsFullWordHereDirection(row, col, word, 1, 0)) { count++; }

            // Upwards;
            if (this.IsFullWordHereDirection(row, col, word, -1, 0)) { count++; }


            // Rightwards;
            if (this.IsFullWordHereDirection(row, col, word, 0, 1)) { count++; }

            // Leftwards;
            if (this.IsFullWordHereDirection(row, col, word, 0, -1)) { count++; }

            // Diagonal
            if (this.IsFullWordHereDirection(row, col, word, 1, 1)) {count++; }
            if (this.IsFullWordHereDirection(row, col, word, -1, -1)) { count++; }
            if (this.IsFullWordHereDirection(row, col, word, 1, -1)) { count++; }
            if (this.IsFullWordHereDirection(row, col, word, -1, 1)) { count++; }

            return count;
        }
    }
}
