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
            Day06();

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
    }
}