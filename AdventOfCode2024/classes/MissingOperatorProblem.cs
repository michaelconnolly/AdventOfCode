using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AdventOfCode2024 {


    internal class MissingOperatorProblem {

        public long Answer;
        public List<long> Parts = new List<long>();

        public MissingOperatorProblem(string input) {

            string[] inputParts = input.Split(':');
            this.Answer = long.Parse(inputParts[0]);
            string partsRaw = inputParts[1].Substring(1);
            string[] parts = partsRaw.Split(' ');

            foreach (string partRaw in parts) {
                this.Parts.Add(long.Parse(partRaw));
            }
        }


        private List<long> possibleValues(List<long> values1, long value2, char[] operators) {

            List<long> returnValues = new List<long>();

            for (int value1Index = 0; value1Index < values1.Count; value1Index++) {

                long value1 = values1[value1Index];

                for (int i = 0; i < operators.Length; i++) {

                    switch (operators[i]) {

                        case '+':
                            returnValues.Add(value1 + value2);
                            break;

                        case '*':
                            returnValues.Add(value2 * value1);
                            break;
                        
                        case '|':

                            int lengthOfValue2 = value2.ToString().Length;
                            int multiplier = (int) Math.Pow(10, lengthOfValue2);
                            long newValue = (value1 * multiplier) + value2;
                            returnValues.Add(newValue);
                            break;

                        default:
                            Debug.Assert(false, "should not have gotten here.");
                            break;
                    }
                }
            }
            return returnValues;
        }

        public bool Evalute(char[] operators) {

            List<long> possibleAnswers = new List<long>();

            bool fContinue = true;
            possibleAnswers.Add(this.Parts[0]);
            int valueIndex2 = 1;

            while (fContinue) {
     
                long value2 = this.Parts[valueIndex2];
                possibleAnswers = this.possibleValues(possibleAnswers, value2, operators);

                valueIndex2++;

                if (valueIndex2 >= this.Parts.Count) {
                    fContinue = false;
                }
            }

            foreach (long possibleValue in possibleAnswers) {

                if (possibleValue == this.Answer) return true;
            }

            return false;
        }
    }
}

    

