using System.Collections.Generic;


namespace AdventOfCode2024 {

    internal class MissingOperatorEvaluator {

        public List<MissingOperatorProblem> Problems = new List<MissingOperatorProblem>();


        public MissingOperatorEvaluator(string[] lines) {

            foreach (string line in lines) {
                this.Problems.Add(new MissingOperatorProblem(line));
            }
        }

        public long TotalCalibrationValue() {

            long sum = 0;
            char[] operators = { '*', '+' };

            foreach (MissingOperatorProblem problem in Problems) {

                if (problem.Evalute(operators)) {
                    sum += problem.Answer;
                }
            }

            return sum;
        }


        public long TotalCalibrationValue2() {

            long sum = 0;
            char[] operators = { '*', '+', '|' };

            foreach (MissingOperatorProblem problem in Problems) {

                if (problem.Evalute(operators)) {
                    sum += problem.Answer;
                }
            }

            return sum;
        }
    }
}
