using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {


    internal class DiskMap {

        private List<DiskMapItem> items = new List<DiskMapItem>();
        private int totalLength = 0;
        private char[] startingFileBlocks;
        private long checkSum;
        //public char[] endingFileBlocks;

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

            //this.endingFileBlocks = this.MoveBlocks();

            this.MoveBlocks2();
            int x = 5;
        }

        // Bad Guess
        // 6164517236960
        // 6164517236960

        //public char[] MoveBlocks() {

        //    int currentIndexAdd = 0;
        //    char[] output = new char[this.totalLength];
        //    this.startingFileBlocks.CopyTo(output, 0);


        //    //for each character that needs to be moved ...
        //    for (int currentIndexRemove = (this.totalLength - 1);
        //        currentIndexRemove > currentIndexAdd; currentIndexRemove--) {

        //        //for (int diskMapItemSegement = 0; diskMapItemSegement < this.items.Count; diskMapItemSegement++) {

        //        // As long as it's not a gap, figure out were we can move it.
        //        if (output[currentIndexRemove] != '.') {

        //            bool fContinue = true;
        //            while (fContinue) {

        //                //if (currentIndexAdd >= currentIndexRemove) { //this.totalLength) {
        //                //    fContinue = false;
        //                //}
        //                if (output[currentIndexAdd] == '.') {
        //                    output[currentIndexAdd] = output[currentIndexRemove];
        //                    output[currentIndexRemove] = '.';
        //                    fContinue = false;
        //                }
        //                currentIndexAdd++;

        //                // this.items[.id.ToString()[0];
        //                //       fContinue = false;
        //                //   }
        //                //    else {
        //                //       currentIndexAdd++;
        //                //   }
        //                // }

        //                // break;
        //                //}
        //            }
        //        }
        //    }

        //    return output;      
        //}


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


        public void MoveBlocks2() {

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

            //    int checksum = 0;

            //    for (int i = 0; i < this.totalLength; i++) {

            //        if (false) { // (this.endingFileBlocks[i] == '.') {
            //            //break;
            //        }
            //        else {
            //            int factor1 = i;
            //            int factor2 = 0; // (int)Char.GetNumericValue(this.endingFileBlocks[i]);
            //            //int factor2 = this.endingFileBlocks[i].ToString()[0];
            //            int product = factor1 * factor2;
            //            checksum += product;
            //        }
            //    }

            //    return checksum;
            //}
        }
    }
}

