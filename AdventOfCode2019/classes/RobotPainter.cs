using System;
using System.Collections.Generic;

namespace AdventOfCode2019 {

    class RobotPainter {

        string[] instructions;
        long instructionPointer = 0;
        public int runCount = 0;
        public bool currentlyRunning = false;
        long relativeBase = 0;

        string TURN_LEFT = "0";
        string TURN_RIGHT = "1";

        ForwardDirection forwardDirection;
        int row;
        int col;
        string input;

        public enum ForwardDirection {
            UP = 0,
            LEFT = 1,
            DOWN = 2,
            RIGHT = 3
        }
    
        public RobotPainter(string[]instructionsInput) {

            // The computer's available memory should be much larger than the initial program. Memory beyond the initial program starts with
            // the value 0 and can be read or written like any other memory. (It is invalid to try to access memory at a negative address, though.)
            this.instructions = new string[10000];
            for (int index = 0; index < instructions.Length; index++) {
                instructions.SetValue("0", index);
            }
            for (int index = 0; index < instructionsInput.Length; index++) {
                instructions.SetValue(instructionsInput[index], index);
            }

            this.forwardDirection = ForwardDirection.UP;
            this.row = 0;
            this.col = 0;
        }

        void SetCell(List<String> rows, int row, int col, char value) {

            char[] chars = rows[row].ToCharArray();
            chars[col] = value;
            string output = "";
            foreach (char c in chars) {
                output += c;
            }
            //this.rows[row] = chars.
            rows[row] = output;
        }

        public void Paint(Dictionary<string, string> robotCommands) {

            // Find max width and max height.
            // create an array of strings, initializing all to black. 
            // Walk through the set of robot commands.
            //      Parse it.
            //      Paint it.

            int smallestCol = int.MaxValue;
            int smallestRow = int.MaxValue;
            int largestCol = int.MinValue;
            int largestRow = int.MinValue;
            foreach (string key in robotCommands.Keys) {
                string[] keyValues = key.Split(",");
                int row = Convert.ToInt32(keyValues[0]);
                int col = Convert.ToInt32(keyValues[1]);
                smallestCol = (col < smallestCol ? col : smallestCol);
                smallestRow = (row < smallestRow ? row : smallestRow);
                largestCol = (col > largestCol ? col : largestCol);
                largestRow = (row > largestRow ? row : largestRow);
            }

            // Modify them to be all base 0.
            int rowModifier = -smallestRow;
            int colModifier = -smallestCol;
            smallestCol += colModifier;
            largestCol += colModifier;
            smallestRow += rowModifier;
            largestRow += rowModifier;

            List<string> rows = new List<string>();
            for (int i = 0; i < (largestRow + 1); i++) {
                rows.Add(new String('0', (largestCol + 1)));
            }

            // Print it out.
            foreach (string row in rows) {
                Console.WriteLine(row);
            }

            foreach (string key in robotCommands.Keys) {
                string[] keyValues = key.Split(",");
                int row = Convert.ToInt32(keyValues[0]);
                int col = Convert.ToInt32(keyValues[1]);
                row += rowModifier;
                col += colModifier;
                string color = robotCommands[key];
                char colorToUse = (color == "0" ? ' ' : 'X');

                SetCell(rows, (row), (col), colorToUse);
            }

            // Print it out.
            foreach (string row in rows) {
                Console.WriteLine(row);
            }
        }

            private string ContinueProgram() {

            Assert(this.currentlyRunning == true, "continueProgram run but we aren't running");

            string output = this.IntcodeProgram();
            this.runCount++;

            return output;
        }

        public void Assert(bool f, string warning) {
            if (!f) Console.WriteLine(warning);
        }

        private string RunProgram(string inputSignal) {

            this.currentlyRunning = true;
            this.input = inputSignal;
            string output = this.IntcodeProgram();
            this.runCount++;

            return output;
        }

        void MoveForward() {

            switch (this.forwardDirection) {

                case ForwardDirection.UP:

                    this.row--;
                    break;

                case ForwardDirection.LEFT:

                    this.col--;
                    break;

                case ForwardDirection.DOWN:

                    this.row++;
                    break;

                case ForwardDirection.RIGHT:

                    this.col++;
                    break;

                default:

                    Console.WriteLine("ERROR DUDE!");
                    break;
            }
        }

