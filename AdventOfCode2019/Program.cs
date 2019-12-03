using System;


namespace AdventOfCode2019 {

    class Program {

        static void Main(string[] args) {

            //Day1();
            Day2();

            // Keep the console window open.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static int FuelRequired(int inputValue) {

            int fuelRequired = ((int)Math.Truncate(((double)(inputValue / 3)))) - 2;

            return fuelRequired;
        }

        static int FuelRequiredNested(int inputValue) {

            int fuelRequired = FuelRequired(inputValue);
            int additionalFuelRequired = FuelRequired(fuelRequired);
            
            while (additionalFuelRequired > 0) {
                fuelRequired += additionalFuelRequired;
                additionalFuelRequired = FuelRequired(additionalFuelRequired);
            }

            return fuelRequired;
        }

        static void Day1() {

            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day1_input.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int numberOfLines = 0;
            int totalFuelRequired = 0;
         
            // Display the file contents.
            System.Console.WriteLine("Contents of file = ");

           // Day 1, Part 1.
           // Fuel required to launch a given module is based on its mass. Specifically, to find the fuel required for a module, take its mass, divide by three, round down, and subtract 2.
            foreach (string line in lines) {
                int value = Convert.ToInt32(line);
                int fuelRequired = FuelRequired(value);
                numberOfLines++;
                totalFuelRequired += fuelRequired;
                Console.WriteLine("\t" + line + "; fuel required: " + fuelRequired.ToString());
            }

            Console.WriteLine("Total number of lines: " + numberOfLines.ToString());
            Console.WriteLine("Total fuel required: " + totalFuelRequired.ToString());

            // Day 1, Part 2.
            numberOfLines = 0;
            totalFuelRequired = 0;
            foreach (string line in lines) {
                int value = Convert.ToInt32(line);
                int fuelRequired = FuelRequiredNested(value); 
                numberOfLines++;
                totalFuelRequired += fuelRequired;
                Console.WriteLine("\t" + line + "; fuel required: " + fuelRequired.ToString());
            }

            Console.WriteLine("Total number of lines: " + numberOfLines.ToString());
            Console.WriteLine("Total fuel required, nested: " + totalFuelRequired.ToString());
        }

        static void Day2() {

            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day2_input.txt";
            string content = System.IO.File.ReadAllText(fileName);

            // Some of the example test cases.
            //string content = "1,0,0,0,99";
            //string content = "1,1,1,4,99,5,6,0,99";

            string[] opCodes = content.Split(",");

            // Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) to the "1202 program alarm" state 
            // it had just before the last computer caught fire. To do this, before running the program, replace position 1 with the value 12 and replace
            // position 2 with the value 2. What value is left at position 0 after the program halts?
            opCodes[1] = "12";
            opCodes[2] = "2";

            // Display the file contents.
            System.Console.WriteLine("Contents of file = ");

            // Day 2, Part 1.
            foreach (string opCode in opCodes) {
                Console.WriteLine("\t" + opCode);
            }
            int numberOfOpCodes = opCodes.Length;
            Console.WriteLine("Total number of opCodes: " + numberOfOpCodes.ToString());

            bool keepGoing = true;
            int valueLocationA, valueLocationB, valueLocationC;
            int valueA, valueB, valueC;

            for (int i=0; i<numberOfOpCodes; i=i+4) {

                if (keepGoing) {

                    switch (opCodes[i]) {

                        case "1":

                            // Opcode 1 adds together numbers read from two positions and stores the result in a third position.The three integers immediately after the opcode
                            // tell you these three positions -the first two indicate the positions from which you should read the input values, and the third indicates the position
                            // at which the output should be stored.
                            valueLocationA = Convert.ToInt32(opCodes[i + 1]);
                            valueLocationB = Convert.ToInt32(opCodes[i + 2]);
                            valueLocationC = Convert.ToInt32(opCodes[i + 3]);

                            valueA = Convert.ToInt32(opCodes[valueLocationA]);
                            valueB = Convert.ToInt32(opCodes[valueLocationB]);
                            valueC = (valueA + valueB);
                            opCodes[valueLocationC] = valueC.ToString();

                            break;

                        case "2":

                            // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again, the three integers after the opcode indicate
                            // where the inputs and outputs are, not their values.
                            valueLocationA = Convert.ToInt32(opCodes[i + 1]);
                            valueLocationB = Convert.ToInt32(opCodes[i + 2]);
                            valueLocationC = Convert.ToInt32(opCodes[i + 3]);

                            valueA = Convert.ToInt32(opCodes[valueLocationA]);
                            valueB = Convert.ToInt32(opCodes[valueLocationB]);
                            valueC = valueA * valueB;
                            opCodes[valueLocationC] = (valueC).ToString();

                            break;

                        case "99":

                            keepGoing = false;
                            break;

                        default:
                            break;
                    }

                }
            }

            Console.WriteLine("Final list:");
            foreach (string opCode in opCodes) {
                Console.WriteLine("\t" + opCode);
            }

            Console.WriteLine("Final value in position 0: " + opCodes[0]);
        }
    }
}
