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
                // input = Amplifier(instructions, phaseSettings[i], input);
                //input = new Amplifier(instructions, phaseSettings[i], input).Run();
            }

            return input;
        }

        public void AmplifierChangedOutput(Amplifier amplifier, string outputValue) {

            int nextAmplifier = -1; // whichAmplifierChanged++;
            //if (nextAmplifier == 5) nextAmplifier = 0;

            for (int i=0; i<this.amplifiers.Count; i++) {
                if (this.amplifiers[i] == amplifier) {
                    nextAmplifier = i++;
                }
            }

            if (nextAmplifier == 5) nextAmplifier = 0;
            if (nextAmplifier == -1) Console.WriteLine("ERROR: AmplifierChangedOutput: bad Amplifier id!");

            this.amplifiers[nextAmplifier].ChangeInputValue(outputValue);
        }

        public string RunFeedbackLoop(string inputSignal) {

            string input = inputSignal;
            string output;

            Console.WriteLine("Begin Part 2.");

            for (int loopCount = 0; loopCount < 10; loopCount++) {

                // Run through them.
                for (int i = 0; i < this.amplifiers.Count; i++) {

                    Console.WriteLine("\tStarting loop " + loopCount.ToString() + " amplifier " + i.ToString() + " ...");

                    output = this.amplifiers[i].Run(input);
                   
                    Console.WriteLine("\tFinished loop " + loopCount.ToString() + " amplifier " + i.ToString() + " - input: " + input + "; output: " + output);

                    input = output;

                    // input = Amplifier(instructions, phaseSettings[i], input);
                    //input = new Amplifier(instructions, phaseSettings[i], input).Run();
                }

                
            }

            Console.WriteLine("End of Part 2: " + input);

            return input;
        }

    }
}
