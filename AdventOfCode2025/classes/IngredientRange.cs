using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    public class IngredientRange {

        public ulong start;
        public ulong end;

        public IngredientRange(string input) {

            // Example input: "1-3 or 15-17"
            string[] parts = input.Split('-');
            this.start = ulong.Parse(parts[0]);
            this.end = ulong.Parse(parts[1]);
        }

        public ulong span {
            get {
                return this.end - this.start + 1;
            }
        }

        public void Print() {
            Console.WriteLine("start: " + this.start + ", end: " + this.end + ", span: " + this.span);
        }

        public bool IsInRange(ulong value) {
            return (value >= this.start) && (value <= this.end);
        }


        public bool TryToMerge(IngredientRange other) {

            // if the other.start is between my start and my end, then just change my end to be max of our ends.
            if ((other.start >= this.start) && (other.start <= this.end + 1)) {

                // Merge other into this.
                this.end = Math.Max(this.end, other.end);
                return true;
            }

            else if ((other.end >= this.start - 1) && (other.end <= this.end)) {
                // Merge other into this.
                this.start = Math.Min(this.start, other.start);
                return true;
            }
            
            return false;
        }
    }
}


