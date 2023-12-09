using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdventOfCode2023 {

    internal class OasisSensor {

        Collection<string[]> sequences = new Collection<string[]>();

        public OasisSensor(string[] lines) {
        
            foreach (string line in lines) {

                string[] sequence = line.Split(" ");
                sequences.Add(sequence);
            }
        }

        private bool AreTheseAllZeros(string[] sequence) {

            bool allZeros = true;

            for (int i=0; i < sequence.Length; i++) {
                if (sequence[i] != "0") {
                allZeros = false;
                    break;
                }
            }
            return allZeros;
        }

        private string SequenceToString(string[] sequence) {
            string s = "";
            foreach (string line in sequence) {
                s += line + ", ";
            }
            return s;
        }

        private string[] GenerateDiffSequence(string[] sequence) {

            string[] diffSequence = new string[sequence.Length-1];

            for (int i = 0; i < sequence.Length-1; i++) {

                diffSequence[i] = (Convert.ToInt32(sequence[i+1]) - Convert.ToInt32(sequence[i])).ToString();
            }

            return diffSequence;
        }

        private int FindNextValue(string[] sequence) {
            
            string[] diffSequence = this.GenerateDiffSequence(sequence);
            int currentLastValue = Convert.ToInt32(sequence.Last());

            if (this.AreTheseAllZeros(diffSequence)) {
                return ((currentLastValue) + 0);
            }

            return (currentLastValue + this.FindNextValue(diffSequence));
        }

        private int FindPreviousValue(string[] sequence) {

            string[] diffSequence = this.GenerateDiffSequence(sequence);
            int currentPreviousValue = Convert.ToInt32(sequence.First());

            if (this.AreTheseAllZeros(diffSequence)) {
                return ((currentPreviousValue) - 0);
            }

            return (currentPreviousValue - this.FindPreviousValue(diffSequence));
        }

        public void print() {
             
            for (int i = 0; i < this.sequences.Count; i++) {
                Console.Write(this.SequenceToString(this.sequences[i]));
                Console.Write(" - ");
                string[] diffSequence = this.GenerateDiffSequence(this.sequences[i]);
                Console.Write(this.SequenceToString(diffSequence));
                Console.Write(" - " + this.FindNextValue(this.sequences[i]));
                Console.Write(this.SequenceToString(diffSequence));
                Console.Write(" - " + this.FindPreviousValue(this.sequences[i]));

                Console.WriteLine();
            }
        }

        public long GetSumOfNextValues() {

            long sum = 0;

            for (int i=0; i<this.sequences.Count;i++) {
                sum += this.FindNextValue(this.sequences[i]);
            }

            return sum;
        }

        public long GetSumOfPreviousValues() {

            long sum = 0;

            for (int i = 0; i < this.sequences.Count; i++) {
                sum += this.FindPreviousValue(this.sequences[i]);
            }

            return sum;
        }
    }
}
