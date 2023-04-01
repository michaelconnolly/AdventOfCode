using System;
using System.Collections.Generic;

namespace AdventOfCode2022 {

    public class Valve {

        public int flowRate;
        public string name;
        public string[] tunnelsTo;
        public bool open;
        public Dictionary<string, int> costToOpen;
        ValveManager manager;

        public Valve(string name, int flowRate, string[] tunnelsTo, ValveManager manager) {

            // Examples:
            // Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
            // Valve BB has flow rate = 13; tunnels lead to valves CC, AA

            this.name = name;
            this.flowRate = flowRate;
            this.tunnelsTo = tunnelsTo;
            this.open = false;
            this.manager = manager;
        }
        
        public Valve(Valve valveToCopy) {

            this.name = valveToCopy.name;
            this.flowRate= valveToCopy.flowRate;
            this.tunnelsTo= valveToCopy.tunnelsTo;
            this.open = valveToCopy.open;
        }
        
        public Dictionary<string,Valve> BestPathTo(Dictionary<string, Valve> valves, string destValveName, 
            Dictionary<string, Valve> history=null, int additionalBenefit=0 ) {

            bool firstTime = false;
            if (history == null) {
                history = new Dictionary<string, Valve>();
                firstTime = true;
            }

      
            // Do we have a direct path?
            foreach (string tunnelTo in this.tunnelsTo) {
                if (tunnelTo == destValveName) {
                    history[destValveName] = null;
                    return history;
                }
            }

            int shortestPathSize = int.MaxValue;
            Dictionary<string, Valve> bestPath = null;
            int bestAdditionalBenefit = 0;

            // all paths from here are either null, or now contain this valve.
            if (!firstTime) {
                history[this.name] = null;
            }

            foreach (string tunnelTo in this.tunnelsTo) {

                if (!(history.ContainsKey(tunnelTo))) {

                    Valve valve = valves[tunnelTo];
                    Dictionary<string, Valve> historyThisPath = new Dictionary<string, Valve>(history);
                    historyThisPath[tunnelTo] = null;
                    //historyThisPath[this.name] = null;

                    int additionalBenefitOfThisPath = (valve.Openable() ? valve.flowRate : 0);
                    int totalBenefitThisPath = additionalBenefit + additionalBenefitOfThisPath;

                    Dictionary<string, Valve> bestPathThisPath = valve.BestPathTo(valves, destValveName, historyThisPath, totalBenefitThisPath);
                    if (bestPathThisPath != null) {
                        int pathSize = bestPathThisPath.Keys.Count;

                        shortestPathSize = Math.Min(pathSize, shortestPathSize);

                        if (shortestPathSize == pathSize) {
                            if (totalBenefitThisPath >= bestAdditionalBenefit) {
                                bestAdditionalBenefit = totalBenefitThisPath; 
                                bestPath = bestPathThisPath;
                            }
                        }
                    }
                }
            }

            return bestPath;

        }

        //public Dictionary<string,Valve> PathToBestOption() {


        //}


        public string NextDestination(Dictionary<string, Valve> valves, int currentMinute, int totalMinutes) {

            string bestOptionName = "";
            int bestOptionValue = 0;

            foreach (string key in valves.Keys) {

                if (valves[key].Openable()) {

                    string name = key;
                    int flowRate = valves[key].flowRate;
                    int potentialDuration = (flowRate) * ((totalMinutes - currentMinute) - this.costToOpen[key] - 1);

                    //int relativeValue = Math.Max((flowRate - this.costToOpen[key]), 0);
                    int relativeValue = Math.Max((potentialDuration), 0);

                    if (relativeValue > bestOptionValue) {
                        bestOptionValue = relativeValue;
                        bestOptionName = name;
                    }
                }
            }

            return bestOptionName;
        }



        public string BestMove(Dictionary<string, Valve> valves) {

          //  string bestMoveValveName = "";
         //   int bestMoveBenefit = int.MinValue ;

            //string closestBestThingName = "";
            //int closestBestThingValue;
            int lowestMoveCost = int.MaxValue;
            int bestFlowRate = int.MinValue;
            string bestFlowRateName = ""; ;

            foreach (string valveName in valves.Keys) {

                if (valves[valveName].Openable()) {

                    int moveCost = this.costToOpen[valveName];
                    int flowRate = valves[valveName].flowRate;

                    lowestMoveCost = Math.Min(moveCost, lowestMoveCost);

                    if (moveCost == lowestMoveCost) {
                        if (flowRate > bestFlowRate) {
                            bestFlowRate = flowRate;
                            bestFlowRateName = valveName;
                        }
                    }

                    //int moveBenefit = valves[valveName].flowRate - (moveCost + 1);
                    //if (moveBenefit > bestMoveBenefit) {
                    //    bestMoveBenefit = moveBenefit;
                    //    bestMoveValveName = valveName;
                    //}
                }

                //int moveCost =
            }

            if (bestFlowRateName == "") throw new Exception();

            return bestFlowRateName;

        }


        public int TravelCost(string finalValveName, ValveManager valveManager, 
            Dictionary<string,Valve> valveHistory=null) { //int costAlreadySpent=0) {

            int[] costs = new int[this.tunnelsTo.Length];

            if (valveHistory == null) {
                valveHistory = new Dictionary<string,Valve>();
            }

            // costs zero to get to where i already am.
            if (finalValveName == this.name) return valveHistory.Keys.Count;

            valveHistory[this.name] = this;

            for (int i = 0; i < this.tunnelsTo.Length; i++) {

                string nextValveName = this.tunnelsTo[i];
                if (finalValveName == nextValveName) {
                    return valveHistory.Count;
                }

                if (!(valveHistory.ContainsKey(nextValveName))) {
                    //valveHistory[valveName] = valveManager.valves[valveName];
                    costs[i] = valveManager.valves[nextValveName].TravelCost(finalValveName,
                        valveManager, (valveHistory));

                }
                else {
                    costs[i] = int.MaxValue;
                }
            }

            int bestCost = int.MaxValue;
            for (int i = 0; i< tunnelsTo.Length; i++) {
                bestCost = Math.Min(bestCost, costs[i]);
            }

            return bestCost;
        }

        public bool Openable() {

            return (!(this.open) && this.flowRate > 0);

        }
    }
}
