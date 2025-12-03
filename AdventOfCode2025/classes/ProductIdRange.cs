using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode2025 {

    internal class ProductIdRange {

        private long start;
        private long end;
        private List<long> invalidIds = new List<long>();
        private List<long> invalidIds2 = new List<long>();


        public ProductIdRange(long _start, long _end) {
            
            this.start = _start;
            this.end = _end;

            this.CalculateInvalidIds();
            this.CalculateInvalidIds2();
        }

        private void CalculateInvalidIds() {

            for (long i=start; i<=end; i++) {

                string currentId = i.ToString();
                int length = currentId.Length;

                if (length % 2 == 0) { // is this number even?
                    
                    string firstHalf = currentId.Substring(0, (length / 2));
                    string secondHalf = currentId.Substring((length / 2));

                    if (firstHalf == secondHalf) {
                        this.invalidIds.Add(i);
                    }
                }
            }
        }


        private void CalculateInvalidIds2() {

            // Now, an ID is invalid if it is made only of some sequence of digits repeated at least twice.
            // So, 12341234(1234 two times), 123123123(123 three times), 1212121212(12 five times),
            // and 1111111(1 seven times) are all invalid IDs.

            for (long i = start; i <= end; i++) {

                string currentId = i.ToString();
                int length = currentId.Length;

                int halfLength = currentId.Length / 2;

                for (int j = 0; j < halfLength; j++) {

                    int currentCharCount = 1 + j;
                    string currentSubStringToTest = currentId.Substring(0, currentCharCount);
                    string constructedCurrentId = "";
                    int repeatCount = length / currentCharCount;

                    for (int k = 0; k < repeatCount; k++) {
                        constructedCurrentId += currentSubStringToTest;
                    }

                    if (currentId == constructedCurrentId) {
                        this.invalidIds2.Add(i);
                        break;
                    }
                }
            }

            return;
        }


        public int GetInvalidIdCount() {
            return this.invalidIds.Count;
        }   

        public long GetInvalidIdSum() {
            return this.invalidIds.Sum();
        }

        public int GetInvalidId2Count() {
            return this.invalidIds2.Count;
        }

        public long GetInvalidId2Sum() {
            return this.invalidIds2.Sum();
        }


        public void Print() {
            Console.WriteLine("start: " + this.start + ", end: " + this.end );
        }
    }
}
