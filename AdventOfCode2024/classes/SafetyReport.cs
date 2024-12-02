using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {

    public enum LevelChange {
        Upward,
        Downward,
        Flat
    }

    internal class SafetyReport {

        public List<Int32> levels = new List<Int32>();

        public SafetyReport(string levels) {

            string[] levels_string = levels.Split(' ');
            foreach (string level_string in levels_string) {
                this.levels.Add(Int32.Parse(level_string));
            }
        }

        private LevelChange CalcLevelChange(int level1, int level2) {

            if (level1 > level2) return LevelChange.Downward;
            else if (level1 < level2) return LevelChange.Upward;
            return LevelChange.Flat;
        }

        public string Print() {

            string returnString = "";

            foreach (Int32 level1 in this.levels) {
                returnString += level1.ToString() + " ";
            }

            returnString += "-- " + this.IsSafe(this.levels).ToString();
            returnString += " -- " + this.IsSafeWithDampener().ToString();
            return returnString;
        }

        public bool IsSafe(List<Int32> levelsToCheck) {

            // If right out the gate we are flat, fail fast.
            int changeInitial = Math.Abs(levelsToCheck[0] - levelsToCheck[1]);
            LevelChange levelChangeRequired = this.CalcLevelChange(levelsToCheck[0], levelsToCheck[1]);
            if ((levelChangeRequired == LevelChange.Flat) || (changeInitial > 3)) {

                return false;
            }

            for (int i = 1; i < levelsToCheck.Count; i++) {

                int first = i - 1;
                int second = i;

                int changeAmount = Math.Abs(levelsToCheck[first] - levelsToCheck[second]);
                LevelChange levelChangeActual = this.CalcLevelChange(levelsToCheck[first], levelsToCheck[second]);

                if ((levelChangeActual != levelChangeRequired)
                        || (changeAmount > 3)) {
                    return false;
                }
            }

            return true;
        }

        public bool IsSafeWithDampener() {

            // Try it without the dampener.
            if (this.IsSafe(this.levels)) return true;

            // Let's try again with removing one of the levels.
            for (int i = 0; i < this.levels.Count; i++) {

                List<Int32> smallerList = new List<Int32>(this.levels);
                smallerList.RemoveAt(i);

                if (this.IsSafe(smallerList)) return true;
            }

            return false;
        }
    }
}
