using System;
using System.Collections.Generic;

namespace AdventOfCode2019 {

    class Program {

        static void Main(string[] args) {

            //Day1();
            //Day2();
            //Day3();
            //Day4();
            //Day5();
            //Day6();
            //Day7();
            //Day8();
            //Day9();
            //Day10();
            //Day11();
            //Day12();
            //Day13();
            Day14();

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
            string[] opCodes = (string[])opCodesInput.Clone();

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

        static bool Day4_isValidCombination(string combination, bool includePart2Criteria = false) {

            // It is a six - digit number.
            // The value is within the range given in your puzzle input.
            // Two adjacent digits are the same(like 22 in 122345).
            // Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
            // Part 2 Criteria: the two adjacent matching digits are not part of a larger group of matching digits.

            if (combination.Length != 6) return false;

            bool foundSameDigitsAdjacent = false;
            bool digitsNeverDecrease = true;
            int part2_digitsInRowCount = 1;
            bool part2_foundAtLeastOnePair = false;

            char lastDigit = combination[0];
            for (int i = 1; i < (combination.Length); i++) {

                // Is the current digit the same as the one before it?
                char currentDigit = combination[i];
                if (currentDigit == lastDigit) {
                    foundSameDigitsAdjacent = true;
                    part2_digitsInRowCount++;

                    // special case the last character.
                    if ((i == (combination.Length - 1)) && (part2_digitsInRowCount == 2)) {
                        part2_foundAtLeastOnePair = true;
                    }
                }
                else {
                    if (part2_digitsInRowCount == 2) {
                        part2_foundAtLeastOnePair = true;
                    }
                    part2_digitsInRowCount = 1;
                }

                // Is the current digit smaller than the one before it? 
                if (Convert.ToInt32(lastDigit) > Convert.ToInt32(currentDigit)) {

                    digitsNeverDecrease = false;
                }

                lastDigit = currentDigit;
            }

            if (!foundSameDigitsAdjacent) return false;

            if (!digitsNeverDecrease) return false;

            if (includePart2Criteria) {
                if (!part2_foundAtLeastOnePair) return false;
            }

            return true;
        }

        static bool Day4_TestValue(string combination, bool expectedAnswer, bool includePart2Criteria = false) {

            bool isValid = Day4_isValidCombination(combination, includePart2Criteria);
            Console.WriteLine(combination + ": " + isValid.ToString() + "; expected: " + expectedAnswer.ToString());

            return isValid;
        }

        static void Day4() {

            // Day 4 Part 1: Test cases.
            Day4_TestValue("111111", true);  // 111111 meets these criteria(double 11, never decreases).
            Day4_TestValue("223450", false);  // 223450 does not meet these criteria(decreasing pair of digits 50).
            Day4_TestValue("123789", false);  // 123789 does not meet these criteria(no double).
            Day4_TestValue("111123", true);  // true
            Day4_TestValue("135679", false);  // false

            // Day 4 part 1: Real range of 125730-579381; replace these values with whatever you have been assigned.
            int startValue = 125730;
            int endValue = 579381;
            int validCombinations = 0;

            for (int i = startValue; i <= endValue; i++) {

                if (Day4_isValidCombination(i.ToString())) {
                    validCombinations++;
                }
            }

            Console.WriteLine("Part 1: Amount of valid combinations in the range of " + startValue.ToString() + "-" + endValue.ToString() + ": " + validCombinations.ToString());

            // Day 4 Part 2: Test Cases.
            Day4_TestValue("112233", true, includePart2Criteria: true); // 112233 meets these criteria because the digits never decrease and all repeated digits are exactly two digits long.
            Day4_TestValue("123444", false, includePart2Criteria: true); // 123444 no longer meets the criteria(the repeated 44 is part of a larger group of 444).
            Day4_TestValue("111122", true, includePart2Criteria: true); // 111122 meets the criteria(even though 1 is repeated more than twice, it still contains a double 22).
            Day4_TestValue("111779", true, includePart2Criteria: true);  // true
            Day4_TestValue("135779", true, includePart2Criteria: true);
            Day4_TestValue("111779", true, includePart2Criteria: true);
            Day4_TestValue("555777", false, includePart2Criteria: true);

            // Day 4 part 2: Real range.
            validCombinations = 0;

            for (int i = startValue; i <= endValue; i++) {

                if (Day4_isValidCombination(i.ToString(), includePart2Criteria: true)) {
                    validCombinations++;
                }
            }

            Console.WriteLine("Part 2: Amount of valid combinations in the range of " + startValue.ToString() + "-" + endValue.ToString() + ": " + validCombinations.ToString());
        }


        static int Day5_GetValue(string[] instructions, int index, int parameterMode) {

            //int value1;
            int parameterInt = Convert.ToInt32(instructions[index]);
            if (parameterMode == 0) {
                return Convert.ToInt32(instructions[parameterInt]);
            }
            else {
                return parameterInt;
            }
        }

        static string[] Day5_Intcode(string[] instructionsInput, string input) {

            // Opcodes(like 1, 2, or 99) mark the beginning of an instruction. The values used immediately after an opcode, if any, are called the instruction's
            // parameters. For example, in the instruction 1,2,3,4, 1 is the opcode; 2, 3, and 4 are the parameters. The instruction 99 contains only an opcode
            // and has no parameters.

            string[] instructions = (string[])instructionsInput.Clone();
            string output = "";

            // Display the file contents.
            Console.WriteLine("Contents of file = ");
            foreach (string opCode in instructions) {
                Console.WriteLine("\t" + opCode);
            }
            int numberOfOpCodes = instructions.Length;
            Console.WriteLine("Total number of opCodes: " + numberOfOpCodes.ToString());

            bool keepGoing = true;
            int i = 0;
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

                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        value2 = Day5_GetValue(instructions, (i + 2), parameterMode2);
                        value3Location = Convert.ToInt32(instructions[i + 3]);
                        value3 = value1 + value2;
                        instructions[value3Location] = (value3).ToString();

                        i += 4;
                        break;

                    case 2:

                        // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again, the three integers after the opcode indicate
                        // where the inputs and outputs are, not their values.

                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        value2 = Day5_GetValue(instructions, (i + 2), parameterMode2);
                        value3Location = Convert.ToInt32(instructions[i + 3]);
                        value3 = value1 * value2;
                        instructions[value3Location] = (value3).ToString();

                        i += 4;
                        break;

                    case 3:

                        // Opcode 3 takes a single integer as input and saves it to the address given by its only parameter.For example, the instruction 3,50 would take an input value and store it at address 50.
                        // We should never get a parameterMode1 == 1 for case 3.
                        if (parameterMode1 == 1) {
                            Console.WriteLine("ERROR! Received parameterMode==1 in OpCode 3.  Ignoring.");
                        }

                        int valueLocation1 = Convert.ToInt32(instructions[(i + 1)]);
                        instructions[valueLocation1] = input;

                        i += 2;
                        break;

                    case 4:

                        // Opcode 4 outputs the value of its only parameter.For example, the instruction 4,50 would output the value at address 50.
                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        output = value1.ToString();

                        i += 2;
                        break;

                    case 5:

                        // Opcode 5 is jump -if-true: if the first parameter is non - zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.
                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        value2 = Day5_GetValue(instructions, (i + 2), parameterMode2);
                        if (value1 != 0) {
                            i = value2;
                        }
                        else {
                            i += 3;
                        }

                        break;

                    case 6:

                        // Opcode 6 is jump -if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.
                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        value2 = Day5_GetValue(instructions, (i + 2), parameterMode2);
                        if (value1 == 0) {
                            i = value2;
                        }
                        else {
                            i += 3;
                        }

                        break;

                    case 7:

                        // Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        value2 = Day5_GetValue(instructions, (i + 2), parameterMode2);
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
                        value1 = Day5_GetValue(instructions, (i + 1), parameterMode1);
                        value2 = Day5_GetValue(instructions, (i + 2), parameterMode2);
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
                        break;
                }
            }

