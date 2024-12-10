using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {

    internal class AntennaNode {

        public char antenna;
        public List<char> antinodes = new List<char>();
        public List<char> antinodes2 = new List<char>();
        public int row;
        public int col;

        public AntennaNode(char antenna, int row, int col) {
            this.antenna = antenna; 
            this.row = row;
            this.col = col;
        }
    }
}
