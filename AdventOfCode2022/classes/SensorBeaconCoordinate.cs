using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2022 {

    public class SensorBeaconCoordinate {

        public long row;
        public long col;
        public SensorBeaconCoordinate closestBeacon;

        public SensorBeaconCoordinate(string coordinateRaw, SensorBeaconCoordinate closestBeacon) {

            // example: "x=2, y=18"

            string[] parts = coordinateRaw.Split(", ");
            this.col = long.Parse(parts[0].Substring(2));
            this.row = long.Parse(parts[1].Substring(2));

            this.closestBeacon = closestBeacon;
        }

        public long DistanceToBeacon() {

            if (this.closestBeacon == null) {
                throw new Exception();
            }

            try {
                long height = Math.Abs(this.row - this.closestBeacon.row);
                long width = Math.Abs(this.col - this.closestBeacon.col);
                return (height + width);
            }
            catch (OverflowException e) {
                int x = 5;
                return -1;
            }
        }

        //    public Collection<SensorBeaconCoordinate> NoCoverageZone(long minValue, long MaxValue) {

        //        Collection<SensorBeaconCoordinate> blindZone = new Collection<SensorBeaconCoordinate>();

        //        long sensorRange = this.DistanceToBeacon();
        //        long xLowestRange = this.col - sensorRange;
        //        long xHighestRange = this.col + sensorRange;


        //        long sensorDistance = Math.Abs(this.row - this.goldenRow);
        //        if (sensorRange >= sensorDistance) {


        //            if (this.)

        //    }
        //}
    }
}
