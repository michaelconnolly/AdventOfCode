using System;

namespace AdventOfCode2023 {
    public class CubeBagGameSet {

        public int redCount = 0;
        public int greenCount = 0;
        public int blueCount = 0;

        public int power {
            get {
                return (this.redCount * this.greenCount * this.blueCount);
            }
        }
        
        public CubeBagGameSet(string line) {

            // Format: "1 red, 2 green, 6 blue"
            string[] subset = line.Split(',');
            foreach (string s in subset)
            {
                string[] parts = s.Trim().Split(" ");
                int count = Convert.ToInt32(parts[0]);
                string color = parts[1];

                switch (color) {

                    case "red":
                        this.redCount += count;
                        break;
                    case "green":
                        this.greenCount += count;
                        break;
                    case "blue":
                        this.blueCount += count;
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        public CubeBagGameSet(int red, int green, int blue) {
            
            this.redCount = red;
            this.greenCount = green;
            this.blueCount = blue;
        }


        public void print() {

            if (this.redCount > 0) Console.Write("red: " + this.redCount + ", ");
            if (this.greenCount > 0) Console.Write("green: " + this.greenCount + ", ");
            if (this.blueCount > 0) Console.Write("blue: " + this.blueCount + ", ");
            Console.Write("power: " + this.power);
        }
    }
}
