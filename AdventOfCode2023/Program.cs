﻿using AdventOfCode2023.classes;
using System;
using System.Collections.Generic;
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
            //Day09();
            //Day10();
            //Day11();
            //Day12(); // not done
            //Day13();
            //Day14();
            //Day15();
            //Day16();
            Day17();

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

        static void Day12() {

            string[] lines = GetInputData("input_12_test3.txt");

            SpringMap springMap = new SpringMap(lines);
            springMap.print();

            long sum = springMap.GetSumOfEachRowsComboOptions();
            Console.WriteLine("Day 12a: combo sum: " + sum);
        }


        static void Day13() {

            string[] lines = GetInputData("input_13.txt");

            MirrorMapManager mirrorMapManager = new MirrorMapManager(lines);
            mirrorMapManager.print();

            long sum = mirrorMapManager.GetSumOfAllPoints();
            Console.WriteLine("Day 13a: combo sum: " + sum);

            sum = mirrorMapManager.GetSumOfAllPoints2();
            Console.WriteLine("Day 13b: combo sum: " + sum);
        }

        static void Day14() {

            string[] lines = GetInputData("input_14.txt");

            RollingRockMap rollingRockMap = new RollingRockMap(lines);
            rollingRockMap.print();

            rollingRockMap.TiltNorthOrSouth(Direction.North);
            rollingRockMap.print();
            long sum = rollingRockMap.CalculateLoad();
            Console.WriteLine("Day 14a: total load north: " + sum);

            // Part Two.
            rollingRockMap = new RollingRockMap(lines);
            rollingRockMap.print();

            long iterations = 1000000000;
            for (long i = 0; i < iterations; i++) {
                rollingRockMap.TiltNorthOrSouth(Direction.North);
                //rollingRockMap.print();
                rollingRockMap.TiltWestOrEast(Direction.West);
                //rollingRockMap.print();
                rollingRockMap.TiltNorthOrSouth(Direction.South);
                //rollingRockMap.print();
                rollingRockMap.TiltWestOrEast(Direction.East);
                //rollingRockMap.print(); 
            }
            rollingRockMap.print();

            sum = rollingRockMap.CalculateLoad();
            Console.WriteLine("Day 14b: total load north: " + sum);




        }

        static void Day15() {

            string line = GetInputDataAll("input_15.txt");

            HashManager hashManager = new HashManager(line);
            hashManager.print();

            long sum = hashManager.SumOfHashResults();
            Console.WriteLine("Day 15a: sum of hash results: " + sum);

            sum = hashManager.TotalFocusingPower();
            Console.WriteLine("Day 15b: total focusing power: " + sum);
        }

        static void Day16() {

            string[] lines = GetInputData("input_16.txt");

            BeamMirrorMap beamMirrorMap = new BeamMirrorMap(lines);
            Beam initialBeam = new Beam(0, 0, BeamDirection.Right);
            beamMirrorMap.ProcessBeam(initialBeam);
            beamMirrorMap.print();
            long sum = beamMirrorMap.SumOfEnergizedMirrors();

            Console.WriteLine("Day 16a: sum of energized mirrors: " + sum);

            // Let's do part two.
            int height = lines.Length;
            int width = lines[0].Length;
            long best = 0;
            long current = 0;

            // left and right edges
            for (int y = 0; y < height; y++) {

                // left edge.
                beamMirrorMap = new BeamMirrorMap(lines);
                initialBeam = new Beam(0, y, BeamDirection.Right);
                beamMirrorMap.ProcessBeam(initialBeam);
                current = beamMirrorMap.SumOfEnergizedMirrors();
                if (current > best) { best = current; }

                // right edge.
                beamMirrorMap = new BeamMirrorMap(lines);
                initialBeam = new Beam(width - 1, y, BeamDirection.Left);
                beamMirrorMap.ProcessBeam(initialBeam);
                current = beamMirrorMap.SumOfEnergizedMirrors();
                if (current > best) { best = current; }
            }

            // top and bottom edges
            for (int x = 0; x < width; x++) {

                // top edge.
                beamMirrorMap = new BeamMirrorMap(lines);
                initialBeam = new Beam(x, 0, BeamDirection.Down);
                beamMirrorMap.ProcessBeam(initialBeam);
                current = beamMirrorMap.SumOfEnergizedMirrors();
                if (current > best) { best = current; }

                // bottom edge.
                beamMirrorMap = new BeamMirrorMap(lines);
                initialBeam = new Beam(x, height - 1, BeamDirection.Up);
                beamMirrorMap.ProcessBeam(initialBeam);
                current = beamMirrorMap.SumOfEnergizedMirrors();
                if (current > best) { best = current; }
            }

            Console.WriteLine("Day 16b: best way to energized mirrors: " + best);
        }

        static void Day17() {

            string[] lines = GetInputData("input_17_test.txt");

            CityBlockMap cityBlockMap = new CityBlockMap(lines);
            cityBlockMap.print();

            Dictionary<int, CityBlock> history = new Dictionary<int, CityBlock>();
            CityBlockCoordinate start = new CityBlockCoordinate(cityBlockMap.cityBlocks[0, 0], CityBlockFacing.Right);
            
            DateTime begin = DateTime.Now;
            int sum = cityBlockMap.LeastHeatLoss(start, history, 0, 0, true);
            DateTime finished = DateTime.Now;
            Console.WriteLine("execution time: " + finished.Subtract(begin).TotalMilliseconds);
            
            Console.WriteLine("Day 17a: sum of least heat loss: " + sum);
        }
    }
}
