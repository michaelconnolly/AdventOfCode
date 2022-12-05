using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdventOfCode2022 {

    internal class CrateStack {

        public Collection<char> crates = new Collection<char>();

        public CrateStack() {}

        public void Push(char crate) {
            crates.Add(crate);
        }

        public void Push(char[] cratesInput) {

            foreach (char crate in cratesInput) {
                this.crates.Add(crate);
            }
        }

        public char Pop() {

            char crate = this.crates.Last();
            this.crates.RemoveAt(this.crates.Count - 1);
            return crate;
        }

        public char[] Pop(int count) {

            char[] cratesOutput = new char[count];
            int firstCrateToPop = this.crates.Count - count;
            int j = count - 1;

            for (int i = (this.crates.Count - 1); i >= firstCrateToPop; i--) {
                cratesOutput[j] = this.crates.Last();
                this.crates.RemoveAt(this.crates.Count - 1);
                j--;
            }
            
            return cratesOutput;
        }

        public void Print() {

            foreach (char crate in crates) {
                Console.Write(crate);
            }
            Console.WriteLine();
        }
    }
}
