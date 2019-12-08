
namespace AdventOfCode2019 {

    public class Amplifier {

        string[] instructions;
        string phaseSetting;
        string inputSignal;

        public Amplifier(string[] instructions, string phaseSetting, string inputSignal) {

            this.instructions = instructions;
            this.phaseSetting = phaseSetting;
            this.inputSignal = inputSignal;
        }

        public string Run() {

            object[] returnObject = Program.Day7_Intcode(instructions, phaseSetting, inputSignal);
            string output = (string)returnObject[0];
            string[] instructionsModified = (string[])returnObject[1];

            this.instructions = instructionsModified;
            return output;
        }
    }
}
