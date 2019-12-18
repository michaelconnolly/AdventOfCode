using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {

    public class ArcadeCabinet {

        string[] instructions;
        long instructionPointer = 0;
        public int runCount = 0;
        public bool currentlyRunning = false;
        long relativeBase = 0;
        string input;

        public string GAME_OVER = "-89";
        public string UPDATE_DISPLAY = "-1";
        string JOYSTICK_CENTER = "0";
        string JOYSTICK_LEFT = "-1";
        string JOYSTICK_RIGHT = "1";
        public string CurrentScore = "0";

        int maximumX, maximumY;

        int currentColPaddle = -1;
        int currentColBall = -1;

        public ArcadeCabinet(string[] instructionsInput) {

          
            // The computer's available memory should be much larger than the initial program. Memory beyond the initial program starts with
            // the value 0 and can be read or written like any other memory. (It is invalid to try to access memory at a negative address, though.)
            this.instructions = new string[10000];
            for (int index = 0; index < instructions.Length; index++) {
                instructions.SetValue("0", index);
            }
            for (int index = 0; index < instructionsInput.Length; index++) {
                instructions.SetValue(instructionsInput[index], index);
            }
        }

        public void MakeItPlayForFree() {

            this.instructions[0] = "2";
        }

        public void DrawScreen() {


        }

        public void CalculateScreenSize(Dictionary<string, string> robotCommands) {

            this.maximumX = 0;
            this.maximumY = 0;

            foreach (string key in robotCommands.Keys) {

                string[] coordinates = key.Split(',');
                int x = Convert.ToInt32(coordinates[0]);
                int y = Convert.ToInt32(coordinates[1]);
                if (x > maximumX) maximumX = x;
                if (y > maximumY) maximumY = y;
            }

            this.maximumX++;
            this.maximumY++;
        }

        public char DrawThisTile(char tileId) {

            // 0 is an empty tile.No game object appears in this tile.
            // 1 is a wall tile.Walls are indestructible barriers.
            // 2 is a block tile.Blocks can be broken by the ball.
            // 3 is a horizontal paddle tile. The paddle is indestructible.
            // 4 is a ball tile.The ball moves diagonally and bounces off objects.

            switch (tileId) {

                case '0':

                    return ' ';

                case '1':

                    return 'W';

                case '2':

                    return 'B';

                case '3':

                    return 'P';

                case '4':


                    return 'O';

                default:

                    return '?';
            }
        }

        public void PrintScreen(Dictionary<string, string> robotCommands) {

            Console.WriteLine("SCORE: " + this.CurrentScore);

            char[][] rows = new char[this.maximumY][];

            for (int row=0; row<this.maximumY; row++) {

                char[] rowData = new char[this.maximumX];
                rows[row] = rowData;
            }

            foreach (string key in robotCommands.Keys) {

                string[] coordinates = key.Split(',');
                int x = Convert.ToInt32(coordinates[0]);
                int y = Convert.ToInt32(coordinates[1]);
                string rawChar = (string)robotCommands[key];
                char actualChar = rawChar.ToCharArray()[0];
                rows[y][x] = this.DrawThisTile(actualChar);

                // Where is the paddle and ball?
                if (actualChar == '3') {
                    this.currentColPaddle = x;
                }
                else if  (actualChar == '4') {
                    this.currentColBall = x;
                }
            }

            foreach (char[] row in rows) {

                string rowString = "";
                foreach (char currentChar in row) {
                    rowString += currentChar;
                }

                Console.WriteLine(rowString);
            }

            return;
        }

        public Dictionary<string, string> PlayTheGame(bool interactiveMode) {

            this.input = JOYSTICK_CENTER;
            bool keepGoing = true;
            bool calculatedScreenSize = false;
            this.currentlyRunning = true;

            Dictionary<string, string> robotCommands = new Dictionary<string, string>();

            string xPos, yPos, tileId;

            while (keepGoing) {

                string output1 = this.ContinueProgram();
                if (output1 == GAME_OVER) {
                    keepGoing = false;
                }

                else {
                    xPos = output1;
                    yPos = this.ContinueProgram();
                    tileId = this.ContinueProgram();

                    if (xPos == "-1" && yPos == "0") {
                        this.CurrentScore = tileId;

                        if (!calculatedScreenSize) {
                            this.CalculateScreenSize(robotCommands);
                            calculatedScreenSize = true;
                        }

                    }
                    else {
                        string outputPair = xPos + "," + yPos;
                        robotCommands[outputPair] = tileId;
                    }
                }

                if (calculatedScreenSize) {

                    PrintScreen(robotCommands);

                    // If you want the game to play itself, use this code.
                    if (this.currentColBall != -1 && this.currentColPaddle != -1) {

                        if (this.currentColPaddle < this.currentColBall) {
                            this.input = "1";
                        }
                        else if (this.currentColPaddle > this.currentColBall) {
                            this.input = "-1";
                        }
                        else {
                            this.input = "0";
                        }
                    }

                    // If you want to manually control the joystick, uncomment this code.
                    //ConsoleKeyInfo consoleKey = Console.ReadKey();
                    //char pressedChar = consoleKey.KeyChar;
                    //switch (pressedChar) {
                    //    case ',':
                    //        this.input = "-1";
                    //        break;
                    //    case '.':
                    //        this.input = "1";
                    //        break;
                    //    case ' ':
                    //        this.input = "0";
                    //        break;
                    //    default:
                    //        this.input = "0";
                    //        break;

                    //}
                }

            }
            return robotCommands;
        }


        public void Assert(bool f, string warning) {
            if (!f) Console.WriteLine(warning);
        }

        private string ContinueProgram() {


            string output = this.IntcodeProgram();
            this.runCount++;

            return output;
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
                        return GAME_OVER;

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
