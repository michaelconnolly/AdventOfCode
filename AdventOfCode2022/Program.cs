using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022 {

    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2022\\data\\";

        static void Main(string[] args) {

            //Day1();
            //Day2();
            Day3();

            // Keep the console window open.
            Console.WriteLine("\nPress any key to exit.");
            System.Console.ReadKey();
        }

        static void Day1() {

            string fileName = dataFolderPath + "input_01.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            ArrayList elves = new ArrayList();
            int caloriesCount = 0;

            foreach (string line in lines) {
                if (line == "") {
                    elves.Add(caloriesCount);
                    caloriesCount = 0;
                }
                else {
                    int calories = Convert.ToInt32(line);
                    caloriesCount = caloriesCount + calories;
                }
            }
            elves.Add(caloriesCount);

            int max1CaloriesCount = 0;
            int max2CaloriesCount = 0;
            int max3CaloriesCount = 0;

            foreach (int elf in elves) {
                Console.WriteLine(elf);
                if (elf > max1CaloriesCount) {
                    max3CaloriesCount = max2CaloriesCount;
                    max2CaloriesCount = max1CaloriesCount;
                    max1CaloriesCount = elf;
                }
                else if (elf > max2CaloriesCount) {
                    max3CaloriesCount = max2CaloriesCount;
                    max2CaloriesCount = elf;
                }
                else if (elf > max3CaloriesCount) {
                    max3CaloriesCount = elf;
                }
            }

            // I have your answer for you.
            Console.WriteLine("Number of elves: " + elves.Count);
            Console.WriteLine("Max calories 1: " + max1CaloriesCount);
            Console.WriteLine("Max calories 2: " + max2CaloriesCount);
            Console.WriteLine("Max calories 3: " + max3CaloriesCount);
            Console.WriteLine("Total of top 3: " + (max1CaloriesCount + max2CaloriesCount + max3CaloriesCount));
        }

        static void Day2() {

            string fileName = dataFolderPath + "input_02.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            int totalScore = 0;
            int totalScore2 = 0;

            foreach (string line in lines) {
                rpsHand hand = new rpsHand(line);
                totalScore += hand.score();
                totalScore2 += hand.score2();
                hand.print();
            }

            // I have your answer for you.
            Console.WriteLine("total score: " + totalScore);
            Console.WriteLine("total score 2: " + totalScore2);
        }

        static void Day3() {

            string fileName = dataFolderPath + "input_03.txt";
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Size of input array: " + lines.Length);

            // each line is one rucksack.
            // rucksack has two compartments.
            // each rucksack has a pile of stuff, one half in each compartment.
            // there is one thing that is illegally represented in both compartments.
            // that one thing has a score.  Tell me the score.

            int totalScore = 0;
 
            foreach (string line in lines) {

                rucksack rucksack = new rucksack(line);
                totalScore += rucksack.score();
                rucksack.print();
            }

            // I have your answer for you.
            Console.WriteLine("total score: " + totalScore);

            // Part 2.
            int countOfGroups = (lines.Length / 3);
            Console.WriteLine("count of groups: " + countOfGroups);
            int totalScore2 = 0;

            for (int i = 0; i < countOfGroups; i++) {

                // Figure out, in each group of 3, what is the one item that is common amongst all three.  

                rucksack rucksack1 = new rucksack(lines[(i * 3) + 0]);
                rucksack rucksack2 = new rucksack(lines[(i * 3) + 1]);
                rucksack rucksack3 = new rucksack(lines[(i * 3) + 2]);

                Dictionary<string, int> contents1 = rucksack1.contents();
                Dictionary<string, int> contents2 = rucksack2.contents();
                Dictionary<string, int> contents3 = rucksack3.contents();

                bool foundIt = false;

                foreach (string key in contents1.Keys) {

                    if ((contents2.ContainsKey(key)) && (contents3.ContainsKey(key))) {
                        totalScore2 += rucksack.ConvertStringToInt(key);
                        foundIt = true;
                        break;
                    }
                }

                if (!foundIt) { throw new Exception(); }
            }

            Console.WriteLine("total score 2: " + totalScore2);
        }
    }
}