            Console.WriteLine("Final list:");
            foreach (string opCode in instructions) {
                Console.WriteLine("\t" + opCode);
            }

            Console.WriteLine("Input: " + input);
            Console.WriteLine("Output: " + output);
            Console.WriteLine("Final value in position 0: " + instructions[0]);

            return instructions;
        }

        static void Day5() {

            string content;
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day5_input.txt";
            content = System.IO.File.ReadAllText(fileName);

            // Test cases.
            // content = "1002,4,3,4,33"; // should just exit.
            // content = "3,0,4,0,99";  // Should output whatever you input.
            // content = "1101,100,-1,4,0"; // Should just exit. 
            // content = "1101,34,53,0,99"; // Should write 87 into 0 position.
            // content = "1102,34,53,0,99"; // Should write 1802 into 0 position.
            // content = "103,0,99"; // Should get an ignorable error and writes the 'input' to 0 position;
            // content = "104,56,99"; // Should output to 'output' the value 56.
            // content = "4,2,99"; // Should output 99 to 'output'.

            // Test cases for part 2.
            // content = "3,9,8,9,10,9,4,9,99,-1,8"; // - Using position mode, consider whether the input is equal to 8; output 1(if it is) or 0(if it is not).
            // content = "3,9,7,9,10,9,4,9,99,-1,8"; // - Using position mode, consider whether the input is less than 8; output 1(if it is) or 0(if it is not).
            // content = "3,3,1108,-1,8,3,4,3,99"; // - Using immediate mode, consider whether the input is equal to 8; output 1(if it is) or 0(if it is not).
            // content = "3,3,1107,-1,8,3,4,3,99"; // - Using immediate mode, consider whether the input is less than 8; output 1(if it is) or 0(if it is not).
            // content = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9"; // take an input, then output 0 if the input was zero or 1 if the input was non-zero
            // content = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1"; //  take an input, then output 0 if the input was zero or 1 if the input was non-zero
            // content = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99"; // input instruction to ask for a single number. The program will then output 999 if the input value is below 8, output 1000 if the input value is equal to 8, or output 1001 if the input value is greater than 8.

            string[] instructions = content.Split(",");
            string input = "5";

            // Day 5 - actual running of program.
            string[] opCodesReturned = Day5_Intcode(instructions, input);
        }

