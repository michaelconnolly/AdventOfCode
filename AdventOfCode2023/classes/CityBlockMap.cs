using System;
using System.Collections.Generic;


namespace AdventOfCode2023.classes {


    internal enum CityBlockDirection {

        Straight = 0,
        Left,
        Right
    }

    internal enum CityBlockFacing {

        Up = 0,
        Down,
        Left,
        Right
    }

    internal class CityBlockMap {

        public int height, width;
        public CityBlock[,] cityBlocks;

        public CityBlockMap(string[] lines) {

            this.height = lines.Length;
            this.width = lines[0].Length;
            this.cityBlocks = new CityBlock[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int heatLoss = Convert.ToInt32(lines[y][x].ToString());
                    this.cityBlocks[x, y] = new CityBlock(x, y, heatLoss);
                }
            }
        }

        public void print() {

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(this.cityBlocks[x, y].heatLoss.ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        public CityBlockCoordinate GetNextCoordinate(CityBlockCoordinate start,
                CityBlockDirection newDirection, Dictionary<int, CityBlock> history) {

            int newX = start.x;
            int newY = start.y;
            CityBlockFacing newFacing = start.facing;

            switch (start.facing) {

                case CityBlockFacing.Up:

                    switch (newDirection) {
                        case CityBlockDirection.Straight:
                            newY--;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x, start.y - 1], direction, start.facing);
                        case CityBlockDirection.Left:
                            newX--;
                            newFacing = CityBlockFacing.Left;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x - 1, start.y], CityBlockDirection.Left, CityBlockFacing.Left);
                        case CityBlockDirection.Right:
                            newX++;
                            newFacing = CityBlockFacing.Right;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x + 1, start.y], CityBlockDirection.Right, CityBlockFacing.Right);
                        default:
                            throw new Exception();

                    }
                    break;

                case CityBlockFacing.Down:

                    switch (newDirection) {
                        case CityBlockDirection.Straight:
                            newY++;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x, start.y + 1], direction, start.facing);
                        case CityBlockDirection.Left:
                            newX++;
                            newFacing = CityBlockFacing.Right;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x + 1, start.y], CityBlockDirection.Right, CityBlockFacing.Right);
                        case CityBlockDirection.Right:
                            newX--;
                            newFacing = CityBlockFacing.Left;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x - 1, start.y], CityBlockDirection.Left, CityBlockFacing.Left);
                        default:
                            throw new Exception();

                    }
                    break;


                case CityBlockFacing.Left:

                    switch (newDirection) {
                        case CityBlockDirection.Straight:
                            newX--;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x - 1, start.y], direction, start.facing);
                        case CityBlockDirection.Left:
                            newY++;
                            newFacing = CityBlockFacing.Down;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x, start.y + 1], CityBlockDirection.Straight, CityBlockFacing.Down);
                        case CityBlockDirection.Right:
                            newY--;
                            newFacing = CityBlockFacing.Up;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x, start.y - 1], CityBlockDirection.Straight, CityBlockFacing.Up);
                        default:
                            throw new Exception();

                    }
                    break;


                case CityBlockFacing.Right:

                    switch (newDirection) {
                        case CityBlockDirection.Straight:
                            newX++;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x + 1, start.y], direction, start.facing);
                        case CityBlockDirection.Left:
                            newY--;
                            newFacing = CityBlockFacing.Up;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x, start.y - 1], CityBlockDirection.Straight, CityBlockFacing.Up);
                        case CityBlockDirection.Right:
                            newY++;
                            newFacing = CityBlockFacing.Down;
                            break;
                        //return new CityBlockCoordinate(this.cityBlocks[start.x, start.y + 1], CityBlockDirection.Straight, CityBlockFacing.Down);
                        default:
                            throw new Exception();
                    }
                    break;

                default:
                    throw new Exception();
            }

            if (IsLegal(newX, newY, history)) {
                
                return new CityBlockCoordinate(this.cityBlocks[newX, newY], newFacing);
            }
            else {
                return null;
            }
        }


        public bool IsLegal(int x, int y, Dictionary<int, CityBlock> history) {

            // Are we outside bounds?
            if ((x < 0 || x >= width || y < 0 || y >= height)) {
                return false;
            }

            CityBlock cityBlock = this.cityBlocks[x, y];

            // if we have already been here, forget about it.
            if (history.ContainsKey(cityBlock.id)) {
                return false;
            }

            return true;
        }

        private void PrintHistory(Dictionary<int, CityBlock> history) {

            Console.Write("history: ");
            foreach (int key in history.Keys) {
                Console.Write(history[key].x + "," + history[key].y + " (" + history[key].heatLoss + ")  ");
            }
            Console.WriteLine();
        }

        private string ConstructKey(CityBlockFacing facing, int straightCount) {

            return facing.ToString() + straightCount.ToString();
        }


        public int LeastHeatLoss(CityBlockCoordinate start, Dictionary<int, CityBlock> history,
            int carryOverHeatLoss, int accumulatedGoingStraight, bool useCache) {

            // Assume we already paid a cost to get to where we are. 
            // We have to add the cost of moving to the next space.
          
            // Did we get to the end? 
            if ((start.cityBlock.x == this.width - 1) && (start.cityBlock.y == this.height - 1)) {
                return carryOverHeatLoss;
            }

            // Do we have this cached?
            string keyName = this.ConstructKey(start.facing, accumulatedGoingStraight);
            if (useCache && (start.cityBlock.bestValues.ContainsKey(keyName))) {
            //if (useCache && (start.cityBlock.bestValues.ContainsKey(start.facing))) {
                return carryOverHeatLoss + start.cityBlock.bestValues[keyName];
                //if (accumulatedGoingStraight < (3 - start.cityBlock.bestValueStraightCount[start.facing])) {
                //    return carryOverHeatLoss + start.cityBlock.bestValues[start.facing];
                //}
                //return start.cityBlock.bestValues[start.facing];
            }

            // Put the current coordinate in the history.
            Dictionary<int, CityBlock> historyNew = new Dictionary<int, CityBlock>(history);
            historyNew[start.cityBlock.id] = start.cityBlock;

            int goLeftValue = int.MaxValue;
            int goStraightValue = int.MaxValue;
            int goRightValue = int.MaxValue;
            bool wentStraight = false;
         

            // Can we go left?
            CityBlockCoordinate nextCoordinate = this.GetNextCoordinate(start, CityBlockDirection.Left, history);
            if (nextCoordinate != null)  {

                // Did we have this cached?
                //if (nextCoordinate.cityBlock.bestValue != int.MaxValue) { 
                //    return nextCoordinate.cityBlock.bestValue; 
                //}

                goLeftValue = this.LeastHeatLoss(nextCoordinate, historyNew, 
                    (carryOverHeatLoss + nextCoordinate.cityBlock.heatLoss), 0, useCache);
            }

            // Can we go right?
            nextCoordinate = this.GetNextCoordinate(start, CityBlockDirection.Right, history);
            if (nextCoordinate != null) {

                //if (nextCoordinate.cityBlock.bestValue != int.MaxValue) { return nextCoordinate.cityBlock.bestValue; }

                //Dictionary<int, CityBlock> historyNew = new Dictionary<int, CityBlock>(history);
                //historyNew[nextCoordinate.cityBlock.id] = nextCoordinate.cityBlock;
                //historyNew[nextCoordinate.cityBlock.id] = null;
                //accumulatedHeatLoss += nextCoordinate.cityBlock.heatLoss;
                //accumulatedGoingStraight = 0;

                goRightValue = this.LeastHeatLoss(nextCoordinate, historyNew, 
                    (carryOverHeatLoss + nextCoordinate.cityBlock.heatLoss), 0, useCache);
            }

            // Can we go straight?
            nextCoordinate = this.GetNextCoordinate(start, CityBlockDirection.Straight, history);
            if (nextCoordinate != null) {
           
                if (accumulatedGoingStraight >= 3) {
                    goStraightValue = int.MaxValue;
                    nextCoordinate = null;
                }
                else {

                    //if (nextCoordinate.cityBlock.bestValue != int.MaxValue) { return nextCoordinate.cityBlock.bestValue; }

                    // Dictionary<int, CityBlock> historyNew = new Dictionary<int, CityBlock>(history);
                    // historyNew[nextCoordinate.cityBlock.id] = nextCoordinate.cityBlock;
                    //historyNew[nextCoordinate.cityBlock.id] = null;
                    //accumulatedHeatLoss += nextCoordinate.cityBlock.heatLoss;
                    //accumulatedGoingStraight += 1;

                    goStraightValue = this.LeastHeatLoss(nextCoordinate, historyNew,
                        carryOverHeatLoss + nextCoordinate.cityBlock.heatLoss,
                        (accumulatedGoingStraight + 1), useCache);
                }
            }

            // Return the lowest of the three values.
            int sumToReturn = int.MaxValue;
            if (goLeftValue < sumToReturn) {
                sumToReturn = goLeftValue;
            }
            if (goRightValue < sumToReturn) {
                sumToReturn = goRightValue;
            }
            if (goStraightValue < sumToReturn) {
                sumToReturn = goStraightValue;
                wentStraight = true;
            }

            // Cache the value.
            //if (useCache && (nextCoordinate != null && sumToReturn != int.MaxValue)) {
            if (useCache && (sumToReturn != int.MaxValue)) {

                // We need to record
                int straightCount = accumulatedGoingStraight;
                if (wentStraight) straightCount++;

                

                int amountDifferent = sumToReturn - carryOverHeatLoss;
                //start.cityBlock.bestValues[start.facing] = amountDifferent;
                //start.cityBlock.bestValueStraightCount[start.facing] = straightCount;
                start.cityBlock.bestValues[keyName] = amountDifferent;
            }

            //Console.WriteLine("value: " + sumToReturn);
            //this.PrintHistory(history);

            return sumToReturn;
        }
    }
}
