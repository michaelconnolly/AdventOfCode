using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace AdventOfCode2023 {

    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2023\\data\\";

        static void Main(string[] args) {

            Day01();

            // Keep the console window open.
            Console.WriteLine("\nPress any key to exit.");
            System.Console.ReadKey();
        }

        static void Day01() {

            string fileName = dataFolderPath + "input_01.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            CalibrationDocument calibrationDoc = new CalibrationDocument(lines);

            int calibrationValuesTotal = calibrationDoc.GetCalibrationValuesTotal();
            Console.WriteLine("Day 1a: Calibration values total: " + calibrationValuesTotal.ToString());

            Console.WriteLine("**************************************************************************");

            calibrationValuesTotal = calibrationDoc.GetCalibrationValuesTotal(true);
            Console.WriteLine("Day 1b: Calibration values total: " + calibrationValuesTotal.ToString());
        }
    }
}