        static void Day6() {

            string[] orbitList;
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day6_input.txt";
            orbitList = System.IO.File.ReadAllLines(fileName);

            //string orbitListRaw = "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L";
            //string orbitListRaw = "COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L,K)YOU,I)SAN"; // should be 4.
            //string orbitListRaw = "COM)K,K)YOU,K)SAN"; // should be 0.
            // string orbitListRaw = "COM)A,COM)B,A)YOU,B)SAN"; // should be 2.
            // string orbitListRaw = "COM)A,COM)B,A)F,A)YOU,F)SAN"; // should be 1
            //string orbitListRaw = "COM)A,A)C,C)E,E)G,G)I,E)YOU,I)SAN"; // should be 2.
            //string orbitListRaw = "COM)A,A)C,C)E,E)G,G)I,C)YOU,I)SAN"; // should be 3.
            //orbitList = orbitListRaw.Split(",");

            Dictionary<string, Day6_Object> dict = new Dictionary<string, Day6_Object>();

            foreach (string orbit in orbitList) {

                string[] objects = orbit.Split(')');
                string parent = objects[0];
                string child = objects[1];

                Day6_Object parentObject;
                if (!dict.ContainsKey(parent)) {

                    parentObject = new Day6_Object(parent, null);
                    dict[parent] = parentObject;

                    // I think this shouldn't ever happen, skipping if it happens but warn me.
                    if (parent != "COM") {
                        Console.WriteLine("ERROR! We ran into a parent object that isn't COM that we hadn't yet run into.");
                    }
                }
                else {
                    parentObject = dict[parent];
                }

                Day6_Object childObject;
                if (!dict.ContainsKey(child)) {

                    childObject = new Day6_Object(child, parentObject);
                    dict[child] = childObject;
                }
                else {

                    childObject = dict[child];
                    childObject.AddParent(parentObject);
                    // I think this shouldn't ever happen, skippit if it happens but warn me.
                    Console.WriteLine("WARNING! We ran into a child object that was already referenced.");
                }
            }

            int totalOrbitCount = 0;

            foreach (string item in dict.Keys) {

                Console.WriteLine(dict[item].description);
                totalOrbitCount += dict[item].indirectOrbitalChildrenCount;
            }

            Console.WriteLine("TOTAL: " + totalOrbitCount.ToString());

            // Part 2.
            Day6_Object you = dict["YOU"];
            Day6_Object santa = dict["SAN"];

            Console.WriteLine(you.description);
            Console.WriteLine(santa.description);

            int distance = you.orbitalParent.DoYouHavePathTo(santa, you);
            Console.WriteLine("distance: " + distance.ToString());
        }

        static object[] CalculateMaximumOutput(string[] instructions, int startPhaseSetting, bool feedbackMode) {

            int maxSignal = int.MinValue;
            string[] maxPhaseSettings = new string[] { };

