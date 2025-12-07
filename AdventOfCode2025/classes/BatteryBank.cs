using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

  
    internal class BatteryBank {

        string batteryList;
        List<int> firstBatteryCandidates = new List<int>();
        int firstBattery = 0;
        int secondBattery = 0;
        string batteryList12;


        public BatteryBank(string input) {
            this.batteryList = input;

            this.FindFirstBatteryCandidates();
            this.FindSecondBattery();

            this.FindTwelveBatteries();

            this.Print();
        }


        private void FindTwelveBatteries() {

           this.batteryList12 = this.batteryList;
           int deletesRemaining = this.batteryList.Length - 12;
           int currentIndex;

            for (int i = 1; i<=9; i++) {

                currentIndex = 0;

                while (currentIndex < this.batteryList12.Length) {
                    if (this.batteryList12[currentIndex] == i.ToString()[0]) {

                        this.batteryList12 = this.batteryList12.Remove(currentIndex, 1);
                        deletesRemaining--;
                       

                        if (deletesRemaining == 0) return;

                        if (currentIndex >= this.batteryList12.Length) {
                            Debug.Assert(false);
                        }
                    }

                    currentIndex++;
                }
            }




        }

        private void FindFirstBatteryCandidates() {

            bool foundCandidates = false;

            for (int batteryLevel = 9; batteryLevel > 0; batteryLevel--) {

                // skip the last battery, because if it doesn't have a battery to the right of it,
                // it's not a candidate for first battery. 
                for (int index = 0; index < (this.batteryList.Length - 1); index++) {

                    if (this.batteryList[index] == batteryLevel.ToString()[0]) {
                        firstBatteryCandidates.Add(index);
                        foundCandidates = true;
                    }
                }

                if (foundCandidates) break;

               
            }

    
        }

        private void FindSecondBattery() {

            // Assumption is that all firstBatteryCandidates are of the same battery level.
            // So, all we have to do is compare who found the best second battery to the right of themselves.  

            int indexBestFirstBattery = 0;
            int indexBestSecondBattery = 0;

            foreach (int firstBatteryCandidate in this.firstBatteryCandidates) {

                int startIndex = firstBatteryCandidate + 1;
                bool foundSecondBattery = false;
                //if (startIndex <= 9) {

                for (int i = startIndex; i < this.batteryList.Length; i++) {

                    for (int batteryLevel = 9; batteryLevel > 0; batteryLevel--) {

                        if (this.batteryList[i] == batteryLevel.ToString()[0]) {

                            if (indexBestSecondBattery == 0) {

                                indexBestFirstBattery = firstBatteryCandidate;
                                indexBestSecondBattery = i;
                                foundSecondBattery = true;
                                break;
                            }
                            else {
                                if (int.Parse(this.batteryList[i].ToString()) >
                                    int.Parse(this.batteryList[indexBestSecondBattery].ToString())) {
                                    
                                    indexBestFirstBattery = firstBatteryCandidate;
                                    indexBestSecondBattery = i;
                                    foundSecondBattery = true;
                                    break;
                                }
                            }
                        }
                    }

                  //  if (foundSecondBattery) break;

                }
            }

            this.firstBattery = indexBestFirstBattery;
            this.secondBattery = indexBestSecondBattery;
        }


        public int JoltageOutput() {

            return (int.Parse(this.batteryList[this.firstBattery].ToString()) * 10) +
                int.Parse(this.batteryList[this.secondBattery].ToString());
        }

        public long JoltageOutput2() {

            return (long.Parse(this.batteryList12));
        }

        public void Print() {

            foreach (int candidate in this.firstBatteryCandidates) {
                Console.Write(candidate.ToString() + " ");
            }
            Console.Write("; output: " + this.JoltageOutput());
            Console.WriteLine();

        }
    }
}
