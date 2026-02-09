using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    internal class IngredientAnalyzer {

        List<IngredientRange> ingredientRanges = new List<IngredientRange>();
        List<ulong> ingredientRequests = new List<ulong>();

        public IngredientAnalyzer(string[] input) {

            bool atSectionTwo = false;

            for (int i = 0; i < input.Length; i++) {

                if (!atSectionTwo) {  // in Section One.

                    if (input[i] == "") {
                        atSectionTwo = true;
                    }
                    else {
                        // Create the database of ingredient ranges.
                        ingredientRanges.Add(new IngredientRange(input[i]));
                    }
                }
                else {  // in Section Two.
                    // Create the list of ingredients being requested.
                    this.ingredientRequests.Add(ulong.Parse(input[i]));
                }

            }

            Console.WriteLine("Count of ranges: " + this.ingredientRanges.Count);
            Console.WriteLine("Count of requests: " + this.ingredientRequests.Count);
            return;
        }


        List<bool> AnalyzeIngredients() {

            List<bool> results = new List<bool>();

            // For each request ....
            foreach (ulong request in this.ingredientRequests) {

                // Walk through each range and see if i'm in there.
                foreach (IngredientRange range in this.ingredientRanges) {
                    bool result = range.IsInRange(request);
                    results.Add(range.IsInRange(request));
                    if (result) break;

                }
            }

            return results;
        }


        List<IngredientRange> MergeRanges(List<IngredientRange> input) {

            bool fContinue = true;
            List<IngredientRange> listToReview = new List<IngredientRange>(input);
            List<IngredientRange> rangesMerged = new List<IngredientRange>();

            while (fContinue) {

                fContinue = false;

                // for each existing range ...
                foreach (IngredientRange range in listToReview) {

                    bool foundMergeTarget = false;

                    // ... see if i can merge it with an existing merged range.
                    foreach (IngredientRange rangeMerged in rangesMerged) {

                        foundMergeTarget = rangeMerged.TryToMerge(range);
                        if (foundMergeTarget) {
                            fContinue = true;
                            break;
                        }
                    }

                    if (!foundMergeTarget) {
                        rangesMerged.Add(range);
                    }
                }

                if (fContinue) {
                    listToReview = new List<IngredientRange>(rangesMerged);
                    rangesMerged.Clear();
                }
            }

            return rangesMerged;
        }

        public List<IngredientRange> Sort(List<IngredientRange> source) {

            List<IngredientRange> input = new List<IngredientRange>(source);
            List<IngredientRange> output = new List<IngredientRange>();
            IngredientRange lowestStart = null;

            while (output.Count < source.Count) {

                foreach (IngredientRange original in input) {

                    if (lowestStart == null) {
                        lowestStart = original;
                    }
                    else if (original.start < lowestStart.start) {
                        lowestStart = original;
                    }
                }

                if (lowestStart == null) Debug.Assert(false);

                output.Add(lowestStart);
                input.Remove(lowestStart);
                lowestStart = null;
            }

            return output;
        }


        public int CountOfFreshAvailableIngredients() {

            List<bool> analysis = AnalyzeIngredients();

            int count = 0;
            foreach (bool result in analysis) {
                if (result) {
                    count++;
                }
            }
            return count;
        }

        public ulong CountOfFreshAvailableIngredients2() {

            List<IngredientRange> inputSorted = this.Sort(this.ingredientRanges);

            ulong count = 0;
            foreach (IngredientRange range in inputSorted) {
                range.Print();
                count += range.span;
            }
            Console.WriteLine("Total pre-merged: " + count);

            List<IngredientRange> mergedRanges = MergeRanges(inputSorted);
            List<IngredientRange> outputSorted = this.Sort(mergedRanges);

            count = 0;
            foreach (IngredientRange range in outputSorted) {
                range.Print();
                count += range.span;
            }
            Console.WriteLine("Total post-merged: " + count);

            return count;
        }
    }
}
