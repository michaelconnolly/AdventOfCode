using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {
    internal class HashValue {

        public string input;
        public int output;
        public string label;
        public char oper;
        public int focalLength = -1;
        public int boxId = -1;

        public HashValue(string input) {

            this.input = input;

            this.output = GenerateHashValue(this.input);

            // Is the operator a '-'?
            if (this.input.Last() == '-') {

                this.label = this.input.Substring(0, this.input.Length - 1);
                this.oper = '-';
            }
            // Otherwise, it has to be a '='.  Let's just assume that. Error handling is for the weak.
            else {

                string[] parts = this.input.Split("=");
                this.label = parts[0];
                this.oper = '=';
                this.focalLength = Convert.ToInt32(parts[1]);
            }

            this.boxId = this.GenerateHashValue(this.label);
        }

        private int GenerateHashValue(string s) {

            int val = 0;

            foreach (char c in s) {

                // Step one: add the ascii code value.
                int asciiCode = Convert.ToInt32(c);
                val += asciiCode;

                // Step two: Set the current value to itself multiplied by 17.
                val = (val * 17);

                // Step three: Set the current value to the remainder of dividing itself by 256.
                val = val % 256;
            }

            return val;
        }
    }
}
