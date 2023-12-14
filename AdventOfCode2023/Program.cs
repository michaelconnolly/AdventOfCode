using System;
using System.IO;

namespace AdventOfCode2023 {

    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2023\\data\\";

        static void Main(string[] args) {

            //Day01();
            //Day02();
            //Day03();
            //Day04();
            //Day05();
            //Day06();
            //Day07();
            //Day08();
            // Day09();
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

        static void Day01() {

            string[] lines = GetInputData("input_01.txt");

            CalibrationDocument calibrationDoc = new CalibrationDocument(lines);

            int calibrationValuesTotal = calibrationDoc.GetCalibrationValuesTotal();
            Console.WriteLine("Day 1a: Calibration values total: " + calibrationValuesTotal.ToString());

            Console.WriteLine("**************************************************************************");

            calibrationValuesTotal = calibrationDoc.GetCalibrationValuesTotal(true);
            Console.WriteLine("Day 1b: Calibration values total: " + calibrationValuesTotal.ToString());
        }

        static void Day02() {

            string[] lines = GetInputData("input_02.txt");

            CubeBagGameManager cubeBagGameManager = new CubeBagGameManager(lines);
            CubeBagGameSet constraintSet = new CubeBagGameSet(12, 13, 14);
            cubeBagGameManager.print();

            int idSum = cubeBagGameManager.GetIdSumWithConstraint(constraintSet);
            Console.WriteLine("Day 2a: sum of id's that meet constraints: " + idSum);

            Console.WriteLine("**************************************************************************");
            idSum = cubeBagGameManager.GetMinimalSetPowerSum();
            Console.WriteLine("Day 2b: sum of each game's minimal set power: " + idSum);
        }

        static void Day03() {

            string[] lines = GetInputData("input_03.txt");

            EngineSchematic engineSchematic = new EngineSchematic(lines);
            engineSchematic.print();

            int sum = engineSchematic.GetSumOfPartNumbers();
            Console.WriteLine("Day 3a: sum of part numbers: " + sum);

            sum = engineSchematic.GetSumofGearRatios();
            Console.WriteLine("Day 3b: sum of gear ratios: " + sum);
        }

        static void Day04() {

            string[] lines = GetInputData("input_04.txt");

            ScratchCardManager scratchCardManager = new ScratchCardManager(lines);

            int sum = scratchCardManager.GetSumOfCardsPoints();
            Console.WriteLine("Day 4a: sum of all card points: " + sum);

            sum = scratchCardManager.GetSumOfCardsAfterCopying();
            Console.WriteLine("Day 4b: sum of all cards after copying: " + sum);
        }

        static void Day05() {

            string[] lines = GetInputData("input_05.txt");

            Almanac almanac = new Almanac(lines);
            almanac.print();

            long value = almanac.GetLowestLocationFromInitialSeeds();
            Console.WriteLine("Day 5a: lowest location from initial seeds: " + value);
            value = almanac.GetLowestLocationFromInitialSeeds(true);
            Console.WriteLine("Day 5b: lowest location from initial seeds (range): " + value);
        }

        static void Day06() {

            string[] lines = GetInputData("input_06.txt");

            BoatRaceManager boatRaceManager = new BoatRaceManager(lines);
            boatRaceManager.print();

            long value = boatRaceManager.CalculateProductOfAllCountOfWinningOptions();
            Console.WriteLine("Day 6a: product of all winning options count: " + value);

            value = boatRaceManager.CalculateCountOfOfWinningOptionsOneRace();
            Console.WriteLine("Day 6a: product of all winning options count: " + value);

        }

        static void Day07() {

            string[] lines = GetInputData("input_07.txt");

            CamelCardManager camelCardManager = new CamelCardManager(lines);
            camelCardManager.print();

            int value = camelCardManager.GetTotalWinnings(usePartTwoRules: false);
            Console.WriteLine("Day 7a: total winnings: " + value);

            value = camelCardManager.GetTotalWinnings(usePartTwoRules: true);
            Console.WriteLine("Day 7b: total winnings: " + value);
        }

        static void Day08() {

            DateTime start = DateTime.Now;
            string[] lines = GetInputData("input_08.txt");

            DesertMap desertMap = new DesertMap(lines);
            desertMap.print();

            long value = desertMap.StepCount();
            Console.WriteLine("Day 8a: step count: " + value);

            DateTime beginPartTwo = DateTime.Now;
            value = desertMap.StepCountPartTwo();
            DateTime endPartTwo = DateTime.Now;
            Console.WriteLine("Day 8b: step count: " + value);
            Console.WriteLine("Execution Time: " + (endPartTwo - beginPartTwo).ToString());
        }

        static void Day09() {

            DateTime start = DateTime.Now;
            string[] lines = GetInputData("input_09.txt");

            OasisSensor oasisSensor = new OasisSensor(lines);
            oasisSensor.print();

            long value = oasisSensor.GetSumOfNextValues();
            Console.WriteLine("Day 9a: step count: " + value);

            value = oasisSensor.GetSumOfPreviousValues();
            Console.WriteLine("Day 9b: step count: " + value);
        }

        static void Day10() {

            string[] lines = GetInputData("input_10_test1.txt");

            PipeMap pipeMap = new PipeMap(lines);
            pipeMap.print();

            int count = pipeMap.GetStepCountOfFurthestPointFromStart();
            Console.WriteLine("Day 10a: step count: " + count);

            // PART TWO NOT FINISHED.
            count = pipeMap.CalculateEnclosedTiles();
            Console.WriteLine("Day 10b: enclosed tile count: " + count);

        }

        static void Day11() {

            string[] lines = GetInputData("input_11.txt");

            GalaxyMap galaxyMap = new GalaxyMap(lines, 2);
            galaxyMap.print();
            long count = galaxyMap.GetSumOfAllDistanceLengths();
            Console.WriteLine("Day 11a: distance sum: " + count);

            GalaxyMap galaxyMap2 = new GalaxyMap(lines, 1000000);
            galaxyMap2.print();
            count = galaxyMap2.GetSumOfAllDistanceLengths();
            Console.WriteLine("Day 11b: distance sum: " + count);
        }
    }
}