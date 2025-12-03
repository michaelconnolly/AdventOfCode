using System;
using System.IO;


namespace AdventOfCode2025 {

    internal class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2025\\data\\";

        static void Main(string[] args) {

            //Day01();
            Day02();

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
    }
}
