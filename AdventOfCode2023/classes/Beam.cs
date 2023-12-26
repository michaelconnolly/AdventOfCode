using System;

namespace AdventOfCode2023 {
    public class Beam {

        public int id;
        public int x;
        public int y;
        public BeamDirection direction;
        public Beam parent;

        public Beam(int x, int y, BeamDirection direction, Beam parent=null) {

            this.id = BeamMirrorMap.nextBeamId;
            this.x = x;
            this.y = y;
            this.direction = direction;
            this.parent = parent;

            this.print("creation");
        }

        public void print(string eventName) {

            Console.WriteLine(eventName + ": beam " + this.id + " at " + x + "," + y + " : " + this.direction.ToString());
        }
    }
}
