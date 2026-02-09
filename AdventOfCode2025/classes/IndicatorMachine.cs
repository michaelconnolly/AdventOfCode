using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    internal class IndicatorMachine {

        private List<IndicatorLight> IndicatorLights = new List<IndicatorLight>();
        private List<IndicatorButton> IndicatorButtons = new List<IndicatorButton>();

        public IndicatorMachine(string machineInput) {

            // example: [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}

            // first clause: indicator lights: count and desired state.
            int openBracket = machineInput.IndexOf('[');
            int closeBracket = machineInput.IndexOf(']');
            string lights = machineInput.Substring((openBracket + 1), (closeBracket - openBracket - 1));

            foreach (char light in lights) {
                IndicatorLight indicatorLight = new IndicatorLight(light);
                this.IndicatorLights.Add(indicatorLight);
            }

            // third clause: joltage requirements.
            int openCurlyBracket = machineInput.IndexOf("{");
            int closeCurlyBracket = machineInput.IndexOf("}");

            // second clause: indicator buttons.
            int startOfSecondClause = closeBracket + 2;
            int endOfSecondClause = openCurlyBracket - 2;
            string buttonsInput = machineInput.Substring(startOfSecondClause, (endOfSecondClause - startOfSecondClause + 1));
            //char[] delimiters = { '(', ' ', ')' };
            //string[] buttons = buttonsInput.Substring(1, buttonsInput.Length - 2).Split(delimiters);
            string[] buttons = buttonsInput.Substring(1, buttonsInput.Length - 2).Split(new string[] { ") (" }, StringSplitOptions.None);

            foreach (string buttonInput in buttons) {
                IndicatorButton indicatorButton = new IndicatorButton(buttonInput);
                this.IndicatorButtons.Add(indicatorButton);
            }

        }


        //private bool IsDesiredState(List<IndicatorLight> lights)  {

         
        //    foreach (IndicatorLight light in lights) {
        //        if (!(light.IsInDesiredState())) {
        //            return false;
        //        }
        //    }
        //    return true;
        //}


    
       // public long PressSomeButtons(int depthLeft, int previousPresses, List<IndicatorLight> lights) {
       public long PressSomeButtons(int depthLeft, int previousPresses, IndicatorLightGroup lightGroup) {

            // circuit breaker if i'm 20 deep, bail;
            if (depthLeft <= 0) return long.MaxValue;
            depthLeft--;

            //// Did i achieve my goal?
            if (lightGroup.IsInDesiredState()) {
                return previousPresses;
            }

            // foreach button ...
            // make a copy of the lights as they are right now.
            // press that button
            // recursively start over and let any button be pressed next.

            long lowestCount = long.MaxValue;
            //previousPresses++;

            foreach (IndicatorButton button in this.IndicatorButtons) {

                if (previousPresses == 0) {
                    int x = 5;
                }

                // note to self: you can probably remove the last button
                // selected from the group, because hitting any button twice will just be a no-op.

               // List<IndicatorLight> lightsCopy = new List<IndicatorLight>(lights);
               IndicatorLightGroup groupCopy = new IndicatorLightGroup(lightGroup.indicatorLights);
                button.Press(groupCopy);
                long count = PressSomeButtons(depthLeft, (previousPresses + 1), groupCopy);
                if (count < lowestCount) {
                    lowestCount = count;
                }
            }

            return lowestCount;
        }

        public void Print() {
            
            
            
        }

        public long FewestPresses() {

            // return 0;
            IndicatorLightGroup indicatorLightGroup = new IndicatorLightGroup(this.IndicatorLights);

            long fewestPresses = this.PressSomeButtons(15, 0, indicatorLightGroup);
            Console.WriteLine("Fewest Presses: " + fewestPresses);
            return fewestPresses;
        }
       
    }
}
