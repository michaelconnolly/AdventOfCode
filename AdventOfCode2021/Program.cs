using System;

// https://adventofcode.com/2021/

namespace AdventOfCode2021 {


    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2021\\data\\";


        static void Main(string[] args) {

            //Day1();
            Day2();

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


        static void Day2() {

            string fileName = dataFolderPath + "input_day_02.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Debug check.
            Console.WriteLine("Size of input array: " + lines.Length);

            // Day 2, Part 1.
            int currentHorizontal = 0;
            int currentDepth = 0;
            foreach (string line in lines) {

                string[] commandData = line.Split(' ');
                string command = commandData[0];
                int distance = readNumber(commandData, 1);

                switch (command) {

                    case "forward":

                        currentHorizontal += distance;
                        break;

                    case "down":

                        currentDepth += distance;
                        break;

                    case "up":

                        currentDepth -= distance;
                        break;

                    default:

                        Console.WriteLine("error!");
                        throw new Exception();
                }
            }

            Console.Write("2a: horizontal: " + currentHorizontal);
            Console.Write(", depth: " + currentDepth);
            Console.WriteLine(", product: " + (currentDepth * currentHorizontal));

            // Day 2, Part 2.
            currentHorizontal = 0;
            currentDepth = 0;
            int currentAim = 0;
            foreach (string line in lines) {

                string[] commandData = line.Split(' ');
                string command = commandData[0];
                int distance = readNumber(commandData, 1);

                switch (command) {

                    case "forward":
                        // forward X does two things:
                        // It increases your horizontal position by X units.
                        // It increases your depth by your aim multiplied by X.
                        currentHorizontal += distance;
                        currentDepth += (currentAim * distance);
                        break;

                    case "down":
                        // down X increases your aim by X units.
                        currentAim += distance;
                        break;

                    case "up":
                        // up X decreases your aim by X units.
                        currentAim -= distance;
                        break;

                    default:

                        Console.WriteLine("error!");
                        throw new Exception();
                }
            }

            Console.Write("2b: horizontal: " + currentHorizontal);
            Console.Write(", depth: " + currentDepth);
            Console.WriteLine(", product: " + (currentDepth * currentHorizontal));

        }
    }
}

