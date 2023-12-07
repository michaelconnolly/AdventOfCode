using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {

    internal class BoatRace {

        public int id;
        public long time;
        public long bestDistance;

        public BoatRace(int id, long time, long bestDistance) {
            this.id = id;
            this.time = time;
            this.bestDistance = bestDistance;
        }

        public void print() {

            Console.WriteLine("race " + this.id + ": time: " + this.time +
                " bestDistance: " + this.bestDistance + " countOfWinningOptions: " + calculateCountOfWinningOptions()); ;
        }

        public int calculateCountOfWinningOptions() {
        
            int count = 0;

            for (long secondTicker = 0; secondTicker <= this.time; secondTicker++) {

                long speed = secondTicker * 1;
                long timeTravelled = this.time - secondTicker;
                long distanceTravelled = timeTravelled * speed;
                if (distanceTravelled > this.bestDistance) { count++; }
            }

            return count;
        }
    }
}
