using AdventOfCode2022.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022 {

    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2022\\data\\";

        static void Main(string[] args) {

            //Day1(); // completed.
            //Day2(); // completed.
            //Day3(); // completed.
            //Day4(); // completed.
            //Day5(); // completed.
            //Day6(); // completed.
            //Day7(); // completed.
            //Day8(); // completed.
            //Day9(); // completed.
            //Day10(); // completed.
            //Day11(); // not completed part 2.
            //Day12(); // test data complete; not completed part 1 & 2.
            //Day13(); // test data complete; not completed part 1 & 2.
            //Day14(); // completed.
            //Day15(); // completed part 1; not completed part 2.
            Day16();

            // Keep the console window open.
            Console.WriteLine("\nPress any key to exit.");
            System.Console.ReadKey();
        }

        static void Day1() {

            string fileName = dataFolderPath + "input_01.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            ArrayList elves = new ArrayList();
            int caloriesCount = 0;

            foreach (string line in lines) {
                if (line == "") {
                    elves.Add(caloriesCount);
                    caloriesCount = 0;
                }
                else {
                    int calories = Convert.ToInt32(line);
                    caloriesCount = caloriesCount + calories;
                }
            }
            elves.Add(caloriesCount);

            int max1CaloriesCount = 0;
            int max2CaloriesCount = 0;
            int max3CaloriesCount = 0;

            foreach (int elf in elves) {
                Console.WriteLine(elf);
                if (elf > max1CaloriesCount) {
                    max3CaloriesCount = max2CaloriesCount;
                    max2CaloriesCount = max1CaloriesCount;
                    max1CaloriesCount = elf;
                }
                else if (elf > max2CaloriesCount) {
                    max3CaloriesCount = max2CaloriesCount;
                    max2CaloriesCount = elf;
                }
                else if (elf > max3CaloriesCount) {
                    max3CaloriesCount = elf;
                }
            }

            // I have your answer for you.
            Console.WriteLine("Number of elves: " + elves.Count);
            Console.WriteLine("Max calories 1: " + max1CaloriesCount);
            Console.WriteLine("Max calories 2: " + max2CaloriesCount);
            Console.WriteLine("Max calories 3: " + max3CaloriesCount);
            Console.WriteLine("Total of top 3: " + (max1CaloriesCount + max2CaloriesCount + max3CaloriesCount));
        }

        static void Day2() {

            string fileName = dataFolderPath + "input_02.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            int totalScore = 0;
            int totalScore2 = 0;

            foreach (string line in lines) {
                RpsHand hand = new RpsHand(line);
                totalScore += hand.score();
                totalScore2 += hand.score2();
                hand.print();
            }

            // I have your answer for you.
            Console.WriteLine("total score: " + totalScore);
            Console.WriteLine("total score 2: " + totalScore2);
        }

        static void Day3() {

            string fileName = dataFolderPath + "input_03.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            // each line is one rucksack.
            // rucksack has two compartments.
            // each rucksack has a pile of stuff, one half in each compartment.
            // there is one thing that is illegally represented in both compartments.
            // that one thing has a score.  Tell me the score.

            int totalScore = 0;

            foreach (string line in lines) {

                Rucksack rucksack = new Rucksack(line);
                totalScore += rucksack.score();
                rucksack.print();
            }

            // I have your answer for you.
            Console.WriteLine("total score: " + totalScore);

            // Part 2.
            int countOfGroups = (lines.Length / 3);
            Console.WriteLine("count of groups: " + countOfGroups);
            int totalScore2 = 0;

            for (int i = 0; i < countOfGroups; i++) {

                // Figure out, in each group of 3, what is the one item that is common amongst all three.  

                Rucksack rucksack1 = new Rucksack(lines[(i * 3) + 0]);
                Rucksack rucksack2 = new Rucksack(lines[(i * 3) + 1]);
                Rucksack rucksack3 = new Rucksack(lines[(i * 3) + 2]);

                Dictionary<string, int> contents1 = rucksack1.contents();
                Dictionary<string, int> contents2 = rucksack2.contents();
                Dictionary<string, int> contents3 = rucksack3.contents();

                bool foundIt = false;

                foreach (string key in contents1.Keys) {

                    if ((contents2.ContainsKey(key)) && (contents3.ContainsKey(key))) {
                        totalScore2 += Rucksack.ConvertStringToInt(key);
                        foundIt = true;
                        break;
                    }
                }

                if (!foundIt) { throw new Exception(); }
            }

            Console.WriteLine("total score 2: " + totalScore2);
        }

        static void Day4() {

            string fileName = dataFolderPath + "input_04.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            // each line is a pair of section ranges, looking like [2-4,6-8];

            int countOfWithins = 0;
            int countOfOverlaps = 0;

            foreach (string line in lines) {

                string[] ranges = line.Split(",");
                SectionRange range1 = new SectionRange(ranges[0]);
                SectionRange range2 = new SectionRange(ranges[1]);
                range1.print();
                range2.print();

                if (range1.isWithin(range2) || range2.isWithin(range1)) {
                    countOfWithins++;
                    Console.WriteLine("*** within!");
                }
                if (range1.overlaps(range2)) {
                    countOfOverlaps++;
                    Console.WriteLine("*** overlap!");
                }
            }

            // I have your answer for you.
            Console.WriteLine("total withins " + countOfWithins);
            Console.WriteLine("total overlaps: " + countOfOverlaps);
        }

        static void Day5() {

            string fileName = dataFolderPath + "input_05.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            // there are two sections in the input file, delimited by a blank line.
            // section 1: each column is a stack of crates, with the bottom row being an integer (column id)
            // section 2: a set of commands to move the top most crate from a column to another column

            //   Part One.
            CrateController crateController = new CrateController(lines);
            crateController.print();
            crateController.ExecuteCommands();
            crateController.print();

            // I have your answer for you.
            Console.WriteLine("top crates: " + crateController.TopCrates());

            //   Part Two.
            crateController = new CrateController(lines);
            crateController.print();
            crateController.ExecuteCommands2();
            crateController.print();

            // I have your answer for you.
            Console.WriteLine("top crates: " + crateController.TopCrates());
        }

        static void Day6() {

            string fileName = dataFolderPath + "input_06.txt";
            string line = File.ReadAllText(fileName);
            Console.WriteLine("Size of input string: " + line.Length);

            DataStream dataStream = new DataStream(line);
            dataStream.Print();

            // I have your answer for you.
            Console.WriteLine("start marker: " + dataStream.StartMarker());
            Console.WriteLine("message marker: " + dataStream.MessageMarker());
        }

        static void Day7() {

            string fileName = dataFolderPath + "input_07.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            FileSystem fileSystem = new FileSystem(lines);
            fileSystem.ParseCommands();

            // I have your answer for you.
            long size = fileSystem.root.size;
            Console.WriteLine("root size: " + size);
            Console.WriteLine("question one: " + fileSystem.QuestionOne());
            Console.WriteLine("question two: " + fileSystem.QuestionTwo());
        }

        static void Day8() {

            string fileName = dataFolderPath + "input_08.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            TreeMatrix treeMatrix = new TreeMatrix(lines);
            treeMatrix.Print();

            //// I have your answer for you.
            Console.WriteLine("visible tree count: " + treeMatrix.VisibleCount());
            Console.WriteLine("best scenic score: " + treeMatrix.CheckScenicScore());
        }

        static void Day9() {

            string fileName = dataFolderPath + "input_09.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            RopeMap ropeMap = new RopeMap(lines, 1);
            ropeMap.ExecuteCommands();
            ropeMap.Print();

            // I have your answer for you.
            Console.WriteLine("Q1: visited count: " + ropeMap.VisitedCount());

            ropeMap = new RopeMap(lines, 9);
            ropeMap.ExecuteCommands();
            ropeMap.Print();

            // I have your answer for you.
            Console.WriteLine("Q2: visited count: " + ropeMap.VisitedCount());
        }

        static void Day10() {

            string fileName = dataFolderPath + "input_10.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            VideoSignal videoSignal = new VideoSignal(lines);
            videoSignal.RunInstructions();

            //// I have your answer for you.
            Console.WriteLine("final value: " + videoSignal.registerX);
            Console.WriteLine("question one: " + videoSignal.QuestionOne());

            videoSignal = new VideoSignal(lines);
            videoSignal.RunInstructions(true);
        }

        static void Day11() {

            string fileName = dataFolderPath + "input_11_test.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            //// Question One.
            MonkeyManager monkeyManager = new MonkeyManager(lines);
            monkeyManager.Print();
            monkeyManager.StartMonkeyRounds(20);
            monkeyManager.Print();

            //// I have your answer for you.
            Console.WriteLine("question one: " + monkeyManager.AnswerTheQuestion());

            //// Question Two.
            monkeyManager = new MonkeyManager(lines);
            monkeyManager.Print();
            monkeyManager.StartMonkeyRounds(20, true);
            monkeyManager.Print();
            Console.WriteLine("question two: " + monkeyManager.AnswerTheQuestion());
        }


        static void Day12() {

            string fileName = dataFolderPath + "input_12.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            HillMap hillMap = new HillMap(lines);
            hillMap.Print();
            int shortestPath = hillMap.FindShortestPath(0, hillMap.start, hillMap.CreateHistoryMap(),
                hillMap.CreatePreCalcBestPath(null));

            Console.WriteLine("shortest path: " + shortestPath);
        }




        static void Day13() {

            string fileName = dataFolderPath + "input_13.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            SignalPacketManager signalPacketManager = new SignalPacketManager(lines);
            signalPacketManager.Print();

            int value = signalPacketManager.QuestionOne();
            Console.WriteLine("question one: " + value);
        }

        static void Day14() {

            string fileName = dataFolderPath + "input_14.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            // Question One.
            SandManager sandManager = new SandManager(lines);
            sandManager.Print();
            int value = sandManager.initiateSandPouring();
            Console.WriteLine("question one: " + value);

            // Question Two.
            sandManager = new SandManager(lines, true);
            sandManager.Print();
            value = sandManager.initiateSandPouring();
            sandManager.Print();

            Console.WriteLine("question two: " + value);
        }

        static void Day15() {

            string fileName = dataFolderPath + "input_15_test.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            // Question One.
            //SensorBeaconManager sensorBeaconManager = new SensorBeaconManager(lines, 2000000);
            SensorBeaconManager sensorBeaconManager = new SensorBeaconManager(lines, 10);
            sensorBeaconManager.Print();
            long value = sensorBeaconManager.QuestionOne();
            Console.WriteLine("question one: " + value);

            // Question Two.
            sensorBeaconManager = new SensorBeaconManager(lines, -1, 0, 20);
            value = sensorBeaconManager.QuestionTwo();
            Console.WriteLine("question one: " + value);

        }

        static void Day16() {

            string fileName = dataFolderPath + "input_16.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input: " + lines.Length);

            // Question One.
            ValveManager valveManager = new ValveManager(lines);
            valveManager.Print();
            int pressureReleased = valveManager.QuestionOne();
            //int bestPath = valveManager.StartPass();
            Console.WriteLine("question one: " + pressureReleased);
        }

    }
}
