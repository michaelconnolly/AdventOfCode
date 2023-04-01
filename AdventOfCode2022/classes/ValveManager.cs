using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2022 {

    public class ValveManager {

        public Dictionary<string, Valve> valves;
        int totalMinutes;
  
        public ValveManager(string[] input) { 
        
            this.valves = new Dictionary<string, Valve>();
            this.ProcessInput(input);
        }


        private void ProcessInput(string[] input) {

            // Examples:
            // Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
            // Valve BB has flow rate=13; tunnels lead to valves CC, AA
            // Valve HH has flow rate=22; tunnel leads to valve GG

            foreach (string line in input) {

                string s = line.Substring(6);
                string[] parts = s.Split("; ");
                string name = parts[0].Substring(0, 2);
                int flowRate = int.Parse(parts[0].Split("=")[1]);
                
                string tunnelsRaw = parts[1].Substring(22); 
                if (tunnelsRaw[0] == ' ') {
                    tunnelsRaw = tunnelsRaw.Substring(1);
                }
                string[] tunnelsTo = tunnelsRaw.Split(", ");
                this.valves[name] = new Valve(name, flowRate, tunnelsTo, this);
            }

            // Pre-process best distance.
            foreach (string valveNameStart in this.valves.Keys) {

                this.valves[valveNameStart].costToOpen = new Dictionary<string, int>();

                foreach (string valveNameEnd in this.valves.Keys) {

                    this.valves[valveNameStart].costToOpen[valveNameEnd] =
                        this.valves[valveNameStart].TravelCost(valveNameEnd, this);
                }
            }

        }

        public void Print() {

            Console.WriteLine("Total valves: " + this.valves.Keys.Count);
        }


        //public int StartPass() {

        //    int totalMinutes = 30;
        //    //this.currentMinute = 1;
        //    //this.bestSoFar = 0;
        //    Dictionary<string, Valve> valves = this.CopyValves(this.valves);

        //    return this.BestPathFromHere(1, "AA", 0, valves);

        //    //Dictionary<string, Valve> dictCopy = this.Valves.ToDictionary(entry => entry.Key, entry => (TValue)entry.Value.Clone());

        //    //this.Valves.


        //}


        Dictionary<string, Valve> CopyValves(Dictionary<string, Valve> valvesToCopy) {

            Dictionary<string,Valve> valves = new Dictionary<string,Valve>();

            foreach(string key in valvesToCopy.Keys) {

                valves[key] = new Valve(valvesToCopy[key]);
            }

            return valves;
        }


        public int PressureCollected(Dictionary<string, Valve> valves) {

            int total = 0;

            foreach (string key in valves.Keys) {
                if (valves[key].open) {
                    total += valves[key].flowRate;
                }
            }

            return total;
        }

        public bool AllValvesOpen(Dictionary<string, Valve> valves) {

            foreach (string key in valves.Keys) {

                // is it closed, and has a positive flow rate?  
                //if (!(valves[key].open) && (valves[key].flowRate > 0)) return false;
                if (valves[key].Openable()) return false;
            }

            return true;
        }


        //public bool AllValvesOpenButThisOne(Dictionary<string, Valve> valves, string valveName) {

        //    foreach (string key in valves.Keys) {

        //        // is it closed, and has a positive flow rate, and isn't valveName?  
        //        //if (!(valves[key].open) && (valves[key].flowRate > 0) && 
        //        //    (valves[key].name != valveName)) return false;

        //        if (valves[key].Openable() && valves[key].name != valveName) return false;
        //    }

        //    //return (!(valves[valveName].open) && valves[valveName].flowRate > 0);
        //    return valves[valveName].Openable();
        //}


        //public int ReturnScore(int score) {

        //    if (score > this.bestSoFar) {
        //        this.bestSoFar = score;
        //    }

        //    return score;
        //}


        public int BestPathFromHere(int minute, string valveName, int totalPressureSoFar, 
            Dictionary<string, Valve> currentValveConfig) {

            // Did we run out of time?  Return what we have so far.
            if (minute > this.totalMinutes) return totalPressureSoFar;

            // No matter what we do, we will be collecting at least a minute of pressure.
            int newPressure = this.PressureCollected(currentValveConfig);

            // Are all valves open?  If so, just hang out.
            if (this.AllValvesOpen(currentValveConfig)) {
                //int newPressure = this.PressureCollected(currentValveConfig);
                int cycles = (this.totalMinutes - minute + 1);
                return (newPressure * cycles) + totalPressureSoFar;
            }

            // Let's initialize some variables.
            Dictionary<string, Valve> newValveConfig = this.CopyValves(currentValveConfig);
            Valve currentValve = newValveConfig[valveName];
            int bestPathOpenValve = 0;
            int[] bestPathsMove = new int[currentValve.tunnelsTo.Length];
            //for (int i = 0; i < currentValve.tunnelsTo.Length; i++) {
            //    bestPathsMove[i] = -1;
            //}
         

            // Choices: I can spend one minute opening the valve if it's closed, or
            // spend one minute moving to one of the other valves.

            // Let's start with whether we have a choice of opening current valve.
            if (currentValve.Openable()) { 

                //Dictionary<string, Valve> newValveConfig = this.CopyValves(currentValveConfig);
                // collection pressure number.
                //int newPressure = this.PressureCollected(newValveConfig);
                // open the valve.
                newValveConfig[valveName].open = true;
                // best path from here.
                //Console.WriteLine("choice: open valve " + valveName);
                bestPathOpenValve = this.BestPathFromHere((minute + 1), valveName,
                    (totalPressureSoFar + newPressure), newValveConfig);
            }

            // Bail if that was the last valve.
            //if (this.AllValvesOpenButThisOne(currentValveConfig, valveName)) {
            //    return bestPathOpenValve;
            //}
            if (this.AllValvesOpen(newValveConfig)) {
                return bestPathOpenValve;
            }

            // Other choices are to move to another valve;
            // But don't bother moving to another valve past minute 28.
            if (minute < (this.totalMinutes - 2)) {
                for (int i = 0; i < currentValve.tunnelsTo.Length; i++) {

                    //Dictionary<string, Valve> newValveConfig = this.CopyValves(currentValveConfig);
                    string nearbyValveName = currentValve.tunnelsTo[i];
                    //int newPressure = this.PressureCollected(newValveConfig);

                    //Console.WriteLine("choice: move from " + valveName + " to " + nearbyValveName);
                    bestPathsMove[i] = this.BestPathFromHere((minute + 1), nearbyValveName,
                        (totalPressureSoFar + newPressure), newValveConfig);

                }
            }

            // Compare.
            int veryBestOption = bestPathOpenValve;
            for (int i=0; i < bestPathsMove.Length; i++) {
                if (bestPathsMove[i] > veryBestOption) {
                    veryBestOption = bestPathsMove[i];
                }
            }

            return veryBestOption;


        }






        public int QuestionOne() {

            int minute = 0;
            this.totalMinutes = 30;
            string currentValveName = "AA";
            int totalPressureReleased = 0;
            
            // Assumption that AA is always flowrate==0;
            
            while (minute <= totalMinutes) {

                Valve currentValve = this.valves[currentValveName];
                string nextValveName = currentValve.NextDestination(this.valves, minute, totalMinutes);

                if (nextValveName != "") {
                    Dictionary<string, Valve> bestPath = currentValve.BestPathTo(this.valves, nextValveName);

                    int counter = 0;
                    foreach (string pathStep in bestPath.Keys) {
                        //   for (int i=0; i<bestPath.Keys.Count; i++) {

                        counter++;

                        minute++;
                        if (minute > totalMinutes) throw new Exception();
                        int additionalRelease = this.PressureCollected(this.valves);
                        totalPressureReleased += additionalRelease;

                        Valve nextStepValve = this.valves[pathStep];
                        if (nextStepValve.Openable()) {

                            // Logic for opening:
                            // 1) it's openable
                            // 2) it's (flow rate * the amount of time it takes to get to the real destination) is > destination flow rate.

                            if (nextStepValve.flowRate * (bestPath.Keys.Count - counter + 1) >=
                                this.valves[nextValveName].flowRate) {

                                minute++;
                                if (minute > totalMinutes) throw new Exception();
                                additionalRelease = this.PressureCollected(this.valves);
                                totalPressureReleased += additionalRelease;

                                nextStepValve.open = true;
                            }
                        }
                    }

                    currentValveName = nextValveName;
                }

                else {

                    int moveForwardInTime = (this.totalMinutes - minute);
                    int currentReleaseRate = this.PressureCollected(this.valves);
                    totalPressureReleased += (moveForwardInTime * currentReleaseRate);

                    return totalPressureReleased;

                }


                // string nextValve = this.valves[currentValveName].BestMove(this.valves);
                // if (nextValve != "") {
                //int costToOpen = this.valves[currentValveName].costToOpen[nextValve] + 1;
                //    int currentReleaseRate = this.PressureCollected(this.valves);
                //    minute += costToOpen;
                //    totalPressureReleased += (costToOpen * currentReleaseRate);

                //    // Actually open the valve.
                //    currentValveName = nextValve;
                //    this.valves[currentValveName].open = true;
                //}
                //else {
                //    int moveForwardInTime = (this.totalMinutes - minute + 1);
                //    int currentReleaseRate = this.PressureCollected(this.valves);
                //    totalPressureReleased += (moveForwardInTime * currentReleaseRate);
                //}
            }

            //string foo = this.valves["AA"].BestMove(this.valves);

            throw new Exception();
            return totalPressureReleased;
        }
    }
}
