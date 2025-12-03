using System;
using System.Diagnostics;


namespace AdventOfCode2025 {

    internal class SafeCracker {

        const int positionStart = 50;
        string[] instructions;
        int currentPosition = positionStart;
        int leftAtZeroCount = 0;
        int passingAtZeroCount = 0;


        public SafeCracker(string[] lines) {

            this.instructions = lines;

            Console.WriteLine("Starting position: " + this.currentPosition);

            foreach (string instruction in this.instructions) {
                this.ProcessInstruction(instruction);
            }
        }


        void ProcessInstruction(string instruction) {

            char direction = instruction.Substring(0, 1)[0];
            int localLaps = 0;
       
            // how many times should i turn to the left or right?  
            // remember to mod it by 100, and count how many times you do that for question 2.
            int rawCount = int.Parse(instruction.Substring(1));
            int count = rawCount;
            if (count >= 100) {
                localLaps = count / 100;
                count = count % 100;
                this.passingAtZeroCount += localLaps;
            }
            
            Console.WriteLine("Direction: " + direction + ", Count: " + count + ", RawCount: " + rawCount);

            if (direction == 'L') {
                if (this.currentPosition > count) {
                    this.currentPosition -= count;
                }
                else if (this.currentPosition == count) {
                    this.currentPosition = 0; 
                    this.passingAtZeroCount++;
                }
                else { // this.currentPosition < count)
                    if (this.currentPosition != 0) {
                        this.passingAtZeroCount++;
                    }
                    this.currentPosition = (100 - (count - this.currentPosition));
                }
            }
            else if (direction == 'R') {  // direction == 'R'
                if ((this.currentPosition + count < 100)) {
                    this.currentPosition += count;
                }
                else if (this.currentPosition + count == 100) {
                    this.currentPosition = 0;
                    this.passingAtZeroCount++;
                }
                else { // this.currentPosition + count > 100
                    if (this.currentPosition != 0) {
                        this.passingAtZeroCount++;
                    }
                    this.currentPosition = (this.currentPosition + count - 100);  
                }
            }

            else {
                Debug.Assert(false, "Invalid direction: " + direction);
            }

            if (this.currentPosition == 0) {
                this.leftAtZeroCount++;
            }

            Console.WriteLine("Current position: " + this.currentPosition + "; atZero: " + this.leftAtZeroCount + "; passedZero: " + this.passingAtZeroCount);
        }

        public int QuestionOne() {
            return this.leftAtZeroCount;
        }

        public int QuestionTwo() {
            return this.passingAtZeroCount;
        }
    }
}