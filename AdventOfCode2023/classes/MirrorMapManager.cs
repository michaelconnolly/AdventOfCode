using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdventOfCode2023 {

    internal class MirrorMapManager {

        Collection<MirrorMap> mirrorMaps = new Collection<MirrorMap>();

        public MirrorMapManager(string[] lines) {

            int startIndex = 0;
            string[] mirrorMapString;

            for (int i=0; i<lines.Length; i++) {

                if (lines[i] == "") {
                    mirrorMapString = lines.Skip(startIndex).Take(i - startIndex).ToArray();
                    this.mirrorMaps.Add(new MirrorMap(mirrorMapString));
                    startIndex = i + 1;
                }
            }

            // Slurp up whatever is remaining.
            mirrorMapString = lines.Skip(startIndex).ToArray();
            this.mirrorMaps.Add(new MirrorMap(mirrorMapString));
        }

        public void print() {

            foreach (MirrorMap mirrorMap in this.mirrorMaps) {
                mirrorMap.print();
            }

            Console.WriteLine("Total number of mirror maps: " + this.mirrorMaps.Count);
        }

        public long GetSumOfAllPoints() {

            long sum = 0;

            foreach (MirrorMap mirrorMap in this.mirrorMaps) {
                sum += mirrorMap.GetPoints();
            }

            return sum;
        }

        public long GetSumOfAllPoints2() {

            long sum = 0;

            foreach (MirrorMap mirrorMap in this.mirrorMaps) {
                sum += mirrorMap.GetPoints2();
            }

            return sum;
        }

    }
}
