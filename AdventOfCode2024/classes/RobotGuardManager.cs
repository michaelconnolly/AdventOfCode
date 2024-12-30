using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    internal class RobotGuardManager {

        List<RobotGuard> robotGuards = new List<RobotGuard>();
        int width;
        int height;

        public RobotGuardManager(string[] input, int width, int height) {

            this.width = width;
            this.height = height;
            // Example:
            // p=0,4 v=3,-3

            foreach (string robotRaw in input) {

                string[] parts = robotRaw.Split(' ');
                string locationRaw = parts[0].Substring(2);
                string velocityRaw = parts[1].Substring(2);
                string[] locationPartsRaw = locationRaw.Split(',');
                string[] velocityPartsRaw = velocityRaw.Split(',');

                this.robotGuards.Add(new RobotGuard(
                    new Coordinate(int.Parse(locationPartsRaw[1]), int.Parse(locationPartsRaw[0])),
                    new Coordinate(int.Parse(velocityPartsRaw[1]), int.Parse(velocityPartsRaw[0])),
                    this.width, this.height));
            }
        }


        public void LetTimeProceed(int ticks) {

            foreach (RobotGuard robotGuard in this.robotGuards) {
                robotGuard.Tick(ticks);
            }

        }


        public int[] RobotCountByQuadrant() {

            int[] quadrantTotals = new int[5];

            int midLineRow = (this.height - 1) / 2;
            int midLineCol = (this.width - 1) / 2;

            foreach (RobotGuard robotGuard in this.robotGuards) {

                // Top Left.
                if (robotGuard.location.row < midLineRow && robotGuard.location.col < midLineCol) {
                    quadrantTotals[0]++;
                }
                // Top right. 
                else if (robotGuard.location.row < midLineRow && robotGuard.location.col > midLineCol) {
                    quadrantTotals[1]++;
                }
                // Bottom Left.
                else if (robotGuard.location.row > midLineRow && robotGuard.location.col < midLineCol) {
                    quadrantTotals[2]++;
                }
                // Bottom right. 
                else if (robotGuard.location.row > midLineRow && robotGuard.location.col > midLineCol) {
                    quadrantTotals[3]++;
                }
                // Skipped because they were on a middle line. 
                else {
                    quadrantTotals[4]++;
                }
            }

            return quadrantTotals;
        }


        public void Print() {

            int[,] map = new int[this.width, this.height];
            for (int i=0; i<this.height; i++) {
                for (int j=0; j<this.width; j++) {
                    map[j, i] = 0;
                }
            }

            foreach (RobotGuard robotGuard in this.robotGuards) {
                map[robotGuard.location.col, robotGuard.location.row]++;
            }


            for (int i = 0; i < this.height; i++) {
                for (int j = 0; j < this.width; j++) {
                    if (map[j, i] == 0) Console.Write('.');
                    else Console.Write(map[j, i]);
                }
                Console.WriteLine(); 
            }
            Console.WriteLine();
        }


        public int GetSafetyFactor() {

            // To determine the safest area, count the number of robots in each quadrant
            // after 100 seconds.Robots that are exactly in the middle(horizontally or vertically)
            // don't count as being in any quadrant.
            //this.LetTimeProceed(100);

            // In this example, the quadrants contain 1, 3, 4, and 1 robot. Multiplying these together
            // gives a total safety factor of 12.
            int[] quadrantValues = this.RobotCountByQuadrant();
            int safetyFactor = quadrantValues[0] * quadrantValues[1] * quadrantValues[2] * quadrantValues[3];

            // Predict the motion of the robots in your list within a space which is 101 tiles wide
            // and 103 tiles tall. What will the safety factor be after exactly 100 seconds have elapsed ?
            return safetyFactor;
        }
    }
}
