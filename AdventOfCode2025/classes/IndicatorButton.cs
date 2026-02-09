using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    
    internal class IndicatorButton {

        //IndicatorMachine machine;
        List<int> connectedLights = new List<int>();

       // public IndicatorButton(IndicatorMachine machine, string buttonInput) {
       public IndicatorButton(string buttonInput) {

            //this.machine = machine;

            string[] parts = buttonInput.Split(',');
            foreach (string part in parts) {
                this.connectedLights.Add(int.Parse(part));
            }
        }


       // public void Press(List<IndicatorLight> lights) {
       public void Press(IndicatorLightGroup group) {

            foreach (int i in connectedLights) {
                group.indicatorLights[i].Toggle();
            }
        }
    }
}
