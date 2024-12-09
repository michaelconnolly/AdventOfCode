using System;
using System.Diagnostics;


namespace AdventOfCode2024 {

    public enum SecurityGuardDirection {

        Unknown, Up,
        Down, Left, Right
    }

    internal class SecurityGuardMap {

        SecurityGuardMapNode[,] map;
        SecurityGuardDirection direction = SecurityGuardDirection.Unknown;
        int guardRow = -1;
        int guardCol = -1;
        int initialGuardRow;
        int initialGuardCol;
        SecurityGuardDirection initialDirection;

        public SecurityGuardMap(string[] input) {

            this.map = new SecurityGuardMapNode[input.Length, input[0].Length];

            for (int row =  0; row < input.Length; row++) {
                for (int col = 0; col < input[0].Length; col++) {
                    this.map[row, col] = new SecurityGuardMapNode(input[row][col]);
                }
            }

            this.FindGuard();
            this.map[this.guardRow, this.guardCol].visitedUp = true;

            // Go for it.
            //this.ProcessMap();
        }

        private char DrawGuard() {

            switch (this.direction) {
                case SecurityGuardDirection.Unknown:
                    Debug.Assert(false, "shouldn't get here");
                    return 'O';
                case SecurityGuardDirection.Up: return '^';
                case SecurityGuardDirection.Down: return 'v';
                case SecurityGuardDirection.Left: return '<';
                case SecurityGuardDirection.Right: return '>';
                default:
                    Debug.Assert(false, "shouldn't get here");
                    return 'P';
            }
        }

        public bool FindGuard() {

            bool found = false;

            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {

                    switch (map[row,col].type) {

                        case '^':
                            this.direction = SecurityGuardDirection.Up;
                            found = true;
                            break;

                        case 'v':
                            this.direction = SecurityGuardDirection.Down;
                            found = true;
                            break;

                        case '<':
                            this.direction = SecurityGuardDirection.Left;
                            found = true;
                            break;

                        case '>':
                            this.direction = SecurityGuardDirection.Right;
                            found = true;
                            break;
                    }

                    if (found) {
                        this.guardRow = row;
                        this.guardCol = col;
                        this.initialGuardRow = row;
                        this.initialGuardCol = col;
                        this.initialDirection = direction;
                        return true;

                    }
                }
            }

            return false;
        }

