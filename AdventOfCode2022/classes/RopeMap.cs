using System;

namespace AdventOfCode2022 {

    public class RopeMap {

        string[] commands;
        bool[,] map;
        RopeCoordinate head;
        public RopeCoordinate lastTail = null;

        // This is a hack that i'm not proud of, but does the job.  Not sure how far and wide
        // the head will travel, so creating a big board.  What i really should do is do a 
        // pre-inspection of the commands and figure out how big of a space i need.  But
        // i am lazily not doing that.
        int width = 1000;
        int height = 1000;
        int startRow = 500;
        int startCol = 500;

        public RopeMap(string[] lines, int tailCount) {

            this.commands = lines;

            this.InitializeMap();

            RopeCoordinate currentTail = null;

            for (int i = 0; i < tailCount; i++) {

                RopeCoordinate newTail = new RopeCoordinate(startRow, startCol, this, currentTail);
                if (i == 0) this.lastTail = newTail;
                currentTail = newTail;
            }

            this.head = new RopeCoordinate(startRow, startCol, this, currentTail); // this.start.Copy();

            this.MarkMapAsVisited(this.lastTail);
        }

        public void MarkMapAsVisited(RopeCoordinate coordinate) {

            this.map[coordinate.col, coordinate.row] = true;
        }

        private void InitializeMap() {

            this.map = new bool[width, height];
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    this.map[i, j] = false;
                }
            }
        }

        public int VisitedCount() {

            int count = 0;
  
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (this.map[i, j]) count++;
                }
            }

            return count;
        }

        public void ExecuteCommands() {

            Console.WriteLine("Starting Positions: ");
            this.Print();
            Console.WriteLine("--------------------");

            foreach (string command in this.commands) {

                string[] parts = command.Split(" ");
                string direction = parts[0];
                int distance = int.Parse(parts[1]);

                // Move Head.
                head.Move(direction, distance);
                this.Print();
            }
        }

        public void Print() {
            Console.Write("head: ");
            head.Print();
            Console.Write("tail: ");
            this.lastTail.Print();
        }
    }
}
