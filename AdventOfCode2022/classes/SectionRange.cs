using System;

namespace AdventOfCode2022.classes {

    public class SectionRange {

        public int start;
        public int end;

        public SectionRange(string line) {

            string[] range = line.Split('-');
            this.start = int.Parse(range[0]);
            this.end = int.Parse(range[1]);
        }

        public void print() {
            Console.WriteLine("start: " + this.start + ", end:" + end);
        }

        public bool isWithin(SectionRange other) {

            if ((other.start >= this.start) && (other.end <= this.end)) {
                return true;
            }

            return false;
        }

        public bool overlaps(SectionRange other) {

            if ((this.start <= other.start && this.end >= other.start) ||
                (this.start <= other.end && this.start >= other.start))
                {
                return true;
            }

            return false;
        }
    }
}
