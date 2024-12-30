using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AdventOfCode2024.classes {

    internal class ArcadeMachine {

        ArcadeButton a;
        ArcadeButton b;
        ArcadeButton prize;

        List<long> solutions = new List<long>();
        private long cheapestPath;
        public int taps = 0;
        public CoordinateLong justPast;


        public ArcadeMachine(string in_buttonA, string in_buttonB, string in_prize, bool usePartTwoRules) {

            // Button A: X+13, Y+43
            // Button B: X+56, Y+32
            // Prize: X=6619, Y=373

            string buttonA = in_buttonA.Substring(10);
            string buttonB = in_buttonB.Substring(10);
            string prize = in_prize.Substring(7);

            string[] buttonCoordsA = buttonA.Split((", ").ToCharArray());
            string[] buttonCoordsB = buttonB.Split((", ").ToCharArray());
            string[] prizeCoords = prize.Split((", ").ToCharArray());

            this.a = new ArcadeButton(this.SplitCoordinate('+', buttonCoordsA[0])[1],
                this.SplitCoordinate('+', buttonCoordsA[2])[1]);
            this.b = new ArcadeButton(this.SplitCoordinate('+', buttonCoordsB[0])[1],
              this.SplitCoordinate('+', buttonCoordsB[2])[1]);

            // Part one rules: use prize coordinates as-is.
            // Part two rules: add 10000000000000 to each prize coordinate.
            long prizeX = long.Parse(this.SplitCoordinate('=', prizeCoords[0])[1]);
            long prizeY = long.Parse(this.SplitCoordinate('=', prizeCoords[2])[1]);
            if (usePartTwoRules) {
                prizeX += 10000000000000;
                prizeY += 10000000000000;
            }
            this.prize = new ArcadeButton(prizeX, prizeY);
         

            //this.prize = new ArcadeButton(this.SplitCoordinate('=', prizeCoords[0])[1],
            //    this.SplitCoordinate('=', prizeCoords[2])[1]);


            //this.CalculateFewestTokens(0, new Coordinate(0, 0));
            //if (this.solutions.Count == 0) {
            //    this.cheapestPath = -1;
            //}
            //else {
            //    this.solutions.Sort();
            //    this.cheapestPath = this.solutions[0];
            //}

            long x = this.JustPast(new CoordinateLong(0, 0), a, 3, 0);
            this.WalkBack(this.justPast, a, b, x, 3, 1);
            int y = 5;

            // Calculate cheapest path.
            if (this.solutions.Count == 0) {
                this.cheapestPath = -1;
            }
            else {
                this.solutions.Sort();
                this.cheapestPath = this.solutions[0];
            }
        }

        public string[] SplitCoordinate(char separator, string input) {

            return input.Split(separator);
        }


        //public int FewestTokensForPrize() {

        //    int costA_x = PathToSuccess(this.buttonA_x, buttonB_x, 0, prize_x, 0);
        //    int costA_y = PathToSuccess(this.buttonA_y, buttonB_y, 0, prize_y, 0);

        //    if (costA_x == -1 || costA_y == -1) return -1;

        //    return costA_x + costA_y;
        //}

        public CoordinateLong AddToCoordinate(CoordinateLong coord, long x, long y) {

            return new CoordinateLong(coord.row + y, coord.col + x);
        }

        public CoordinateLong SubtractFromCoordinate(CoordinateLong coord, long x, long y) {

            return new CoordinateLong(coord.row - y, coord.col - x);
        }

        public void WalkBack(CoordinateLong start, ArcadeButton buttonToRemove, 
            ArcadeButton buttonToAdd, long tokenCount,
            int buttonToRemoveCost, int buttonToAddCost) {

            int currentProgress = -1;
            long startingTokenCount = tokenCount;

            bool fContinue = true;
            while (fContinue) {

                double progress = (double)(startingTokenCount - tokenCount) / (double)startingTokenCount;
                int newProgress = (int) Math.Round(progress * 100);

                if (newProgress != currentProgress) {
                    currentProgress = newProgress;
                    Console.WriteLine("progress: " + currentProgress + "%");
                }

                // Are we on it exactly? 
                if ((start.col == this.prize.x) && (start.row == this.prize.y)) {
                    this.solutions.Add(tokenCount);
                }
                //return tokenCount;

                // Did we walk it back so far that we are past the starting point?
                else if ((start.col < 0 || start.row < 0)) {
                    fContinue = false;
                }

                else {
                    // Remove one.
                    start = SubtractFromCoordinate(start, buttonToRemove.x, buttonToRemove.y);
                    tokenCount = tokenCount - buttonToRemoveCost;

                    long xDelta = prize.x - start.col;
                    long yDelta = prize.y - start.row;

                    bool isEvenlyDivisibleX = (xDelta % buttonToAdd.x == 0);
                    long divisibleCountX = xDelta / buttonToAdd.x;
                    bool isEvenlyDivisibleY = (yDelta % buttonToAdd.y == 0);
                    long divisbleCountY = yDelta / buttonToAdd.y;

                    if (isEvenlyDivisibleX && isEvenlyDivisibleY && (divisibleCountX == divisbleCountY)) {
                        tokenCount += (buttonToAddCost * divisbleCountY);
                        this.solutions.Add(tokenCount);
                    }
                }
            }
        }


        public long JustPast(CoordinateLong start, ArcadeButton button, int buttonCost, int tapCount) {

            long xDelta = this.prize.x - start.col;
            long yDelta = this.prize.y - start.row;

            //bool isEvenlyDivisibleX = (xDelta % button.x == 0);
            long divisibleCountX = (xDelta / button.x) + 1;
            //bool isEvenlyDivisibleY = (yDelta % button.y == 0);
            long divisbleCountY = (yDelta / button.y) + 1;
            long longPole = ((divisibleCountX > divisbleCountY) ? divisibleCountX : divisbleCountY);

            // Hack global.
            this.justPast = new CoordinateLong(divisibleCountX * button.x, 
                divisbleCountY * button.y);

            return longPole * buttonCost;

            //if (isEvenlyDivisibleX && isEvenlyDivisibleY && (divisibleCountX == divisbleCountY)) {
            //    tokenCount += (buttonToAddCost * divisbleCountY);
            //    this.solutions.Add(tokenCount);
            //}







            //bool fContinue = true;
            //while (fContinue) {


            //    tapCount += buttonCost;
            //    CoordinateLong end = this.AddToCoordinate(start, button.x, button.y);
            //    if ((end.col > this.prize.x) || (end.row > this.prize.y)) {
            //        // Hack global.
            //        this.justPast = end;
            //        return tapCount;
            //    }

            //    //return this.JustPast(end, button, buttonCost, tapCount);
            //}

            //Debug.Assert(false, "should never have gotten here.");
            //return -1;
        }


        public int JustPastOld(CoordinateLong start, ArcadeButton button, int buttonCost, int tapCount) {

            tapCount += buttonCost;
            CoordinateLong end = this.AddToCoordinate(start, button.x, button.y);
            if ((end.col > this.prize.x) || (end.row > this.prize.y)) {
                // Hack global.
                this.justPast = end;
                return tapCount;
            }

            return this.JustPastOld(end, button, buttonCost, tapCount);
        }
        

        public void CalculateFewestTokens(int costSoFar, CoordinateLong start) {

            //Coordinate start = new Coordinate(0, 0);
            CoordinateLong end = new CoordinateLong(this.prize.x, this.prize.y);

            // Try button A.
            int costSoFar_A = costSoFar + 3;
            CoordinateLong endA = AddToCoordinate(start, a.x, a.y);
            if ((endA.row == end.row) && (endA.col == end.col)) {
                this.solutions.Add(costSoFar_A);
                // return;
            }
            else if ((endA.row >= end.row) || (endA.col >= end.col)) {
                int x = 5;
                //return;
            }
            else {
                this.taps++;
                this.CalculateFewestTokens(costSoFar_A, endA);
            }

            // Try button B.
            int costSoFar_B = costSoFar + 1;
            CoordinateLong endB = AddToCoordinate(start, b.x, b.y);
            if ((endB.row == end.row) && (endB.col == end.col)) {
                this.solutions.Add(costSoFar_B);
                //return;
            }
            else if ((endB.row >= end.row) || (endB.col >= end.col)) {
                int x = 5;
                //return;
            }
            else {
                this.taps++;
                this.CalculateFewestTokens(costSoFar_B, endB);
            }

            return;

            ////int costA_x = PathToSuccess(this.buttonA_x, buttonB_x, 0, prize_x, 0);
            ////int costA_y = PathToSuccess(this.buttonA_y, buttonB_y, 0, prize_y, 0);

            //if (costA_x == -1 || costA_y == -1) return -1;

            //return costA_x + costA_y;
        }



        public long FewestTokens() {

            return this.cheapestPath;
        }




        public int PathToSuccess(int value1, int value2, int valueStart, int valueEnd, int costSoFar) {

            const int value1_cost = 3;
            const int value2_cost = 1;
            //int newValue = valueStart; // (valueStart + value1 + value2);
         
            // We have achieved victory. 
            if (valueStart == valueEnd) {
                return costSoFar;
            }

            // Have we gone over?
            if (valueStart > valueEnd) {
                return -1;
            }

            int costSoFar1 = this.PathToSuccess(value1, value2, valueStart + value1, valueEnd, costSoFar + value1_cost);
            int costSoFar2 = this.PathToSuccess(value1, value2, valueStart + value2, valueEnd, costSoFar + value2_cost);

            if (costSoFar1 >= costSoFar2) return costSoFar1;

            return costSoFar2;
        }
    }
}