        void ChangeForwardDirection(string turnDirection) {

            if (turnDirection == TURN_LEFT) {

                switch (this.forwardDirection) {
                    case ForwardDirection.UP:
                        this.forwardDirection = ForwardDirection.LEFT;
                        break;
                    case ForwardDirection.LEFT:
                        this.forwardDirection = ForwardDirection.DOWN;
                        break;
                    case ForwardDirection.DOWN:
                        this.forwardDirection = ForwardDirection.RIGHT;
                        break;
                    case ForwardDirection.RIGHT:
                        this.forwardDirection = ForwardDirection.UP;
                        break;

                }
            }
            else {

                switch (this.forwardDirection) {
                    case ForwardDirection.UP:
                        this.forwardDirection = ForwardDirection.RIGHT;
                        break;
                    case ForwardDirection.LEFT:
                        this.forwardDirection = ForwardDirection.UP;
                        break;
                    case ForwardDirection.DOWN:
                        this.forwardDirection = ForwardDirection.LEFT;
                        break;
                    case ForwardDirection.RIGHT:
                        this.forwardDirection = ForwardDirection.DOWN;
                        break;

                }
            }

            //    //this.forwardDirection++;
            //    this.forwardDirection = (this.forwardDirection == ForwardDirection.RIGHT ? ForwardDirection.UP : 
            //        (ForwardDirection)(Forward++);
            //}
            //else {
            //    this.forwardDirection = (this.forwardDirection == ForwardDirection.UP ? ForwardDirection.RIGHT : this.forwardDirection++);
            //}
        }

        public Dictionary<string, string> RunTheRobot(string inputThingy) {

            this.input = inputThingy;
            bool keepGoing = true;

            string BLACK = "0";
            string WHITE = "1";

            Dictionary<string, string> robotCommands = new Dictionary<string, string>();
            //List<string> robotInstructions = new List<string>();

            string color = input;
            string direction;

            while (keepGoing) {

                string output1 = this.RunProgram(color);
                if (output1 == "-1") {
                    keepGoing = false;
                }
                else {
                    direction = this.ContinueProgram();
                    color = output1;
                    string outputPair = this.row + "," + this.col;

                    // Tasks: 
                    // 1) Paint the current color where i stand.
                    robotCommands[outputPair] = color;

                    // 2) Turn 90 degrees to the left or the right, based on 'direction'.
                    this.ChangeForwardDirection(direction);

                    // 3) Move forward one space.
                    this.MoveForward();

                    // 4) Calculate the color of the tile i am currently sitting on.
                    string newOutputPair = this.row + "," + this.col;
                    if (robotCommands.ContainsKey(newOutputPair)) {
                        color = robotCommands[newOutputPair];
                    }
                    else {
                        color = BLACK;
                    }
                }
            }
            return robotCommands;
        }

