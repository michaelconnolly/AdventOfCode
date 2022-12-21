using System;


namespace AdventOfCode2022 {

    internal class SandManager {

        char[,] map = new char[1000, 1000];
        int lowestRow = 0;
        int highestRow = 0;
        int lowestColumn = 500;
        int highestColumn = 500;
        int totalSandPoured = 0;
        bool isSecondQuestion;

        public SandManager(string[] input, bool isQuestionTwo=false) {

            this.isSecondQuestion = isQuestionTwo;
            this.ProcessInput(input);
        }
        

        private void ProcessInput(string[] input) {
            
            // Initialize map.
            for (int col=0; col<1000; col++) {
                for (int row=0; row<1000; row++) {
                    this.map[col, row] = '.';
                }
            }
            this.map[500, 0] = '+';

            // example
            // 498,4 -> 498,6 -> 496,6

            foreach (string line in input) {

                //Console.WriteLine(line);
                string[] coordinates = line.Split(" -> ");
                SandCoordinate start = new SandCoordinate(coordinates[0], this.map);
                CheckCoordinate(start);

                for (int i=1; i<(coordinates.Length); i++) {
                
                    SandCoordinate end = new SandCoordinate(coordinates[i], this.map);
                    CheckCoordinate(end);

                    // Is it a horizontal line?
                    if (start.row == end.row) {

                        int startCol = Math.Min(start.col, end.col);
                        int endCol = Math.Max(start.col, end.col);

                        for (int col = startCol; col <= endCol; col++) {
                            this.map[col, start.row] = '#';
                        }
                    }
                    else if (start.col == end.col) {

                        int startRow = Math.Min(start.row, end.row);
                        int endRow = Math.Max(start.row, end.row);

                        for (int row = startRow; row <= endRow; row++) {
                            this.map[start.col, row] = '#';
                        }
                    }
                    else throw new Exception();

                    //this.Print();
                    start = end;
                }
            }

            // Special rules for second question.
            if (this.isSecondQuestion) {
                this.highestRow = this.highestRow + 2;
                for (int col = 0; col < 1000; col++) {
                    this.map[col, this.highestRow] = '#';
                }
            }
        }

        private void CheckCoordinate(SandCoordinate coord) {

            if (coord.row > this.highestRow) this.highestRow = coord.row;
            if (coord.row < this.lowestRow) this.lowestRow = coord.row;
            if (coord.col > this.highestColumn) this.highestColumn = coord.col;
            if (coord.col < this.lowestColumn) this.lowestColumn = coord.col;
        }


        public void Print() {

            for (int row = this.lowestRow; row <= this.highestRow; row++) {

                for (int col = this.lowestColumn; col <= this.highestColumn; col++) {
                    Console.Write(this.map[col, row]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int initiateSandPouring() {

            this.totalSandPoured = 0;
   
            while (true) {

                // create a sand pixel.
                SandCoordinate sand = new SandCoordinate("500,0", this.map);
                
                if (sand.Fell()) {
                    this.totalSandPoured++;
                    this.map[sand.col, sand.row] = 'o';
                    //this.Print();
                }
                else {

                    // if it is stuck at the faucet, do that, otherwise it fell into abyss.
                    if (sand.row == 0 && sand.col == 500) {
                        this.totalSandPoured++;
                        this.map[sand.col, sand.row] = 'o';
                    }

                    break;
                }
            }

            return this.totalSandPoured;
        }
    }
}
