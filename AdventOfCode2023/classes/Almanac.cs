using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023 {



    internal class Almanac {

        // Format:
        // seeds: 79 14 55 13
        //
        // seed-to-soil map:
        // 50 98 2
        // 52 50 48

        string[] seeds;
        Collection<string> seedsToSoilMap;
        Collection<string> soilToFertilizerMap;
        Collection<string> fertilizerToWaterMap;
        Collection<string> waterToLightMap;
        Collection<string> lightToTemperatureMap;
        Collection<string> temperatureToHumidityMap;
        Collection<string> humidityToLocationMap;

        public Almanac(string[] lines) {

            // Find seeds list.
            this.seeds = lines[0].Substring(7).Split(' ');

            // Find seed-to-soil list.
            for (int i = 2; i < lines.Length; i++) {

                switch (lines[i]) {

                    case "seed-to-soil map:":
                        this.seedsToSoilMap = this.ImportMap(lines, i);
                        break;

                    case "soil-to-fertilizer map:":
                        this.soilToFertilizerMap = this.ImportMap(lines, i);
                        break;

                    case "fertilizer-to-water map:":
                        this.fertilizerToWaterMap = this.ImportMap(lines, i);

                        break;

                    case "water-to-light map:":
                        this.waterToLightMap = this.ImportMap(lines, i);

                        break;

                    case "light-to-temperature map:":
                        this.lightToTemperatureMap = this.ImportMap(lines, i);

                        break;

                    case "temperature-to-humidity map:":
                        this.temperatureToHumidityMap = this.ImportMap(lines, i);

                        break;

                    case "humidity-to-location map:":
                        this.humidityToLocationMap = this.ImportMap(lines, i);

                        break;
                }





                //if (lines[i] == "seed-to-soil map:") {
                //    int j = 1;
                //    while (lines[i + j] != "") {
                //        this.seedsToSoilMap.Add(lines[i + j]);
                //        j++;
                //    }
                //}
                //else if (lines[i] == "soil-to-fertilizer map:") {
                //    int j = 1;
                //    while (lines[i + j] != "") {
                //        this.soilToFertilizerMap.Add(lines[i + j]);
                //        j++;
                //    }

                //}
            }



        }


        private Collection<string> ImportMap(string[] lines, int currentLine) {  //}, string headerToFind) {

            Collection<string> output = new Collection<string>();
            int j = 1;

            while ((lines.Length > (currentLine + j)) && lines[currentLine + j] != "") {
                output.Add(lines[currentLine + j]);
                j++;
            }

            return output;
        }


        public long GetLowestLocationFromInitialSeeds(bool fSeedsAsRange = false) {

            long lowestLocation = int.MaxValue;
            Collection<string> seedsToUse = new Collection<string>();

            if (!fSeedsAsRange) {
                foreach (string seed in this.seeds) {
                    seedsToUse.Add(seed);
                }
            }
            else {
                //Console.WriteLine("ERROR!");

                for (int i=0; i<this.seeds.Length; i=i+2) {

                    long start = Convert.ToInt64(this.seeds[i + 0]);
                    long length = Convert.ToInt64(this.seeds[i + 1]);

                    for (int j=0; j<length; j++) {
                        seedsToUse.Add((start + j).ToString());
                    }
                }
                Console.Write("seeds: ");
                for (int i=0; i<seedsToUse.Count; i++) {
                    Console.Write(seedsToUse[i] + ", ");
                }
                Console.WriteLine("");
                Console.WriteLine("seed count: " + seedsToUse.Count);
            }

            int count = 0;

            foreach (string seed in seedsToUse) {

                Console.WriteLine("Processing seed " + ++count + ": ...");


                long soil = this.SourceToDestination(Convert.ToInt64(seed), this.seedsToSoilMap);
                long fertilizer = this.SourceToDestination(soil, this.soilToFertilizerMap);
                long water = this.SourceToDestination(fertilizer, this.fertilizerToWaterMap);
                long light = this.SourceToDestination(water, this.waterToLightMap);
                long temperature = this.SourceToDestination(light, this.lightToTemperatureMap);
                long humidity = this.SourceToDestination(temperature, this.temperatureToHumidityMap);
                long location = this.SourceToDestination(humidity, this.humidityToLocationMap);

                if (location < lowestLocation) { lowestLocation = location; }

            }

            return lowestLocation;
        }

        public void print() {

            // Hack: // Find seeds list.
            //string[] seeds = this.lines[0].Substring(7).Split(' ');


            foreach (string seed in this.seeds) {

                long soil = this.SourceToDestination(Convert.ToInt64(seed), this.seedsToSoilMap);
                long fertilizer = this.SourceToDestination(soil, this.soilToFertilizerMap);
                long water = this.SourceToDestination(fertilizer, this.fertilizerToWaterMap);
                long light = this.SourceToDestination(water, this.waterToLightMap);
                long temperature = this.SourceToDestination(light, this.lightToTemperatureMap);
                long humidity = this.SourceToDestination(temperature, this.temperatureToHumidityMap);
                long location = this.SourceToDestination(humidity, this.humidityToLocationMap);

                Console.Write("seed " + seed);
                Console.Write(" -> soil " + soil.ToString());
                Console.Write(" -> fertilizer " + fertilizer.ToString());
                Console.Write(" -> water " + water.ToString());
                Console.Write(" -> light " + light.ToString());
                Console.Write(" -> temperature " + temperature.ToString());
                Console.Write(" -> humidity " + humidity.ToString());
                Console.Write(" -> location " + location.ToString());

                Console.WriteLine();
            }
        }

        long SourceToDestination(long sourceId, Collection<string> map) {

            foreach (string rule in map) {

                string[] parts = rule.Split(' ');
                long destRangeStart = Convert.ToInt64(parts[0]);
                long sourceRangeStart = Convert.ToInt64(parts[1]);
                long rangeLength = Convert.ToInt64(parts[2]);
                long modifier = -(sourceRangeStart - destRangeStart);

                // Does this rule apply?
                if (sourceId >= sourceRangeStart && (sourceId <= sourceRangeStart + rangeLength)) {
                    return (sourceId + modifier);
                }
            }

            // if no rules apply, return the same number.
            return sourceId;
        }

    }
}

