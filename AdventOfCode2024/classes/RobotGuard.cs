using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {

    internal class RobotGuard {

        public Coordinate location;
        Coordinate direction;
        int width;
        int height;
        Coordinate initialLocation;

        public RobotGuard(Coordinate _location, Coordinate direction, int width, int height) {
            this.initialLocation = new Coordinate(_location.row, _location.col);
            this.location = _location;
            this.direction = direction;
            this.width = width;
            this.height = height;
        }

        public void Tick(int tickCount) {

            int changeInRow = tickCount * this.direction.row;
            int changeInCol = tickCount * this.direction.col;

            int remainderRow = changeInRow % this.height;
            int remainderCol = changeInCol % this.width;

            int finalRow = (remainderRow + this.location.row);
            if (finalRow < 0) {
                finalRow += this.height;
            }
            if (finalRow >= this.height) {
                finalRow -= this.height;
            }

            int finalCol = remainderCol + this.location.col;
            if (finalCol < 0) {
                finalCol += this.width;
            }
            if (finalCol >= this.width) {
                finalCol -= this.width;
            }

            Debug.Assert(finalRow >= 0 && finalRow < this.height);
            Debug.Assert(finalCol >= 0 && finalCol < this.width);

            this.location.row = finalRow;
            this.location.col = finalCol;
        }
    }
}
