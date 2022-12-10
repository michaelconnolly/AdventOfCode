using System;
using System.Collections.Generic;

namespace AdventOfCode2022 {
   
    public class VideoSignal {

        public int registerX;
        string[] instructions;
        Dictionary<int, int> historicalX;
        const int screenWidth = 40;

        public VideoSignal(string[] lines) {

            this.instructions = lines;
        }


        private void DrawScreen(int cycle) {

            int col = ((cycle % screenWidth));
            bool endOfLine = (col == 0);
            if (col == 0) col = 40;

            if (cycle > screenWidth) { int x = 5; }
            if (cycle == 200) { int x = 5; }

            if ((this.registerX >= (col - 2)) && (this.registerX <= (col))) {
                Console.Write("#");
            }
            else Console.Write(".");
            
            if (endOfLine) Console.WriteLine();
        }


        public void RunInstructions(bool drawScreen = false) {

            // initialize stuff.
            this.registerX = 1;
            this.historicalX = new Dictionary<int, int>();
            int cycle = 1;

            // Let's loop through our instructions.
            for (int i = 0; i < instructions.Length; i++) {

                string[] parts = instructions[i].Split(" ");
                string instruction = parts[0];

                switch (instruction) {

                    case "noop":

                        this.historicalX[cycle] = this.registerX;
                        if (drawScreen) this.DrawScreen(cycle);
                        cycle++;
                        break;

                    case "addx":

                        int increment = int.Parse(parts[1]);

                        this.historicalX[cycle] = this.registerX;
                        if (drawScreen) this.DrawScreen(cycle);
                        cycle++;

                        this.historicalX[cycle] = this.registerX;
                        if (drawScreen) this.DrawScreen(cycle);
                        cycle++;

                        this.registerX += increment;
                        break;

                    default:

                        throw new Exception();
                }
            }

            return;
        }

        public int QuestionOne() {

            // error handling.
            if (this.historicalX.Keys.Count < 220) return -1;

            int cycle20 = this.historicalX[20] * 20;
            int cycle60 = this.historicalX[60] * 60;
            int cycle100 = this.historicalX[100] * 100;
            int cycle140 = this.historicalX[140] * 140;
            int cycle180 = this.historicalX[180] * 180;
            int cycle220 = this.historicalX[220] * 220;

            return (cycle20 + cycle60 + cycle100 + cycle140 + cycle180 + cycle220);
        }
    }
}
