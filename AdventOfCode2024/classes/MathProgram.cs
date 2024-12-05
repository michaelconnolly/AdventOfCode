using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {

    public class MathProgram {

        const string prefix = "mul(";
        const string postfix = ")";
        const string enableCommand = "do()";
        const string disableCommand = "don't()";

        string input;
        List<MathProblem> mathProblems = new List<MathProblem>();
        List<MathProblem> mathProblemsWithDisables = new List<MathProblem>();

        public MathProgram(string input) {

            this.input = input;

            // Process without supporting disable/enable.
            int i = 0;
            while (i < input.Length) {

                int newIndex = input.IndexOf(prefix, i);
                if (newIndex == -1) break;

                int postfixIndex = input.Substring(newIndex + prefix.Length).IndexOf(postfix);
                if (postfixIndex != -1) {
                    int startFactors = newIndex + prefix.Length;
                    int endFactors = postfixIndex;
                    string factors = input.Substring(startFactors, (endFactors));
                    this.IsWellFormed(factors);
                }

                i = newIndex + prefix.Length;
            }


            // Process with supporting disable/enable.
            List<string> inputParts = new List<string>();

            // First, let's find the disables/enables, and break the string into parts.
            i = 0;
            while (i < input.Length) {

                int newIndex = input.IndexOf(disableCommand, i);
                if (newIndex == -1) {
                    inputParts.Add(input.Substring(i));
                    break;
                }

                inputParts.Add(input.Substring(i, (newIndex - i)));
                i = newIndex + disableCommand.Length;

                int postfixIndex = input.Substring(i).IndexOf(enableCommand);
                if (postfixIndex == -1) {
                    break;
                }
                i = i + postfixIndex + enableCommand.Length;
            }
  
            foreach (string inputPart in inputParts) {

                i = 0;
                while (i < inputPart.Length) {

                    int newIndex = inputPart.IndexOf(prefix, i);
                    if (newIndex == -1) break;

                    int postfixIndex = inputPart.Substring(newIndex + prefix.Length).IndexOf(postfix);
                    if (postfixIndex != -1) {
                        int startFactors = newIndex + prefix.Length;
                        int endFactors = postfixIndex;
                        string factors = inputPart.Substring(startFactors, (endFactors));
                        this.IsWellFormed(factors, true);
                    }

                    i = newIndex + prefix.Length;
                }
            }
        }

        private bool IsWellFormed(string factors, bool supportDisables=false) {

            string[] parts = factors.Split(',');
            int part1, part2;

            if (parts.Length != 2) return false;

            try {
                part1 = Int32.Parse(parts[0]);
                part2 = Int32.Parse(parts[1]);
                if (part1 > 999 || part1 < 0) return false;
                if (part2 > 999 || part2 < 0) return false;
            }
            catch {
                return false;
            }

           
            MathProblem mathProblem = new MathProblem("mul", part1, part2);
            if (!supportDisables) {
                this.mathProblems.Add(mathProblem);
            }
            else {
                this.mathProblemsWithDisables.Add(mathProblem);
            }

            return true;
        }

        public int ProductSums() {

            int sum = 0;

            foreach (MathProblem mathProblem in this.mathProblems) {

                int product = mathProblem.number1 * mathProblem.number2;
                sum += product;
            }

            return sum;
        }

        public int ProductSumsWithDisables() {

            int sum = 0;

            foreach (MathProblem mathProblem in this.mathProblemsWithDisables) {

                int product = mathProblem.number1 * mathProblem.number2;
                sum += product;
            }

            return sum;
        }   
    }
}
