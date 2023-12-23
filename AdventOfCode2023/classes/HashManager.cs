using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {
    internal class HashManager {

        Collection<HashValue> hashValues = new Collection<HashValue>();
        Collection<HashLenseBox> lenseBoxes = new Collection<HashLenseBox>();

        public HashManager(string line) {

            // Create the list of sequences.
            string[] sequences = line.Split(',');
            foreach (string sequence in sequences) {
                this.hashValues.Add(new HashValue(sequence));
            }

            // Create the hash boxes.
            for (int i = 0; i < 256; i++) {
                lenseBoxes.Add(new HashLenseBox(i));
            }

            // Process the list of sequences and place them into the correct lensebox.
            this.ProcessSequences();
        }

        private void RemoveLens(string label, int boxId) {

            HashLenseBox lensBoxOld = this.lenseBoxes[boxId];
            HashLenseBox lensBoxNew = new HashLenseBox(lensBoxOld.id);


            foreach (HashLense lens in lensBoxOld.lenses) {
                if (lens.label != label) {
                    lensBoxNew.lenses.Add(lens);
                }
            }

            this.lenseBoxes[boxId] = lensBoxNew;
        }

        private void AddLens(string label, int focalLength, int boxId) {

            HashLenseBox lenseBox = this.lenseBoxes[boxId];
            bool found = false;

            foreach (HashLense lens in lenseBox.lenses) {
                if (lens.label == label) {
                    found = true;
                    lens.focalLength = focalLength;
                }
            }

            if (!found) {
                lenseBox.lenses.Add(new HashLense(focalLength, label));
            }
        }

        private void ProcessSequences() {

            // Examples:
            // rn=1,cm-

            foreach (HashValue hashValue in this.hashValues) {

                // Is the operator a '-'?
                if (hashValue.oper == '-') {
                    this.RemoveLens(hashValue.label, hashValue.boxId);
                }

                // Otherwise, it has to be a '='.  Let's just assume that. Error handling is for the weak.
                else {
                    this.AddLens(hashValue.label, hashValue.focalLength, hashValue.boxId);
                }
            }
        }


        public void print() {

        }

        public long SumOfHashResults() {

            long sum = 0;

            foreach (HashValue hashValue in this.hashValues) {
                sum += hashValue.output;
            }

            return sum;
        }

        public long TotalFocusingPower() {

            long sum = 0;

            foreach (HashLenseBox lenseBox in this.lenseBoxes) {
                sum += lenseBox.FocusingPower();
            }

            return sum;
        }
    }
}
