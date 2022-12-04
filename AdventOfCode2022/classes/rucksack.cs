using System;
using System.Collections.Generic;

namespace AdventOfCode2022 {

    internal class Rucksack {

        public string compartment1;
        public string compartment2;

        public Rucksack(string line) {

            this.compartment1 = line.Substring(0, ((line.Length / 2)));
            this.compartment2 = line.Substring(line.Length / 2);
        }

        public void print() {
            Console.WriteLine("compartment 1: " + compartment1 + ", compartment 2: " + compartment2 +
                ", bad item: " + this.badItem() + ", score: " + this.score());
        }

        public string badItem() {

            Dictionary<string, int> thingsInCompartment1 = this.contents(1);

            for (int i = 0; i < this.compartment2.Length; i++) {

                string currentThing = this.compartment2.Substring(i, 1);
                if (thingsInCompartment1.ContainsKey(currentThing)) {
                    return currentThing;
                }
            }

            throw new Exception();
        }

        static public int ConvertStringToInt(string input) {

            char badItemChar = input[0];
            int badItemInt = Convert.ToInt32(badItemChar);

            if (badItemInt >= 97 && badItemInt <= 122) {
                badItemInt -= 96;
            }
            else if (badItemInt >= 65 && badItemInt <= 90) {
                badItemInt -= (65 - 27);
            }
            else {
                throw new Exception();
            }

            return badItemInt;
        }

        public int score() {

            string badItem = this.badItem();
            return Rucksack.ConvertStringToInt(badItem);
        }

        public Dictionary<string, int> contents() {

            Dictionary<string, int> contents1 = this.contents(1);
            Dictionary<string, int> contents2 = this.contents(2);

            foreach (string key in contents2.Keys) {

                if (contents1.ContainsKey(key)) {
                    contents1[key]++;
                }
                else {
                    contents1.Add(key, 1);
                }
            }

            return contents1;
        }


        public Dictionary<string, int> contents(int compartmentId) {

            Dictionary<string, int> thingsInCompartment = new Dictionary<string, int>();

            string compartment;
            if (compartmentId == 1) { compartment = this.compartment1; }
            else { compartment = this.compartment2; }

            for (int i = 0; i < compartment.Length; i++) {

                string currentThing = compartment.Substring(i, 1);
                if (thingsInCompartment.ContainsKey(currentThing)) {
                    thingsInCompartment[currentThing]++;
                }
                else {
                    thingsInCompartment.Add(currentThing, 1);
                }
            }

            return thingsInCompartment;
        }
    }
}
