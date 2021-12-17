using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2021 {
    public class FoldablePaper {

        public Collection<string[]> folds = new Collection<string[]>();
        private int[,] dots;
        public char[,] map;
        int width = int.MinValue;
        int height = int.MinValue;
         

        public FoldablePaper(string[] lines) {

            bool foundBreak = false;
            for (int i = (lines.Length-1); i>=0; i--) {

                if (lines[i] == "") {
                    foundBreak = true;
                    dots = new int[i, 2];
                }
                else if (!foundBreak) {
                    string[] processInput = lines[i].Split(" ");
                    string[] fold = processInput[2].Split("=");
                    folds.Insert(0, fold);
                }
                else { // foundBreak ==  true
                    string[] processInput = lines[i].Split(",");
                    int x = Convert.ToInt32(processInput[0]); 
                    int y = Convert.ToInt32(processInput[1]);
                    if (x+1 > width) width = x+1;
                    if (y+1 > height) height = y+1;
                    dots[i, 0] = x;
                    dots[i, 1] = y;
                }

            }

            // Now build the map.
            map = new char[width, height];
            for (int y=0; y<height; y++) {
                for (int x=0; x<width; x++) {
                    map[x, y] = '.';
                }
            }
            for (int i=0; i<(dots.Length/2); i++) {
                int x = dots[i, 0];
                int y = dots[i, 1];
                map[x, y] = '#';
            }

            return;    
        }


        public void doFold(string[] fold) {

            char direction = fold[0][0];
            int location = Convert.ToInt32(fold[1]);
            char[,] newMap;

            // horizontal fold.
            if (direction == 'y') {

                newMap = new char[width, location];

                // The easy part: copy the top half.
                for (int y = 0; y < (location); y++) {
                    for (int x = 0; x < width; x++) {
                        newMap[x, y] = map[x, y];
                    }
                }
                // The other part, copy the bottom half onto the top half.
                for (int y = location + 1; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        if (map[x, y] == '#') {
                            newMap[x, ((location * 2) - y)] = '#'; // (location - (y - location) == ((2*location) - y)
                        }
                    }
                }

                // Update our public properties.
                this.height = location;
            }
            else {

                newMap = new char[location, height];

                // The easy part: copy the left half.
                for (int y = 0; y < (height); y++) {
                    for (int x = 0; x < location; x++) {
                        newMap[x, y] = map[x, y];
                    }
                }
                // The other part, copy the right half onto the left half.
                for (int y = 0; y < height; y++) {
                    for (int x = (location + 1); x < width; x++) {
                        if (map[x, y] == '#') {
                            newMap[((location * 2) - x), y] = '#'; // (location - (x - location) == ((2*location) - x)
                        }
                    }
                }

                // Update our public properties.
                this.width = location;
            }

            this.map = newMap;
        }

        public void print() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(map[x, y]);
                  
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
        }


        public int getDotCount() {

            int count = 0;

            for (int y=0; y < height; y++) {
                for (int x=0; x<width; x++) {
                    if (map[x, y] == '#') count++;
                }
            }
            return count;
        }
    }
}
