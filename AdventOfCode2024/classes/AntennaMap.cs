using System;
using System.Collections.Generic;


namespace AdventOfCode2024.classes {

    internal class AntennaMap {

        public AntennaNode[,] map;
        public Dictionary<char, List<AntennaNode>> dict = new Dictionary<char, List<AntennaNode>>();

        public AntennaMap(string[] input) {

            this.map = new AntennaNode[input.Length, input[0].Length];

            for (int row = 0; row < input.Length; row++) {
                for (int col = 0; col < input[row].Length; col++) {

                    char antennaType = input[row][col];

                    AntennaNode node = new AntennaNode(antennaType, row, col);
                    this.map[row, col] = node;

                    // Map all antennas into a dictionary for faster lookup.
                    if (antennaType != '.') {
                        if (!(this.dict.ContainsKey(antennaType))) {
                            this.dict[antennaType] = new List<AntennaNode>();
                        }
                        this.dict[antennaType].Add(node);
                    }
                }
            }

            this.ProcessMap();
        }

        private bool IsLegal(Coordinate coord) {

            return (coord.row >= 0 && coord.row < this.map.GetLength(0) &&
                coord.col >= 0 && coord.col < this.map.GetLength(1));
        }

        private void ProcessMap() {

            // For each antenna type listed in the dictionary ...
            foreach (char antennaType in this.dict.Keys) {

                // For each antenna1 instance from 0 to total - 1
                for (int antennaIndex1 = 0; antennaIndex1 < this.dict[antennaType].Count - 1; antennaIndex1++) {

                    AntennaNode antenna1 = this.dict[antennaType][antennaIndex1];

                    // For each antenna2 instance from 1 to total ...
                    for (int antennaIndex2 = antennaIndex1 + 1; antennaIndex2 < this.dict[antennaType].Count; antennaIndex2++) {

                        // the distance between the two is [x,y]
                        AntennaNode antenna2 = this.dict[antennaType][antennaIndex2];
                        int rowDelta = antenna1.row - antenna2.row;
                        int colDelta = antenna1.col - antenna2.col;

                        // calculate two spots where the distance is the other side
                        Coordinate possibleAntinode1 = new Coordinate(antenna1.row + rowDelta, antenna1.col + colDelta);
                        Coordinate possibleAntinode2 = new Coordinate(antenna2.row - rowDelta, antenna2.col - colDelta);

                        // if these nodes are legal on the board, add to dict.
                        if (this.IsLegal(possibleAntinode1)) {
                            this.map[possibleAntinode1.row, possibleAntinode1.col].antinodes.Add(antennaType);
                        }
                        if (this.IsLegal(possibleAntinode2)) {
                            this.map[possibleAntinode2.row, possibleAntinode2.col].antinodes.Add(antennaType);
                        }

                        // Additional logic for part two!  More generous logic for antinodes.
                        bool fContinue = true;
                        int currentRow = antenna1.row;
                        int currentCol = antenna1.col;
                        while (fContinue) {
                            Coordinate possibleAntinode = new Coordinate(currentRow, currentCol);
                            if (this.IsLegal(possibleAntinode)) {
                                this.map[possibleAntinode.row, possibleAntinode.col].antinodes2.Add(antennaType); ;
                                currentRow += rowDelta;
                                currentCol += colDelta;
                            }
                            else {
                                fContinue = false;
                            }
                        }
                        
                        fContinue = true;
                        currentRow = antenna1.row;
                        currentCol = antenna1.col;
                        while (fContinue) {
                            Coordinate possibleAntinode = new Coordinate(currentRow, currentCol);
                            if (this.IsLegal(possibleAntinode)) {
                                this.map[possibleAntinode.row, possibleAntinode.col].antinodes2.Add(antennaType); ;
                                currentRow -= rowDelta;
                                currentCol -= colDelta;
                            }
                            else {
                                fContinue = false;
                            }
                        }
                    }
                }
            }
        }


        public void Print() {

            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {

                    if (this.map[row, col].antinodes.Count > 0) {
                        Console.Write('#');
                    }
                    else {
                        Console.Write(this.map[row, col].antenna);
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine();

            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {

                    if (this.map[row, col].antinodes2.Count > 0) {
                        Console.Write('*');
                    }
                    else {
                        Console.Write(this.map[row, col].antenna);
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public int CountOfAntinodeLocations() {

            int count = 0;

            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {

                    if (this.map[row, col].antinodes.Count > 0) {
                        count++;
                    }
                }
            }

            return count;
        }

        public int CountOfAntinodeLocations2() {

            int count = 0;

            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {

                    if (this.map[row, col].antinodes2.Count > 0) {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
