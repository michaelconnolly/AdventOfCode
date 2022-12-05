using System;

namespace AdventOfCode2022 {

    internal class CrateCommand {

        public string command;
        public int count;
        public int stackStart;
        public int stackEnd;

        public CrateCommand(string line) {

            string[] parts = line.Split(' ');

            // example: "move 1 from 2 to 1"
            this.command = parts[0];
            this.count = int.Parse(parts[1]);
            this.stackStart = int.Parse(parts[3]);
            this.stackEnd = int.Parse(parts[5]);
        }

        public void Print() {

            Console.WriteLine(this.command + ", " + this.count + ", " + this.stackStart + ", " + this.stackEnd);
        }
    }
}
