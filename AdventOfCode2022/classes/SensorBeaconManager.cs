using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2022 {

    public class SensorBeaconManager {

        //char[,] map;
        //long lowestRow = int.MaxValue;
        //long highestRow = int.MinValue;
        //long lowestColumn = int.MaxValue;
        //long highestColumn = int.MinValue;
        ////long absoluteWidth;
        //long absoluteHeight;
        Collection<SensorBeaconCoordinate> sensors = new Collection<SensorBeaconCoordinate>();
        long goldenRow;
        //char[] mapGoldenRow;
        Dictionary<long, char> goldenRowDictionary = new Dictionary<long, char>();
        int beaconCountGoldenRow;
        long minValue;
        long maxValue;

        public SensorBeaconManager(string[] input, long goldenRow,
            long minValue=-1, long maxValue=-1) { 

            this.goldenRow = goldenRow;
            this.minValue = minValue;
            this.maxValue = maxValue;

            bool[,] map = new bool[4000000, 4000000];

            this.ProcessInput(input);
        }


        private void ProcessInput(string[] lines) {

            foreach (string line in lines) {

                // Extract the sensor and it's nearest beacon.
                // Example: Sensor at x=2, y=18: closest beacon is at x=-2, y=15
                string[] parts = line.Split(": ");
                string sensorString = parts[0].Substring(10);
                string beaconString = parts[1].Substring(21);
                SensorBeaconCoordinate beacon = new SensorBeaconCoordinate(beaconString, null);
                SensorBeaconCoordinate sensor = new SensorBeaconCoordinate(sensorString, beacon);
                sensors.Add(sensor);

                // Figure out the dimensions of the world we live in.
                //this.lowestRow = Math.Min(this.lowestRow, sensor.row - sensor.DistanceToBeacon());
                //this.highestRow = Math.Max(this.highestRow, sensor.row + sensor.DistanceToBeacon());
                //this.lowestColumn = Math.Min(this.lowestColumn, sensor.col - sensor.DistanceToBeacon());
                //this.highestColumn = Math.Max(this.highestColumn, sensor.col + sensor.DistanceToBeacon());
                //this.lowestRow = Math.Min(this.lowestRow, beacon.row);
                //this.highestRow = Math.Max(this.highestRow, beacon.row);
                //this.lowestColumn = Math.Min(this.lowestColumn, beacon.col);
                //this.highestColumn = Math.Max(this.highestColumn, beacon.col);
            }

            // Store the map dimensions!
            ///this.absoluteWidth = this.highestColumn - this.lowestColumn + 1;
            //this.absoluteHeight = this.highestRow - this.lowestRow + 1;

            //if (this.goldenRow == -1) {
            //    this.map = new char[absoluteHeight, absoluteWidth];

            //    for (int col = 0; col < absoluteWidth; col++) {
            //        for (int row = 0; row < absoluteHeight; row++) {
            //            this.map[row, col] = '.';
            //        }
            //    }
            //}
            //      else {

            // Initialize the map!
            //this.mapGoldenRow = new char[absoluteWidth];
            //for (int col = 0; col < absoluteWidth; col++) {
            //    //for (int row = 0; row < absoluteHeight; row++) {
            //    this.mapGoldenRow[col] = '.';
            //    // }
            //}

            //  }

            //this.Print();

            // Populate the map!
            foreach (SensorBeaconCoordinate sensor in this.sensors) {

                //long colModifier = -(this.lowestColumn);


                // golden row logic.
                if (sensor.row == this.goldenRow) {
                    //this.mapGoldenRow[colModifier + sensor.col] = 'S';
                    //this.goldenRowDictionary[colModifier + sensor.col] = 'S';
                    this.goldenRowDictionary[sensor.col] = 'S';

                }
                if (sensor.closestBeacon.row == this.goldenRow) {
                    //this.mapGoldenRow[colModifier + sensor.closestBeacon.col] = 'B';
                    //this.goldenRowDictionary[colModifier + sensor.closestBeacon.col] = 'B';
                    this.goldenRowDictionary[sensor.closestBeacon.col] = 'B';

                }
                //if (this.goldenRow == -1) {

                //    long rowModifier = -(this.lowestRow);

                //    this.map[rowModifier + sensor.row, colModifier + sensor.col] = 'S';
                //    this.map[rowModifier + sensor.closestBeacon.row,
                //        colModifier + sensor.closestBeacon.col] = 'B';
                //}
                //this.Print();
            }

            // Populate Sensor Ranges!
            //this.Print();
            this.PopulateSensorRange();
            //this.Print();
        }




        //public void MarkOneSensorRange(long row, long col) {

        //    // Golden row logic; i already modified the row index before calling this,
        //    // so to properly test, have to set row back to unmodified state.
        //    //if (this.useGoldenRow != -1) {
        //    if ((row + this.lowestRow) == this.goldenRow) {

        //        if (col >= 0 && col < this.absoluteWidth) {
        //            if (this.mapGoldenRow[col] == '.') {
        //                this.mapGoldenRow[col] = '#';
        //            }
        //        }
        //    }

        //    else if ((this.goldenRow == -1) && row >= 0 && row < this.absoluteHeight
        //                  && col >= 0 && col < this.absoluteWidth) {

        //        if (this.map[row, col] == '.') {
        //            this.map[row, col] = '#';
        //        }
        //    }
        //}


        public void PopulateSensorRange() {

            foreach (SensorBeaconCoordinate sensor in this.sensors) {

                //long range = sensor.DistanceToBeacon();
               // long rowModifier = -(this.lowestRow);
                //long colModifier = -(this.lowestColumn);

                // make sure to skip a sensor if it can't sense the golden row.
                long sensorRange = sensor.DistanceToBeacon();
                long sensorDistance = Math.Abs(sensor.row - this.goldenRow);
                if (sensorRange >= sensorDistance) {

                    long blastRadiusStart = sensor.col - (sensorRange - sensorDistance);
                    long blastRadiusLength = ((sensorRange - sensorDistance) * 2)  + 1;

                    for (long i = blastRadiusStart; i < (blastRadiusStart + blastRadiusLength); i++) {

                        if (!(this.goldenRowDictionary.ContainsKey(i))) {
                            this.goldenRowDictionary[i] = '#';
                        }
                    }


                    //for (long colIndex = 0; colIndex <= sensorRange; colIndex++) {

                    //    long distanceInThisColumn = range - colIndex;
                    //    long colIndexModifiedRight = colIndex + sensor.col + colModifier;
                    //    long colIndexModifiedLeft = -(colIndex) + sensor.col + colModifier;

                    //    for (long rowIndex = 0; rowIndex <= distanceInThisColumn; rowIndex++) {

                    //        long rowIndexModifiedBelow = rowIndex + sensor.row + rowModifier;
                    //        long rowIndexModifiedAbove = -(rowIndex) + sensor.row + rowModifier; //rowIndex + sensor.row + rowModifier;

                    //        this.MarkOneSensorRange(rowIndexModifiedBelow, colIndexModifiedRight);
                    //        this.MarkOneSensorRange(rowIndexModifiedAbove, colIndexModifiedRight);
                    //        this.MarkOneSensorRange(rowIndexModifiedBelow, colIndexModifiedLeft);
                    //        this.MarkOneSensorRange(rowIndexModifiedAbove, colIndexModifiedLeft);
                    //        //this.Print();
                    //        //if (rowIndexModifiedBelow >= 0 && rowIndexModifiedBelow <= this.highestRow
                    //        //    && colIndexModified >= 0 && colIndexModified <= this.highestColumn) {

                    //        //    if (this.map[rowIndexModifiedBelow, colIndexModified] == '.') {
                    //        //        this.map[rowIndexModifiedBelow, colIndexModified] = '#';
                    //        //    }
                    //        //}
                    //    }


                }
            }

            //this.Print();
        }
    

        public void Print() {

            if (this.goldenRow != -1) {
                Console.WriteLine("too big, can't print.");
                Console.WriteLine("");
                return;
            }

            //long absoluteWidth = this.highestColumn - this.lowestColumn + 1;
            //long absoluteHeight = this.highestRow - this.lowestRow + 1;

            //for (int row = 0; row < absoluteHeight; row++) {
            //    for (int col = 0; col < absoluteWidth; col++) {
            //        Console.Write(this.map[row, col]);
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
        }

        public long QuestionOne() {

            long countOfCoveredAreas = 0;

            //Optimize: just count the keys, and subtract how many beacons i know are on that row.

            foreach (long key in this.goldenRowDictionary.Keys) {

                if (goldenRowDictionary[key] == '#' || goldenRowDictionary[key] == 'S') {
                    countOfCoveredAreas++;
                }
            }

            return countOfCoveredAreas;
        }

        public long QuestionTwo() {

            long countOfCoveredAreas = 0;
      
            //Optimize: just count the keys, and subtract how many beacons i know are on that row.

            foreach (long key in this.goldenRowDictionary.Keys) {

                if (goldenRowDictionary[key] == '#' || goldenRowDictionary[key] == 'S') {
                    countOfCoveredAreas++;
                }
            }

            return countOfCoveredAreas;
        }

    }
}
