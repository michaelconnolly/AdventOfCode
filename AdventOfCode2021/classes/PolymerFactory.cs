using System;
using System.Collections.Generic;
using System.Text;


namespace AdventOfCode2021 {


    public class PolymerFactory {

        private string template; // we only use this in the non-scalable solution.
        public string[,] pairInsertionRules;

        private bool DO_IT_IN_A_SCALABLE_WAY = true;
        private Dictionary<string, long> countOfPairs = new Dictionary<string, long>(); // we only use this in the scalable solution.
        string firstPair;
        string lastPair;


        public PolymerFactory(string[] lines) {


            // populate the pair Insertion rules.
            this.pairInsertionRules = new string[lines.Length - 2, 2];
            for (int i = 2; i < lines.Length; i++) {
                string[] pairInsertionRule = lines[i].Split(" -> ");
                this.pairInsertionRules[i - 2, 0] = pairInsertionRule[0];
                this.pairInsertionRules[i - 2, 1] = pairInsertionRule[1];
            }

            if (!DO_IT_IN_A_SCALABLE_WAY) {

                this.template = lines[0];
            }
            else {
                // Prep our dictionary.
                this.firstPair = lines[0].Substring(0, 2);
                this.lastPair = lines[0].Substring(lines[0].Length - 2, 2);

                for (int i = 0; i < lines[0].Length - 1; i++) {

                    string pair = lines[0][i].ToString() + lines[0][i + 1].ToString();
                
                    if (countOfPairs.ContainsKey(pair)) {
                        countOfPairs[pair]++;
                    }
                    else countOfPairs[pair] = 1;
                }
            }
        }




        public long GetTemplateLength() {

            // Sometimes we can be simple about things.
            if (!DO_IT_IN_A_SCALABLE_WAY) return this.template.Length;

            // And sometimes we can't.
            long count = 0;
            foreach (string pair in this.countOfPairs.Keys) {
                count += this.countOfPairs[pair];
            }
            count++; // the last character!

            return count;
        }

        public void iterate() {
          
            // This first algorithm works fine for 14a, but chokes hard on 14b, due to scale issues of traversing the string every time.
            if (!DO_IT_IN_A_SCALABLE_WAY) {

                string newTemplate = "";


                for (int i = 0; i < template.Length - 1; i++) {
                    string pairToMatch = template[i].ToString() + template[i + 1].ToString();
                    string toInsert = this.findInsertionRule(pairToMatch);
                    if (i == 0) newTemplate += pairToMatch[0]; // for the very first one, lay down the first char.  Afterwards, it's already there.
                    newTemplate += toInsert + pairToMatch[1];
                }


                template = newTemplate;
                return;
            }

            // Must use this algorithm to succeed in 14b.
            Dictionary<string, long> newCountOfPairs = new Dictionary<string, long>();

            bool foundFirstPair = false;
            bool foundLastPair = false;

            // Prep our dictionary.
            foreach (string key in this.countOfPairs.Keys) {

                string toInsert = this.findInsertionRule(key);
                string newPair1 = key[0] + toInsert;
                string newPair2 = toInsert + key[1];
                long count = this.countOfPairs[key];

                if (newCountOfPairs.ContainsKey(newPair1)) {
                    newCountOfPairs[newPair1] += count;
                }
                else newCountOfPairs[newPair1] = count;

                if (newCountOfPairs.ContainsKey(newPair2)) {
                    newCountOfPairs[newPair2] += count;
                }
                else newCountOfPairs[newPair2] = count;

                // Make sure we are accurately tracking the firstPair and lastPair; important for math later.
                if (!foundFirstPair && key == this.firstPair) {
                    this.firstPair = newPair1;
                    foundFirstPair = true;
                }
                if (!foundLastPair && key == this.lastPair) {
                    this.lastPair = newPair2;
                    foundLastPair = true;
                }

            }

            this.countOfPairs = newCountOfPairs;
        }


        private string findInsertionRule(string pair) {

            for (int i=0; i<(pairInsertionRules.Length/2); i++) {
                if (pairInsertionRules[i, 0] == pair) return pairInsertionRules[i, 1];
            }

            throw new Exception();
        }


        public long MostCommonMinusLeastCommon() {

            long mostCommon = long.MinValue;
            long leastCommon = long.MaxValue;
            Dictionary<char, long> countOfChars = new Dictionary<char, long>();

            if (!this.DO_IT_IN_A_SCALABLE_WAY) {
         
                for (int i = 0; i < this.template.Length; i++) {

                    char currentChar = this.template[i];

                    if (!countOfChars.ContainsKey(currentChar)) {
                        countOfChars[currentChar] = 1;
                    }
                    else {
                        countOfChars[currentChar]++;
                    }
                }
            }
            else {

                foreach (string key in this.countOfPairs.Keys) {

                    char char1 = key[0];
                    char char2 = key[1];
                    long countChar1 = this.countOfPairs[key];
                    long countChar2 = this.countOfPairs[key];

                    if (countOfChars.ContainsKey(char1)) {
                        countOfChars[char1] += countChar1; // this.countOfPairs[key];
                    }
                    else countOfChars[char1] = countChar1; // this.countOfPairs[key];

                    if (countOfChars.ContainsKey(char2)) {
                        countOfChars[char2] += countChar2; // this.countOfPairs[key];
                    }
                    else countOfChars[char2] = countChar2; // this.countOfPairs[key];

                }
            }

            // Fix up.  We need to halve all the amounts, but before we do that, bump up the first and last char count by 1.
            countOfChars[this.firstPair[0]]++;
            countOfChars[this.lastPair[1]]++;

            Dictionary<char, long> realCountOfChars = new Dictionary<char, long>();
            foreach (char key in countOfChars.Keys) {
                realCountOfChars[key] = countOfChars[key] / 2;
            }
            countOfChars = realCountOfChars;


                foreach (char key in countOfChars.Keys) {

                // DEBUG
                Console.WriteLine(key.ToString() + ": " + countOfChars[key]);

                // Divide by half!
                long realCount = (countOfChars[key]);

                // Figure out which is highest or lowest.
                if (realCount > mostCommon) mostCommon = realCount;
                if (realCount < leastCommon) leastCommon = realCount;
            }

            return (mostCommon - leastCommon);
        }


        public void print() {

            if (!DO_IT_IN_A_SCALABLE_WAY) {
                Console.WriteLine(this.template);
            }

            foreach (string key in this.countOfPairs.Keys) {
                Console.WriteLine(key + ": " + this.countOfPairs[key]);
            }
            Console.WriteLine("");

            Console.WriteLine("first pair: " + this.firstPair + "; last pair: " + this.lastPair);
            Console.WriteLine("");
        }
    }
}
