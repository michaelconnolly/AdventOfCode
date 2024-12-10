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
            Day08();

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

            string[] input = GetInputData("input_07.txt");

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
    }
}
