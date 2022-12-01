using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;


// https://adventofcode.com/2022

namespace AdventOfCode2022 {


    class Program {

        static string dataFolderPath = "C:\\dev\\AdventOfCode\\AdventOfCode2022\\data\\";


        static void Main(string[] args) {

            Day1();
  
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
    }
}
