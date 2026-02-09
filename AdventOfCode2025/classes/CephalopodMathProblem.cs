using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    internal class CephalopodMathProblem {

        public string _operator;
        public List<string> elements = new List<string>();

        public CephalopodMathProblem(string _operator) {

            this._operator = _operator;
        }

        public long Process() {

            long output;
            if (this._operator == "+") {
                output = 0;
            }
            else if (this._operator == "*") {
                output = 1;
            }
            else {
                Debug.Assert(false);
                output = 1;
            }

            foreach (string element in elements) {

                long value = long.Parse(element);
                if (this._operator == "+") {
                    output += value;
                }
                else if (this._operator == "*") {
                    output *= value;
                }
                else {
                    Debug.Assert(false);

                }
            }
            
            return output;
        }

        public void Print() {

            Console.WriteLine("output: " + this.Process());
        }
    }
}
