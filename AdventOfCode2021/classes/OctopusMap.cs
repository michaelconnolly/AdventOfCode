using System;
using System.Collections.Generic;
using System.Text;


namespace AdventOfCode2021 {


    public class OctopusMap {

        int width;
        int height;
        int[,] octopusMap;
        public int firstSynchronization = -1;

        public OctopusMap(string[] lines) {

            width = lines[0].Length;
            height = lines.Length;
            octopusMap = new int[width, height];
            
            for (int y = 0; y < height; y++) {
                string currentLine = lines[y];
                for (int x = 0; x < width; x++) {
                    octopusMap[x, y] = Convert.ToInt32(currentLine[x].ToString());
                }
            }
        }


        public void print() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int currentChar = octopusMap[x, y];
                    string currentCharAsString;
                    if (currentChar > 9) {
                        if (currentChar == 10) currentCharAsString = "A";
                        else currentCharAsString = "B";
                    }
                    else currentCharAsString = currentChar.ToString();
                    Console.Write(currentCharAsString);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }


        public int oneStep() {

            int countOfFlashes = 0;

            // Everybody level up!
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    addOctopusEnergy(x, y);
                }
            }

          //  this.print();

            // OK, who had too much to drink?
            // This is going to get recursive, yo.
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {

                    int currentOctopus = octopusMap[x, y];
                    if (currentOctopus > 9) {
                        countOfFlashes += addOctopusFlashEnergy(x, y);
                    }

                }
            }

            // Lastly, reset everything above 9 to zero.
            // Everybody level up!
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (octopusMap[x, y] > 9) octopusMap[x, y] = 0;         
                }
            }

            return countOfFlashes;
        }


        private void addOctopusEnergy(int x, int y) {

            int currentOctopus = octopusMap[x, y];
            currentOctopus++;
            octopusMap[x, y] = currentOctopus;
        }
     

        private int addOctopusFlashEnergy(int x, int y) {

            int countOfFlashes = 0;

            // We assume that the cell coordinate input is the cell that just blinked, and it needs to cascade it's energy.
            if (octopusMap[x, y] == 99) return countOfFlashes;

            countOfFlashes++;
            octopusMap[x, y] = 99; // let's mark this;'

            // hit all directions.
            if (y > 0) countOfFlashes += addOctopusFlashEnergyOneCell(x, y - 1); // up
            if (y < height-1) countOfFlashes += addOctopusFlashEnergyOneCell(x, y + 1); // down
            if (x > 0) countOfFlashes += addOctopusFlashEnergyOneCell(x-1, y ); // left
            if (x < width-1) countOfFlashes += addOctopusFlashEnergyOneCell(x+1, y); // right
            
            if (y > 0 && x > 0) countOfFlashes += addOctopusFlashEnergyOneCell(x-1, y - 1); // NW
            if (y > 0 && x < width-1) countOfFlashes += addOctopusFlashEnergyOneCell(x+1, y - 1); // NE
            if (y < height-1 && x > 0) countOfFlashes += addOctopusFlashEnergyOneCell(x-1, y + 1); // SW
            if (y < height-1 && x < width-1) countOfFlashes += addOctopusFlashEnergyOneCell(x+1, y + 1); // SE

            return countOfFlashes;
        }


        private int addOctopusFlashEnergyOneCell(int x, int y) {

            int countOfFlashes = 0;

            // I already got zero'd earlier, no need to be recurisive here.
            if (octopusMap[x, y] == 99) return countOfFlashes;

            int currentOctopus = octopusMap[x, y];
            currentOctopus++;
            if (currentOctopus > 9) {
                octopusMap[x, y] = 0;
                countOfFlashes += addOctopusFlashEnergy(x, y);
            }
            else octopusMap[x, y] = currentOctopus++;

            return countOfFlashes;
        }
         

        public void checkSynchronization(int iteration) {

            // Check for first synchronization.
            if (this.firstSynchronization == -1) {
                if (this.isFlashSynchronized()) this.firstSynchronization = iteration;
            }
        }


        public bool isFlashSynchronized() {

            // Check value.
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (this.octopusMap[x, y] != 0) return false;
                }
            }

            return true;
        }
    }
}
