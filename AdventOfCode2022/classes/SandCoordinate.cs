namespace AdventOfCode2022 {

    public class SandCoordinate {

        public int row;
        public int col;
        public char[,] map;

        public SandCoordinate(string input, char[,] map) {

            string[] parts = input.Split(",");

            this.col = int.Parse(parts[0]);
            this.row = int.Parse(parts[1]);
            this.map = map;
        }

        public string Print() {
            return this.col.ToString() + "," + this.row.ToString();
        }

        public bool Fell() {

            bool isFalling = true;
            bool didFall = false;

            while (isFalling) {

                // If the sand slipped past our boundary, we are out!
                if ((this.row +1) >= 1000) return false;

                char spaceBelowMe = this.map[this.col, this.row + 1];

                // is the space directly below me clear?
                if (spaceBelowMe == '.') {
                    this.row = this.row + 1;
                    didFall = true;
                }

                // Or, Is it blocked from falling? 
                else {
         
                    // diagonal to left?
                    if ((this.col > 0) && (this.map[this.col - 1, this.row + 1] == '.')) {
                        this.row = this.row + 1;
                        this.col = this.col - 1;
                        didFall = true;
                    }

                    // diagonal to right?
                    else if ((this.col < 10000) && (this.map[this.col + 1, this.row + 1] == '.')) {
                        this.row = this.row + 1;
                        this.col = this.col + 1;
                        didFall = true;
                    }

                    // otherwise, we are going to hang right here.
                    else {
                        
                        isFalling = false;
                    }
                }
            }

            return didFall;
        }
    }
}

