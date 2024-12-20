using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AdventOfCode2024 {


    public enum HikingDirection {

        Up, Down, Left, Right
    }


    internal class HikingGuide {

        string[] map;
        List<HikingLocation> trailHeads = new List<HikingLocation>();
        List<HikingLocation> trailEnds = new List<HikingLocation>();

        public HikingGuide(string[] input) {

            this.map = input;

            // Calculate trail heads and ends.
            for (int row = 0; row < map.Length; row++) {
                for (int col = 0; col < map[0].Length; col++) {
                    if (map[row][col] == '0') {
                        this.trailHeads.Add(new HikingLocation(new Coordinate(row, col)));
                    }
                    else if (map[row][col] == '9') {
                        this.trailEnds.Add(new HikingLocation(new Coordinate(row, col)));
                    }
                }
            }

            // Score trail heads.
            this.ScoreTrailHeads();
        }

        public void Print() {

            for (int row = 0; row < map.Length; row++) {
                for (int col = 0; col < map[0].Length; col++) {
                    Console.Write(this.map[row][col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public void ScoreTrailHeads() {

            foreach (HikingLocation trailHead in this.trailHeads) {

                Dictionary<string, int> dictTrailEnds = new Dictionary<string, int>();

                dictTrailEnds = this.FindValidPaths(null, trailHead.coordinate, dictTrailEnds);
                trailHead.score = dictTrailEnds.Count;

                // Figure out how many times we made it to each trail end!
                foreach (string key in dictTrailEnds.Keys) {
                    trailHead.score2 += dictTrailEnds[key];
                }
            }
        }

        private string CreateKey(Coordinate coord) {

            return (coord.row.ToString() + ',' + coord.col.ToString());
        }

        public Coordinate GetNeighbor(Coordinate start, HikingDirection direction) {

            switch (direction) {
                case HikingDirection.Up:
                    return new Coordinate(start.row - 1, start.col);
                case HikingDirection.Down:
                    return new Coordinate(start.row + 1, start.col);
                case HikingDirection.Left:
                    return new Coordinate(start.row, start.col - 1);
                case HikingDirection.Right:
                    return new Coordinate(start.row, start.col + 1);
                default:
                    Debug.Assert(false, "should not have gotten here.");
                    return new Coordinate(start.row, start.col);
            }
        }

        private bool LegalCell(Coordinate cell) {

            int row = cell.row;
            int col = cell.col;

            return (row >= 0 && row < this.map.Length &&
                col >= 0 && col < this.map[0].Length);
        }

        public Dictionary<string,int> FindValidPaths(Coordinate start, Coordinate end, Dictionary<string, int> dictIn) { //Coordinate end) {

            // copy that stuff.
            Dictionary<string, int> dictOut = new Dictionary<string, int>();
            foreach (string key in dictIn.Keys) {
                dictOut[key] = dictIn[key];
            }

            // The destination is not a legal cell.
            if (!(LegalCell(end))) {
                return dictOut;
            }

            // bail if bad destination;
            if (end == null) {
                Debug.Assert(false, "should not have gotten here.");
                return dictOut;
            }

            // i hit a dead end.
            if (this.map[end.row][end.col] == '.') {
                return dictOut;
            }

            // If the new level isn't one step value higher, than bail.
            int startLevel;
            bool youPassMyTest = false;
            int endLevel = Int32.Parse(this.map[end.row][end.col].ToString());

            // is this a trailhead?
            if (start == null) {
                Debug.Assert(endLevel == 0, "We should not have gotten here.");
                youPassMyTest = true;
            }
            else {
                startLevel = Int32.Parse(this.map[start.row][start.col].ToString());
                youPassMyTest = (endLevel == (startLevel + 1));
            }

            if (!youPassMyTest) {
                return dictOut;
            }

            // If we made it to 9, then add to list and go home.
            if (endLevel == 9) {

                string key = CreateKey(end);

                if (dictOut.ContainsKey(key)) {
                    dictOut[key] += 1;
                }
                else {
                    dictOut[key] = 1;
                }
                return dictOut;
            }

            Coordinate neighborUp = this.GetNeighbor(end, HikingDirection.Up);
            Coordinate neighborDown = this.GetNeighbor(end, HikingDirection.Down);
            Coordinate neighborLeft = this.GetNeighbor(end, HikingDirection.Left);
            Coordinate neighborRight = this.GetNeighbor(end, HikingDirection.Right);

            dictOut = FindValidPaths(end, neighborUp, dictOut);
            dictOut = FindValidPaths(end, neighborDown, dictOut);
            dictOut = FindValidPaths(end, neighborLeft, dictOut);
            dictOut = FindValidPaths(end, neighborRight, dictOut);

            return dictOut;
        }


        public int TrailHeadCount() {
            return this.trailHeads.Count;
        }

        public int TrailEndCount() {
            return this.trailEnds.Count;
        }

        public int SumOfAllTrailHeadScores() {

            int score = 0;

            foreach (HikingLocation trailhead in this.trailHeads) {
                score += trailhead.score;
            }

            return score;
        }

        public int SumOfAllTrailHeadScores2() {

            int score2 = 0;

            foreach (HikingLocation trailhead in this.trailHeads) {
                score2 += trailhead.score2;
            }

            return score2;
        }
    }
}

