using AdventOfCode2024.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AdventOfCode2024 {

    internal class SafetyManualPages {

       public List<Int32> pages = new List<Int32>();

        public SafetyManualPages(List<Int32> pages) {
        
            this.pages = pages;
        }

        public SafetyManualPages(string input) {

            // input format --> 75,47,61,53,29
            string[] rawInput = input.Split(',');

            foreach (string rawPage in rawInput) {

                pages.Add(Int32.Parse(rawPage));
            }
        }

        public bool IsCorrectlyOrdered(List<PageOrderingRule> rules) {

            foreach (PageOrderingRule rule in rules) {

                foreach (Int32 page in this.pages) {

                    if (rule.page1 == page) {

                        if (!(this.VerifyOrder(rule.page1, rule.page2))) {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool VerifyOrder(Int32 page1, Int32 page2) {

            foreach (Int32 page in this.pages) {

                if (page == page1) {
                    return true;
                }
                else if (page == page2) {
                    return false;
                }
            }

            Debug.Assert(false, "We should not have gotten here, my friend.");
            return false;
        }

        public int MiddleNumber() {

            double middleIndexRaw = this.pages.Count / 2;
            int middleIndex = (int) Math.Floor(middleIndexRaw);
            
            return this.pages[middleIndex];
        }

        public SafetyManualPages Fix(List<PageOrderingRule> rules) {

            List<Int32> outputPages = new List<Int32>();

            // For each new page you want to add to the output list ...
            foreach (Int32 page in this.pages) {

                // take a look at each existing output page ...
                bool bail = false;
                for (int outputPageIndex = 0; outputPageIndex < outputPages.Count; outputPageIndex++) {

                    // and look at each rule ...
                    foreach (PageOrderingRule rule in rules) {

                        // and see if that rule tells us the new page has to be before that output page ...
                        if ((rule.page1 == page) && (rule.page2 == outputPages[outputPageIndex])) {
                            outputPages.Insert(outputPageIndex, page);
                            bail = true;
                            break;
                        }
                    }
                    if (bail) break;
                }

               if (!(bail)) {
                    outputPages.Add(page);
                }
            }

            SafetyManualPages output = new SafetyManualPages(outputPages);
            return output;
        }        
    }
}
