using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {

    public class AmplifierSeries {

        string[] instructions;
        List<Amplifier> amplifiers = new List<Amplifier>();

        public AmplifierSeries(string[] instructions, string[] phaseSettings) {

            this.instructions = instructions;

            // Create a series of 5 amplifiers.
            for (int i = 0; i < 5; i++) {
                this.amplifiers.Add(new Amplifier(this.instructions, phaseSettings[i], this));
            }
        }

        public string Run(string inputSignal ) {

            string input = inputSignal;

            // Run through them.
            for (int i = 0; i < this.amplifiers.Count; i++) {
                input = this.amplifiers[i].Run(input);
            }

            return input;
        }

        public string RunFeedbackLoop(string inputSignal) {

            // Assert that we have all amplifiers waiting to go.
            for (int i = 0; i < this.amplifiers.Count; i++) {
                Program.Assert((!(this.amplifiers[i].currentlyRunning)), "ERROR! Amplifier " + i + " is running!");
            }

            string outputSignal = "-1";
            int currentAmplifier = 0;
            bool keepGoing = true;

            while (keepGoing) {

                string returnValue = this.amplifiers[currentAmplifier].Run(inputSignal);

                // special case!  If return value = -2, and error occurred.
                if (returnValue == "-2") return returnValue;

                if (returnValue != "-1") outputSignal = returnValue;
                inputSignal = outputSignal;
                keepGoing = this.amplifiers[currentAmplifier].currentlyRunning;
                currentAmplifier++;
                if (currentAmplifier == this.amplifiers.Count) currentAmplifier = 0;
            }

            return outputSignal;
        }
    }
}
