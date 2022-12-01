using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2021 {


    public struct Step {
        public Coordinate start;
        public Coordinate end;
     
        public Step(Coordinate start, Coordinate end) : this() {
        //public Step(Coordinate start) : this() {
                this.start = start;
            this.end = end;
        }

        public override bool Equals(object obj) {
            if (!(obj is Step))
                return false;

            Step step = (Step)obj;
            return (this.start.Equals(step.start) && this.end.Equals(step.end));
        }
    }


    public struct Coordinate {
        public int x;
        public int y;
       
        public Coordinate(int x, int y) : this() {
            this.x = x;
            this.y = y;
        }


        public override bool Equals(object obj) {
            if (!(obj is Coordinate))
                return false;

            Coordinate coordinate = (Coordinate)obj;
            return (this.x == coordinate.x && this.y == coordinate.y);
            
        }


        public Coordinate GoEast() { return new Coordinate(x + 1, y); }
        public Coordinate GoSouth() { return new Coordinate(x, y + 1); }
        public Coordinate GoWest() { return new Coordinate(x - 1, y); }
        public Coordinate GoNorth() { return new Coordinate(x, y - 1); }

    }


    public class RiskLevelMap {

        public int[,] map;
        // public int[,] mapTotalCheapestCost;
        public int[,] mapCost;

        int width;
        int height;
        public int iterations = 0;
        public Dictionary<Step, int> mapTotalCheapestCost;
        private int cheapestPathSoFar = Int32.MaxValue;
        public int paceCarCost;




        public RiskLevelMap(string[] lines) {

            width = lines[0].Length;
            height = lines.Length;
            this.map = new int[width, height];
            //this.mapTotalCheapestCost = new int[width, height];
            this.mapTotalCheapestCost = new Dictionary<Step, int>();

            this.mapCost = new int[width, height];

           
            // initialize the map.
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    this.map[x, y] = System.Convert.ToInt32(lines[y][x].ToString());
                }
            }

            // initialize mapCost, including setting the cost of last step.
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    this.mapCost[x, y] = -1;
                }
            }
            this.mapCost[width - 1, height - 1] = this.map[width - 1, height - 1];



            // initialize our cached values.
            //this.mapTotalCheapestCost[width - 1, height - 1] = this.map[width - 1, height - 1];
            int paceCarCost = 0;
            //for (int y = 0; y < height; y++) {
            // go across the top row.
            for (int x = 1; x < width; x++) {
                paceCarCost += this.map[x, 0];
            }
            for (int y = 1; y < height; y++) {
                paceCarCost += this.map[(width - 1), y];
            }
            this.paceCarCost = paceCarCost;
        }

                //public int FindCheapestPathBackwards() {

                //    int eastCost = Int32.MaxValue;
                //    int southCost = Int32.MaxValue;
                //    int westCost = Int32.MaxValue;
                //    int northCost = Int32.MaxValue;


                //    this.mapTotalCheapestCost[this.width - 1, this.height - 1] = this.map[this.width - 1, this.height - 1];
                //    Collection<Coordinate> pathSoFar = new Collection<Coordinate>();
                //    //pathSoFar.Add(here);


                //    for (int y=this.height-1; y>=0; y--) {
                //        for (int x=this.width-1; x>= 0; x--) {

                //            Coordinate here = new Coordinate(x, y);

                //            //// Have we been here before?
                //            //if (this.mapTotalCheapestCost[here.x, here.y] != 0) {
                //            //    // do nothing .
                //            //}
                //            //else {

                //            // Can i go east?
                //            if ((here.GoEast().x < this.width) && (!pathSoFar.Contains(here.GoEast()))) {
                //                    eastCost = FindCheapestPath(pathSoFar, here.GoEast());
                //                    this.mapTotalCheapestCost[here.GoEast().x, here.GoEast().y] = eastCost;

                //                }

                //                // Can i go south?
                //                if ((here.GoSouth().y < this.height) && (!pathSoFar.Contains(here.GoSouth()))) {
                //                    southCost = FindCheapestPath(pathSoFar, here.GoSouth());
                //                    this.mapTotalCheapestCost[here.GoSouth().x, here.GoSouth().y] = southCost;

                //                }

                //                // Can i go west?
                //                if ((here.GoWest().x >= 0) && (!pathSoFar.Contains(here.GoWest()))) {
                //                    westCost = FindCheapestPath(pathSoFar, here.GoWest());
                //                    this.mapTotalCheapestCost[here.GoWest().x, here.GoWest().y] = westCost;

                //                }

                //                // Can i go north?
                //                if ((here.GoNorth().y >= 0) && (!pathSoFar.Contains(here.GoNorth()))) {
                //                    northCost = FindCheapestPath(pathSoFar, here.GoNorth());
                //                    this.mapTotalCheapestCost[here.GoNorth().x, here.GoNorth().y] = northCost;
                //                }

                //                int cheapestPath = Int32.MaxValue;
                //                if (eastCost < cheapestPath) cheapestPath = eastCost;
                //                if (southCost < cheapestPath) cheapestPath = southCost;
                //                if (westCost < cheapestPath) cheapestPath = westCost;
                //                if (northCost < cheapestPath) cheapestPath = northCost;

                //                // if all paths lead nowhere, give up.
                //                if (cheapestPath != Int32.MaxValue) {

                //                    // Add my own risk cost and return that.
                //                    //cheapestPath += map[here.x, here.y];
                //                }

                //                // Cache the cheapest path if someone stepped on this coordinate again in the future.
                //                //this.mapTotalCheapestCost[here.x, here.y] = cheapestPath;

                //                //return cheapestPath;

                //          //  }
                //        }
                //    }

                //    return this.mapTotalCheapestCost[0, 0];
                //}







                public int InvestigateDirection(Collection<Coordinate> pathSoFarIncludingHere, Coordinate here, Coordinate there) {

            // out of bounds?  Return maxvalue.
            if (there.x >= this.width || there.x < 0 || there.y >= height || there.y < 0) return int.MaxValue;

            // Already been there?  Return maxvalue.
            if (pathSoFarIncludingHere.Contains(there)) return int.MaxValue;

            // did we already cache the value of the cheapest path from that cell?
            //if (this.mapTotalCheapestCost[there.x, there.y] != 0) return this.mapTotalCheapestCost[there.x, there.y];
            Step step = new Step(here, there);
            if (this.mapTotalCheapestCost.ContainsKey(step)) {
                return this.mapTotalCheapestCost[step];
            }

            //if ((here.GoEast().x < this.width) && (!pathSoFar.Contains(here.GoEast()))) {
            //if ((here.GoEast().x < this.width) && (!pathSoFar.Contains(here.GoEast()))) {

            // Oh, we need to do some work there.
            int cost = FindCheapestPath(pathSoFarIncludingHere, there);
            return cost;
        }



        //private bool IsMyNeighborLockedDown(Coordinate here) {

        //    Coordinate south = here.GoSouth();
        //    Coordinate east = here.GoEast();

        //    if (((south.x >= 0 && south.x < width && south.y >=0 && south.y < height) &&
        //         (this.mapTotalCheapestCost[south.x, south.y] != 0)) ||
        //        ((east.x >= 0 && east.x < width && east.y >= 0 && east.y < height) &&
        //          this.mapTotalCheapestCost[east.x, east.y] != 0)) {
        //        return true;
        //        }

        //    return false;


        //    }

        // private costMap


        public int MyLastTry(Coordinate here, Collection<Coordinate> pathSoFar) {

            int totalCost = int.MaxValue;

            // Do we already have the value?  
            if (this.mapCost[here.x, here.y] != -1) {
                return this.mapCost[here.x, here.y];
            }

            // Add me to the new history of all coordinates travelled. 
            Collection<Coordinate> newPathSoFar = new Collection<Coordinate>();
            foreach (Coordinate coordinate in pathSoFar) newPathSoFar.Add(coordinate);
            newPathSoFar.Add(here);

            // Otherwise, let's ask our neighbor parths.
            Collection<Coordinate> neighbors = new Collection<Coordinate>();
            neighbors.Add(here.GoEast());
            neighbors.Add(here.GoSouth());
            neighbors.Add(here.GoWest());
            neighbors.Add(here.GoNorth());

            int bestCost = int.MaxValue;
            //Coordinate bestNeighbor;
            foreach (Coordinate neighbor in neighbors) {

                int currentCost;
                if (neighbor.x < 0 || neighbor.x >= width || neighbor.y < 0 || neighbor.y >= height) {
                    currentCost = int.MaxValue;
                }
                else if (pathSoFar.Contains(neighbor)) {
                    currentCost = int.MaxValue;
                }
                else {
                    currentCost = MyLastTry(neighbor, newPathSoFar);
                }

                if (currentCost < bestCost) {
                    bestCost = currentCost;

                   

                    //    totalCost = bestCost + this.map[here.x, here.y];
                    //   // bestNeighbor = neighbor;
                    //    this.mapCost[here.x, here.y] = totalCost;
                    //}
                }
            }

            if (bestCost == int.MaxValue) return int.MaxValue;

            if (here.x == 0 && here.y == 0) {
                totalCost = bestCost;
            }
            else {
                totalCost = bestCost + this.map[here.x, here.y];
            }
            // bestNeighbor = neighbor;
            this.mapCost[here.x, here.y] = totalCost;


            return totalCost;
            

        }


        public int FindCheapestPath2(Coordinate here, int costSoFar, Collection<Coordinate> pathSoFar, int recursiveLevel) {

            // out of bounds?  Return maxvalue.
            if (here.x >= this.width || here.x < 0 || here.y >= height || here.y < 0) {
                return int.MaxValue;
            }

            // Have i already surpassed my paceCarCost?
            if (costSoFar >= this.paceCarCost) {
                return int.MaxValue;
            }

            // is this a 9?  bail?
            if (this.map[here.x, here.y] == 9) {
                return int.MaxValue;
            }

            // Have i already been here?
            if (pathSoFar.Contains(here)) {
                return int.MaxValue;
            }

            // Debugging.
            if (recursiveLevel <= 3) {
                Console.WriteLine(recursiveLevel.ToString() + ": happening.");
            }

            // Add me to the new history of all coordinates travelled. 
            Collection<Coordinate> newPathSoFar = new Collection<Coordinate>();
            foreach (Coordinate coordinate in pathSoFar) newPathSoFar.Add(coordinate);
            newPathSoFar.Add(here);

            // increment our cost, except if we are at 0,0.
            if (!(here.x == 0 && here.y == 0)) {
                costSoFar += this.map[here.x, here.y];
                if (costSoFar >= this.cheapestPathSoFar) return int.MaxValue;
            }

            // Did i get to final destination?
            if (here.x == (this.width - 1) && here.y == (this.height - 1)) {
                if (costSoFar < this.cheapestPathSoFar) this.cheapestPathSoFar = costSoFar;
                return costSoFar;
            }

            // Can i go east?
            int eastCost = this.FindCheapestPath2(here.GoEast(), costSoFar, newPathSoFar, (recursiveLevel + 1));
            int southCost = this.FindCheapestPath2(here.GoSouth(), costSoFar, newPathSoFar, (recursiveLevel + 1));
            int westCost = this.FindCheapestPath2(here.GoWest(), costSoFar, newPathSoFar, (recursiveLevel + 1));
            int northCost = this.FindCheapestPath2(here.GoNorth(), costSoFar, newPathSoFar, (recursiveLevel + 1));

            int cheapestPath = Math.Min(eastCost, southCost);
            cheapestPath = Math.Min(cheapestPath, westCost);
            cheapestPath = Math.Min(cheapestPath, northCost);
            return cheapestPath;

        }





        public int FindCheapestPath(Collection<Coordinate> pathSoFar, Coordinate here) {

            this.iterations++;

            //// Have we been here before?
            //if (this.mapTotalCheapestCost[here.x, here.y] != 0) return this.mapTotalCheapestCost[here.x, here.y]; // this should never fire.

            // Take the list we have already traversed, and make a copy, and then add here to it.
            Collection<Coordinate> newPath = new Collection<Coordinate>();
            foreach (Coordinate coordinate in pathSoFar) newPath.Add(new Coordinate(coordinate.x, coordinate.y));
            newPath.Add(here);

            // Did we get to the final destination?  Return the cost of stepping on it.
            if (here.x == this.width - 1 && here.y == this.height - 1) {
                return map[here.x, here.y]; // this hsould never fire.
            }

            // Can i go east?
            int eastCost = this.InvestigateDirection(newPath, here, here.GoEast());
            int southCost = this.InvestigateDirection(newPath, here, here.GoSouth());
            int westCost = this.InvestigateDirection(newPath, here, here.GoWest());
            int northCost = this.InvestigateDirection(newPath, here, here.GoNorth());

            //if ( (here.GoEast().x  < this.width)  && (!pathSoFar.Contains(here.GoEast()))) {
            //    //if ((here.GoEast().x < this.width) && (!pathSoFar.Contains(here.GoEast()))) {
            //        eastCost = FindCheapestPath(newPath, here.GoEast());
            //}

            //// Can i go south?
            //if ((here.GoSouth().y < this.height) && (!pathSoFar.Contains(here.GoSouth()))) {
            //    southCost = FindCheapestPath(newPath, here.GoSouth());
            //}

            //// Can i go west?
            //if ((here.GoWest().x >= 0) && (!pathSoFar.Contains(here.GoWest()))) {
            //    westCost = FindCheapestPath(newPath, here.GoWest());
            //}

            //// Can i go north?
            //if ((here.GoNorth().y >= 0) && (!pathSoFar.Contains(here.GoNorth()))) {
            //    northCost = FindCheapestPath(newPath, here.GoNorth());
            //}

            //if ((-1 == eastCost) && (-1 == southCost) && (-1 == Cost) && (-1 == southCost))

            int cheapestPath = Int32.MaxValue;
            Coordinate destination = here;

            if (eastCost < cheapestPath) {
                cheapestPath = eastCost;
                destination = here.GoEast();
            }
            if (southCost < cheapestPath) {
                cheapestPath = southCost;
                destination = here.GoSouth();
            }
            if (westCost < cheapestPath) {
                cheapestPath = westCost;
                destination = here.GoWest();
            }
            if (northCost < cheapestPath) {
                cheapestPath = northCost;
                destination = here.GoNorth();
            }

            // if all paths lead nowhere, give up.
            if (cheapestPath == Int32.MaxValue) {
                Step stepGoesNowhere = new Step(here, destination);
                this.mapTotalCheapestCost[stepGoesNowhere] = cheapestPath;
                return Int32.MaxValue;
            }

            // Add my own risk cost and return th
            if (!(here.x == 0 && here.y == 0)) {
                cheapestPath += map[here.x, here.y];
            }

            // Cache the cheapest path if someone stepped on this coordinate again in the future.
            //if (IsMyNeighborLockedDown(here)) { 
            //if (this.mapTotalCheapestCost[here.GoSouth().x, here.GoSouth().y] != 0 && this.mapTotalCheapestCost[here.GoEast().x, here.GoEast().y] != 0) {
            //    this.mapTotalCheapestCost[here.x, here.y] = cheapestPath;
            Step step = new Step(here, destination);
            this.mapTotalCheapestCost[step] = cheapestPath;
            //}
            //this.mapTotalCheapestCost[]

            return cheapestPath;
        }
    }
}
