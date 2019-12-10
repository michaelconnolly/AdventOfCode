
using System;

namespace AdventOfCode2019 {

    public class Amplifier {

        string[] instructions;
        string phaseSetting;
        string inputSignal = int.MinValue.ToString();
        string outputSignal = int.MinValue.ToString();
        bool processedPhaseSetting = false;
        AmplifierSeries amplifierSeries = null;

        public int runCount = 0;
        public bool currentlyRunning = false;

        public Amplifier(string[] instructionsInput, string phaseSetting, AmplifierSeries amplifierSeries) {

            this.instructions = (string[]) instructionsInput.Clone();
            this.phaseSetting = phaseSetting;
            this.amplifierSeries = amplifierSeries;
        }

        public void ChangeInputValue(string inputSignal) {
            this.inputSignal = inputSignal;
        }

        private int GetValue(string[] instructions, int index, int parameterMode) {

            //int value1;
            int parameterInt = Convert.ToInt32(instructions[index]);
            if (parameterMode == 0) {
                return Convert.ToInt32(instructions[parameterInt]);
            }
            else {
                return parameterInt;
            }
        }

        public string Run(string inputSignal) {

            this.currentlyRunning = true;
            this.inputSignal = inputSignal;
            this.outputSignal = this.IntcodeProgram();
            this.currentlyRunning = false;
            this.runCount++;
           
            return this.outputSignal;
        }
        private string IntcodeProgram() {

            // Opcodes(like 1, 2, or 99) mark the beginning of an instruction. The values used immediately after an opcode, if any, are called the instruction's
            // parameters. For example, in the instruction 1,2,3,4, 1 is the opcode; 2, 3, and 4 are the parameters. The instruction 99 contains only an opcode
            // and has no parameters.

            //string[] instructions = (string[])instructionsInput.Clone();
            //string output = "";
            int i = 0;

            //instructions[1] = input;
            //instructions[3] = input2;
            //i += 2;

            //// Display the file contents.
            //Console.WriteLine("Contents of file = ");
            //foreach (string opCode in instructions) {
            //    Console.WriteLine("\t" + opCode);
            //}
            //int numberOfOpCodes = instructions.Length;
            //Console.WriteLine("Total number of opCodes: " + numberOfOpCodes.ToString());

            bool keepGoing = true;
            //int processedInputCount = 0;

            while (keepGoing) {

                // Grab opCode
                string instruction = instructions[i].PadLeft(5, '0');
                int opCode = Convert.ToInt32(instruction.Substring(instruction.Length - 2));

                int parameterMode1 = Convert.ToInt32(instruction.Substring(instruction.Length - 3, 1));
                int parameterMode2 = Convert.ToInt32(instruction.Substring(instruction.Length - 4, 1));
                int parameterMode3 = Convert.ToInt32(instruction.Substring(instruction.Length - 5, 1));

                int value1, value2, value3, value3Location;

                switch (opCode) {

                    case 1:

                        // Opcode 1 adds together numbers read from two positions and stores the result in a third position.The three integers immediately after the opcode
                        // tell you these three positions -the first two indicate the positions from which you should read the input values, and the third indicates the position
                        // at which the output should be stored.

                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        value2 = this.GetValue(instructions, (i + 2), parameterMode2);
                        value3Location = Convert.ToInt32(instructions[i + 3]);
                        value3 = value1 + value2;
                        instructions[value3Location] = (value3).ToString();

                        i += 4;
                        break;

                    case 2:

                        // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again, the three integers after the opcode indicate
                        // where the inputs and outputs are, not their values.

                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        value2 = this.GetValue(instructions, (i + 2), parameterMode2);
                        value3Location = Convert.ToInt32(instructions[i + 3]);
                        value3 = value1 * value2;
                        instructions[value3Location] = (value3).ToString();

                        i += 4;
                        break;

                    case 3:

                        string inputToUse;
                        if (!(this.processedPhaseSetting)) {
                            inputToUse = phaseSetting;
                            this.processedPhaseSetting = true;
                        }
                        else {
                            inputToUse = this.inputSignal;
                        }
                       
                        // Opcode 3 takes a single integer as input and saves it to the address given by its only parameter.For example, the instruction 3,50 would take an input value and store it at address 50.
                        // We should never get a parameterMode1 == 1 for case 3.
                        if (parameterMode1 == 1) {
                            Console.WriteLine("ERROR! Received parameterMode==1 in OpCode 3.  Ignoring.");
                        }

                        int valueLocation1 = Convert.ToInt32(instructions[(i + 1)]);
                        instructions[valueLocation1] = inputToUse;

                        i += 2;
                        break;

                    case 4:

                        // Opcode 4 outputs the value of its only parameter.For example, the instruction 4,50 would output the value at address 50.
                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        this.outputSignal = value1.ToString();

                        // If we are in feedback loop mode, tell our AmplifierSeries parent that we changed our output.
                        int setting = Convert.ToInt32(this.phaseSetting);
                        if (setting >= 5 && setting <= 9) {
                            this.amplifierSeries.AmplifierChangedOutput(this, this.outputSignal);
                        }

                        //if (this.phaseSetting in { 5..9}) {

                        //}

                        i += 2;
                        break;

                    case 5:

                        // Opcode 5 is jump -if-true: if the first parameter is non - zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.
                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        value2 = this.GetValue(instructions, (i + 2), parameterMode2);
                        if (value1 != 0) {
                            i = value2;
                        }
                        else {
                            i += 3;
                        }

                        break;

                    case 6:

                        // Opcode 6 is jump -if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.
                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        value2 = this.GetValue(instructions, (i + 2), parameterMode2);
                        if (value1 == 0) {
                            i = value2;
                        }
                        else {
                            i += 3;
                        }

                        break;

                    case 7:

                        // Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        value2 = this.GetValue(instructions, (i + 2), parameterMode2);
                        value3Location = Convert.ToInt32(instructions[(i + 3)]);
                        if (value1 < value2) {
                            instructions[value3Location] = "1";
                        }
                        else {
                            instructions[value3Location] = "0";
                        }

                        i += 4;
                        break;

                    case 8:

                        //  Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.Otherwise, it stores 0.
                        value1 = this.GetValue(instructions, (i + 1), parameterMode1);
                        value2 = this.GetValue(instructions, (i + 2), parameterMode2);
                        value3Location = Convert.ToInt32(instructions[(i + 3)]);
                        if (value1 == value2) {
                            instructions[value3Location] = "1";
                        }
                        else {
                            instructions[value3Location] = "0";
                        }

                        i += 4;
                        break;

                    case 99:

                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("ERROR: illegal opcode: " + opCode);
                        break;
                }
            }

            //Console.WriteLine("Final list:");
            //foreach (string opCode in instructions) {
            //    Console.WriteLine("\t" + opCode);
            //}

            //Console.WriteLine("Phase Setting: " + phaseSetting);
            //Console.WriteLine("Input: " + inputSignal);
            //Console.WriteLine("Output: " + output);
            //Console.WriteLine("Final value in position 0: " + instructions[0]);

            // object[] returnObject = new object[] { output, instructions };
            //return returnObject;
            return this.outputSignal;
        }
    }
}
