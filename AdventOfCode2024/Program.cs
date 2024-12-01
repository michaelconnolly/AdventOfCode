using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    internal class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2024\\data\\";

        static void Main(string[] args) {

            Day01();
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

            //string[] lines = GetInputData("input_01.txt");

            //CalibrationDocument calibrationDoc = new CalibrationDocument(lines);

            //int calibrationValuesTotal = calibrationDoc.GetCalibrationValuesTotal();
            //Console.WriteLine("Day 1a: Calibration values total: " + calibrationValuesTotal.ToString());

            //Console.WriteLine("**************************************************************************");

            //calibrationValuesTotal = calibrationDoc.GetCalibrationValuesTotal(true);
            //Console.WriteLine("Day 1b: Calibration values total: " + calibrationValuesTotal.ToString());
        }
    }
}
