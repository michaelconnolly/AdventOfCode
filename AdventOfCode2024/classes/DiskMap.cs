using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {


    internal class DiskMap {

        private List<DiskMapItem> items = new List<DiskMapItem>();
        private int totalLength = 0;
        private char[] startingFileBlocks;
        private long checkSum;
        private long checkSum2;
        private Dictionary<int, int> dictUsed2 = new Dictionary<int, int>();

        public DiskMap(string input) {

            bool currentDigitIsFile = true;
            int currentFileIndex = 0;
            int currentPosition = 0;

            for (int i = 0; i < input.Length; i++) {

                int currentDigit = int.Parse(input[i].ToString());
                this.totalLength += currentDigit;

                if (currentDigitIsFile) {

                    this.items.Add(new DiskMapItem(currentFileIndex, currentPosition, currentDigit));
                    currentDigitIsFile = false;
                    currentFileIndex++;
                    currentPosition += currentDigit;
                }
                else {
                    currentDigitIsFile = true;
                    currentPosition += currentDigit;
                }
            }

            this.startingFileBlocks = this.CalculateStartPosition();
            Console.WriteLine(this.startingFileBlocks);

            this.MoveBlocks1();
            this.MoveBlocks2();
        }



        public char[] Print1() {

            long _checkSum = 0;

            // Pre-format the output string to have spacers.
            char[] output = new char[this.totalLength];
            for (int i = 0; i < this.totalLength; i++) {
                output[i] = '.';
            }

            // For each item ....
            for (int i = 0; i < this.items.Count; i++) {

                // If there are no final positions, that means this was not moved.
                if (this.items[i].finalPositions.Count == 0) {

                    for (int j = 0; j < this.items[i].length; j++) {
                        int currentLocation = this.items[i].startPos + j;
                        output[currentLocation] = i.ToString()[0];
                        _checkSum += i * currentLocation;
                    }
                }


                else {
                    foreach (int newLocation in this.items[i].finalPositions) {
                        char toPrint = i.ToString()[0];
                        //output[i] = toPrint;
                        output[newLocation] = toPrint;
                        _checkSum += i * newLocation;
                    }

                    // Are there final bits that need to be shared out, if we only half moved it?
                    if (this.items[i].finalPositions.Count < this.items[i].length) {
                        for (int j = 0; j < this.items[i].length - this.items[i].finalPositions.Count; j++) {
                            int currentLocation = this.items[i].startPos + j;
                            output[currentLocation] = i.ToString()[0];
                            _checkSum += i * currentLocation;
                        }
                    }
                }
            }

            int countAll = 0;
            int countNone = 0;
            int countPartial = 0;
            for (int index = 0; index < this.items.Count; index++) {
                if (this.items[index].finalPositions.Count == this.items[index].length) {
                    countAll++;
                }
                else if (this.items[index].finalPositions.Count == 0) {
                    countNone++;
                }
                else {
                    countPartial++;
                }
            }
            Console.WriteLine("full: " + countAll + ", partial: " + countPartial + ", none: " + countNone);


            this.checkSum = _checkSum;
            return output;

        }

        public char[] Print2() {

            long _checkSum = 0;

            // Pre-format the output string to have spacers.
            char[] output = new char[this.totalLength];
            for (int i = 0; i < this.totalLength; i++) {
                output[i] = '.';
            }

            // For each item ....
            for (int i = 0; i < this.items.Count; i++) {

                // If there are no final positions, that means this was not moved.
                if (this.items[i].finalPositions2.Count == 0) {

                    for (int j = 0; j < this.items[i].length; j++) {
                        int currentLocation = this.items[i].startPos + j;
                        output[currentLocation] = i.ToString()[0];
                        _checkSum += i * currentLocation;
                    }
                }


                else {
                    foreach (int newLocation in this.items[i].finalPositions2) {
                        char toPrint = i.ToString()[0];
                        //output[i] = toPrint;
                        output[newLocation] = toPrint;
                        _checkSum += i * newLocation;
                    }

                    // Are there final bits that need to be shared out, if we only half moved it?
                    if (this.items[i].finalPositions2.Count < this.items[i].length) {
                        for (int j = 0; j < this.items[i].length - this.items[i].finalPositions2.Count; j++) {
                            int currentLocation = this.items[i].startPos + j;
                            output[currentLocation] = i.ToString()[0];
                            _checkSum += i * currentLocation;
                        }
                    }
                }
            }

            int countAll = 0;
            int countNone = 0;
            int countPartial = 0;
            for (int index = 0; index < this.items.Count; index++) {
                if (this.items[index].finalPositions2.Count == this.items[index].length) {
                    countAll++;
                }
                else if (this.items[index].finalPositions2.Count == 0) {
                    countNone++;
                }
                else {
                    countPartial++;
                }
            }
            Console.WriteLine("full: " + countAll + ", partial: " + countPartial + ", none: " + countNone);


            this.checkSum2 = _checkSum;
            return output;

        }


        public void MoveBlocks1() {

            int currentIndexMove = this.items.Count - 1;
            int currentMoveRemaining = this.items[currentIndexMove].length;

            int currentIndexAdd1 = -1;
            int currentIndexAdd2 = currentIndexAdd1 + 1;
            int gapStart = 0;
            int gapLength = 0;
            int gapCurrent = 0;


            bool fContinue = true;
            while (fContinue) {

                while (currentMoveRemaining > 0) {

                    if (currentIndexMove == 5247) {
                        int x = 5;
                    }

                    // If we ran out of gap space, find the next gap.
                    // MC-TODO: not sure i'm dealing with gapLength zero right here.
                    if ((gapLength <= 0) || (gapCurrent > (gapStart + gapLength - 1))) {

                        do {
                            currentIndexAdd1++;
                            currentIndexAdd2 = currentIndexAdd1 + 1;
                            gapStart = this.items[currentIndexAdd1].startPos + this.items[currentIndexAdd1].length;
                            gapLength = (this.items[currentIndexAdd2].startPos) - gapStart;
                            gapCurrent = gapStart;
                        } while (gapLength == 0);

                        if (gapLength == 0) {
                            int x = 5;
                        }
                    }

                    // As soon as we move the gap window, if that gap window is higher than us, bail. 
                    if (currentIndexMove <= currentIndexAdd1) {
                        fContinue = false;
                        currentMoveRemaining = 0;
                    }
                    else {

                        // Note: make sure we cover the case where the last one we remove we might do partially.



                        //int moveDestination = gapCurrent;
                        //gapStart++;
                        this.items[currentIndexMove].finalPositions.Add(gapCurrent);
                        gapCurrent++;
                        currentMoveRemaining--;

                        //Console.WriteLine();
                        //Console.WriteLine(this.Print2());
                        //Console.WriteLine();

                        // we bail
                        if (gapCurrent >= this.items[currentIndexMove].startPos) {
                            fContinue = false;
                            break;
                        }
                    }
                }

                currentIndexMove--;
                currentMoveRemaining = this.items[currentIndexMove].length;

                if (currentIndexMove <= currentIndexAdd1) {
                    fContinue = false;

                }
            }
        }


        public void MoveBlocks2() {

            // Index for items to move.  Just start with the last one.
            int currentIndexMove = this.items.Count - 1;
            bool moveToTheNextItem = true;

            // Data we need to track for the gaps that we need to check, starting from leftmost.
            int currentIndexAdd1 = -1;
            int currentIndexAdd2 = currentIndexAdd1 + 1;
            int gapStart = 0;
            int gapLength = 0;
            int gapCurrent = 0;
            int gapRemaining = 0;
         
            // Keep this loop going until we finished each item, and each item checked each gap.
            bool fContinue = true;
            while (fContinue) {

                //Console.WriteLine(this.Print2());

                // Do we fit in this gap?
                if (gapRemaining < this.items[currentIndexMove].length) {

                    // Identify the next gap to check.
                    bool ranOutOfGaps = false;
                    do {

                        // Find the next gap!
                        currentIndexAdd1++;
                        currentIndexAdd2 = currentIndexAdd1 + 1;

                        if (currentIndexAdd2 > currentIndexMove) {

                        //if (currentIndexAdd2 >= this.items.Count) {
                            ranOutOfGaps = true;
                            moveToTheNextItem = true;
                            break;
                        }
                        else {
                            gapStart = this.items[currentIndexAdd1].startPos + this.items[currentIndexAdd1].length;
                            gapLength = (this.items[currentIndexAdd2].startPos) - gapStart;
                            gapCurrent = gapStart;
                            gapRemaining = (gapStart + gapLength) - gapCurrent;

                            // This is the new logic to actually then calculate things already moved into empty space.
                            int originalGapLength = gapLength;
                            int originalGapStart = gapStart;
                            for (int i = 0; i < originalGapLength; i++) {
                                if (this.dictUsed2.ContainsKey(originalGapStart + i)) {
                                    gapStart++;
                                    gapLength--;
                                    gapCurrent++;
                                    gapRemaining--;
                                }
                            }
                        }

                    } while (gapLength == 0);
      
                    if (currentIndexMove <= 0) {
                        fContinue = false;
                    }

                   //// if (!ranOutOfGaps) {

                   //     // As soon as we move the gap window, if that gap window is higher than us, bail. 
                   //     //if (currentIndexMove <= currentIndexAdd1) {
                   //     //   if (currentIndexMove == 0) {

                   //     //     fContinue = false;
                   //     //currentMoveRemaining = 0;
                   // }
                   // else {

                   if (!ranOutOfGaps) { 

                        // Note: make sure we cover the case where the last one we remove we might do partially.

                        if (this.items[currentIndexMove].length <= gapRemaining) {

                            for (int i = 0; i < this.items[currentIndexMove].length; i++) {
                                this.items[currentIndexMove].finalPositions2.Add(gapCurrent);
                                this.dictUsed2[gapCurrent] = currentIndexMove;
                                gapCurrent += 1;

                            }
                            gapRemaining = (gapStart + gapLength - 1) - gapCurrent;
                            moveToTheNextItem = true;
                        }
                        else {
                            moveToTheNextItem = false;
                        }
                    }
                    

                    // Are we ready to move to the next item?  Two obvious cases:
                    // 1) I ran out of gaps to investigate for this one.
                    // 2) i correctly place it.  
                    if (moveToTheNextItem) {
                        
                        currentIndexMove--;
                        currentIndexAdd1 = -1;
                        currentIndexAdd2 = currentIndexAdd1 + 1;
                        gapStart = 0;
                        gapLength = 0;
                        gapCurrent = 0;
                        gapRemaining = 0;
                    }
                }
            }
        }
        

        public char[] CalculateStartPosition() {

            char[] output = new char[this.totalLength];

            for (int i = 0; i < this.totalLength; i++) {
                output[i] = '.';
            }

            foreach (DiskMapItem item in this.items) {

                for (int i = item.startPos; i < (item.startPos + item.length); i++) {
                    output[i] = item.id.ToString()[0];
                }
            }

            //Console.WriteLine(output);
            return output;
        }

        public char[] GetStartPosition() {

            return this.startingFileBlocks;
        }

        public long GetChecksum() {

            return this.checkSum;
        }

        public long GetChecksum2() {

            return this.checkSum2;
        }
    }
}

