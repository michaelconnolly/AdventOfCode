using AdventOfCode2024.classes;
using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {

    internal class SafetyManualManager {

        List<PageOrderingRule> rules = new List<PageOrderingRule>();
        List<SafetyManualPages> pagesList = new List<SafetyManualPages>();

        public SafetyManualManager(string[] lines) {

            bool stillProcessingRules = true;
            for (int i = 0; i < lines.Length; i++) {

                if (lines[i] == "") {
                    stillProcessingRules = false;
                }

                // We are at the front, processing ordering rules.
                else if (stillProcessingRules) {

                    string[] rawRules = lines[i].Split('|');
                    this.rules.Add(new PageOrderingRule(Int32.Parse(rawRules[0]), Int32.Parse(rawRules[1])));
                }

                // We are at the tail, processing lists of pages.
                else {
                    this.pagesList.Add(new SafetyManualPages(lines[i]));
                }
            }
        }

        // What do you get if you add up the middle page number
        // from those correctly-ordered updates?
        public int SumOfMiddleNumbersCorrectlyOrdered() {

            int sum = 0;

            foreach (SafetyManualPages page in pagesList) {

                if (page.IsCorrectlyOrdered(this.rules)) {
                    sum += page.MiddleNumber();
                }
            }

            return sum;
        }

        public int SumOfMiddleNumbersIncorrectlyOrderedThenFixed() {

            int sum = 0;

            foreach (SafetyManualPages page in pagesList) {

                if (!(page.IsCorrectlyOrdered(this.rules))) {

                    SafetyManualPages fixedPages = page.Fix(this.rules);
                    sum += fixedPages.MiddleNumber();
                }
            }

            return sum;
        }
    }
}

        
