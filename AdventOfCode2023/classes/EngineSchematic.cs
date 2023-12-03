using System;
using System.Collections.ObjectModel;


namespace AdventOfCode2023 {

    internal class EngineSchematic {

        char[,] map;
        int rowCount;
        int colCount;
        Collection<EngineGear> gears = new Collection<EngineGear>();
        Collection<int> partNumbers;


        public EngineSchematic(string[] lines) {

            rowCount = lines.Length;
            colCount = lines[0].Length;
            map = new char[rowCount, colCount];

            // import data into our internal data model.
            for (int i = 0; i < rowCount; i++) {
                char[] characters = lines[i].ToCharArray();
                for (int j = 0; j < colCount; j++) {
                    map[i, j] = characters[j];
                }
            }

            // pre-calculate where the gears are.
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < colCount; j++) {
                    if (map[i, j] == '*')
                        this.gears.Add(new EngineGear(i, j));

                }
            }

            // calculate where the partnumbers are.
            this.partNumbers = FindPartNumbers();

            // remove any gears that don't have exactly two neighbors.
            Collection<EngineGear> engineGearsCopy = new Collection<EngineGear>();
            foreach (EngineGear gear in this.gears) {
                if (gear.neighbors.Count == 2) engineGearsCopy.Add(gear);
            }
            this.gears = engineGearsCopy;
        }

        private void FindNeighborGear(int row, int startCol, int endCol, int partNumber) {

            // scan current row.
            if (startCol > 0) {
                if (this.map[row, startCol - 1] == '*') this.AddNeighborToGear(row, startCol - 1, partNumber);
            }
            if (endCol < (this.colCount - 1)) {
                if (this.map[row, endCol + 1] == '*') this.AddNeighborToGear(row, endCol + 1, partNumber);
            }

            // Establish the length of each row (above and below) we need to scan.
            int realStartCol = (startCol == 0 ? startCol : startCol - 1);
            int realEndCol = (endCol == (this.colCount - 1) ? endCol : endCol + 1);

            // scan row below.
            if (row > 0) {
                for (int currentCol = realStartCol; currentCol <= realEndCol; currentCol++) {
                    if (this.map[row - 1, currentCol] == '*') this.AddNeighborToGear(row - 1, currentCol, partNumber);
                }
            }
            // scan row above.
            if (row < (this.rowCount - 1)) {
                for (int currentCol = realStartCol; currentCol <= realEndCol; currentCol++) {
                    if (this.map[row + 1, currentCol] == '*') this.AddNeighborToGear(row + 1, currentCol, partNumber);
                }
            }
        }

        private void AddNeighborToGear(int row, int col, int neighborValue) {

            foreach (EngineGear gear in this.gears) {
                if (gear.row == row && gear.col == col) {
                    gear.neighbors.Add(neighborValue);
                }
            }
        }

        private bool IsValidSymbol(char c) {

            switch (c) {

                case '*':
                case '#':
                case '+':
                case '$':
                case '-':
                case '@':
                case '%':
                case '&':
                case '=':
                case '/':
                    return true;

                default:
                    return false;
            }
        }

        private bool IsValidPartNumber(int row, int startCol, int endCol) {

            // scan current row.
            if (startCol > 0) {
                if (this.IsValidSymbol(this.map[row, startCol - 1])) return true;
            }
            if (endCol < (this.colCount - 1)) {
                if (this.IsValidSymbol(this.map[row, endCol + 1])) return true;
            }

            // Establish the length of each row (above and below) we need to scan.
            int realStartCol = (startCol == 0 ? startCol : startCol - 1);
            int realEndCol = (endCol == (this.colCount - 1) ? endCol : endCol + 1);

            // scan row below.
            if (row > 0) {
                for (int currentCol = realStartCol; currentCol <= realEndCol; currentCol++) {
                    if (this.IsValidSymbol(this.map[(row - 1), currentCol])) return true;
                }
            }
            // scan row above.
            if (row < (this.rowCount - 1)) {
                for (int currentCol = realStartCol; currentCol <= realEndCol; currentCol++) {
                    if (this.IsValidSymbol(this.map[(row + 1), currentCol])) return true;
                }
            }

            return false;
        }

        public int GetSumofGearRatios() {

            int sumofGearRatios = 0;

            foreach (EngineGear gear in this.gears) {
                sumofGearRatios += gear.CalculateGearRatio();
            }

            return sumofGearRatios;
        }

        public int GetSumOfPartNumbers() {

            int sum = 0;
   
            foreach (int partNumber in this.partNumbers) {
                sum += partNumber;
            }

            return sum;
        }

        private Collection<int> FindPartNumbers() {

            Collection<int> partNumbers = new Collection<int>();

            for (int currentRow = 0; currentRow < this.rowCount; currentRow++) {

                string currentBuffer = "";
                int startCol = -1;
                bool processing = false;

                for (int currentCol = 0; currentCol < colCount; currentCol++) {

                    switch (this.map[currentRow, currentCol]) {

                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':

                            if (!processing) {
                                startCol = currentCol;
                                processing = true;
                            }
                            currentBuffer += this.map[currentRow, currentCol];
                            break;

                        case '.':
                        case '*':
                        case '#':
                        case '+':
                        case '$':
                        case '-':
                        case '@':
                        case '%':
                        case '&':
                        case '=':
                        case '/':

                            if (processing) {
                                int partNumber = Convert.ToInt32(currentBuffer);

                                if (this.IsValidPartNumber(currentRow, startCol, currentCol - 1)) {
                                    this.FindNeighborGear(currentRow, startCol, currentCol - 1, partNumber);
                                    partNumbers.Add(partNumber);
                                }

                                currentBuffer = "";
                                startCol = -1;
                            }
                            processing = false;
                            break;

                        default:
                            Console.WriteLine("ERROR: unknown character: " + this.map[currentRow, currentCol]);
                            break;
                    }
                }

                if (processing) {
                    int partNumber = Convert.ToInt32(currentBuffer);
                    if (this.IsValidPartNumber(currentRow, startCol, this.colCount - 1)) {
                        this.FindNeighborGear(currentRow, startCol, this.colCount - 1, partNumber);

                        partNumbers.Add(partNumber);
                    }
                }


            }

            return partNumbers;
        }

        public void print() {

            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < colCount; j++) {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("gears: ");
            foreach (EngineGear gear in this.gears) {
                Console.WriteLine(gear.row + "," + gear.col + ": " + gear.neighbors.Count);
            }
        }
    }
}
