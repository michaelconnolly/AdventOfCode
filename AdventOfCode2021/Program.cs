using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;


// https://adventofcode.com/2021/

namespace AdventOfCode2021 {


    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2021\\data\\";


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
            Day12();

            // Keep the console window open.
            Console.WriteLine("\nPress any key to exit.");
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

            for (int i = (binaryNumber.Length - 1); i >= 0; i--) {

                if (binaryNumber[i] == '1') {

                    if (slot == 1) {
                        returnValue += 1;
                    }
                    else {
                        returnValue += (int)Math.Pow(2, (slot - 1));
                    }
                }

                slot++;
            }

            return returnValue;
        }


        static string[] discardAllBut(string[] source, int index, char filter) {

            ArrayList output = new ArrayList();

            for (int i = 0; i < source.Length; i++) {
                if (source[i][index] == filter) {
                    output.Add(source[i]);
                }
            }

            // I am dumb and don't know how to quickly convert an array to a string[];
            string[] realOutput = new string[output.Count];
            for (int i = 0; i < realOutput.Length; i++) {
                realOutput[i] = (string)output[i];
            }
            return realOutput;
        }


        //static string[] hackDiscardBlanks(string[] source) {

        //    int blankCount = 0;
        //    foreach (string line in source) {
        //        if (line == "") {
        //            blankCount++;
        //        }
        //    }

        //    if (blankCount >0) {
        //        string[] output = new string[source.Length - blankCount];
        //        int currentIndex = 0;
        //        foreach (string line in source) {
        //            if (line != "") {
        //                output[currentIndex] = line;
        //                currentIndex++;

        //            }
        //        }
        //        return output;
        //    }

        //    return source;
        //}

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


        static void Day4() {

            // Load data.
            string fileName = dataFolderPath + "input_day_04.txt";
            //string fileName = dataFolderPath + "input_day_04_test.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Day 4, Part 1.
            Console.WriteLine("Starting 4a ...");
            BingoFactory bingoFactory = new BingoFactory(lines);
            bool foundWinner = false;

            foreach (string calledNumber in bingoFactory.callableNumbers) {
                foreach (BingoCard bingoCard in bingoFactory.bingoCards) {

                    bool winner = bingoCard.callNumber(calledNumber);

                    if (winner) {
                        Console.Write("Winner! Card #" + bingoCard.id);
                        Console.WriteLine(" , Score: " + bingoCard.getScore());
                        foundWinner = true;
                    }
                }

                // Debugging.
                if (false) {
                    Console.WriteLine("Called number: " + calledNumber);
                    Console.WriteLine("");
                    foreach (BingoCard bingoCard1 in bingoFactory.bingoCards) {
                        bingoCard1.print();
                    }
                }

                if (foundWinner)
                    break;
            }

            // Day 4, Part 2.
            Console.WriteLine("\nStarting 4b ...");
            bingoFactory = new BingoFactory(lines);

            foreach (string calledNumber in bingoFactory.callableNumbers) {
                foreach (BingoCard bingoCard in bingoFactory.bingoCards) {

                    bool winner = bingoCard.callNumber(calledNumber);

                    // Debug
                    if (false) {
                        Console.WriteLine("Number: " + calledNumber + ", winner: " + winner + ", Open cards: " + bingoFactory.countOfOpenCards());
                        Console.WriteLine("");
                        foreach (BingoCard bingoCard1 in bingoFactory.bingoCards) {
                            bingoCard1.print();
                        }
                    }

                    if (bingoFactory.countOfOpenCards() == 0) {

                        Console.Write("Last Winner! Card #" + bingoCard.id);
                        Console.WriteLine(" , Score: " + bingoCard.getScore());
                        return;
                    }
                }
            }
        }


        static void Day5() {

            // Load data.
            string fileName = dataFolderPath + "input_day_05.txt";
            //string fileName = dataFolderPath + "input_day_05_test.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // 5a.
            OceanFloorMap map = new OceanFloorMap(lines);
            map.plotStaightLinesOnly();
            //map.print();
            Console.WriteLine("\n5a: Amount of overlaps (2 or more): " + map.countOfOverlaps(2));

            // 5b.
            map = new OceanFloorMap(lines);
            map.plotAllLines();
            //map.print();
            Console.WriteLine("\n5b: Amount of overlaps (2 or more): " + map.countOfOverlaps(2));
        }


        static long LetFishReproduce(System.Collections.Generic.List<LanternFish> lanternFishes, int days) {

            bool simpleImplementation = false;

            if (simpleImplementation) {

                for (int i = 0; i < days; i++) {
                    Console.WriteLine("day " + i + " ...");

                    List<LanternFish> newFishes = new List<LanternFish>();
                    foreach (LanternFish lanternFish in lanternFishes) {
                        if (lanternFish.timer == 0) {
                            lanternFish.timer = 6;
                            newFishes.Add(new LanternFish("8"));
                        }
                        else {
                            lanternFish.timer--;
                        }
                    }

                    lanternFishes.AddRange(newFishes);
                }

                return lanternFishes.Count;
            }

            // Scalable implementaton.
            else {

                long[] countofFishPerCohort = new long[9];

                // Map the amount of fish in each stage to our new summary array.
                foreach (LanternFish fish in lanternFishes) {
                    countofFishPerCohort[fish.timer]++;
                }

                for (int day = 0; day < days; day++) {

                    Console.WriteLine("day " + day + " ...");

                    // for any fish at timer=0, spawn and restart yourself.
                    long countOfMatureFishAtZero = countofFishPerCohort[0];

                    // drop the timer down for each cohort that is not at zero.
                    for (int i = 1; i < 9; i++) {
                        countofFishPerCohort[i - 1] = countofFishPerCohort[i];
                    }

                    // for any fish at timer=0, spawn and restart yourself.
                    countofFishPerCohort[6] += countOfMatureFishAtZero;
                    countofFishPerCohort[8] = countOfMatureFishAtZero;
                }

                long total = 0;
                for (int i = 0; i < 9; i++) {
                    total += countofFishPerCohort[i];
                }
                return total;
            }
        }


        static void Day6() {

            // Load data.
            string fileName = dataFolderPath + "input_day_06.txt";
            //string fileName = dataFolderPath + "input_day_06_test.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Fish data.
            List<LanternFish> lanternFishes = new List<LanternFish>();
            string[] fishData = lines[0].Split(',');
            foreach (string aFish in fishData) {
                lanternFishes.Add(new LanternFish(aFish));
            }

            // 6a.
            long count = LetFishReproduce(lanternFishes, 80);
            Console.WriteLine("6a: count of LanternFish after 80 days: " + count);

            // 6b.
            count = LetFishReproduce(lanternFishes, 256);
            Console.WriteLine("6b: count of LanternFish after 256 days: " + count);
        }


        static int CalculateCostOfMovingPositions(int start, int end) {

            int moves = Math.Abs(start - end);
            int costTotal = 0;
            int costIncrement = 0;

            for (int i = 0; i < moves; i++) {
                costIncrement++;
                costTotal += costIncrement;
            }

            return costTotal;
        }


        static void Day7() {

            // Load data.
            string fileName = dataFolderPath + "input_day_07.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            string[] crabPositions = lines[0].Split(',');

            // Let's figure out the max and min values, since the optional position is between them.
            int maxPos = int.MinValue;
            int minPos = int.MaxValue;
            foreach (string position in crabPositions) {
                int pos = Convert.ToInt32(position);
                if (pos > maxPos) maxPos = pos;
                if (pos < minPos) minPos = pos;
            }

            // 7a.
            // Let's figure out the minimal amount of moves to get everyone on same position.
            int bestPosition = -1;
            int minFuelSpent = int.MaxValue;
            for (int i = minPos; i <= maxPos; i++) {
                int currentFuelSpent = 0;
                foreach (string position in crabPositions) {
                    int pos = Convert.ToInt32(position);
                    currentFuelSpent += Math.Abs(i - pos);
                }
                if (currentFuelSpent < minFuelSpent) {
                    bestPosition = i;
                    minFuelSpent = currentFuelSpent;
                }
            }
            Console.WriteLine("7a: best position: " + bestPosition + ", fuel spent:  " + minFuelSpent);

            // 7b.
            // Let's figure out the minimal amount of moves to get everyone on same position.
            bestPosition = -1;
            minFuelSpent = int.MaxValue;
            for (int i = minPos; i <= maxPos; i++) {
                int currentFuelSpent = 0;
                foreach (string position in crabPositions) {
                    int pos = Convert.ToInt32(position);
                    //currentFuelSpent += Math.Abs(i - pos);
                    currentFuelSpent += CalculateCostOfMovingPositions(i, pos);
                }
                if (currentFuelSpent < minFuelSpent) {
                    bestPosition = i;
                    minFuelSpent = currentFuelSpent;
                }
            }
            Console.WriteLine("7b: best position: " + bestPosition + ", fuel spent:  " + minFuelSpent);
        }



        static void Day8() {

            // Load data.
            string fileName = dataFolderPath + "input_day_08.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            Collection<SevenSegmentDisplay> displays = new Collection<SevenSegmentDisplay>();
            foreach (string line in lines) {
                string[] lineSegments = line.Split(" | ");
                string[] tenValues = lineSegments[0].Split(' ');
                string[] outputCodes = lineSegments[1].Split(' ');
                displays.Add(new SevenSegmentDisplay(tenValues, outputCodes));
            }

            // 8a
            int count = 0;
            foreach (SevenSegmentDisplay display in displays) {
                count += display.countOfUniqueLengthCodes;
            }
            Console.WriteLine("8a: count of output codes with unique lengths: " + count);

            // 8b.
            int total = 0;
            foreach (SevenSegmentDisplay display in displays) {
                total += display.outputValue;
            }
            Console.WriteLine("8b: " + total);
        }


        static string coordinate(int x, int y) {
            return "(" + x + "," + y + ")";
        }


        static Collection<string> calculateBasin(int[,] map, int mapWidth, int mapHeight, int x, int y, Collection<string> basin) {

            basin.Add(coordinate(x, y));

            // Check up.
            if ((y != 0) && (map[x, y - 1] != 9) && (!basin.Contains(coordinate(x, y - 1))))
                calculateBasin(map, mapWidth, mapHeight, x, y - 1, basin);

            // Check down.
            if ((y != (mapHeight - 1) && (map[x, y + 1] != 9) && (!basin.Contains(coordinate(x, y + 1)))))
                calculateBasin(map, mapWidth, mapHeight, x, y + 1, basin);

            // Check left.
            if ((x != 0) && (map[x - 1, y] != 9) && (!basin.Contains(coordinate(x - 1, y))))
                calculateBasin(map, mapWidth, mapHeight, x - 1, y, basin);

            // Check down.
            if ((x != (mapWidth - 1) && (map[x + 1, y] != 9) && (!basin.Contains(coordinate(x + 1, y)))))
                calculateBasin(map, mapWidth, mapHeight, x + 1, y, basin);

            return basin;
        }


        static void Day9() {

            // Load data.
            string fileName = dataFolderPath + "input_day_09.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            int mapWidth = lines[0].Length;
            int mapHeight = lines.Length;
            int[,] map = new int[mapWidth, mapHeight];
            for (int y = 0; y < mapHeight; y++) {
                string currentRow = lines[y];
                char[] currentRowValues = currentRow.ToCharArray();
                for (int x = 0; x < mapWidth; x++) {
                    map[x, y] = Convert.ToInt32(currentRowValues[x].ToString());
                }
            }

            // 9a: find low points.
            Collection<int> lowPoints = new Collection<int>();
            Collection<Collection<string>> basins = new Collection<Collection<string>>();

            for (int y = 0; y < mapHeight; y++) {
                for (int x = 0; x < mapWidth; x++) {

                    if (((y == 0) || (map[x, y] < map[x, y - 1])) &&  // above
                        ((y == (mapHeight - 1)) || (map[x, y] < map[x, y + 1])) &&  // below
                        ((x == 0) || (map[x, y] < map[x - 1, y])) &&  // left
                        ((x == (mapWidth - 1)) || (map[x, y] < map[x + 1, y]))) {  // right

                        lowPoints.Add(map[x, y]);

                        basins.Add(calculateBasin(map, mapWidth, mapHeight, x, y, new Collection<string>()));
                    }
                }
            }

            // Calculate risk level.
            int riskLevel = 0;
            foreach (int lowPoint in lowPoints) riskLevel += (1 + lowPoint);
            Console.WriteLine("9a: risklevel sum is " + riskLevel);

            // Poor man's sorting algorithm: just find top 3.
            int size1 = 0;
            int size2 = 0;
            int size3 = 0;
            foreach (Collection<string> basin in basins) {
                int count = basin.Count;
                Console.WriteLine("9b: basin size: " + count);
                if (count > size1) {
                    size3 = size2;
                    size2 = size1;
                    size1 = count;
                }
                else if (count > size2) {
                    size3 = size2;
                    size2 = count;
                }
                else if (count > size3) {
                    size3 = count;
                }
            }
            Console.WriteLine("9b: product of top 3 basin sizes: " + (size1 * size2 * size3));
            return;
        }


        static void Day10() {

            // Load data.
            string fileName = dataFolderPath + "input_day_10.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Our "globals" for this exercise.
            int totalErrorCheckingScore = 0;
            List<long> autocorrectScores = new List<long>();

            // Parse each line, find corrupt and incomplete lines.
            foreach (string line in lines) {

                bool corrupt = false;
                Collection<char> expectedClosures = new Collection<char>();

                for (int i = 0; i < line.Length; i++) {

                    char currentChar = line[i];
                    if (corrupt) break;

                    switch (currentChar) {

                        case '(':
                            expectedClosures.Add(')');
                            break;
                        case '[':
                            expectedClosures.Add(']');
                            break;
                        case '<':
                            expectedClosures.Add('>');
                            break;
                        case '{':
                            expectedClosures.Add('}');
                            break;

                        case ')':
                        case ']':
                        case '>':
                        case '}':
                            if (currentChar != expectedClosures[expectedClosures.Count - 1]) {
                                corrupt = true;

                                if (currentChar == ')') totalErrorCheckingScore += 3;
                                if (currentChar == ']') totalErrorCheckingScore += 57;
                                if (currentChar == '}') totalErrorCheckingScore += 1197;
                                if (currentChar == '>') totalErrorCheckingScore += 25137;
                            }
                            else {
                                expectedClosures.RemoveAt(expectedClosures.Count - 1);
                            }
                            break;

                        default:
                            break;
                    }
                }

                // cleanup for this line.
                if (corrupt) {
                    Console.WriteLine("line " + line + " is corrupt!");
                }
                else if (expectedClosures.Count > 0) {
                    Console.WriteLine("line " + line + " is not complete.");

                    long autocorrectScore = 0;
                    for (int j = expectedClosures.Count - 1; j >= 0; j--) {
                        if (expectedClosures[j] == ')') autocorrectScore = (autocorrectScore * 5) + 1;
                        else if (expectedClosures[j] == ']') autocorrectScore = (autocorrectScore * 5) + 2;
                        else if (expectedClosures[j] == '}') autocorrectScore = (autocorrectScore * 5) + 3;
                        else if (expectedClosures[j] == '>') autocorrectScore = (autocorrectScore * 5) + 4;
                        else throw new Exception();
                    }
                    autocorrectScores.Add(autocorrectScore);

                }
                else {
                    Console.WriteLine("line " + line + " is OK.");
                }


            }

            // 10a.
            Console.WriteLine("10a: " + totalErrorCheckingScore);

            // 10b.
            foreach (long score in autocorrectScores) {
                Console.WriteLine("autocorrect score: " + score);
            }

            autocorrectScores.Sort();
            Console.WriteLine("middle: " + autocorrectScores[(((autocorrectScores.Count + 1) / 2) - 1)]);
        }


        static void Day11() {

            // Load data.
            string fileName = dataFolderPath + "input_day_11.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // iterations for 11a.
            OctopusMap octopusMap = new OctopusMap(lines);
            int iterations = 100;
            int countOfFlashes = 0;
            // octopusMap.print();
            for (int i = 0; i < iterations; i++) {
                countOfFlashes += octopusMap.oneStep();
                octopusMap.checkSynchronization(i);
                //Console.WriteLine("\nEnd of iteration " + (i+1) + ": \n");
                //octopusMap.print();
            }
            Console.WriteLine("11a: " + countOfFlashes);


            // iterations for 11b.
            octopusMap = new OctopusMap(lines);
            iterations = 1000;
            countOfFlashes = 0;
            //octopusMap.print();
            for (int i = 0; i < iterations; i++) {
                countOfFlashes += octopusMap.oneStep();
                octopusMap.checkSynchronization(i);
                //Console.WriteLine("\nEnd of iteration " + (i+1) + ": \n");
                //octopusMap.print();
            }

            Console.WriteLine("11b: " + (octopusMap.firstSynchronization + 1));
        }


        static int CountOfInstances(Collection<string> collection, string termToSearchFor) {

            int count = 0;

            foreach (string item in collection) {
                if (item == termToSearchFor) count++;
            }

            return count;
        }


        static Collection<string> findChoices(string[,] paths, string node, Collection<string> previousChoices, string smallCaveTwice = null) {

            Collection<string> newChoicesProposed = new Collection<string>();
            Collection<string> newChoicesFinal = new Collection<string>();

            // Find the raw choices.
            for (int i = 0; i < (paths.Length / 2); i++) {
                if (paths[i, 0] == node) newChoicesProposed.Add(paths[i, 1]);
                else if (paths[i, 1] == node) newChoicesProposed.Add(paths[i, 0]);
            }

            foreach (string newChoice in newChoicesProposed) {
                if (!(Char.IsLower(newChoice[0]) && previousChoices.Contains(newChoice))) {
                    newChoicesFinal.Add(newChoice);
                }
                else if ((Char.IsLower(newChoice[0])) && (smallCaveTwice == newChoice) && (2 > CountOfInstances(previousChoices, smallCaveTwice))) {
                    newChoicesFinal.Add(newChoice);
                }
            }

            return newChoicesFinal;
        }


        static bool IsThisCollectionAlreadyHere(Collection<Collection<string>> allLegalPaths, Collection<string> proposedLegalPath) {

            foreach (Collection<string> legitLegalPath in allLegalPaths) {

                if (legitLegalPath.Count == proposedLegalPath.Count) {

                    bool same = true;
                    for (int i = 0; i < legitLegalPath.Count; i++) {
                        if (legitLegalPath[i] != proposedLegalPath[i]) {
                            same = false;
                            break;
                        }
                    }
                    if (same) return same;
                }
            }
            return false;
        }


        static Collection<Collection<string>> FindLegalPaths(string[,] paths, Collection<string> pathSoFar, Collection<Collection<string>> allLegalPaths, string smallCaveTwice = null) {

            string currentNode = pathSoFar[pathSoFar.Count - 1];
            Collection<string> nextChoices = findChoices(paths, currentNode, pathSoFar, smallCaveTwice);

            foreach (string nextChoice in nextChoices) {

                // to be safe, make a copy.
                Collection<string> legalPath = new Collection<string>();
                foreach (string node in pathSoFar) legalPath.Add(node);
                legalPath.Add(nextChoice);

                // did we reach the end.
                if (nextChoice == "end") {

                    if (!IsThisCollectionAlreadyHere(allLegalPaths, legalPath)) {
                        allLegalPaths.Add(legalPath);
                    }
                }
                else {

                    allLegalPaths = FindLegalPaths(paths, legalPath, allLegalPaths, smallCaveTwice);
                }
            }

            return allLegalPaths;
        }


        static void Day12() {

            // Load data.
            string fileName = dataFolderPath + "input_day_12.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            string[,] paths = new string[lines.Length, 2];
            for (int i = 0; i < lines.Length; i++) {
                string[] currentPath = lines[i].Split('-');
                paths[i, 0] = currentPath[0];
                paths[i, 1] = currentPath[1];
            }

            // 12a.
            Collection<string> pathSoFar = new Collection<string>();
            Collection<Collection<string>> allLegalPaths = new Collection<Collection<string>>();
            pathSoFar.Add("start");
            allLegalPaths = FindLegalPaths(paths, pathSoFar, allLegalPaths);
            Console.WriteLine("12a: count of legal paths: " + allLegalPaths.Count);

            // Debugging.
            if (false) {
                foreach (Collection<string> path in allLegalPaths) {
                    foreach (string step in path) {
                        Console.Write(step);
                        Console.Write(",");
                    }
                    Console.WriteLine("");
                }
            }

            // 12b.
            pathSoFar = new Collection<string>();
            allLegalPaths = new Collection<Collection<string>>();
            pathSoFar.Add("start");

            // Figure out which small caves exist.
            Collection<string> smallCaves = new Collection<string>();
            for (int i = 0; i < (paths.Length / 2); i++) {
                string first = paths[i, 0];
                string second = paths[i, 1];
                if (first != "start" && first != "end" && Char.IsLower(first[0])) {
                    if (!smallCaves.Contains(first)) smallCaves.Add(first);
                }
                if (second != "start" && second != "end" && Char.IsLower(second[0])) {
                    if (!smallCaves.Contains(second)) smallCaves.Add(second);
                }
            }

            foreach (string smallCave in smallCaves) {
                allLegalPaths = FindLegalPaths(paths, pathSoFar, allLegalPaths, smallCave);
            }

            Console.WriteLine("12b: count of legal paths: " + allLegalPaths.Count);

            // Debug.
            if (false) {
                foreach (Collection<string> path in allLegalPaths) {
                    foreach (string step in path) {
                        Console.Write(step);
                        Console.Write(",");
                    }
                    Console.WriteLine("");
                }
            }

            return;
        }
    }
}
