using System;
using System.IO;


namespace AdventOfCode2024 {

    internal class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2024\\data\\";

        static void Main(string[] args) {

            Day01();

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
    }
}
