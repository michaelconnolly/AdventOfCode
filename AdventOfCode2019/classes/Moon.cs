using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {

    public class Moon {

        private List<Moon> moons;

        public int positionX;
        public int positionY;
        public int positionZ;
        public int velocityX = 0;
        public int velocityY = 0;
        public int velocityZ = 0;

        public int initialPositionX;
        public int initialPositionY;
        public int initialPositionZ;
        public int initialVelocityX = 0;
        public int initialVelocityY = 0;
        public int initialVelocityZ = 0;

        public Moon(int x, int y, int z, List<Moon> moons) {

            this.positionX = x;
            this.positionY = y;
            this.positionZ = z;
            this.initialPositionX = x;
            this.initialPositionY = y;
            this.initialPositionZ = z;

            this.moons = moons;
            this.moons.Add(this);
        }

        public static void PrintOutMoons(List<Moon> moons, int stepCount) {

            Console.WriteLine("After " + stepCount + ": ");
            foreach (Moon moon in moons) {
                Console.WriteLine("\tpos=<x=" + moon.positionX + ", y=" + moon.positionY + ", z=" + moon.positionZ +
                    ">, vel=<x=" + moon.velocityX + ", y=" + moon.velocityY + ", z=" + moon.velocityZ + ">, pot=" + moon.potentialEnergy + ", kin=" + moon.kineticEnergy);
            }
        }

        // To apply gravity, consider every pair of moons.On each axis(x, y, and z), the velocity of each moon changes
        // by exactly +1 or -1 to pull the moons together.For example, if Ganymede has an x position of 3, and Callisto
        // has a x position of 5, then Ganymede's x velocity changes by +1 (because 5 > 3) and Callisto's x velocity 
        // changes by -1 (because 3 < 5). However, if the positions on a given axis are the same, the velocity on that 
        // axis does not change for that pair of moons.
        public static void ApplyGravity(List<Moon> moons) {

            for (int i=0; i<(moons.Count - 1); i++) {
                for (int j=i+1; j<moons.Count; j++) {
                    Moon currentMoon = moons[i];
                    Moon otherMoon = moons[j];

                    int positionXDiff = currentMoon.positionX - otherMoon.positionX;
                    if (positionXDiff > 0) {
                        currentMoon.velocityX--;
                        otherMoon.velocityX++;
                    }
                    else if (positionXDiff < 0) {
                        currentMoon.velocityX++;
                        otherMoon.velocityX--;
                    }

                    int positionYDiff = currentMoon.positionY - otherMoon.positionY;
                    if (positionYDiff > 0) {
                        currentMoon.velocityY--;
                        otherMoon.velocityY++;
                    }
                    else if (positionYDiff < 0) {
                        currentMoon.velocityY++;
                        otherMoon.velocityY--;
                    }

                    int positionZDiff = currentMoon.positionZ - otherMoon.positionZ;
                    if (positionZDiff > 0) {
                        currentMoon.velocityZ--;
                        otherMoon.velocityZ++;
                    }
                    else if (positionZDiff < 0) {
                        currentMoon.velocityZ++;
                        otherMoon.velocityZ--;
                    }
                }
            }
        }

        // Once all gravity has been applied, apply velocity: simply add the velocity of each moon to its own position.
        // For example, if Europa has a position of x = 1, y = 2, z = 3 and a velocity of x=-2, y=0,z=3, then its new 
        // position would be x = -1, y = 2, z = 6.This process does not modify the velocity of any moon
        public static void ApplyVelocity(List<Moon> moons) {

            foreach (Moon moon in moons) {

                moon.positionX += moon.velocityX;
                moon.positionY += moon.velocityY;
                moon.positionZ += moon.velocityZ;
            }
        }

        // Then, it might help to calculate the total energy in the system. The total energy for a single moon
        // is its potential energy multiplied by its kinetic energy. A moon's potential energy is the sum of the
        // absolute values of its x, y, and z position coordinates. A moon's kinetic energy is the sum of the absolute 
        // values of its velocity coordinates.
        public int potentialEnergy {
            get {
                return Math.Abs(positionX) + Math.Abs(positionY) + Math.Abs(positionZ);
            }
        }

        public int kineticEnergy {
            get {
                return Math.Abs(velocityX) + Math.Abs(velocityY) + Math.Abs(velocityZ);
            }
        }

        public static int TotalEnergy(List<Moon> moons) {

            // Then, it might help to calculate the total energy in the system. The total energy for a single moon
            // is its potential energy multiplied by its kinetic energy. A moon's potential energy is the sum of the
            // absolute values of its x, y, and z position coordinates. A moon's kinetic energy is the sum of the absolute 
            // values of its velocity coordinates.
            int totalEnergy = 0;
            foreach (Moon moon in moons) {
                totalEnergy += (moon.potentialEnergy * moon.kineticEnergy);
            }

            return totalEnergy;
        }

        public bool BackToInitialState() {

            return (this.initialPositionX == this.positionX && this.initialPositionY == this.positionY && this.initialPositionZ == this.positionZ &&
                this.initialVelocityX == this.velocityX && this.initialVelocityY == this.velocityY && this.initialVelocityZ == this.velocityZ);
        }

        public bool BackToInitialStateX() {

            return (this.initialPositionX == this.positionX);
        }

        public bool BackToInitialStateY() {

            return (this.initialPositionY == this.positionY);
        }

        public bool BackToInitialStateZ() {

            return ( this.initialPositionZ == this.positionZ);
        }

        public static bool BackToInitialStates(List<Moon> moons) {

            foreach (Moon moon in moons) {
                if (!moon.BackToInitialState()) return false;
            }

            return true;
        }

        public static bool BackToInitialStatesX(List<Moon> moons) {

            foreach (Moon moon in moons) {
                if (!moon.BackToInitialStateX()) return false;
            }

            return true;
        }

        public static bool BackToInitialStatesY(List<Moon> moons) {

            foreach (Moon moon in moons) {
                if (!moon.BackToInitialStateY()) return false;
            }

            return true;
        }

        public static bool BackToInitialStatesZ(List<Moon> moons) {

            foreach (Moon moon in moons) {
                if (!moon.BackToInitialStateZ()) return false;
            }

            return true;
        }
    }
}
