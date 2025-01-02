using AdventOfCode2024.classes;
using System;
using System.IO;


namespace AdventOfCode2024 {

    internal class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2024\\data\\";

        static void Main(string[] args) {

            //Day01();
            //Day02();
            //Day03();
            //Day04();
            //Day05();
            //Day06();
            //Day07();
            //Day08();
            //Day09();
            //Day10();
            //Day11();
            //Day12();
            //Day13();
            //Day14();
            Day15();

            // Keep the console window open.
            Console.WriteLine("\nPress any key to exit.");
            System.Console.ReadKey();
        }

        static string[] GetInputData(string fileName) {

            string[] lines = File.ReadAllLines(dataFolderPath + fileName);
            Console.WriteLine("Size of input array: " + lines.Length);
            return lines;
        }

        static string GetInputDataAll(string fileName) {

            string line = File.ReadAllText(dataFolderPath + fileName);
            Console.WriteLine("Size of input text: " + line.Length);
            line = line.Trim('\n');
            return line;
        }

        static void Day01() {

            string[] lines = GetInputData("input_01.txt");
            LocationLists locationLists = new LocationLists(lines);

            Console.WriteLine("Distance score: " + locationLists.DistanceSum());
            Console.WriteLine("Similarity score: " + locationLists.SimilaritySum());
        }

        static void Day02() {

            string[] lines = GetInputData("input_02.txt");
            SafetyReportManager safetyReportManager = new SafetyReportManager(lines);

            Console.WriteLine("Safe count: " + safetyReportManager.SafeCount());
            Console.WriteLine("Safe count (with problem dampener): " + safetyReportManager.SafeCountWithDampener());

        }

        static void Day03() {

            string input = GetInputDataAll("input_03.txt");
            MathProgram multiplicationProgram = new MathProgram(input);

            Console.WriteLine("Product sums: " + multiplicationProgram.ProductSums());
            Console.WriteLine("Product sums (supporting disables): " + multiplicationProgram.ProductSumsWithDisables());
        }

        static void Day04() {

            string[] input = GetInputData("input_04.txt");
            WordSearch wordSearch = new WordSearch(input);

            Console.WriteLine("Word count: " + wordSearch.FindWord("XMAS"));
            Console.WriteLine("X count: " + wordSearch.FindX());
        }

        static void Day05() {

            string[] input = GetInputData("input_05.txt");
            SafetyManualManager smm = new SafetyManualManager(input);

            int sum = smm.SumOfMiddleNumbersCorrectlyOrdered();
            Console.WriteLine("Sum of middle numbers correctly ordered: " + sum);

            sum = smm.SumOfMiddleNumbersIncorrectlyOrderedThenFixed();
            Console.WriteLine("Sum of middle numbers icorrectly ordered then fixed: " + sum);
        }

        static void Day06() {

            string[] input = GetInputData("input_06_test.txt");
            SecurityGuardMap sgm = new SecurityGuardMap(input);

            sgm.ProcessMap(false);
            int sum = sgm.CountOfPositionsVisited();
            Console.WriteLine("Count of Positions Visited: " + sum);
            sgm.Print();

            sgm.ProcessMap(true);
            sum = sgm.CountOfObstaclePositions();
            Console.WriteLine("Count of Obstacle Positions: " + sum);
            sgm.Print();

            return;
        }

        static void Day07() {

            string[] input = GetInputData("input_07_test2.txt");

            MissingOperatorEvaluator moe = new MissingOperatorEvaluator(input);

            Console.WriteLine("Total calibration value, two operators: " + moe.TotalCalibrationValue());
            Console.WriteLine("Total calibration value, three operators: " + moe.TotalCalibrationValue2());
        }

        static void Day08() {

            string[] input = GetInputData("input_08.txt");
            AntennaMap antennaMap = new AntennaMap(input);

            antennaMap.Print();
            Console.WriteLine();

            // How many unique locations within the bounds of the map contain an antinode?
            int count = antennaMap.CountOfAntinodeLocations();
            Console.WriteLine("Count of antinode locations: " + count);
            count = antennaMap.CountOfAntinodeLocations2();
            Console.WriteLine("Count of antinode locations 2: " + count);
        }

