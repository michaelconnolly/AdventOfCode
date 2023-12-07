using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2023 {

    internal class BoatRaceManager {

        private Collection<BoatRace> boatRaces = new Collection<BoatRace>();
        private BoatRace oneBoatRace;

        public BoatRaceManager(string[] lines) {

            string[] times = lines[0].Substring(5).Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] distances = lines[1].Substring(9).Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // Part one: multiple boat races.
            for (int i=0; i<times.Length; i++) {
                BoatRace boatRace = new BoatRace((i + 1), Convert.ToInt32(times[i]), Convert.ToInt32(distances[i]));
                this.boatRaces.Add(boatRace);
            }

            // Part two: one boat race.
            int time = Convert.ToInt32(lines[0].Substring(5).Replace(" ", ""));
            long bestDistance = Convert.ToInt64(lines[1].Substring(9).Replace(" ", ""));
            this.oneBoatRace = new BoatRace(0, time, bestDistance);
        }

        public void print() {

            for (int i = 0; i < boatRaces.Count; i++) {
                this.boatRaces[i].print();          }
        }


        public int CalculateProductOfAllCountOfWinningOptions() {

            int product = 1;

            foreach (BoatRace boatRace in boatRaces) {
                product = product * boatRace.calculateCountOfWinningOptions();

            }

            return product;
        }

        public int CalculateCountOfOfWinningOptionsOneRace() {

            this.oneBoatRace.print();
            return this.oneBoatRace.calculateCountOfWinningOptions();
           
        }
    }
}
