using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

    

    internal class IndicatorLightGroup {

        public List<IndicatorLight> indicatorLights = new List<IndicatorLight>();

        public IndicatorLightGroup(List<IndicatorLight> indicatorLightsInput) {
           
            foreach (IndicatorLight light in indicatorLightsInput) {
                indicatorLights.Add(new IndicatorLight(light.desiredState, light.currentState));
            }
        }

        public bool IsInDesiredState() {
       
            foreach (IndicatorLight light in this.indicatorLights) {
                if (!(light.IsInDesiredState())) {
                    return false;
                }
            }
            return true;

        }

        public string currentState {

            get {

                string output = "";
                foreach (IndicatorLight light in indicatorLights) {
                    if (light.currentState) { output += "#"; }       
                     else {    return output += "."; }
                }
                return output;
            }
        }

        public string desiredState {

            get {

                string output = "";
                foreach (IndicatorLight light in indicatorLights) {
                    if (light.desiredState) { output += "#"; }
                    else { return output += "."; }
                }
                return output;
            }
        }
    }
}
