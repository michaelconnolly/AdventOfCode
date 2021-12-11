using System;
using System.Collections;


// https://adventofcode.com/2021/

namespace AdventOfCode2021 {


    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2021\\data\\";


        static void Main(string[] args) {

            //Day1();
            //Day2();
            Day3();

            // Keep the console window open.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }


        static int readNumber(string[] list, int index) {
            return Convert.ToInt32(list[index]);
        }


        static int countValuePerColumn(string[] list, int columnIndex, char charToCount) {

            int totalCount = 0;

            foreach (string rawValue in list) {

                char currentChar = rawValue[columnIndex];
                if (currentChar == charToCount) {
                    totalCount++;
                }
            }

            return totalCount;
        }


        static int convertBinaryToDecimal(string binaryNumber) {

            int returnValue = 0;
            int slot = 1;

            for (int i = (binaryNumber.Length -1); i>=0; i--) {

                if (binaryNumber[i] == '1') {

                    if (slot == 1) {
                        returnValue += 1;
                    }
                    else {
                        returnValue += (int) Math.Pow(2, (slot-1));
                    }
                }

                slot++;
            }

            return returnValue;
        }


        static string[] discardAllBut(string[] source, int index, char filter) {

            ArrayList output = new ArrayList();

            for (int i=0; i<source.Length; i++) {
                if (source[i][index] == filter) {
                    output.Add(source[i]);
                }
            }

            // I am dumb and don't know how to quickly convert an array to a string[];
            string[] realOutput = new string[output.Count];
            for (int i=0; i<realOutput.Length; i++) {
                realOutput[i] = (string) output[i];
            }
            return realOutput;
        }

        static void Day1() {

            string fileName = dataFolderPath + "input_day_01.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Debug check.
            Console.WriteLine("Size of input array: " + lines.Length);

            // Day 1, Part 1.
            int countOfDepthIncreases = 0;
            int lastDepthReading = -1;
            foreach (string line in lines) {
                int currentDepthReading = Convert.ToInt32(line);
                if (lastDepthReading != -1) {
                    if (currentDepthReading > lastDepthReading) {
                        countOfDepthIncreases++;
                    }
                }
                lastDepthReading = currentDepthReading;
            }

            Console.WriteLine("1a: count of depth increases: " + countOfDepthIncreases);

            // Day 1, Part 2.
            // Since we are computing 3 measurements at a time, start at the 3rd, i.e., i=2;
            lastDepthReading = -1;
            countOfDepthIncreases = 0;
            for (int i = 2; i < lines.Length; i++) {

                int currentDepthReading = readNumber(lines, i - 2) + readNumber(lines, i - 1) + readNumber(lines, i);
                //Console.WriteLine(currentDepthReading);

                if (lastDepthReading != -1) {
                    if (currentDepthReading > lastDepthReading) {
                        countOfDepthIncreases++;
                    }
                }
                lastDepthReading = currentDepthReading;
            }

            Console.WriteLine("1b: count of depth increases: " + countOfDepthIncreases);
        }


        static void Day2() {

            string fileName = dataFolderPath + "input_day_02.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Debug check.
            Console.WriteLine("Size of input array: " + lines.Length);

            // Day 2, Part 1.
            int currentHorizontal = 0;
            int currentDepth = 0;
            foreach (string line in lines) {

                string[] commandData = line.Split(' ');
                string command = commandData[0];
                int distance = readNumber(commandData, 1);

                switch (command) {

                    case "forward":

                        currentHorizontal += distance;
                        break;

                    case "down":

                        currentDepth += distance;
                        break;

                    case "up":

                        currentDepth -= distance;
                        break;

                    default:

                        Console.WriteLine("error!");
                        throw new Exception();
                }
            }

            Console.Write("2a: horizontal: " + currentHorizontal);
            Console.Write(", depth: " + currentDepth);
            Console.WriteLine(", product: " + (currentDepth * currentHorizontal));

            // Day 2, Part 2.
            currentHorizontal = 0;
            currentDepth = 0;
            int currentAim = 0;
            foreach (string line in lines) {

                string[] commandData = line.Split(' ');
                string command = commandData[0];
                int distance = readNumber(commandData, 1);

                switch (command) {

                    case "forward":
                        // forward X does two things:
                        // It increases your horizontal position by X units.
                        // It increases your depth by your aim multiplied by X.
                        currentHorizontal += distance;
                        currentDepth += (currentAim * distance);
                        break;

                    case "down":
                        // down X increases your aim by X units.
                        currentAim += distance;
                        break;

                    case "up":
                        // up X decreases your aim by X units.
                        currentAim -= distance;
                        break;

                    default:

                        Console.WriteLine("error!");
                        throw new Exception();
                }
            }

            Console.Write("2b: horizontal: " + currentHorizontal);
            Console.Write(", depth: " + currentDepth);
            Console.WriteLine(", product: " + (currentDepth * currentHorizontal));
        }


        static void Day3() {

            string fileName = dataFolderPath + "input_day_03.txt";
            //string fileName = dataFolderPath + "input_day_03_test.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int lengthOfInputFile = lines.Length;

            // Debug check.
            Console.WriteLine("Size of input array: " + lengthOfInputFile);

            // Day 3, Part 1.

            // Each bit in the gamma rate can be determined by finding the most common bit in the corresponding 
            // position of all numbers in the diagnostic report.
            // The epsilon rate is calculated in a similar way; rather than use the most common bit, 
            // the least common bit from each position is used.

            // Let's assume that the length of each line is the same as the first line.
            int lengthOfInputValue = lines[0].Length;

            string gammaValue = "";
            string epsilonValue = "";

            for (int i = 0; i < lengthOfInputValue; i++) {

                int countOfZeros = countValuePerColumn(lines, i, '0');
                if ((countOfZeros) > (lengthOfInputFile / 2)) {

                    gammaValue += '0';
                    epsilonValue += '1';
                }
                else {
                    gammaValue += '1';
                    epsilonValue += '0';
                }
            }

            int testValue = convertBinaryToDecimal("10110"); // Expect 22.
            Console.WriteLine("Test case: expect 22: actual: " + testValue);

            int gammaRate = convertBinaryToDecimal(gammaValue);
            int epsilonRate = convertBinaryToDecimal(epsilonValue);

            Console.Write("3a: gamma: " + gammaRate);
            Console.Write(", epsilon: " + epsilonRate);
            Console.WriteLine(", product: " + (gammaRate * epsilonRate));

            // Day 3, Part 2.
            int oxygenGeneratorRating = 0;
            int co2scrubberRating = 0;
            int lifeSupportRating;
          
            // To find oxygen generator rating, determine the most common value(0 or 1) in the current bit position, 
            // and keep only numbers with that bit in that position. If 0 and 1 are equally common, keep values with
            // a 1 in the position being considered.
            string[] newLines = (string[])lines.Clone();
            bool foundIt = false;
            int lengthOfRemainder = lengthOfInputFile;
            for (int currentBit = 0; currentBit < lengthOfInputValue; currentBit++) {

                int count = countValuePerColumn(newLines, currentBit, '0');
                if (count > (lengthOfRemainder / 2)) {
                    newLines = discardAllBut(newLines, currentBit, '0');
                }
                else {
                    newLines = discardAllBut(newLines, currentBit, '1');
                }

                lengthOfRemainder = newLines.Length;
                if (lengthOfRemainder == 1) {
                    foundIt = true;
                    oxygenGeneratorRating = convertBinaryToDecimal(newLines[0]);
                    break;
                }
            }
            if (!foundIt) throw new Exception();

            // To find CO2 scrubber rating, determine the least common value(0 or 1) in the current bit position,
            // and keep only numbers with that bit in that position. If 0 and 1 are equally common, keep values
            // with a 0 in the position being considered.
            newLines = (string[])lines.Clone();
            foundIt = false;
            lengthOfRemainder = lengthOfInputFile;
            for (int currentBit = 0; currentBit < lengthOfInputValue; currentBit++) {

                int count = countValuePerColumn(newLines, currentBit, '0');
                if (count <= (lengthOfRemainder / 2)) {
                    newLines = discardAllBut(newLines, currentBit, '0');
                }
                else {
                    newLines = discardAllBut(newLines, currentBit, '1');
                }

                lengthOfRemainder = newLines.Length;
                if (lengthOfRemainder == 1) {
                    foundIt = true;
                    co2scrubberRating = convertBinaryToDecimal(newLines[0]);
                    break;
                }
            }
            if (!foundIt) throw new Exception();

            // Finally, to find the life support rating, multiply the oxygen generator rating (23) by the CO2 scrubber rating (10) to get 230.
            lifeSupportRating = oxygenGeneratorRating * co2scrubberRating;

            Console.Write("3b: oxygenGeneratorRating: " + oxygenGeneratorRating);
            Console.Write(", co2scrubberRating: " + co2scrubberRating);
            Console.WriteLine(", lifeSupportRating: " + lifeSupportRating);
        }
    }
}

