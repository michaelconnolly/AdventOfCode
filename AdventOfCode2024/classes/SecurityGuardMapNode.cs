using System.Collections;
using System.Diagnostics;

namespace AdventOfCode2024 {

    internal class SecurityGuardMapNode {

        public char type;
        //public bool visited = false;
        public bool visitedUp = false;
        public bool visitedDown = false;
        public bool visitedLeft = false;
        public bool visitedRight = false;

        public bool obstacleSpot = false;

        public SecurityGuardMapNode(char type) {
            this.type = type;
        }

        public bool Visited() {
            return (this.visitedUp || visitedDown || visitedLeft || visitedRight);
        }

        public bool Visited(SecurityGuardDirection direction) {

            switch (direction) {
                case SecurityGuardDirection.Left:
                    return this.visitedLeft;
                case SecurityGuardDirection.Right:
                    return this.visitedRight;
                case SecurityGuardDirection.Up:
                    return this.visitedUp;
                case SecurityGuardDirection.Down:
                    return this.visitedDown;
                default:
                    Debug.Assert(false, "never should have gotten here.");
                    return false; 
            }
        }
    }
}
