using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    internal class IndicatorManager {

        public List<IndicatorMachine> indicatorMachines = new List<IndicatorMachine>();


        public IndicatorManager(string[] input) {

            foreach (string machineInput in input) {

                IndicatorMachine indicatorMachine = new IndicatorMachine(machineInput);
                indicatorMachines.Add(indicatorMachine);
            }
        }

        public long FewestTotalPresses() {

            long total = 0;

            foreach (IndicatorMachine machine in this.indicatorMachines) {

                total += machine.FewestPresses();
            }


            return total;
        }
    }
}
