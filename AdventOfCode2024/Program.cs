using System;
using System.IO;


namespace AdventOfCode2024 {

    internal class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2024\\data\\";

        static void Main(string[] args) {

            //Day01();
            //Day02();
            //Day03();
            Day04();

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
    }
}
