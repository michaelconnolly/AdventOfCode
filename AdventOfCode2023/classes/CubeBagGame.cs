using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace AdventOfCode2023 {
    internal class CubeBagGame {

        public int id = 0;
        Collection<CubeBagGameSet> gamesets = new Collection<CubeBagGameSet>();

        public CubeBagGame(string lineOriginal) {

            // Format: "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
            
            // extract id;
            string line2 = lineOriginal.Substring(5);
            int indexColon = line2.IndexOf(':');
            string line3 = line2.Substring(0, indexColon);
            this.id = Convert.ToInt32(line3);
            
            // extract sets;
            line2 = line2.Substring(indexColon + 1);  
            string[] sets = line2.Split(';');
            foreach (string s in sets) {
                this.gamesets.Add(new CubeBagGameSet(s));
            }
        }

        public bool passConstraint(CubeBagGameSet constraintSet) {

            foreach (CubeBagGameSet gameSet in this.gamesets) {
                
                if ((gameSet.redCount > constraintSet.redCount) ||
                   (gameSet.greenCount > constraintSet.greenCount) ||
                   (gameSet.blueCount > constraintSet.blueCount)) {
                    return false;
                }
            }

            return true;
        }

        public CubeBagGameSet MinimalSet() {

            CubeBagGameSet minimalSet = new CubeBagGameSet(0, 0, 0);

            foreach (CubeBagGameSet set in this.gamesets) {
                if (set.redCount > minimalSet.redCount) minimalSet.redCount = set.redCount;
                if (set.greenCount > minimalSet.greenCount) minimalSet.greenCount = set.greenCount;
                if (set.blueCount > minimalSet.blueCount) minimalSet.blueCount = set.blueCount;

            }

            return minimalSet;
        }


        public void print() {

            Console.Write("Game " + this.id + ": ");
            foreach (CubeBagGameSet gameset in this.gamesets) {
                gameset.print();
                Console.Write(";");
            }
            Console.Write(" Minimal Set: "); 
            this.MinimalSet().print();
            Console.WriteLine();
        }
    }
}
