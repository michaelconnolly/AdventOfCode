using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {


    internal class PaperForkliftMap {

        char[,] map;
        //char[,] map2;
        int accessiblePaperCount = 0;
        int accessiblePaperCount2 = 0;


        public PaperForkliftMap(string[] input) {

            // Create internal tables.
            this.map = new char[input.Length, input[0].Length];
            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {
                    this.map[row, col] = input[row][col];
                }
            }
     

            // Question One.
            this.Print();
            this.MarkAccessiblePaper();
            this.Print();

            // Question two;
            // reset the map values.
            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {
                    this.map[row, col] = input[row][col];
                }
            }

            this.MarkAccessiblePaper2();
           // this.Print();

        }


        public void MarkAccessiblePaper2() {

            int foundCount = -1;

            while (foundCount != 0) {

                foundCount = 0;


                for (int row = 0; row < this.map.GetLength(0); row++) {
                    for (int col = 0; col < this.map.GetLength(1); col++) {

                        if (this.IsPaperAccessible(row, col)) {
                            this.map[row, col] = 'X';
                            this.accessiblePaperCount2++;
                            foundCount++;
                        }

                    }
                }

                // reset the X's to .'s.
                for (int row = 0; row < this.map.GetLength(0); row++) {
                    for (int col = 0; col < this.map.GetLength(1); col++) {
                        if (this.map[row, col] == 'X') {
                            this.map[row, col] = '.';
                        }
                    }
                }

               // this.Print();

            }
        }



        public void MarkAccessiblePaper() {

            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {
                    
                    if (this.IsPaperAccessible(row, col)) {
                        this.map[row,col] = 'X';
                        this.accessiblePaperCount++;
                    }
                  
                }
            }
        }


        private bool CellHasPaper(int row, int col) {

            if (row < 0 || col < 0 || row >= this.map.GetLength(0) || col >= this.map.GetLength(1)) {
                return false;
            }

            return (this.map[row, col] != '.');
        }

        private bool IsPaperAccessible(int row, int col) {

            if (this.map[row, col] == '.') return false;

            int countOfAdjacentPaper = 0;

            if (this.CellHasPaper(row - 1, col - 1)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row - 1, col)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row - 1, col + 1)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row, col - 1)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row, col + 1)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row + 1, col - 1)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row + 1, col)) countOfAdjacentPaper++;
            if (this.CellHasPaper(row + 1, col + 1)) countOfAdjacentPaper++;

            return (countOfAdjacentPaper < 4) ;

            //if (row != 0) { 
                
            //    if (col!=0) {
            //        if (this.map[row - 1, col - 1] != '.') countOfAdjacentPaper++;
            //    }
            //    if (this.map[row - 1, col] != '.') countOfAdjacentPaper++;
                
            //    if (col != this.map.GetLength(1)-1) {
            //        if (this.map[row - 1, col + 1] != '.') countOfAdjacentPaper++;
            //    }
            //}
        }

        public int QuestionOne() {
            return this.accessiblePaperCount;
        }

        public int QuestionTwo() {
            return this.accessiblePaperCount2;
        }

        void Print() {

            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {
                    Console.Write(this.map[row, col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
