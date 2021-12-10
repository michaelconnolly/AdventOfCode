using System;


namespace AdventOfCode2021 {


    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2021\\data\\";


        static void Main(string[] args) {

            Day1();

            // Keep the console window open.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }


        static int readNumber(string[] list, int index) {
            return Convert.ToInt32(list[index]);
        }


        static void Day1() {

            string fileName = dataFolderPath + "input_day_01.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Debug check.
            Console.WriteLine("Size of input array: " + lines.Length);

            // Day 1, Part 1.
            int countOfDepthIncreases = 0;
            int lastDepthReading = -1;
            foreach (string line in lines) {
                int currentDepthReading = Convert.ToInt32(line);
                if (lastDepthReading != -1) {
                    if (currentDepthReading > lastDepthReading) {
                        countOfDepthIncreases++;
                    }
                }
                lastDepthReading = currentDepthReading;
            }

            Console.WriteLine("1a: count of depth increases: " + countOfDepthIncreases);

            // Day 1, Part 2.
            // Since we are computing 3 measurements at a time, start at the 3rd, i.e., i=2;
            lastDepthReading = -1;
            countOfDepthIncreases = 0;
            for (int i = 2; i < lines.Length; i++) {

                int currentDepthReading = readNumber(lines, i - 2) + readNumber(lines, i - 1) + readNumber(lines, i);
                //Console.WriteLine(currentDepthReading);

                if (lastDepthReading != -1) {
                    if (currentDepthReading > lastDepthReading) {
                        countOfDepthIncreases++;
                    }
                }
                lastDepthReading = currentDepthReading;
            }

            Console.WriteLine("1b: count of depth increases: " + countOfDepthIncreases);
        }
    }
}

