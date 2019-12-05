using System;


namespace AdventOfCode2019 {

    class Program {

        static void Main(string[] args) {

            //Day1();
            //Day2();
            //Day3();
            //Day4();
            Day5();

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

        static bool Day4_isValidCombination(string combination, bool includePart2Criteria=false) {

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
            for (int i=1; i< (combination.Length); i++) {

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

        static bool Day4_TestValue(string combination, bool expectedAnswer, bool includePart2Criteria=false) {

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

            for (int i=startValue; i <= endValue; i++) {

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
    }
}
