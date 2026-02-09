using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025 {

  
    internal class IndicatorLight {

        public bool desiredState;
        public bool currentState;

        public IndicatorLight(char desiredState) {
            this.desiredState = (desiredState == '#' ? true : false);
            this.currentState = false;
        }

        public IndicatorLight(bool desiredState, bool currentState) {
            this.desiredState = desiredState;
            this.currentState = currentState;
        }

        public bool Toggle() {
            currentState = !currentState;
            return currentState;
        }

        public bool IsInDesiredState() {
            return (currentState == desiredState);
        }
    }
}
