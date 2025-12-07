using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    internal class BatteryBanks {

        //private string[] banks;
        private List<BatteryBank> banks = new List<BatteryBank>();

        public BatteryBanks(string[] input) {

            //this.banks = input;

            foreach (string bank in input) {
                this.banks.Add(new BatteryBank(bank));
                //batteryBank.Print();
            }

        }

        public int JoltageOutput() {

            int output = 0;

            foreach (BatteryBank bank in this.banks) {
                output += bank.JoltageOutput();
            }

            return output;
        }

        public long JoltageOutput2() {
            long output = 0;
            foreach (BatteryBank bank in this.banks) {
                output += bank.JoltageOutput2();
            }
            return output;
        }

    }

}
