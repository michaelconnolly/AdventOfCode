using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021 {
    public class SevenSegmentDisplay {

        public string[] tenValues;
        public string[] outputCodes;
        public int[] countOfEachCodeInOutputCodes;
        public int countOfUniqueLengthCodes = 0;
        public string[] thisDigitIsThatCode = new string[10];
        public int outputValue = 0;


        public SevenSegmentDisplay(string[] tenValues, string[] outputCodes) {

            this.tenValues = tenValues;
            this.outputCodes = outputCodes;

            // Pre-calc some values we will need later, for 8a.
            this.calculateCountOfEachCode();

            // Pre-calc the values necessary for 8b.
            this.decode();
        }


        private void calculateCountOfEachCode() {

            foreach (string outputCode in this.outputCodes) {

                if ((outputCode.Length == 2 || outputCode.Length == 4 || outputCode.Length == 3 || outputCode.Length == 7)) {
                    this.countOfUniqueLengthCodes++;
                }
            }

        }


        private void findDigitBasedOnCodeLength() {

            foreach (string code in this.tenValues) {
                if (code.Length == 2) thisDigitIsThatCode[1] = code;
                if (code.Length == 4) thisDigitIsThatCode[4] = code;
                if (code.Length == 3) thisDigitIsThatCode[7] = code;
                if (code.Length == 7) thisDigitIsThatCode[8] = code;
            }
        }


        private string findExtraChars(string smallerString, string largerString) {

            string output = "";

            foreach (char aChar in largerString) {
                if (!smallerString.Contains(aChar)) output += aChar;
            }

            return output;
        }


        private bool match(string a, string b) {

            if (a.Length != b.Length) return false;

            foreach(char aChar in a.ToCharArray()) {
                if (!(b.Contains(aChar))) return false;
            }

            return true;
        }


        private bool contains(string smallerString, string largerString) {

            foreach (char aChar in smallerString.ToCharArray()) {
                if (!(largerString.Contains(aChar))) return false;
            }

            return true;
        }


        private void decode() {

            // Get the 4 easy one's first.
            this.findDigitBasedOnCodeLength();

            // Figure out what 'a' is, which is the character in the 3-letter code that is not in the 2-letter code.
            string a_only = this.findExtraChars(thisDigitIsThatCode[1], thisDigitIsThatCode[7]);
          
            // The 'CF' pair is the same as digit 1.
            string c_and_f = thisDigitIsThatCode[1];

            // I can calculate the "BD" pair by seeing the delta between the code for '1' and '4'.
            string b_and_d = this.findExtraChars(thisDigitIsThatCode[1], thisDigitIsThatCode[4]);

            foreach (string value in this.tenValues) {

                // For the three five-wire digits, '3' has the 'a' wire plus c_and_f; '5' has b_and_d in it, and '2' is left over.
                if (value.Length == 5) {
                    bool has_b_and_d = this.contains(b_and_d, value);
                    bool match_the_3 = this.contains(a_only + c_and_f, value);

                    if (match_the_3) thisDigitIsThatCode[3] = value;
                    else if (has_b_and_d) thisDigitIsThatCode[5] = value;
                    else thisDigitIsThatCode[2] = value;
                }

                // For the three six-wire digits, '9' has b_and_d and c_and_f in it, and '0' has just c_and_f, and '6' has just b_and_d.
                if (value.Length == 6) {
                    bool has_b_and_d = this.contains(b_and_d, value);
                    bool has_c_and_f = this.contains(c_and_f, value);

                    if (has_b_and_d && has_c_and_f) thisDigitIsThatCode[9] = value;
                    else if (has_b_and_d) thisDigitIsThatCode[6] = value;
                    else thisDigitIsThatCode[0] = value;
                }
            }

            this.outputValue += decodeOutputCode(outputCodes[0]) * 1000;
            this.outputValue += decodeOutputCode(outputCodes[1]) * 100;
            this.outputValue += decodeOutputCode(outputCodes[2]) * 10;
            this.outputValue += decodeOutputCode(outputCodes[3]);
        }


        private int decodeOutputCode(string outputCode) {

            for (int i=0; i<thisDigitIsThatCode.Length; i++) {
                if (this.match(outputCode, thisDigitIsThatCode[i])) {
                    return i;
                }
            }

            throw new Exception(); 
        }
    }
}