        string IntcodeProgram() {

            string output = "";
            bool keepGoing = true;

            while (keepGoing) {

                // Grab opCode
                string instruction = instructions[instructionPointer].PadLeft(5, '0');
                int opCode = Convert.ToInt32(instruction.Substring(instruction.Length - 2));
                int parameterMode1 = Convert.ToInt32(instruction.Substring(instruction.Length - 3, 1));
                int parameterMode2 = Convert.ToInt32(instruction.Substring(instruction.Length - 4, 1));
                int parameterMode3 = Convert.ToInt32(instruction.Substring(instruction.Length - 5, 1));

                long value1, value2, value3, value3Location;

                // Useful for testing, but slows down execution, especially for part 2.
                // Console.WriteLine("\topCode: " + opCode + "; current output: " + output);

                switch (opCode) {

                    case 1:

                        // Opcode 1 adds together numbers read from two positions and stores the result in a third position.The three integers immediately after the opcode
                        // tell you these three positions -the first two indicate the positions from which you should read the input values, and the third indicates the position
                        // at which the output should be stored.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        value2 = Program.GetValue(instructions, (instructionPointer + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[instructionPointer + 3]);
                        value3 = value1 + value2;
                        Program.SetValue(instructions, value3.ToString(), value3Location, parameterMode3, relativeBase);

                        instructionPointer += 4;
                        break;

                    case 2:

                        // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again, the three integers after the opcode indicate
                        // where the inputs and outputs are, not their values.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        value2 = Program.GetValue(instructions, (instructionPointer + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[instructionPointer + 3]);
                        value3 = value1 * value2;
                        Program.SetValue(instructions, value3.ToString(), value3Location, parameterMode3, relativeBase);

                        instructionPointer += 4;
                        break;

                    case 3:

                        // Opcode 3 takes a single integer as input and saves it to the address given by its only parameter.For example, the instruction 3,50 would take an input value and store it at address 50.
                        // We should never get a parameterMode1 == 1 for case 3.

                        if (parameterMode1 == 1) {
                            Console.WriteLine("ERROR! Received parameterMode==1 in OpCode 3.  Ignoring.");
                        }

                        long valueLocation1 = Convert.ToInt32(instructions[(instructionPointer + 1)]);
                        Program.SetValue(instructions, input, valueLocation1, parameterMode1, relativeBase);

                        instructionPointer += 2;
                        break;

                    case 4:

                        // Opcode 4 outputs the value of its only parameter.For example, the instruction 4,50 would output the value at address 50.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        output = value1.ToString();

                        // If we are in feedback loop mode, tell our AmplifierSeries parent that we changed our output.
                        //int setting = Convert.ToInt32(this.phaseSetting);
                        //if (setting >= 5 && setting <= 9) {
                        //    this.amplifierSeries.AmplifierChangedOutput(this, this.outputSignal);
                        //}

                        instructionPointer += 2;

                        return output;

                        break;

                    case 5:

                        // Opcode 5 is jump -if-true: if the first parameter is non - zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        value2 = Program.GetValue(instructions, (instructionPointer + 2), parameterMode2, relativeBase);
                        if (value1 != 0) {
                            instructionPointer = value2;
                        }
                        else {
                            instructionPointer += 3;
                        }

                        break;

                    case 6:

                        // Opcode 6 is jump -if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        value2 = Program.GetValue(instructions, (instructionPointer + 2), parameterMode2, relativeBase);
                        if (value1 == 0) {
                            instructionPointer = value2;
                        }
                        else {
                            instructionPointer += 3;
                        }

                        break;

                    case 7:

                        // Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        value2 = Program.GetValue(instructions, (instructionPointer + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[(instructionPointer + 3)]);

                        if (value1 < value2) {
                            Program.SetValue(instructions, "1", value3Location, parameterMode3, relativeBase);
                        }
                        else {
                            Program.SetValue(instructions, "0", value3Location, parameterMode3, relativeBase);
                        }

                        instructionPointer += 4;
                        break;

                    case 8:

                        //  Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.
                        // Otherwise, it stores 0.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        value2 = Program.GetValue(instructions, (instructionPointer + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[(instructionPointer + 3)]);
                        if (value1 == value2) {
                            Program.SetValue(instructions, "1", value3Location, parameterMode3, relativeBase);
                        }
                        else {
                            Program.SetValue(instructions, "0", value3Location, parameterMode3, relativeBase);
                        }

                        instructionPointer += 4;
                        break;

                    case 9:

                        // Opcode 9 adjusts the relative base by the value of its only parameter. 
                        // The relative base increases (or decreases, if the value is negative) by the value of the parameter.

                        value1 = Program.GetValue(instructions, (instructionPointer + 1), parameterMode1, relativeBase);
                        relativeBase += Convert.ToInt32(value1);

                        instructionPointer += 2;
                        break;

                    case 99:

                        keepGoing = false;
                        this.currentlyRunning = false;
                        return "-1";

                        break;

                    default:

                        // This is probably fine.
                        //Console.WriteLine("ERROR: illegal opcode: " + opCode);
                        return "-2";
                        break;
                }

                //this.instructionPointer = i;
            }

            return output;
        }
    }
}
