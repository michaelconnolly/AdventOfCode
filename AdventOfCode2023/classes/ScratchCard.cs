using System;
using System.Collections.ObjectModel;

namespace AdventOfCode2023 {

    internal class ScratchCard {

        public int id;
        public string[] winningNumbers;
        public string[] haveNumbers;
        Collection<string> matches = new Collection<string>();
        public int points = 0;
        public int copies = 1;

        public int matchCount {  get { return this.matches.Count; } }

        public ScratchCard(string line) {

            // Format: "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"
            string[] split = line.Split(':');
            string idAsString = split[0].Substring(5).Trim();
            this.id = Convert.ToInt32(idAsString);

            // figure out two sets of numbers.
            split = split[1].Split(" | ");
            this.winningNumbers = split[0].Split(' ');
            this.haveNumbers = split[1].Split(' ');                                                                                                                                                               

            // pre-calculate the points for the first puzzle.
            this.points = CalculatePoints();
        }

        private int CalculatePoints() {

            int winCount = 0;

            foreach (string haveNumber in this.haveNumbers) {

                bool isMatch = false;

                foreach (string winningNumber in this.winningNumbers) {

                    if (haveNumber == "" || winningNumber == "") {
                        // do nothing
                    }
                    else if (haveNumber == winningNumber) {
                        isMatch = true;
                        break;
                    }
                }

                if (isMatch) {
                    this.matches.Add(haveNumber);
                    winCount++;
                }
            }

            if (winCount == 0) return 0;
            else if (winCount == 1) return 1;
            else
                return (int) Math.Pow( 2,(winCount - 1));
        }
    }
}
