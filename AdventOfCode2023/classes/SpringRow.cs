using System;
using System.Collections.ObjectModel;
using System.Transactions;

namespace AdventOfCode2023 {
    internal class SpringRow {

        string springList;
        Collection<string> sizeOfBrokenSpringGroups;
        string preferredPattern;
        Collection<string> options = new Collection<string>();

        public SpringRow(string line) {

            // format of each line: ???.### 1,1,3
            string[] parts = line.Split(' ');
            this.springList = parts[0];
            this.sizeOfBrokenSpringGroups = new Collection<string>(parts[1].Split(','));

            this.preferredPattern = this.CalculatePreferredPattern();
            this.CalculateOptions("", 0, this.springList, this.preferredPattern, this.sizeOfBrokenSpringGroups);
        }

        public string CalculatePreferredPattern() {

            string pattern = "";

            for (int i = 0; i < sizeOfBrokenSpringGroups.Count; i++) { 
            //foreach (string s in this.sizeOfBrokenSpringGroups) {

                int sizeOfGroup = Convert.ToInt32(this.sizeOfBrokenSpringGroups[i]);
                pattern += "#".PadRight(sizeOfGroup, '#');
                if (i !=  sizeOfBrokenSpringGroups.Count - 1) {
                    pattern += ".";
                }
            }

            return pattern;
        }

        private int FindEndOfBlock(string line, int currentIndex) {

            for (int i = currentIndex; i< line.Length; i++) {
                if (line[i] == '.') {
                    return i;
                }
            }

            return (line.Length - currentIndex);
        }

        private string AddChars(string s, char c, int count) {

            string output = s;
            for (int i = 0; i < count;i++) {
                output += c;
            }

            return output;
        }

        public void CalculateOptions(string alreadyProcessed, int extraPaddingUsedAlready, string needsProcessing, string patternRemaining, Collection<string> groupSizesRemaining) {

            //int countOfExtraPaddingAllowed = this.springList.Length - this.preferredPattern.Length;
            int countOfExtraPaddingAllowed = this.springList.Length - this.preferredPattern.Length - extraPaddingUsedAlready;

            // quick out if preferredPattern is the same length as the current spring.
            // todo: this needs to have another test when called recursively
            //       also check to make sure remainder satisifies pattern.
            if (countOfExtraPaddingAllowed == 0) {
                this.options.Add(this.preferredPattern);
                return;
            }

            string currentlyProcessingStack = "";
            int indexPreferredPattern = 0;
            int indexActualRow = 0;
            bool fContinue = true;
            int paddingUsed = 0;
            bool expectPaddingNext = false;
            int currentBrokenGroupSize = 0;



            while (fContinue) {


                // TODO: maybe have more checks / harden in some way.
                if (needsProcessing.Length <= indexActualRow) {
                    string option = alreadyProcessed + currentlyProcessingStack;
                    this.options.Add(option);
                    return;
                }


                char currentChar = needsProcessing[indexActualRow];

                if (currentChar == '.') {  //} && !expectPaddingNext) {
                    //paddingUsed++;
                    currentlyProcessingStack += '.';

                }
                //else if (currentChar == '.') {
                    // do nothing.
                //}
                else if (currentChar == '#' || currentChar == '?') {

                    string currentGroupSkippingDots = needsProcessing.Substring(currentlyProcessingStack.Length);
                    int sizeOfCurrentGroup = this.FindEndOfBlock(currentGroupSkippingDots, 0); // indexActualRow);
                    string patternForCurrentGroup = currentGroupSkippingDots.Substring(0, sizeOfCurrentGroup);
                    int expectedLength = Convert.ToInt32(groupSizesRemaining[currentBrokenGroupSize]);
                    
                    if (expectedLength > sizeOfCurrentGroup) {
                        Console.WriteLine("bailing on an option.");
                        return;

                    }

                    for (int  i = 0; i <= (sizeOfCurrentGroup - expectedLength); i++) {
                        string option = alreadyProcessed + currentlyProcessingStack;
                        option = this.AddChars(option, '.', i);
                        option = this.AddChars(option, '#', expectedLength);
                        option = this.AddChars(option, '.', sizeOfCurrentGroup - expectedLength - i);
                        //option.PadRight(i, '.');
                        //option.PadRight((option.Length + sizeOfCurrentGroup), '#');
                        //option.PadRight(sizeOfCurrentGroup-i, '.');
                        // Test right here that option is correct.

                        Collection<string> localSizeOfBSG = new Collection<string>();
                        for (int j = 1; j < groupSizesRemaining.Count; j++) {
                            localSizeOfBSG.Add(groupSizesRemaining[j]);
                        }
                     
                        string needsProcessingSub = this.springList.Substring(option.Length);
                        string patternRemainingSub = patternRemaining.Substring(sizeOfCurrentGroup - 1);

                        if ((needsProcessingSub == "") && (patternRemainingSub == "")) {
                            this.options.Add(option);
                            currentlyProcessingStack = "";
                            //break;
                        }
                        else if ((patternRemainingSub.Length > 0) && (patternRemainingSub[0] == '.')) {
                            patternRemainingSub = patternRemainingSub.Substring(1);
                            needsProcessingSub = needsProcessingSub.Substring(1);
                            option += '.';
                        }

                        else {
                            this.CalculateOptions(option, extraPaddingUsedAlready + 1,
                                needsProcessingSub, patternRemainingSub, localSizeOfBSG);


                            currentlyProcessingStack = "";
                        }
                        //optionsPerGroup.Add(option);
                    }

                }
                //else if (currentChar == '?') {

                //}
                else {
                    Console.WriteLine("ERROR!");
                    throw new Exception();
                }

                if (paddingUsed > countOfExtraPaddingAllowed) {
                    Console.WriteLine("ERROR!");
                    throw new Exception();
                }

                indexActualRow++;
            }

            return;
        }

        public void print() {

            Console.Write(this.springList + " - ");
            foreach (string s in this.sizeOfBrokenSpringGroups) {
                Console.Write(s + ",");
            }
            Console.Write(" - " + this.preferredPattern);
            Console.Write(" - option count: " + this.options.Count);

            Console.WriteLine();
        }

        public long GetCountOfComboOptions() {

            return this.options.Count;
        }
    }
}
