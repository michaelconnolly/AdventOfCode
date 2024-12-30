using AdventOfCode2024.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    internal class ArcadeManager {

        public List<ArcadeMachine> machines = new List<ArcadeMachine>();

        public ArcadeManager(string[] input, bool usePartTwoRules) {

            int lineIndex = 0;
            bool fContinue = true;

            while (fContinue) {

                if ((lineIndex >= input.Length) || (input[lineIndex] == "")) {
                    this.machines.Add(new ArcadeMachine(input[lineIndex - 3], input[lineIndex - 2],
                        input[lineIndex - 1],usePartTwoRules));
                }

                if (lineIndex >= input.Length) {
                    fContinue = false;

                }
                else {
                    lineIndex++;
                }
            }
        }

        // Figure out how to win as many prizes as possible. What is the fewest
        // tokens you would have to spend to win all possible prizes?
        public long FewestTokensForAllPrizes() {

            long count = 0;
            int index = 0;

            foreach (ArcadeMachine machine in machines) {
                Console.WriteLine("machine " + index++);

                long cost = machine.FewestTokens();
                if (cost != -1) {
                    count += cost;
                }
            }

            return count;
        }
    }
}
