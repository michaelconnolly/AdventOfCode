using System;

namespace AdventOfCode2022 {

    public class RopeCoordinate {

        public int row;
        public int col;
        RopeCoordinate follower;
        private RopeMap map;

        public RopeCoordinate(int row, int col, RopeMap map, RopeCoordinate follower) {
            this.row = row;
            this.col = col;
            this.map = map;
            this.follower = follower;
        }

        public void Follow(RopeCoordinate leader) {

            // if follower and leader are in same coordinate, we are good.
            if (leader.row == this.row && leader.col == this.col) return;

            int rowDistance = leader.row - this.row;
            int colDistance = leader.col - this.col;

            // If we are within one, we are good.
            if (Math.Abs(rowDistance) <= 1 && Math.Abs(colDistance) <= 1) return;

            // Are we distant but in same row?
            if (Math.Abs(colDistance) > 1 && (this.row == leader.row)) {
                if (colDistance > 0) this.col++;
                else this.col--;
            }

            // Are we distant but in same col?
            else if (Math.Abs(rowDistance) > 1 && (this.col == leader.col)) {
                if (rowDistance > 0) this.row++;
                else this.row--;
            }

            else {
                // else, we are diagonal.
                if (rowDistance > 0) this.row++;
                else this.row--;
                if (colDistance > 0) this.col++;
                else this.col--;
            }

            if (this.follower != null) {
                this.follower.Follow(this);
            }
            else {
                // this should be the last tail.
                if (this.map.lastTail != this) throw new Exception();
                this.map.MarkMapAsVisited(this);
            }
        }

        public void Print() {

            Console.WriteLine("row: " + this.row + ", col: " + this.col);
        }

        public void Move(string direction, int distance) {

            for (int i = 1; i <= distance; i++) {

                switch (direction) {

                    case "U":
                        this.row = this.row - 1;
                        break;

                    case "D":
                        this.row = this.row + 1;
                        break;

                    case "L":
                        this.col = this.col - 1;
                        break;

                    case "R":
                        this.col = this.col + 1;
                        break;

                    default:
                        throw new Exception();
                }

                if (this.follower != null) {
                    follower.Follow(this);
                }
            }
        }
    }
}

