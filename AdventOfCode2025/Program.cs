using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;


namespace AdventOfCode2025 {

    internal class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2025\\data\\";

        static void Main(string[] args) {

            //Day01();
            //Day02();
            //Day03();
            //Day04();
            // Day05();
            //Day06();
            //Day07();
            //Day08();
            //Day09();
            //Day10();
            Day11();

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
            SafeCracker safeCracker = new SafeCracker(lines);

            Console.WriteLine("Question One: " + safeCracker.QuestionOne());
            Console.WriteLine("Question Two: " + safeCracker.QuestionTwo());
        }

        static void Day02() {

            string input = GetInputDataAll("input_02.txt");
            ProductIdChecker productIdChecker = new ProductIdChecker(input);
            productIdChecker.Print();

            long sum = productIdChecker.GetTotalInvalidIdSum();
            Console.WriteLine("Total Invalid ID Sum: " + sum);

            sum = productIdChecker.GetTotalInvalidId2Sum();
            Console.WriteLine("Total Invalid ID 2 Sum: " + sum);
        }

        static void Day03() {

            string[] input = GetInputData("input_03_test.txt");
            BatteryBanks batteryBanks = new BatteryBanks(input);

            int joltageOutputSum = batteryBanks.JoltageOutput();
            Console.WriteLine("Total joltage output: " + joltageOutputSum);

            long joltageOutputSum2 = batteryBanks.JoltageOutput2();
            Console.WriteLine("Total joltage output 2: " + joltageOutputSum2);

            //sum = productIdChecker.GetTotalInvalidId2Sum();
            //Console.WriteLine("Total Invalid ID 2 Sum: " + sum);
        }

        static void Day04() {

            string[] input = GetInputData("input_04.txt");
            PaperForkliftMap paperForkliftMap = new PaperForkliftMap(input);

            Console.WriteLine("Question 1: " + paperForkliftMap.QuestionOne());
            Console.WriteLine("Question 2: " + paperForkliftMap.QuestionTwo());
        }

        static void Day05() {

            string[] input = GetInputData("input_05.txt");
            IngredientAnalyzer ingredientAnalyzer = new IngredientAnalyzer(input);

            Console.WriteLine("Count of Fresh Available Ingredients: " + ingredientAnalyzer.CountOfFreshAvailableIngredients());
            Console.WriteLine("Count of All Ingredients: " + ingredientAnalyzer.CountOfFreshAvailableIngredients2());
        }

        static void Day06() {

            string[] input = GetInputData("input_06.txt");
            CephalopodMath cephalopodMath = new CephalopodMath(input);

            Console.WriteLine("Question One: " + cephalopodMath.QuestionOne());
            Console.WriteLine("Question Two: " + cephalopodMath.QuestionTwo());
        }

        static void Day07() {

            string[] input = GetInputData("input_07.txt");
            TachyonManifold tachyonManifold = new TachyonManifold(input);

            // Question One.
            int splitCount = tachyonManifold.Tick();
            tachyonManifold.Print();
            Console.WriteLine("Q1: Split count: " + splitCount);

            // Question Two.
            TachyonWorld tachyonWorld = new TachyonWorld(input);
            splitCount = tachyonWorld.Tick(true);
            Console.WriteLine("Q2: Split count: " + splitCount);
        }


        // Day08 is not yet passing. 
        static void Day08() {

            string[] input = GetInputData("input_08_test.txt");
            JunctionBoxManager junctionBoxManager = new JunctionBoxManager(input);

            //Your list contains many junction boxes; connect together the 1000 pairs of junction boxes
            //which are closest together. Afterward, what do you get if you multiply together the sizes
            //of the three largest circuits ?
            double answer = junctionBoxManager.QuestionOne(10);
            Console.WriteLine("Q1: " + answer);



        }


        // Day09 is not yet passing. 
        // https://adventofcode.com/2025/day/9

        static void Day09() {

            string[] input = GetInputData("input_09.txt");

            TileFloor tileFloor = new TileFloor(input);
            tileFloor.PrintMap();

            // Question One.
            long largestArea = tileFloor.LargestArea();
            Console.WriteLine("Q1: " + largestArea);

            // Question Two.
            tileFloor.DrawConnectingLines();
            tileFloor.PrintMap();
            tileFloor.FillInAreas();
            tileFloor.PrintMap();
            largestArea = tileFloor.LargestAreaWithinContiguousSpace();
            Console.WriteLine("Q2: " + largestArea);
            Debug.Assert(false, "this answer requires too much memory");

        }

        static void Day10() {

            string[] input = GetInputData("input_10.txt");
            IndicatorManager manager = new IndicatorManager(input);

            long fewestTotalPresses = manager.FewestTotalPresses();
            Console.WriteLine("Q1: " + fewestTotalPresses);
        }


        static void Day11() {

            // Question One.
            //string[] input = GetInputData("input_11_test.txt");
            string[] input = GetInputData("input_11.txt");
            DeviceManager deviceManager = new DeviceManager(input, "you");
            deviceManager.Print();
            long pathCount = deviceManager.FindPaths();
            Console.WriteLine("Q1: path count: " + pathCount);

            // Question Two.
            //input = GetInputData("input_11_test2.txt");
            input = GetInputData("input_11.txt");
            deviceManager = new DeviceManager(input, "svr");
            pathCount = deviceManager.FindPaths2();
            Console.WriteLine("Q2: path count: " + pathCount);
        }
    }
}