        public void Print() {

            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {

                    if (this.map[row, col].obstacleSpot) {
                        Console.Write('O');
                    }
                    else if (this.guardRow == row && this.guardCol == col) {
                        Console.Write(this.DrawGuard());
                    }
                    else if (this.map[row, col].obstacleSpot) {
                        Console.Write('O');
                    }
                    else if (this.map[row, col].Visited()) {
                        Console.Write('X');
                    }
                    else {
                        Console.Write(this.map[row, col].type);
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private SecurityGuardDirection PivotGuard() {

            switch (this.direction) {

                case SecurityGuardDirection.Up: return SecurityGuardDirection.Right;
                case SecurityGuardDirection.Down: return SecurityGuardDirection.Left;
                case SecurityGuardDirection.Left: return SecurityGuardDirection.Up;
                case SecurityGuardDirection.Right: return SecurityGuardDirection.Down;
                default:
                    Debug.Assert(false, "we should never get here.");
                    return SecurityGuardDirection.Unknown;
            }
        }



        private bool LegalCell(Coordinate cell) {

            int row = cell.row;
            int col = cell.col;

            return (row >= 0 && row < this.map.GetLength(0) &&
                col >= 0 && col < this.map.GetLength(1));
        }

        private Coordinate FindNextCell() {

            int rowMod = 0;
            int colMod = 0;

            switch (this.direction) {

                case SecurityGuardDirection.Up:
                    rowMod = -1;
                    break;
                case SecurityGuardDirection.Down:
                    rowMod = 1;
                    break;
                case SecurityGuardDirection.Left:
                    colMod = -1;
                    break;
                case SecurityGuardDirection.Right:
                    colMod = 1;
                    break;

            }

            int newRow = this.guardRow + rowMod;
            int newCol = this.guardCol + colMod;

            return new Coordinate(newRow, newCol);
        }

        private Coordinate FindRightMostCell() {

            int visitedRowToCheck = this.guardRow;
            int visitedColToCheck = this.guardCol;

            SecurityGuardDirection directionToMyRight = this.PivotGuard();

            if (directionToMyRight == SecurityGuardDirection.Left) {
                visitedColToCheck -= 1;
            }
            else if (directionToMyRight == SecurityGuardDirection.Right) {
                visitedColToCheck += 1;
            }
            else if (directionToMyRight == SecurityGuardDirection.Up) {
                visitedRowToCheck -= 1;
            }
            else if (directionToMyRight == SecurityGuardDirection.Down) {
                visitedRowToCheck += 1;
            }
            else {
                Debug.Assert(false, "this shouldn't happen.");
            }

            return new Coordinate(visitedRowToCheck, visitedColToCheck);

  
        }


        private bool MoveGuard(bool roundTwo) {

            SecurityGuardDirection originalDirection = this.direction;

            // Figure out where we should be going, and if it's 
            // within the bounds of the map.
            Coordinate newCell = this.FindNextCell();
            if (!(this.LegalCell(newCell))) {
                return false;
            }
                
            //// If upon moving, we would bump into a thing,
            //// then forget about the move, pivot to the right.
            if (this.map[newCell.row, newCell.col].type == '#') {
                this.direction = this.PivotGuard();
                //return true;
            }
            else {
                // Do the actual move.
                this.guardRow = newCell.row;
                this.guardCol = newCell.col;
            }

            // I have visited you!
            switch (this.direction) {
                case SecurityGuardDirection.Up:
                    this.map[this.guardRow, this.guardCol].visitedUp = true;
                    break;
                case SecurityGuardDirection.Down:
                    this.map[this.guardRow, this.guardCol].visitedDown = true;
                    break;
                case SecurityGuardDirection.Left:
                    this.map[this.guardRow, this.guardCol].visitedLeft = true;
                    break;

                case SecurityGuardDirection.Right:
                    this.map[this.guardRow, this.guardCol].visitedRight = true;
                    break;

                default:
                    Debug.Assert(false, "we should not have gotten here.");
                    break;

            }


            // If any places around us have been visited before, good 
            // spot for a future obstacle.
            // Look to the right; has this spot been visited? 
            if (roundTwo) {
                //int visitedRowToCheck = this.guardRow;
                //int visitedColToCheck = this.guardCol;

                //SecurityGuardDirection directionToCheck = this.PivotGuard();

                //if (directionToCheck == SecurityGuardDirection.Left) {
                //    visitedColToCheck = this.guardCol - 1;
                //}
                //else if (directionToCheck == SecurityGuardDirection.Right) {
                //    visitedColToCheck = this.guardCol + 1;
                //}
                //else if (directionToCheck == SecurityGuardDirection.Up) {
                //    visitedRowToCheck = this.guardRow - 1;
                //}
                //else if (directionToCheck == SecurityGuardDirection.Down) {
                //    visitedRowToCheck = this.guardRow + 1;
                //}
                //else {
                //    Debug.Assert(false, "this shouldn't happen.");
                //}

                //Coordinate visitedCellToCheck = new Coordinate(visitedRowToCheck, visitedColToCheck);
                SecurityGuardDirection directionToCheck = this.PivotGuard();
                Coordinate visitedCellToCheck = FindRightMostCell();

                // if the cell to peek at isn't a valid cell, bail.
                if (!(this.LegalCell(visitedCellToCheck))) {
                    return true;
                }

                if (visitedCellToCheck.row == 8) {
                    int x = 5;
                }

                if (this.map[visitedCellToCheck.row, visitedCellToCheck.col].Visited(directionToCheck)) {

                    // OK, if the cell we would go to next isn't already an obstacle,
                    // it would be a good place for one to start a loop.
                    int obstacleRow = this.guardRow;
                    int obstacleCol = this.guardCol;



                    switch (this.direction) {
                        case SecurityGuardDirection.Up:
                            obstacleRow--;
                            break;
                        case SecurityGuardDirection.Down:
                            obstacleRow++;
                            break;
                        case SecurityGuardDirection.Left:
                            obstacleCol--;
                            break;
                        case SecurityGuardDirection.Right:
                            obstacleCol++;
                            break;
                    }

                    Coordinate obstacleCell = new Coordinate(obstacleRow, obstacleCol);
                    if (this.LegalCell(obstacleCell) &&
                        this.map[obstacleRow, obstacleCol].type != '#') {
                        this.map[obstacleRow, obstacleCol].obstacleSpot = true;
                        this.Print();
                        int x = 5;
                    }

                }
            }
            
            //if (this.map[this.guardRow, this.guardCol])


            // IDEA! Maybe calculate the second question through a secondary pass;
            // let the first pass mark everything, and then when everything is complete, then 
            // figure out obstacles.

            return true;
        }

        public void ProcessMap(bool roundTwo = false) {

            int stepCount = 0;

            // if round two, reposition the guard.
            this.guardCol = this.initialGuardCol;
            this.guardRow = this.initialGuardRow;
            this.direction = this.initialDirection;

            bool fContinue = true;
            while (fContinue) {

                fContinue = this.MoveGuard(roundTwo);
                stepCount++;
                //this.Print();
            }

            Console.WriteLine("step count: " + stepCount);
            return;
        }

        public int CountOfObstaclePositions() {

            int sum = 0;

            for (int row = 0; row < this.map.GetLength(0); row++) {
                for (int col = 0; col < this.map.GetLength(1); col++) {
                    if (this.map[row, col].obstacleSpot) {
                        sum++;
                    }
                }
            }

            return sum;

        }

        public int CountOfPositionsVisited() {

            int sum = 0;
            for (int row=0; row<this.map.GetLength(0); row++) {
                for (int col=0; col <this.map.GetLength(1); col++) {
                    if (this.map[row,col].Visited()) {
                        sum++;
                    }
                }
            }

            return sum;
        }

    }
}
