using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.classes {

    internal class LanternFishManager {

        char[,] map;
        char[,] mapLarge;
        string moveList = "";
        Coordinate robot;
        Coordinate robotLarge;

        public LanternFishManager(string[] input) {

            // ########
            // #..O.O.#
            // ##@.O..#
            // #...O..#
            // #.#.O..#
            // #...O..#
            // #......#
            // ########
            //
            // <^^>>>vv<v>>v<<

            // Figure out where the gap line is. 
            int gapLine = -1;
            for (int i=0; i<input.Length; i++) {
                if (input[i] == "") {
                    gapLine = i;
                    break;
                }
            }

            // The map is everything above the gap line.  
            string[] mapRaw = new string[gapLine];
            for (int i = 0; i < gapLine; i++) {
                mapRaw[i] = input[i];
            }
            this.ParseMap(mapRaw);

            // the move list is everything below the gap line. 
            for (int i = gapLine + 1; i < input.Length; i++) {
                moveList += input[i];
            }
        }

        private void ParseMap(string[] input) {

            this.map = new char[input[0].Length, input.Length];
            this.mapLarge = new char[input[0].Length, input.Length * 2];

            // small map.
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {
                    if (input[i][j] == '@') {
                        this.robot = new Coordinate(i, j);
                        this.map[i, j] = '.';
                    }
                    else {
                        this.map[i, j] = input[i][j];
                    }
                }
            }

            // large map.
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input[i].Length; j++) {

                    switch (input[i][j]) {

                        case '@':
                            this.robotLarge = new Coordinate(i, j*2);
                            this.mapLarge[i, j*2] = '.';
                            this.mapLarge[i, j*2+1] = '.';
                            break;

                        case '#':
                            this.mapLarge[i, j * 2] = '#';
                            this.mapLarge[i, j * 2+ 1] = '#';
                            break;

                        case '.':
                            this.mapLarge[i, j * 2] = '.';
                            this.mapLarge[i, j * 2+1] = '.';
                            break;

                        case 'O':
                            this.mapLarge[i, j * 2] = '[';
                            this.mapLarge[i, j * 2+1] = ']';
                            break;
                    }
                }
            }
        }


        public void PrintMapLarge() {

            for (int i = 0; i < this.mapLarge.GetLength(0); i++) {
                for (int j = 0; j < this.mapLarge.GetLength(1); j++) {

                    if (this.robotLarge.col == j && this.robotLarge.row == i) {
                        Console.Write('@');
                    }
                    else {
                        Console.Write(this.mapLarge[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }


        public void PrintMap() {

            for (int i = 0; i < this.map.GetLength(0); i++) {
                for (int j = 0; j < this.map.GetLength(1); j++) {

                    if (this.robot.col == j && this.robot.row == i) {
                        Console.Write('@');
                    }
                    else {
                        Console.Write(this.map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        private Coordinate GetNeighbor(Coordinate start, HikingDirection direction, bool wideMode=false) {

            int rowDelta = 0;
            int colDelta = 0;

            switch (direction) {
                case HikingDirection.Up:
                    rowDelta = -1;
                    break;
                case HikingDirection.Down:
                    rowDelta = 1;
                    break;
                case HikingDirection.Left:
                    colDelta = -1;
                    break;
                case HikingDirection.Right:
                    if (wideMode) {
                        colDelta = 2;
                    }
                    else {
                        colDelta = 1;
                    }
                    break;
            }

            return new Coordinate(start.row + rowDelta, start.col + colDelta);
        }


        private bool TryPushBox(HikingDirection direction, Coordinate box) {

            // boxes need to also be pushed, when appropriate, within this function.

            Coordinate neighbor = GetNeighbor(box, direction);
            char neighborItem = this.map[neighbor.row, neighbor.col];

            switch (neighborItem) {
                case '.':
                    // move the box.
                    //this.map[neighbor.row, neighbor.col] = 'O';
                    //this.map[box.row, box.col] = '.';
                    //return true;
                    break;

                case '#':
                    return false;

                case 'O':
                    if (!(TryPushBox(direction, neighbor))) {
                        return false;
                    }
                    break;

                default:
                    Debug.Assert(false);
                    return false;
            }

            this.map[neighbor.row, neighbor.col] = 'O';
            this.map[box.row, box.col] = '.';
            return true;
        }


        private int TryPushBoxLargeInternalOld(HikingDirection direction, Coordinate box, int stackCount) {

            // boxes need to also be pushed, when appropriate, within this function.
            Coordinate box2 = GetNeighbor(box, HikingDirection.Right);
            Coordinate neighbor = GetNeighbor(box, direction, true);
            char neighborItem = this.mapLarge[neighbor.row, neighbor.col];
            Coordinate neighbor2 = GetNeighbor(box2, direction, true);
            char neighborItem2 = this.mapLarge[neighbor2.row, neighbor2.col];

            if (neighborItem == '#' || neighborItem2 == '#') {
                // do nothing.
            }
            else if (neighborItem == '[' || neighborItem == ']' ||
                neighborItem2 == '[' || neighborItem2 == ']') {
                this.TryPushBoxLarge(direction, neighbor);
            }
            else if (neighborItem == '[' || neighborItem == ']' ||
                neighborItem2 == '[' || neighborItem2 == ']') {
                this.TryPushBoxLarge(direction, neighbor);
            }
            else if (neighborItem == '.' && neighborItem2 == '.') {
                this.robotLarge = neighbor;
                //robotLarge2 = neighbor2;
            }
            else {
                Debug.Assert(false);

            }


            //switch (neighborItem) {
            //    case '.':
            //        // move the box.
            //        //this.map[neighbor.row, neighbor.col] = 'O';
            //        //this.map[box.row, box.col] = '.';
            //        //return true;
            //        break;

            //    case '#':
            //        return -1;

            //    case 'O':
            //        if (!(TryPushBox(direction, neighbor))) {
            //            return -1;
            //        }
            //        break;

            //    default:
            //        Debug.Assert(false);
            //        return -1;
            //}

            this.map[neighbor.row, neighbor.col] = 'O';
            this.map[box.row, box.col] = '.';
            return (stackCount + 1);
        }


        private int TryPushBoxLargeHorizontal(HikingDirection direction, Coordinate box, int stackCount) {

            //Coordinate box2 = GetNeighbor(box, HikingDirection.Right);
            Coordinate neighbor = GetNeighbor(box, direction);
            char neighborItem = this.mapLarge[neighbor.row, neighbor.col];
            //Coordinate neighbor2 = GetNeighbor(box2, direction, true);
            //char neighborItem2 = this.mapLarge[neighbor2.row, neighbor2.col];

            if (neighborItem == '#') { // || neighborItem2 == '#') {
                // do nothing.
                return 0;
            }
            else if (neighborItem == '[' || neighborItem == ']') { // ||
               // neighborItem2 == '[' || neighborItem2 == ']') {
                return this.TryPushBoxLargeHorizontal(direction, neighbor, stackCount+1);
            }
            //else if (neighborItem == '[' || neighborItem == ']' ||
            //    neighborItem2 == '[' || neighborItem2 == ']') {
            //    this.TryPushBoxLarge(direction, neighbor);
            //}
            else if (neighborItem == '.') {  //} && neighborItem2 == '.') {
                //this.robotLarge = neighbor;
                //robotLarge2 = neighbor2;
                return (stackCount);
            }
            else {
                Debug.Assert(false);

            }


            //switch (neighborItem) {
            //    case '.':
            //        // move the box.
            //        //this.map[neighbor.row, neighbor.col] = 'O';
            //        //this.map[box.row, box.col] = '.';
            //        //return true;
            //        break;

            //    case '#':
            //        return -1;

            //    case 'O':
            //        if (!(TryPushBox(direction, neighbor))) {
            //            return -1;
            //        }
            //        break;

            //    default:
            //        Debug.Assert(false);
            //        return -1;
            //}

            //this.map[neighbor.row, neighbor.col] = 'O';
            //this.map[box.row, box.col] = '.';
            Debug.Assert(false);
            return (stackCount + 1);
        }


        private int TryPushBoxLargeVertical(HikingDirection direction, Coordinate box, int stackCount) {

            Coordinate box2 = GetNeighbor(box, HikingDirection.Right);

            Coordinate neighbor = GetNeighbor(box, direction);
            char neighborItem = this.mapLarge[neighbor.row, neighbor.col];
            Coordinate neighbor2 = GetNeighbor(box2, direction);
            char neighborItem2 = this.mapLarge[neighbor2.row, neighbor2.col];


            if (neighborItem == '#' || neighborItem2 == '#') {
                // do nothing.
                return 0;
            }
            else if (neighborItem == '.' && neighborItem2 == '.') {
                return stackCount;
            }

            else if (neighborItem == '[' && neighborItem2 == ']') { // ||
                                                                   // neighborItem2 == '[' || neighborItem2 == ']') {
                return this.TryPushBoxLargeVertical(direction, neighbor, stackCount + 1);
            }
            else if (neighborItem == '.' && neighborItem2 == '[') {

                return this.TryPushBoxLargeVertical(direction, neighbor2, stackCount + 1);
                //Debug.Assert(false);
            }
            else if (neighborItem == ']' && neighborItem2 == '.') {
                Coordinate neighbor1 = new Coordinate(neighbor.row, neighbor.col - 1);
                return this.TryPushBoxLargeVertical(direction, neighbor1, stackCount + 1);
                //Debug.Assert(false);
            }
            else if (neighborItem == ']' && neighborItem2 == '[') {
                Coordinate neighbor1 = new Coordinate(neighbor.row, neighbor.col-1);
                int leftPush = this.TryPushBoxLargeVertical(direction, neighbor1, stackCount + 1);
                int rightPush = this.TryPushBoxLargeVertical(direction, neighbor2, stackCount + 1);

                if (leftPush == 0 || rightPush == 0) { return 0; }

                if (leftPush == rightPush) { return leftPush; }

                return 0;


                //Debug.Assert(false);
            }
            //else if (neighborItem == '[' || neighborItem == ']' ||
            //    neighborItem2 == '[' || neighborItem2 == ']') {
            //    this.TryPushBoxLarge(direction, neighbor);
            //}
           // else if (neighborItem == '.') {  //} && neighborItem2 == '.') {
                //this.robotLarge = neighbor;
                //robotLarge2 = neighbor2;
            //    return (stackCount + 1);
            //}
            else {
                Debug.Assert(false);
            }


            //switch (neighborItem) {
            //    case '.':
            //        // move the box.
            //        //this.map[neighbor.row, neighbor.col] = 'O';
            //        //this.map[box.row, box.col] = '.';
            //        //return true;
            //        break;

            //    case '#':
            //        return -1;

            //    case 'O':
            //        if (!(TryPushBox(direction, neighbor))) {
            //            return -1;
            //        }
            //        break;

            //    default:
            //        Debug.Assert(false);
            //        return -1;
            //}

            //this.map[neighbor.row, neighbor.col] = 'O';
            //this.map[box.row, box.col] = '.';
            Debug.Assert(false);
            return (stackCount + 1);
        }



        private void TryPushBoxLarge(HikingDirection direction, Coordinate box) {

            // boxes need to also be pushed, when appropriate, within this function.
            // for this large box, the input variable 'box' represents the left side of the double-wide box.

            // the 'box' variable is always the left-most half of the box.
            Coordinate boxPair = GetNeighbor(box, HikingDirection.Right);
            int pushCount;

            switch (direction) {

                case HikingDirection.Left:

                    pushCount = TryPushBoxLargeHorizontal(direction, box, 1);
                    if (pushCount > 0) {

                        // when we push to the left, add 1, since box points to the left most of the pair.
                        pushCount++;
                        Coordinate current = this.robotLarge;
                        Debug.Assert(this.mapLarge[current.row, current.col - (pushCount + 1)] == '.');

                        for (int i = pushCount; i > 0; i--) {
                            this.mapLarge[current.row, current.col - (i + 1)] = this.mapLarge[current.row, current.col - i];
                        }

                        this.mapLarge[current.row, current.col - (1)] = '.';
                        this.robotLarge = new Coordinate(robotLarge.row, robotLarge.col - 1);
                    }
                    break;

                case HikingDirection.Right:

                    pushCount = TryPushBoxLargeHorizontal(direction, box, 1);
                    if (pushCount > 0) {

                        Coordinate current = this.robotLarge;
                        Debug.Assert(this.mapLarge[current.row, current.col + (pushCount + 1)] == '.');

                        for (int i = pushCount; i > 0; i--) {

                            this.mapLarge[current.row, current.col + (i + 1)] = this.mapLarge[current.row, current.col + i];
                   
                        }
                        this.mapLarge[current.row, current.col + (1)] = '.';
                        this.robotLarge = new Coordinate(robotLarge.row, robotLarge.col + 1);

                    }
                    break;

                case HikingDirection.Up:

                    pushCount = TryPushBoxLargeVertical(direction, box, 1);
                    if (pushCount > 0) {
                        this.ActualPushVertical(direction, box, pushCount);

                        //Coordinate current = this.robotLarge;
                        //Debug.Assert(this.mapLarge[current.row, current.col + (pushCount + 1)] == '.');

                        //for (int i = pushCount; i >= 0; i--) {

                        //    this.mapLarge[current.row + (i -1), current.col] = this.mapLarge[current.row -i, current.col];
                        //}
                        this.robotLarge = new Coordinate(robotLarge.row - 1, robotLarge.col);

                    }
                    break;


                case HikingDirection.Down:

                    pushCount = TryPushBoxLargeVertical(direction, box, 1);
                    if (pushCount > 0) {

                        this.ActualPushVertical(direction, box, pushCount);

                        //Coordinate current = this.robotLarge;
                        //Debug.Assert(this.mapLarge[current.row + (pushCount + 1), current.col] == '.');

                        //for (int i = pushCount; i >= 0; i--) {

                        //    this.mapLarge[current.row + (i + 1), current.col] = this.mapLarge[current.row + i, current.col];
                        //}
                        this.robotLarge = new Coordinate(robotLarge.row + 1, robotLarge.col);

                    }
                    break;


                default:
                    Debug.Assert(false);
                    break;
            }

           // int leftPush = TryPushBoxLargeInternal(direction, box, 0);
            //int rightPush = TryPushBoxLargeInternal(direction, boxPair, 0);

            //if (leftPush > 0 && rightPush > 0) {

            //    Coordinate current = box;
            //    Coordinate neighbor = GetNeighbor(current, direction, true);



            //    for (int i = 0; i < leftPush; i++) {
            //        this.mapLarge[neighbor.row, neighbor.col] = this.mapLarge[current.row, current.col]; // 'O';
            //        this.mapLarge[current.row, current.col] = '.';
            //        current = GetNeighbor(current, direction);
            //        neighbor = GetNeighbor(neighbor, direction);
            //    }

            //    current = boxPair;
            //    neighbor = GetNeighbor(current, direction, true);
            //    for (int i = 0; i < rightPush; i++) {
            //        this.mapLarge[neighbor.row, neighbor.col] = this.mapLarge[current.row, current.col]; // 'O';
            //        this.mapLarge[current.row, current.col] = '.';
            //        current = GetNeighbor(current, direction);
            //        neighbor = GetNeighbor(neighbor, direction);
            //    }
        }
          //  }
       // }
       public void ActualPushVertical(HikingDirection direction, Coordinate box, int  pushCount) {

            Coordinate box2 = GetNeighbor(box, HikingDirection.Right);

            Coordinate neighbor = GetNeighbor(box, direction);
            char neighborItem = this.mapLarge[neighbor.row, neighbor.col];
            Coordinate neighbor2 = GetNeighbor(box2, direction);
            char neighborItem2 = this.mapLarge[neighbor2.row, neighbor2.col];

            if (pushCount == 1) {
                Debug.Assert(neighborItem == '.');
                Debug.Assert(neighborItem2 == '.');
                this.mapLarge[neighbor.row, neighbor.col] = '[';
                this.mapLarge[neighbor2.row, neighbor2.col] = ']';
                this.mapLarge[box.row, box.col] = '.';
                this.mapLarge[box2.row, box2.col] = '.';
                return;
            }


            if (neighborItem == '[' && neighborItem2 == ']') { 
                this.ActualPushVertical(direction, neighbor, pushCount-1);
                this.mapLarge[neighbor.row, neighbor.col] = '[';
                this.mapLarge[neighbor2.row, neighbor2.col] = ']';
                this.mapLarge[box.row, box.col] = '.';
                this.mapLarge[box2.row, box2.col] = '.';
                return;
                //return this.TryPushBoxLargeHorizontal(direction, neighbor, stackCount + 1);
            }

            else if (neighborItem == '.' && neighborItem2 == '[') {

                this.ActualPushVertical(direction, neighbor2, pushCount-1);
                this.mapLarge[neighbor.row, neighbor.col] = '[';
                this.mapLarge[neighbor2.row, neighbor2.col] = ']';
                this.mapLarge[box.row, box.col] = '.';
                this.mapLarge[box2.row, box2.col] = '.';
                return;

               // return this.TryPushBoxLargeVertical(direction, neighbor2, stackCount + 1);
                //Debug.Assert(false);
            }
            else if (neighborItem == ']' && neighborItem2 == '.') {

                Coordinate box1 = new Coordinate(box.row, box.col - 1);
                Coordinate box1neighbor1 = this.GetNeighbor(box1, direction);
                this.ActualPushVertical(direction, box1neighbor1, pushCount-1);

                this.mapLarge[neighbor.row, neighbor.col] = '[';
                this.mapLarge[neighbor2.row, neighbor2.col] = ']';
                this.mapLarge[box.row, box.col] = '.';
                this.mapLarge[box2.row, box2.col] = '.';
                return;

                //return this.TryPushBoxLargeVertical(direction, neighbor, stackCount + 1);
                //Debug.Assert(false);
            }
            else if (neighborItem == ']' && neighborItem2 == '[') {

                Coordinate box1 = new Coordinate(box.row, box.col - 1);
                Coordinate box1neighbor1 = this.GetNeighbor(box1, direction);
                this.ActualPushVertical(direction, box1neighbor1, pushCount - 1);

                this.ActualPushVertical(direction, neighbor2, pushCount - 1);


                //this.ActualPushVertical(direction, box1neighbor2, pushCount - 1);
                this.mapLarge[neighbor.row, neighbor.col] = '[';
                this.mapLarge[neighbor2.row, neighbor2.col] = ']';
                this.mapLarge[box.row, box.col] = '.';
                this.mapLarge[box2.row, box2.col] = '.';
                return;

                //Coordinate neighbor1 = new Coordinate(neighbor.row, neighbor.col - 1);
                //int leftPush = this.TryPushBoxLargeVertical(direction, neighbor1, stackCount + 1);
                //int rightPush = this.TryPushBoxLargeVertical(direction, neighbor2, stackCount + 1);

                //if (leftPush == 0 || rightPush == 0) { return 0; }

                //if (leftPush == rightPush) { return leftPush; }

                //return 0;


                //Debug.Assert(false);
            }
            else {

                // There are corner cases where there is nothing to push
                // and pushCount isn't 1, such as when some other box already pushed it
                // out of the way.
                this.mapLarge[neighbor.row, neighbor.col] = '[';
                this.mapLarge[neighbor2.row, neighbor2.col] = ']';
                this.mapLarge[box.row, box.col] = '.';
                this.mapLarge[box2.row, box2.col] = '.';
                return;
            }

            //if (pushCount == 1) {
            //    Debug.Assert(neighborItem == '.');
            //    Debug.Assert(neighborItem2 == '.');
            //}



        }

        public void RunTheRobot() {

            int index = 0;

            foreach (char move in moveList) {

                // What direction do we need to move into?
                HikingDirection direction;
                switch (move) {
                    case '^':
                        direction = HikingDirection.Up;
                        break;
                    case 'v':
                        direction = HikingDirection.Down;
                        break;
                    case '<':
                        direction = HikingDirection.Left;
                        break;
                    case '>':
                        direction = HikingDirection.Right;
                        break;
                    default:
                        Debug.Assert(false);
                        direction = HikingDirection.Up;
                        return;
                }

                // Figure out the space i need to move into.
                Coordinate nextSpace = this.GetNeighbor(this.robot, direction);
                char nextSpaceItem = this.map[nextSpace.row, nextSpace.col];

                switch (nextSpaceItem) {

                    // if it's a wall, do nothing.
                    case '#':
                        // do nothing.
                        break;

                    // if: it's empty, move into it.
                    case '.':
                        this.robot = nextSpace;
                        break;

                    // if: it's a box, attempt to shove the box in that direction.
                    case 'O':
                        if (this.TryPushBox(direction, nextSpace)) {
                            this.robot = nextSpace;
                        }
                        break;


                }

                // Debug purposes.
                //Console.WriteLine();
                //Console.WriteLine("move " + index++ + ": " + move);
                //this.PrintMap();
            }
        }


        public void RunTheRobotLarge() {

            Console.WriteLine("total moves: " + this.moveList.Length);
            int index = 0;
    
            foreach (char move in moveList) {

                index++;
                
                //Coordinate robotLarge2 = GetNeighbor(this.robotLarge, HikingDirection.Right);

                // What direction do we need to move into?
                HikingDirection direction;
                switch (move) {
                    case '^':
                        direction = HikingDirection.Up;
                        break;
                    case 'v':
                        direction = HikingDirection.Down;
                        break;
                    case '<':
                        direction = HikingDirection.Left;
                        break;
                    case '>':
                        direction = HikingDirection.Right;
                        break;
                    default:
                        Debug.Assert(false);
                        direction = HikingDirection.Up;
                        return;
                }

                //if (index % 100 == 0) {
                //    int x = 5;
                //}

                if (index == 548) {
                    int x = 5;
                }
               // if (index >= 2576) {
                    this.PrintMapLarge();
                  //  int x = 5;
                //}

                // Figure out the space i need to move into.
                Coordinate nextSpace = this.GetNeighbor(this.robotLarge, direction);
                char nextSpaceItem = this.mapLarge[nextSpace.row, nextSpace.col];
                //Coordinate nextSpace2 = this.GetNeighbor(robotLarge2, direction);
                //char nextSpaceItem2 = this.mapLarge[nextSpace2.row, nextSpace2.col];

                if (nextSpaceItem == '#') { // || nextSpaceItem2 == '#') {
                    // do nothing.
                }
                else if (nextSpaceItem == '[') { 
                    this.TryPushBoxLarge(direction, nextSpace);
                }
                else if (nextSpaceItem == ']' ) {
                    Coordinate box = this.GetNeighbor(nextSpace, HikingDirection.Left);
                   // nextSpaceItem2 == '[' || nextSpaceItem2 == ']') {
                    this.TryPushBoxLarge(direction, box);
                }
                else if (nextSpaceItem == '.') {  // && nextSpaceItem2 == '.') {
                    this.robotLarge = nextSpace;
                    //robotLarge2 = nextSpace2;
                }
                else
                {
                    Debug.Assert(false);

                }

                //switch (nextSpaceItem) {

                //    // if it's a wall, do nothing.
                //    case '#':
                //        // do nothing.
                //        break;

                //    // if: it's empty, move into it.
                //    case '.':
                //        this.robotLarge = nextSpace;
                //        break;

                //    // if: it's a box, attempt to shove the box in that direction.
                //    case '[':
                //        if (this.TryPushBoxLarge(direction, nextSpace)) {
                //            this.robotLarge = nextSpace;
                //        }
                //        break;

                //    case ']':
                //        if (this.TryPushBoxLarge(direction, nextSpace)) {
                //            this.robotLarge = nextSpace;
                //        }
                //        break;
                //}

                // Debug purposes.
                //Console.WriteLine();
                //Console.WriteLine("move " + index + ": " + move);
                //this.PrintMapLarge();
            }
        }



        public int CalculateGpsValue(int row, int col) {

            return (100 * row) + col;
        }


        public int SumOfAllBoxCoordinates() {

            int sum = 0;

            for (int i = 0; i < this.map.GetLength(0); i++) {
                for (int j = 0; j < this.map.GetLength(1); j++) {
                    if (this.map[i,j] == 'O') {

                        sum += CalculateGpsValue(i, j);

                    }
                   
                }
                Console.WriteLine();
            }



            return sum;
        }

        public int SumOfAllBoxCoordinatesLarge() {

            int count = 0;
            int sum = 0;
            int height = this.mapLarge.GetLength(0);
            int width = this.mapLarge.GetLength(1);

            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {

                    if (this.mapLarge[row, col] == '[') {

                        count++;

                        // closest horizontal edge.
                        int horizontalAddress1 = col;
                        int horizontalAddress2 = width - col - 2;
                        //int horizontalAddress = Math.Min(horizontalAddress1, horizontalAddress2);
                        int horizontalAddress = horizontalAddress1;


                        //if (col < (width - col + 1)) {
                        //    horizontalAddress = col;
                        //}
                        //else {
                        //    horizontalAddress = width - col + 1;
                        //}

                        // closest vertical edge.
                        int verticalAddress1 = row;
                        int verticalAddress2 = height - row - 1;
                        //int verticalAddress = Math.Min(verticalAddress1, verticalAddress2);
                        int verticalAddress = verticalAddress1;

                        //if (row < (height - row)) {
                        //    verticalAddress = row;
                        //}
                        //else {
                        //    verticalAddress = height - row;
                        //}

                        int subTotal = (verticalAddress * 100) + (horizontalAddress);
                        sum += subTotal;
                    }
                }
                Console.WriteLine();
            }


            Console.WriteLine("count of boxes: " + count);

            return sum;
        }
    }
}
