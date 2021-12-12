using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021 {

    public class BingoCard {

        private string[,] cells;
        private bool[,] selected;
        private int width;
        private int height;
        private int score;
        public bool won = false;


        public int id { get; set; }


        public BingoCard(int id, string[,] cells) {

            this.id = id;
            this.cells = cells;
            this.width = cells.GetLength(1);
            this.height = cells.GetLength(0);

            this.selected = new bool[height, width];
        }


        public void print() {

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Console.Write(cells[i, j]);
                    Console.Write(" ");
                }
                Console.Write("    ");
                for (int j = 0; j < height; j++) {
                    Console.Write(selected[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public bool callNumber(string calledNumber) {

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (cells[i, j] == calledNumber) {
                        selected[i, j] = true;
                    }
                }
            }

            if (justHitBingo()) {
                this.setScore(calledNumber);
                return true;
            }

            return false;
          
        }


        public int getScore() { return this.score; }


        public void setScore(string calledNumber) {

            // The score of the winning board can now be calculated. Start by finding the sum 
            // of all unmarked numbers on that board; in this case, the sum is 188. Then, 
            // multiply that sum by the number that was just called when the board 
            // won, 24, to get the final score, 188 * 24 = 4512.

            int sumOfUnmarked = 0;

            for (int i=0; i < height; i++) {
                for (int j=0; j < width; j++) {
                    if (!(selected[i,j])) {
                        sumOfUnmarked += Convert.ToInt32(cells[i, j]);
                    }
                }
            }

            this.score = sumOfUnmarked * (Convert.ToInt32(calledNumber));
            this.won = true;
        }

        public bool justHitBingo() {
           
            bool bingo;

            // Check each row.
            for (int i = 0; i < height; i++) {
                bingo = true; 
                for (int j = 0; j < width; j++) {
                    if (!(selected[i, j])) {
                        bingo = false;
                        break;
                    }
                }
                if (bingo) return bingo;
            }

            // Check each column.
            for (int i = 0; i < width; i++) {
                bingo = true;
                for (int j = 0; j < height; j++) {
                    if (!(selected[j,i])) {
                        bingo = false;
                        break;
                    }
                }
                if (bingo) return bingo;
            }

            //// Check diagonal: NW to SE.
            //bingo = true;
            //for (int i=0; i<width; i++) {
            //    if (!(selected[i,i])) {
            //        bingo = false;
            //        break;
            //    }
            //}
            //if (bingo) return bingo;

            //// Check diagonal: SW to NE.
            //bingo = true;
            //for (int i = 0; i < width; i++) {
            //    if (!(selected[i, (width - i - 1)])) {
            //        bingo = false;
            //        break;
            //    }
            //}
            //if (bingo) return bingo;

            return false;
        }
    }
 }