        static void Day09() {

            // Note: day 9 was rough for me!  I didn't give up, and finally got both parts,
            // but i'm walking away in disgust of my spaghetti code and refused to clean it up
            // or look at it ever again.  On to day 10! 

            string input = GetInputDataAll("input_09.txt");
            Console.WriteLine(input);
            Console.WriteLine();

            DiskMap diskMap = new DiskMap(input);

            Console.WriteLine(diskMap.GetStartPosition());
            Console.WriteLine();

            Console.WriteLine(diskMap.Print1());
            Console.WriteLine();

            Console.WriteLine(diskMap.Print2());
            Console.WriteLine();

            long checksum = diskMap.GetChecksum();
            Console.WriteLine("Checksum: " + checksum);

            long checksum2 = diskMap.GetChecksum2();
            Console.WriteLine("Checksum2: " + checksum2);
        }

        static void Day10() {

            string[] input = GetInputData("input_10.txt");
            HikingGuide hikingGuide = new HikingGuide(input);

            hikingGuide.Print();

            Console.WriteLine("Count of trail heads: " + hikingGuide.TrailHeadCount());
            Console.WriteLine("Count of trail ends: " + hikingGuide.TrailEndCount());

            Console.WriteLine("Sum of all trailhead scores: " + hikingGuide.SumOfAllTrailHeadScores());
            Console.WriteLine("Sum of all trailhead scores 2: " + hikingGuide.SumOfAllTrailHeadScores2());
        }


        static void Day11() {

            string input = GetInputDataAll("input_11_test3.txt");
            StoneManager stoneManager = new StoneManager(input);

            stoneManager.Blink(75);
            Console.WriteLine("Count of stones: " + stoneManager.GetStoneCount());


            //stoneManager.Blink2(25);
            //Console.WriteLine("Count of stones: " + stoneManager.GetStoneCount());

            // StoneManager stoneManager2 = new StoneManager(input);
            // stoneManager2.Blink2(75);
            // Console.WriteLine("Count of stones: " + stoneManager2.GetStoneCount());
        }

        static void Day12() {

            string[] input = GetInputData("input_12.txt");
            GardenManager gardenManager = new GardenManager(input);

            Console.WriteLine("Total fencing costs: " + gardenManager.TotalPriceOfFencing());
            Console.WriteLine("Total fencing costs 2: " + gardenManager.TotalPriceOfFencing2());
        }

        static void Day13() {

            string[] input = GetInputData("input_13_test2.txt");
            ArcadeManager arcadeManager = new ArcadeManager(input, false);

            Console.WriteLine("Least amount of tokens: " + arcadeManager.FewestTokensForAllPrizes());

            arcadeManager = new ArcadeManager(input, true);
            Console.WriteLine("Least amount of tokens 2: " + arcadeManager.FewestTokensForAllPrizes());

        }

        static void Day14() {

            string[] input = GetInputData("input_14.txt");

            // 101 tiles wide and 103 tiles tall
            //RobotGuardManager rgm = new RobotGuardManager(input, 11, 7);
            RobotGuardManager rgm = new RobotGuardManager(input, 101, 103);

            // What will the safety factor be after exactly 100 seconds have elapsed?
            rgm.Print();
            rgm.LetTimeProceed(652);
            rgm.Print();
            for (int i = 40; i < 80; i++) {
                rgm.LetTimeProceed(101);
                rgm.Print();
                Console.WriteLine();
                Console.WriteLine(652 + (101 * (i - 40 + 1)));
                //Console.WriteLine("above: " + (652 + (101 * i)));
                Console.WriteLine();
            }
            // rgm.Print();
            Console.WriteLine("safety factor: " + rgm.GetSafetyFactor());

            int x = 5;
        }

        static void Day15() {

            string[] input = GetInputData("input_15_test2.txt");
            LanternFishManager lfm = new LanternFishManager(input);
            lfm.PrintMap();
            lfm.PrintMapLarge();
            
            //lfm.RunTheRobot();
            //lfm.PrintMap();

            // The lanternfish would like to know the sum of all boxes' GPS coordinates
            // after the robot finishes moving. In the larger example, the sum of all
            // boxes' GPS coordinates is 10092. In the smaller example, the sum is 2028.

            // Predict the motion of the robot and boxes in the warehouse. After the robot
            // is finished moving, what is the sum of all boxes' GPS coordinates?
            int sum = lfm.SumOfAllBoxCoordinates();
            Console.WriteLine("Sum of all box coordinates: " + sum);

        }
    }
}