            for (int a = startPhaseSetting; a < startPhaseSetting + 5; a++) {
                for (int b = startPhaseSetting; b < startPhaseSetting + 5; b++) {
                    for (int c = startPhaseSetting; c < startPhaseSetting + 5; c++) {
                        for (int d = startPhaseSetting; d < startPhaseSetting + 5; d++) {
                            for (int e = startPhaseSetting; e < startPhaseSetting + 5; e++) {

                                // if this collection of phaseSettings unique, i.e., each setting only used once?
                                string[] phaseSettings = new string[] { a.ToString(), b.ToString(), c.ToString(), d.ToString(), e.ToString() };
                                HashSet<string> phaseSettingsSet = new HashSet<string>(phaseSettings);
                                bool isUnique = phaseSettings.Length == phaseSettingsSet.Count;

                                if (isUnique) {

                                    AmplifierSeries amplifiers = new AmplifierSeries(instructions, phaseSettings);
                                    string output;

                                    if (!feedbackMode) {
                                        output = amplifiers.Run("0");
                                    }
                                    else {
                                        output = amplifiers.RunFeedbackLoop("0");
                                    }
                                    int outputValue = Convert.ToInt32(output);

                                    //if (outputValue > maxSignal) {
                                    if ((outputValue > maxSignal) && outputValue != -2) {

                                        maxSignal = outputValue;
                                        maxPhaseSettings = phaseSettings;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new object[] { maxSignal, maxPhaseSettings };
        }

        static void Day7() {

            string content;
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day7_input.txt";
            content = System.IO.File.ReadAllText(fileName);
            string[] phaseSettings;
            string[] instructions = content.Split(",");

            // Test cases for part1.
            //content = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0"; // Max thruster signal 43210 (from phase setting sequence 4,3,2,1,0)
            //phaseSettings = new string[] { "4", "3", "2", "1", "0" };
            // --
            //content = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"; // Max thruster signal 54321 (from phase setting sequence 0,1,2,3,4)
            //phaseSettings = new string[] { "0", "1", "2", "3", "4" };
            // --
            //content = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0"; // Max thruster signal 65210 (from phase setting sequence 1,0,4,3,2)
            //phaseSettings = new string[] { "1", "0", "4", "3", "2" };
            //instructions = content.Split(",");

            // Calculate maximum output based on algorithm defined in day 7 part 1.
            object[] outputs = CalculateMaximumOutput(instructions, 0, false);
            int maxSignal = (int)outputs[0];
            string[] maxPhaseSettings = (string[])outputs[1];

            // Spit out summary.
            Console.WriteLine("Day 7, Part 1:");
            Console.WriteLine("maximum signal: " + maxSignal.ToString());
            Console.Write("maximum phase settings: ");
            foreach (string phaseSetting in maxPhaseSettings) {
                Console.Write(" " + phaseSetting);
            }
            Console.WriteLine("");

            // **********************
            // Test cases for part 2.
            // **********************
            //content = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5"; // Max thruster signal 139629729 (from phase setting sequence 9,8,7,6,5)
            //phaseSettings = new string[] { "9", "8", "7", "6", "5" };
            //content = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";  // Max thruster signal 18216 (from phase setting sequence 9,7,8,5,6)
            //phaseSettings = new string[] { "9", "7", "8", "5", "6" };
            //instructions = content.Split(",");

            // Test phase: just try one series.
            //AmplifierSeries amplifiers = new AmplifierSeries(instructions, phaseSettings);
            //string outputTest = amplifiers.RunFeedbackLoop("0");
            //Console.WriteLine("Output of part 2 test: " + outputTest);

            // Calculate maximum output based on algorithm defined in day 7 part 2.
            outputs = CalculateMaximumOutput(instructions, 5, true);
            maxSignal = (int)outputs[0];
            maxPhaseSettings = (string[])outputs[1];

            // Spit out summary.
            Console.WriteLine("Day 7, Part 2:");
            Console.WriteLine("maximum signal: " + maxSignal.ToString());
            Console.Write("maximum phase settings: ");
            foreach (string phaseSetting in maxPhaseSettings) {
                Console.Write(" " + phaseSetting);
            }
            Console.WriteLine("");
        }

        static void Day8() {

            string content;
            int imageWidth;
            int imageHeight;

            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day8_input.txt";
            content = System.IO.File.ReadAllText(fileName);
            imageWidth = 25;
            imageHeight = 6;

            // Test cases for part1.
            //content = "123456789012";
            //imageWidth = 3;
            //imageHeight = 2;

            // test case for part 2.
            //content = "0222112222120000";
            //imageWidth = 2;
            //imageHeight = 2;

            // Create new image from our 3 inputs.
            Image image = new Image(content, imageWidth, imageHeight);

            // Which layer in the image has the least zero's? 
            ImageLayer layerWithLeastZeros = image.WhichImageHasLessOf('0');

            // Testing.
            image.PrintOut();

            // Output for Part 1.
            Console.WriteLine("Day 8, Part 1:");
            int countOfOnes = layerWithLeastZeros.HowMany('1');
            int countOfTwos = layerWithLeastZeros.HowMany('2');
            int answer = countOfOnes * countOfTwos;
            Console.WriteLine("\tCount of one's: " + countOfOnes);
            Console.WriteLine("\tCount of two's: " + countOfTwos);
            Console.WriteLine("\tAnswer: " + answer);

            // Part 2.
            Console.WriteLine("Day 8, Part 2:");
            ImageLayer visibleLayer = image.VisibleLayer();
            visibleLayer.PrintOutJustWhite();
        }

        public static void SetValue(string[] instructions, string value, long index, int parameterMode, long relativeBase) {

            long indexToUse = index;

            if (parameterMode == 2) {
                indexToUse += relativeBase;
            }

            instructions[indexToUse] = value;
        }

        public static long GetValue(string[] instructions, long index, int parameterMode, long relativeBase) {

            long parameterInt = Convert.ToInt64(instructions[index]);

            if (parameterMode == 0) {
                return Convert.ToInt64(instructions[parameterInt]);
            }
            else if (parameterMode == 1) {
                return parameterInt;
            }
            else if (parameterMode == 2) {
                // Parameters in mode 2, relative mode, behave very similarly to parameters in position mode: the parameter is interpreted as a position.
                // Like position mode, parameters in relative mode can be read from or written to.
                // The important difference is that relative mode parameters don't count from address 0. Instead, they count from a value called 
                // the relative base. The relative base starts at 0.
                // The address a relative mode parameter refers to is itself plus the current relative base.When the relative base is 0, relative mode 
                // parameters and position mode parameters with the same value refer to the same address.
                long address = relativeBase + parameterInt;
                return Convert.ToInt64(instructions[address]);
            }
            else {
                Console.WriteLine("ERROR!  Unknown parameter mode!");
                return -1;
            }
        }

        static string Day9_IntcodeProgram(string[] instructionsInput, string input) {

            // The computer's available memory should be much larger than the initial program. Memory beyond the initial program starts with
            // the value 0 and can be read or written like any other memory. (It is invalid to try to access memory at a negative address, though.)
            //string[] instructions = (string[])instructionsInput.Clone();
            string[] instructions = new string[10000];
            for (int index = 0; index < instructions.Length; index++) {
                instructions.SetValue("0", index);
            }
            for (int index = 0; index < instructionsInput.Length; index++) {
                instructions.SetValue(instructionsInput[index], index);
            }

            string output = "";
            long i = 0;
            long relativeBase = 0;
            bool keepGoing = true;

            while (keepGoing) {

                // Grab opCode
                string instruction = instructions[i].PadLeft(5, '0');
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

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        value2 = GetValue(instructions, (i + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[i + 3]);
                        value3 = value1 + value2;
                        SetValue(instructions, value3.ToString(), value3Location, parameterMode3, relativeBase);

                        i += 4;
                        break;

                    case 2:

                        // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. Again, the three integers after the opcode indicate
                        // where the inputs and outputs are, not their values.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        value2 = GetValue(instructions, (i + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[i + 3]);
                        value3 = value1 * value2;
                        SetValue(instructions, value3.ToString(), value3Location, parameterMode3, relativeBase);

                        i += 4;
                        break;

                    case 3:

                        // Opcode 3 takes a single integer as input and saves it to the address given by its only parameter.For example, the instruction 3,50 would take an input value and store it at address 50.
                        // We should never get a parameterMode1 == 1 for case 3.

                        if (parameterMode1 == 1) {
                            Console.WriteLine("ERROR! Received parameterMode==1 in OpCode 3.  Ignoring.");
                        }

                        long valueLocation1 = Convert.ToInt32(instructions[(i + 1)]);
                        SetValue(instructions, input, valueLocation1, parameterMode1, relativeBase);

                        i += 2;
                        break;

                    case 4:

                        // Opcode 4 outputs the value of its only parameter.For example, the instruction 4,50 would output the value at address 50.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        output = value1.ToString();

                        // If we are in feedback loop mode, tell our AmplifierSeries parent that we changed our output.
                        //int setting = Convert.ToInt32(this.phaseSetting);
                        //if (setting >= 5 && setting <= 9) {
                        //    this.amplifierSeries.AmplifierChangedOutput(this, this.outputSignal);
                        //}

                        i += 2;
                        break;

                    case 5:

                        // Opcode 5 is jump -if-true: if the first parameter is non - zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        value2 = GetValue(instructions, (i + 2), parameterMode2, relativeBase);
                        if (value1 != 0) {
                            i = value2;
                        }
                        else {
                            i += 3;
                        }

                        break;

                    case 6:

                        // Opcode 6 is jump -if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        value2 = GetValue(instructions, (i + 2), parameterMode2, relativeBase);
                        if (value1 == 0) {
                            i = value2;
                        }
                        else {
                            i += 3;
                        }

                        break;

                    case 7:

                        // Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        value2 = GetValue(instructions, (i + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[(i + 3)]);

                        if (value1 < value2) {
                            SetValue(instructions, "1", value3Location, parameterMode3, relativeBase);
                        }
                        else {
                            SetValue(instructions, "0", value3Location, parameterMode3, relativeBase);
                        }

                        i += 4;
                        break;

                    case 8:

                        //  Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.
                        // Otherwise, it stores 0.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        value2 = GetValue(instructions, (i + 2), parameterMode2, relativeBase);
                        value3Location = Convert.ToInt32(instructions[(i + 3)]);
                        if (value1 == value2) {
                            SetValue(instructions, "1", value3Location, parameterMode3, relativeBase);
                        }
                        else {
                            SetValue(instructions, "0", value3Location, parameterMode3, relativeBase);
                        }

                        i += 4;
                        break;

                    case 9:

                        // Opcode 9 adjusts the relative base by the value of its only parameter. 
                        // The relative base increases (or decreases, if the value is negative) by the value of the parameter.

                        value1 = GetValue(instructions, (i + 1), parameterMode1, relativeBase);
                        relativeBase += Convert.ToInt32(value1);

                        i += 2;
                        break;

                    case 99:

                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("ERROR: illegal opcode: " + opCode);
                        break;
                }
            }

            return output;
        }

        static void Day9() {

            string content;
            string input;
            string output;

            // Main content to run through the program.
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day9_input.txt";
            content = System.IO.File.ReadAllText(fileName);
            input = "1";

            // Test cases for part1.
            //content = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99"; // takes no input and produces a copy of itself as output.
            //content = "1102,34915192,34915192,7,4,7,99,0"; // should output a 16 - digit number.
            //content = "104,1125899906842624,99"; //  should output the large number in the middle.

            //content = "109,2000,3,1985,109,19,204,-34,99"; //For example, if the relative base is 2000, then after the instruction 109,19, the relative base would be 2019.If the next instruction were 204,-34, then the value at address 1985 would be output.
            //input = "69";

            string[] instructions = content.Split(",");

            // Output for Part 1.
            output = Day9_IntcodeProgram(instructions, input);
            Console.WriteLine("Day 9, Part 1:");
            Console.WriteLine("\tInput: " + input);
            Console.WriteLine("\tOutput: " + output);

            // Part 2.
            input = "2";
            output = Day9_IntcodeProgram(instructions, input);
            Console.WriteLine("Day 8, Part 2:");
            Console.WriteLine("\tInput: " + input);
            Console.WriteLine("\tOutput: " + output);
        }


        public static int gcd(int aInput, int bInput) {

            int a = Math.Abs(aInput);
            int b = Math.Abs(bInput);

            if (a == 0 || b == 0) {
                return 1;
            }

            while (a != b)
                if (a < b) b = b - a;
                else a = a - b;
            //since at this point a=b, the gcd can be either of them
            //it is necessary to pass the gcd to the main function
            //Console.WriteLine(aInput + ";" + bInput + ": " + a);
            return (a);
        }


        static void Day10() {

            string contentRaw;
            string[] content;
            int bestRow;
            int bestCol;

            // Main content to run through the program.
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day10_input.txt";
            content = System.IO.File.ReadAllLines(fileName);

            // Test cases for part1.
            //contentRaw = ".#..#\n.....\n#####\n....#\n...##"; // 3,4 because it can detect 8 asteroids
            //contentRaw = ".##.#\n..#..\n#####\n..#.#\n..###";
            //contentRaw = "#.........\n...#......\n...#..#...\n.####....#\n..#.#.#...\n.....#....\n..###.#.##\n.......#..\n....#...#.\n...#..#..#";
            //contentRaw = "......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####"; // Best is 5,8 with 33 other asteroids detected
            //contentRaw = "#.#...#.#.\n.###....#.\n.#....#...\n##.#.#.#.#\n....#.#.#.\n.##..###.#\n..#...##..\n..##....##\n......#...\n.####.###."; // Best is 1,2 with 35 other asteroids detected
            //contentRaw = ".#..#..###\n####.###.#\n....###.#.\n..###.##.#\n##.##.#.#.\n....###..#\n..#.#..#.#\n#..#.#.###\n.##...##.#\n.....#.#.."; // Best is 6,3 with 41 other asteroids detected
            //contentRaw = ".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##"; // Best is 11,13 with 210 other asteroids detected:
            // Test data for Part 2.
            //contentRaw = ".#....#####...#..\n##...##.#####..##\n##...#...#.#####.\n..#.....#...###..\n..#.#.....#....##";
            //content = contentRaw.Split("\n");

            // Create asteroidMap.
            AsteroidMap asteroidMap = new AsteroidMap(content, null);
            asteroidMap.PrintOut();

            // Part 1.
            Console.WriteLine("Day 10, Part 1:");
            asteroidMap.BestLocationForStation(out bestRow, out bestCol);
            AsteroidMap visibilityMap = asteroidMap.VisibilityMap(bestRow, bestCol);
            visibilityMap.PrintOut();

            // Part 2.
            Console.WriteLine("Day 10, Part 2:");
            Console.WriteLine("Total amount of asteroids: " + asteroidMap.AsteroidCount());
            List<Asteroid> asteroids = new List<Asteroid>();

            //asteroidMap.BestLocationForStation(out bestRow, out bestCol);

            //AsteroidMap visibilityMap = asteroidMap.VisibilityMap(bestRow, bestCol);
            //visibilityMap.PrintOut();

            // Logic for Part2:
            // Until all asteroids are destroyed:
            // 12 o'clock. any visible asteroid where col = 0 and row is negative.
            // Between 12 and 6: all visible asteroids that have a positive col: destroy in ascending row/col order.
            // 6 o'clock. any visible asteroid where col = 0 and row is positive.
            // Between 6 adn 12: all visibile asteroids that have a negative col: destroy in ascending row/col order.

            int asteroidCount = asteroidMap.AsteroidCount();

            while (asteroidCount > 1) {

                // Get the current list of visibile asteroids in the right order, and add them to our list.
                List<Asteroid> asteroidsThisWave = visibilityMap.ListOfVisibleAsteroids();
                asteroids.AddRange(asteroidsThisWave);

                // Destroy all visibile asteroids, and reset invisibility asteroids to be plain asteroids.
                visibilityMap.DestroyVisibileAndReset();

                // Reset the count of remaining asteroids.
                asteroidCount = visibilityMap.AsteroidCount();

                // Figure out the next wave of visibile and invisible asteroids. 
                visibilityMap = visibilityMap.VisibilityMap(bestRow, bestCol);
                visibilityMap.PrintOut();
            }

            // Print out list of asteroids destroyed in order.
            for (int i = 0; i < asteroids.Count; i++) {
                Console.WriteLine("asteroid " + (i + 1) + ": value: " + asteroids[i].col + "," + asteroids[i].row);
            }

            // The Elves are placing bets on which will be the 200th asteroid to be vaporized. Win the bet by determining 
            // which asteroid that will be; what do you get if you multiply its X coordinate by 100 and then add its Y 
            // coordinate ? (For example, 8,2 becomes 802.)
            int x = asteroids[199].col;
            int y = asteroids[199].row;
            int myBet = (x * 100) + y;
            Console.WriteLine("Answer: " + myBet);
        }


        static void Day11() {

            string content;
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day11_input.txt";
            content = System.IO.File.ReadAllText(fileName);
            string[] instructions = content.Split(",");

            // Part 1.
            RobotPainter robotPainter = new RobotPainter(instructions);
            Dictionary<string, string> robotCommands = robotPainter.RunTheRobot("0");
            Console.WriteLine("Unique coordinates count: " + robotCommands.Keys.Count);

            // Part 2.
            robotPainter = new RobotPainter(instructions);
            robotCommands = robotPainter.RunTheRobot("1");
            Console.WriteLine("Unique coordinates count: " + robotCommands.Keys.Count);
            robotPainter.Paint(robotCommands);
        }

        // I needed an LCM function, so i found this on the internet.  
        // Seemed like a poor use of my time to implement something standard like this.
        public static long lcm_of_array_elements(long[] element_array) {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true) {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++) {

                    // lcm_of_array_elements (n1, n2, ... 0) = 0. 
                    // For negative number we convert into 
                    // positive and calculate lcm_of_array_elements. 
                    if (element_array[i] == 0) {
                        return 0;
                    }
                    else if (element_array[i] < 0) {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1) {
                        counter++;
                    }

                    // Divide element_array by devisor if complete 
                    // division i.e. without remainder then replace 
                    // number with quotient; used for find next factor 
                    if (element_array[i] % divisor == 0) {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number 
                // from array multiply with lcm_of_array_elements 
                // and store into lcm_of_array_elements and continue 
                // to same divisor for next factor finding. 
                // else increment divisor 
                if (divisible) {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else {
                    divisor++;
                }

                // Check if all element_array is 1 indicate  
                // we found all factors and terminate while loop. 
                if (counter == element_array.Length) {
                    return lcm_of_array_elements;
                }
            }
        }

        static void Day12() {

            int totalSteps;
            List<Moon> moons = new List<Moon>();

            // My inputs:
            //< x = -10, y = -13, z = 7 >
            // < x = 1, y = 2, z = 1 >
            // < x = -15, y = -3, z = 13 >
            //   < x = 3, y = 7, z = -4 >
            Moon moon1 = new Moon(-10, -13, 7, moons);
            Moon moon2 = new Moon(1, 2, 1, moons);
            Moon moon3 = new Moon(-15, -3, 13, moons);
            Moon moon4 = new Moon(3, 7, -4, moons);
            totalSteps = 1000;

            // Test Data 1:
            // <x=-1, y=0, z=2>
            // < x = 2, y = -10, z = -7 >
            // < x = 4, y = -8, z = 8 >
            // < x = 3, y = 5, z = -1 >
            //Moon moon1 = new Moon(-1, 0, 2, moons);
            //Moon moon2 = new Moon(2, -10, -7, moons);
            //Moon moon3 = new Moon(4, -8, 8, moons);
            //Moon moon4 = new Moon(3, 5, -1, moons);
            //totalSteps = 10;

            // Test Data 2:
            // <x=-8, y=-10, z=0>
            // < x = 5, y = 5, z = 10 >
            // < x = 2, y = -7, z = 3 >
            // < x = 9, y = -8, z = -3 >
            //Moon moon1 = new Moon(-8, -10, 0, moons);
            //Moon moon2 = new Moon(5, 5, 10, moons);
            //Moon moon3 = new Moon(2, -7, 3, moons);
            //Moon moon4 = new Moon(9, -8, -3, moons);
            //totalSteps = 100;

            // Create copies for part2.
            List<Moon> moons2 = new List<Moon>();
            foreach (Moon moon in moons) {
                Moon moonNew = new Moon(moon.positionX, moon.positionY, moon.positionZ, moons2);
            }
            List<Moon> moons2x = new List<Moon>();
            foreach (Moon moon in moons) {
                Moon moonNew = new Moon(moon.positionX, moon.positionY, moon.positionZ, moons2x);
            }
            List<Moon> moons2y = new List<Moon>();
            foreach (Moon moon in moons) {
                Moon moonNew = new Moon(moon.positionX, moon.positionY, moon.positionZ, moons2y);
            }
            List<Moon> moons2z = new List<Moon>();
            foreach (Moon moon in moons) {
                Moon moonNew = new Moon(moon.positionX, moon.positionY, moon.positionZ, moons2z);
            }

            // Part1!
            Moon.PrintOutMoons(moons, 0);
            for (int i = 0; i < totalSteps; i++) {
                Moon.ApplyGravity(moons);
                Moon.ApplyVelocity(moons);
                //Moon.PrintOutMoons(moons, (i + 1));
                //Console.WriteLine("Total energy in system: " + Moon.TotalEnergy(moons));
            }

            Console.WriteLine("Part 1:");
            Moon.PrintOutMoons(moons, totalSteps);
            int totalEnergy = Moon.TotalEnergy(moons);
            Console.WriteLine("Total energy in system: " + totalEnergy);

            // Part 2!  Brute Force!
            //bool keepGoing = true;
            //long stepCount = 1;
            //while (keepGoing) {
            //    Moon.ApplyGravity(moons2);
            //    Moon.ApplyVelocity(moons2);

            //    if (Moon.BackToInitialStates(moons2)) {
            //        keepGoing = false;
            //    }
            //    else {
            //        stepCount++;
            //    }
            //}

            // Part 2: New Math!
            // Note: i died on the hill trying to do this brute force, and after 48 hours running, i gave up that i could get this to work.
            // after strugging for a weekend, i gave up and went to reddit for a hint on how to do this.  So, public disclosure that
            // i needed help with this one. 
            bool keepGoing = true;
            long stepCountX = 1;
            while (keepGoing) {
                Moon.ApplyGravity(moons2x);
                Moon.ApplyVelocity(moons2x);

                if (Moon.BackToInitialStatesX(moons2x)) {
                    keepGoing = false;
                }
                else {
                    stepCountX++;
                }
            }
            keepGoing = true;
            long stepCountY = 1;
            while (keepGoing) {
                Moon.ApplyGravity(moons2y);
                Moon.ApplyVelocity(moons2y);

                if (Moon.BackToInitialStatesY(moons2y)) {
                    keepGoing = false;
                }
                else {
                    stepCountY++;
                }
            }
            keepGoing = true;
            long stepCountZ = 1;
            while (keepGoing) {
                Moon.ApplyGravity(moons2z);
                Moon.ApplyVelocity(moons2z);

                if (Moon.BackToInitialStatesZ(moons2z)) {
                    keepGoing = false;
                }
                else {
                    stepCountZ++;
                }
            }

            Console.Write("Part 2:");
            // Console.WriteLine("Steps required to return to initial state: " + stepCount);

            stepCountX++;
            stepCountY++;
            stepCountZ++;
            long[] myList = { stepCountX, stepCountY, stepCountZ };
            Console.WriteLine("stepCountX: " + stepCountX);
            Console.WriteLine("stepCountY: " + stepCountY);
            Console.WriteLine("stepCountZ: " + stepCountZ);

            Console.WriteLine("Steps required to return to initial state: " + lcm_of_array_elements(myList));
        }

        static void Day13() {

            string content;
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day13_input.txt";
            content = System.IO.File.ReadAllText(fileName);

            // if the last character is a \n, get rid of it.
            int length = content.Length;
            if (content.EndsWith('\n')) {
                content = content.Substring(0, length - 1);
            }

            string[] instructions = content.Split(",");

            ArcadeCabinet ac = new ArcadeCabinet(instructions);
            Dictionary<string, string> tiles = ac.PlayTheGame(interactiveMode: false);

            // Part 1.
            int countBlockTiles = 0;
            foreach (string key in tiles.Keys) {
                if (tiles[key] == "2") countBlockTiles++;
            }

            Console.WriteLine("Part 1: how many unique tiles are on the screen? " + countBlockTiles);

            // Part 2.
            ArcadeCabinet ac2 = new ArcadeCabinet(instructions);
            ac2.MakeItPlayForFree();
            ac2.PlayTheGame(interactiveMode: true);
        }

        public static void Assert(bool f, string warning) {
            if (!f) Console.WriteLine(warning);
        }

        static void Day14() {

            string[] content;
            string fileName = "c:\\dev\\AdventOfCode\\AdventOfCode2019\\day14_input.txt";
            content = System.IO.File.ReadAllLines(fileName);

            // Test Data.
            //string[] content = { "1 A, 2 B, 3 C => 2 D" };
            //string[] content = { "10 ORE => 10 A", "1 ORE => 1 B", "7 A, 1 B => 1 C", "7 A, 1 C => 1 D", "7 A, 1 D => 1 E", "7 A, 1 E => 1 FUEL" }; // Answer: 31
            //string[] content = { "10 A, 10 B => 1 FUEL", "2 ORE => 1 A", "1 ORE => 1 B" }; // 30
            //string[] content = { "10 C, 10 D => 1 FUEL", "2 ORE => 1 A", "1 ORE => 1 B", "5 A => 1 C", "1 B => 1 D" }; // 110
            //string[] content = {"9 ORE => 2 A", "8 ORE => 3 B","7 ORE => 5 C","3 A, 4 B => 1 AB","5 B, 7 C => 1 BC","4 C, 1 A => 1 CA","2 AB, 3 BC, 4 CA => 1 FUEL" }; // Answer 165
            //string[] content = { "157 ORE => 5 NZVS", "165 ORE => 6 DCFZ", "44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL", "12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ", "179 ORE => 7 PSHF", "177 ORE => 5 HKGWZ", "7 DCFZ, 7 PSHF => 2 XJWVT", "165 ORE => 2 GPVTF", "3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT" }; // 13312
            //string[] content = { "2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG","17 NVRVD, 3 JNWZP => 8 VPVL","53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL","22 VJHF, 37 MNCFX => 5 FWMGM","139 ORE => 4 NVRVD","144 ORE => 7 JNWZP","5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC","5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV","145 ORE => 6 MNCFX","1 NVRVD => 8 CXFTF","1 VJHF, 6 MNCFX => 4 RFSQX","176 ORE => 6 VJHF" }; // 180697 
            //string[] content = { "171 ORE => 8 CNZTR", "7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL", "114 ORE => 4 BHXH", "14 VRPVC => 6 BMBT", "6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL", "6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT", "15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW", "13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW", "5 BMBT => 4 WPTQ", "189 ORE => 9 KTJDG", "1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP", "12 VRPVC, 27 CNZTR => 2 XDBXC", "15 KTJDG, 12 BHXH => 5 XCVML", "3 BHXH, 2 VRPVC => 7 MZWV", "121 ORE => 7 VRPVC", "7 XCVML => 6 RJRHP", "5 BHXH, 4 VRPVC => 5 LTCX" }; // 2210736 

            // Create the factory with the desired recipes. 
            NanoFactory factory = new NanoFactory(content);
            foreach (NanoFactoryRecipe recipe in factory.recipes) {
                recipe.PrintOut();
            }

            // Initialize materialBag.  Later, we don't want to have to check to see if keys are in there, to optimize loops, so initialize all to zero.
            Dictionary<string, long> materialBag = new Dictionary<string, long>();
            foreach (NanoFactoryRecipe recipe in factory.recipes) {
                materialBag[recipe.output.name] = 0;
            }
            factory.PrintCollection(materialBag, null);

            // PART ONE.
            long oreCost = factory.ProduceMaterial("FUEL", 1, materialBag);
            Console.WriteLine("Part1: ORE count: " + oreCost);
            factory.PrintCollection(materialBag, null);

            // PART TWO.
            // The 13312 ORE - per - FUEL example could produce 82892753 FUEL.
            // The 180697 ORE - per - FUEL example could produce 5586022 FUEL.
            // The 2210736 ORE - per - FUEL example could produce 460664 FUEL.
            long oreBinAmount = 1000000000000;
            factory.RemoveFromCollection("FUEL", 1, materialBag);
            long fuelCount = 0;
            bool shouldContinue = true;

            long tickAmount = 100; // Used for debug messaging. 
            long tickSize = oreBinAmount / tickAmount;
            long nextMessage = oreBinAmount - tickSize;
            long tickCount = 1;

            while (shouldContinue) {

                long currentOreCost = factory.ProduceMaterial("FUEL", 1, materialBag);
                if (currentOreCost > oreBinAmount) {
                    shouldContinue = false;
                }
                else {
                    oreBinAmount -= currentOreCost;
                    fuelCount += 1;
                    factory.RemoveFromCollection("FUEL", 1, materialBag);

                    if (oreBinAmount < nextMessage) {
                        Console.WriteLine("TickCount: " + tickCount++ + ", Fuel: " + fuelCount + ", Ore: " + oreBinAmount);
                        nextMessage -= tickSize;
                    }
                }
            }

            Console.WriteLine("Total Fuel Produced: " + fuelCount);
            factory.PrintCollection(materialBag, null);
        }


        static void Day15() {

            string content;
            string fileName = "C:\\dev\\AdventOfCode\\AdventOfCode2019\\day15_input.txt";
            content = System.IO.File.ReadAllText(fileName);

            // if the last character is a \n, get rid of it.
            int length = content.Length;
            if (content.EndsWith('\n')) {
                content = content.Substring(0, length - 1);
            }

            string[] instructions = content.Split(",");
        }
    }
}
