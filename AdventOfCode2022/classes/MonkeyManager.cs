using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2022 {

    public class MonkeyManager {

        public Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();

        public MonkeyManager(string[] lines) {

            this.ProcessInput(lines);
        }

        public void Print() {

            foreach (int key in this.monkeys.Keys) {

                this.monkeys[key].Print();
            }
        }

        private void ProcessInput(string[] input) {

            for (int i = 0; i < input.Length; i++) {

                string line = input[i];

                // New monkey?  
                if (line.Substring(0, 7) == "Monkey ") {

                    //
                    int id = int.Parse(line.Substring(0, (line.Length - 1)).Substring(7));
                    string[] startItems = input[i + 1].Split(":")[1].Split(",");
                    string[] operationParts = input[i + 2].Substring(19).Split(" ");
                    string[] testParts = input[i + 3].Substring(8).Split(" ");
                    int destinationIfTrue = int.Parse(input[i + 4].Substring(29));
                    int destinationIfFalse = int.Parse(input[i + 5].Substring(30));

                    this.monkeys[id] = new Monkey(this, id, startItems, operationParts, testParts,
                        destinationIfTrue, destinationIfFalse);

                    i = i + 6;
                }
                else throw new Exception();
            }
        }


        public void StartMonkeyRounds(int roundCount, bool theSecondWay=false) {

            for (int i = 0; i < roundCount; i++) {

                for (int monkeyId = 0; monkeyId < monkeys.Keys.Count; monkeyId++) {

                    Monkey monkey = this.monkeys[monkeyId];

                    if (monkey.id != monkeyId) throw new Exception();

                    monkey.InspectTestThrow(theSecondWay);
                }


            }



        }

        public long AnswerTheQuestion() {

            long highest1 = 0;
            long highest2 = 0;

            for (int i = 0; i < this.monkeys.Keys.Count; i++) {

                Monkey monkey = this.monkeys[i];

                if (monkey.inspectionCount > highest1) {
                    highest2 = highest1;
                    highest1 = monkey.inspectionCount;
                }
                else if (monkey.inspectionCount > highest2) {
                    highest2 = monkey.inspectionCount;
                }

            }

            return (highest1 * highest2);
        }
    }
}

