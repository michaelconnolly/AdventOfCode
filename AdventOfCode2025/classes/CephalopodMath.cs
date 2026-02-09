using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2025 {

    internal class CephalopodMath {

        List<CephalopodMathProblem> mathProblems = new List<CephalopodMathProblem>();
        List<CephalopodMathProblem> mathProblems2 = new List<CephalopodMathProblem>();

        public CephalopodMath(string[] input) {

            // preflight the last row to see how many columns there are.
            List<string> operators = this.ParseRow(input[input.Length - 1]);

            // Create the right amount of math problems.
            for (int i = 0; i < operators.Count; i++) {
                mathProblems.Add(new CephalopodMathProblem(operators[i]));
            }

            // Get all the elements, but skip the last one, we already have it.
            for (int i = 0; i < (input.Length - 1); i++) {

                List<string> elements = this.ParseRow(input[i]);
                if (elements.Count != operators.Count) { Debug.Assert(false); }

                for (int j = 0; j < elements.Count; j++) {
                    mathProblems[j].elements.Add(elements[j]);
                }
            }

            // Prep work for part two: identify columns.
            bool[] isColumn = this.SpaceColumnIdentifier(input);

            // part two.
            this.FindMathProblem2(isColumn, input);

        }


        private List<string> ParseRow(string input) {

            List<string> elements = new List<string>();
            int start = -10000;
            bool withinElement = false;

            for (int i = 0; i < input.Length; i++) {

                if ((i == (input.Length - 1)) && withinElement) {

                    string newElement = input.Substring(start);
                    elements.Add(newElement);
                    start = -10000;
                    withinElement = false;

                }

                else if ((i == (input.Length - 1))) {

                    // just one char.
                    Debug.Assert(start == -10000);

                    if (input[i] != ' ') {
                        string newElement = input.Substring(i, 1);
                        elements.Add(newElement);
                        start = -10000;
                        withinElement = false;
                    }

                }

                else if (((input)[i] == ' ') && withinElement) {
                    string newElement = input.Substring(start, i - start);
                    elements.Add(newElement);
                    start = -10000;
                    withinElement = false;
                }
                else if (withinElement) {
                    // do nothing
                }
                else if (input[i] == ' ') {
                    // do nothing.
                }
                else if (i == (input.Length - 1)) {
                    Debug.Assert(false);
                }
                else if (input[i] != ' ') {
                    withinElement = true;
                    start = i;
                }

            }

            return elements;
        }


        public void FindMathProblem2(bool[] isSpaceColumn, string[] input) {

            List<string> elements = new List<string>();

            for (int col = (isSpaceColumn.Length - 1); col >= 0; col--) {

                if (!isSpaceColumn[col]) {

                    string currentElement = "";

                    // remember to skip the last row because that is where operators are.
                    for (int row = 0; row < input.Length - 1; row++) {

                        currentElement += input[row][col];
                    }

                    elements.Add(currentElement);
                }
                else {
                    // the operator is in the column beforehand.
                    string oper = input[input.Length - 1][col+1].ToString();

                    CephalopodMathProblem mathProblem = new CephalopodMathProblem(oper);
                    mathProblem.elements = elements;
                    this.mathProblems2.Add(mathProblem);

                    elements = new List<string>(); 
                }

            }

            // do we have leftover elements?
            if (elements.Count != 0) {

                // the operator is in the first column.
                string oper = input[input.Length - 1][0].ToString();

                CephalopodMathProblem mathProblem = new CephalopodMathProblem(oper);
                mathProblem.elements = elements;
                this.mathProblems2.Add(mathProblem);
                //elements = new List<string>();
            }
        }


        public bool[] SpaceColumnIdentifier(string[] input) {

            int columnCount = input[0].Length;
            bool[] isColumn = new bool[columnCount];
            
            // initialize values.
            for (int i = 0; i < columnCount; i++) {
                isColumn[i] = true;
            }

            // check to see if we store something.
            for (int row=0; row<input.Length; row++) {
                for (int col=0; col<columnCount; col++) {
                    if (input[row][col] != ' ') {
                        isColumn[col] = false;
                    }
                }
            }

            return isColumn;
        }


        public long QuestionOne() {

            long output = 0;

            foreach (CephalopodMathProblem mathProblem in this.mathProblems) {
                mathProblem.Print();
                output += mathProblem.Process();
            }

            return output;
        }

        public long QuestionTwo() {

            long output = 0;

            foreach (CephalopodMathProblem mathProblem in this.mathProblems2) {
                mathProblem.Print();
                output += mathProblem.Process();
            }

            return output;
        }
    }
}
