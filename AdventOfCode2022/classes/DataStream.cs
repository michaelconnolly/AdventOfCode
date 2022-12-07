using System;
using System.Collections.Generic;

namespace AdventOfCode2022 {

    internal class DataStream {

        public string dataStream;

        public DataStream(string line) {
            this.dataStream = line;
        }

        public void Print() {
            Console.WriteLine(this.dataStream);
        }

        public int StartMarker() {
            return this.FindMarker(4);
        }

        public int MessageMarker() {
            return this.FindMarker(14);
        }

        private int FindMarker(int markerLength) {

            for (int i = 0; i < (this.dataStream.Length - (markerLength - 1)); i++) {

                string currentMarker = this.dataStream.Substring(i, markerLength);
                Dictionary<char, int> markerChars = new Dictionary<char, int>();
                bool allUnique = true;

                foreach (char c in currentMarker) {
                    if (markerChars.ContainsKey(c)) {
                        allUnique = false;
                        break;
                    }
                    markerChars[c] = 1;
                }

                if (allUnique) return (i + markerLength);
            }

            throw new Exception();
        }
    }
}
