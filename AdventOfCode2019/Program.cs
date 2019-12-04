using System;


namespace AdventOfCode2019 {

    class Program {

        static void Main(string[] args) {

            //Day1();
            //Day2();
            Day3();

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

        static string[] Day2_Intcode(string[] opCodesInput, string replacementValue1, string replacementValue2) {

            //string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day2_input.txt";
            //string content = System.IO.File.ReadAllText(fileName);

            // Some of the example test cases.
            //string content = "1,0,0,0,99";
            //string content = "1,1,1,4,99,5,6,0,99";

            //string[] opCodes = content.Split(",");
            string[] opCodes = (string[]) opCodesInput.Clone();

            // Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) to the "1202 program alarm" state 
            // it had just before the last computer caught fire. To do this, before running the program, replace position 1 with the value 12 and replace
            // position 2 with the value 2. What value is left at position 0 after the program halts?
            opCodes[1] = replacementValue1;
            opCodes[2] = replacementValue2;

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

            for (int i = 0; i < numberOfOpCodes; i = i + 4) {

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

            return opCodes;
        }

        static void Day2() {

            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day2_input.txt";
            string content = System.IO.File.ReadAllText(fileName);
            string[] opCodes = content.Split(",");

            // Day 2, part 1.
            string[] opCodesReturned = Day2_Intcode(opCodes, "12", "2");

            // Day 2, part 2.
            // This was my double-checking the results i got from the main routine below. 
            //opCodesReturned = Day2_Intcode(opCodes, "48", "47");

            //// Day 2, part 2.
            string targetValue = "19690720";
            int idealValue1 = -1;
            int idealValue2 = -1;

            for (int i = 0; i <= 99; i++) {
                for (int j = 0; j <= 99; j++) {
                    opCodesReturned = Day2_Intcode(opCodes, i.ToString(), j.ToString());
                    if (opCodesReturned[0] == targetValue) {
                        idealValue1 = i;
                        idealValue2 = j;
                        break;
                    }
                }
            }

            Console.WriteLine("value1: " + idealValue1.ToString() + "; value2: " + idealValue2.ToString());
            Console.WriteLine("answer: " + ((100 * idealValue1) + idealValue2).ToString());
        }


        static int[,] Day3_CreateArray(string[] pathList, int sizeOfConsole) {

            // figure out the size of the console.
            int centerPoint = (sizeOfConsole / 2);
            int currentPositionX = centerPoint;
            int currentPositionY = centerPoint;
            int maximumY = currentPositionY;
            int minimumY = currentPositionY;
            int maximumX = currentPositionX;
            int minimumX = currentPositionX;
            int[,] console = new int[sizeOfConsole, sizeOfConsole];
            int stepsCount = 0;

            foreach (string path in pathList) {

                char direction = path[0];
                int distance = Convert.ToInt32(path.Substring(1));
               // stepsCount++;
                Console.WriteLine("direction: " + direction + "; distance: " + distance.ToString());

                switch (direction) {

                    case 'U':
                        for (int i = 0; i < distance; i++) {
                            currentPositionY += 1;
                            console[currentPositionX, currentPositionY] = ++stepsCount;
                        }
                        break;

                    case 'D':
                        for (int i = 0; i < distance; i++) {
                            currentPositionY -= 1;
                            console[currentPositionX, currentPositionY] = ++stepsCount;
                        }
                        break;

                    case 'L':
                        for (int i = 0; i < distance; i++) {
                            currentPositionX -= 1;
                            console[currentPositionX, currentPositionY] = ++stepsCount;
                        }
                        break;

                    case 'R':
                        for (int i = 0; i < distance; i++) {
                            currentPositionX += 1;
                            console[currentPositionX, currentPositionY] = ++stepsCount;
                        }
                        break;

                    default:
                        Console.WriteLine("ERROR!!!!!");
                        break;
                }

                maximumX = (currentPositionX > maximumX ? currentPositionX : maximumX);
                minimumX = (currentPositionX < minimumX ? currentPositionX : minimumX);
                maximumY = (currentPositionY > maximumY ? currentPositionY : maximumY);
                minimumY = (currentPositionY < minimumY ? currentPositionY : minimumY);
            }

            Console.WriteLine("maximumX: " + maximumX.ToString());
            Console.WriteLine("minimumX: " + minimumX.ToString());
            Console.WriteLine("maximumY: " + maximumY.ToString());
            Console.WriteLine("minimumY: " + minimumY.ToString());
            Console.WriteLine("stepsCount: " + stepsCount.ToString());

            return console;
        }

        static void Day3() {

            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day3_input.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Provided test cases.
            //string[] lines = { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };  // Should be 159 distance and 610 steps.
            //string[] lines = { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }; // should be 135 distance and 410 steps.

            string[] path1 = lines[0].Split(",");
            string[] path2 = lines[1].Split(",");
            int sizeOfConsole = 20000;

            // Map out the wires onto the console, given the provided paths.
            int[,] wire1 = Day3_CreateArray(path1, sizeOfConsole);
            int[,] wire2 = Day3_CreateArray(path2, sizeOfConsole);
            int minimumDistance = int.MaxValue;
            int minimumSteps = int.MaxValue; ;
            int centerPoint = sizeOfConsole / 2;

            // Calculate both the shortest distance to an intersection, as well as shortest steps to an intersection.
            for (int i = 0; i < sizeOfConsole; i++) {
                for (int j = 0; j < sizeOfConsole; j++) {
                    if ((wire1[i, j] > 0) && (wire2[i, j] > 0)) {

                        Console.WriteLine("Intersection at " + i.ToString() + "," + j.ToString());

                        // Calculate distance, and store the value if it's the shortest so far.
                        int distanceX = Math.Abs(centerPoint - i);
                        int distanceY = Math.Abs(centerPoint - j);
                        int distance = distanceX + distanceY;
                        minimumDistance = (distance < minimumDistance ? distance : minimumDistance);

                        // Calculate step count, and store the value if it's the shortest so far.
                        int steps1 = wire1[i, j];
                        int steps2 = wire2[i, j];
                        int steps = steps1 + steps2;
                        minimumSteps = (steps < minimumSteps ? steps : minimumSteps);
                    }
                }
            }

            Console.WriteLine("Shortest Distance: " + minimumDistance.ToString());
            Console.WriteLine("Shortest Steps: " + minimumSteps.ToString());
        }
    }
}
