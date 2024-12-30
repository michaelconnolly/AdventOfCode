using AdventOfCode2024.classes;
using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {


    public enum GardenEdge {
        OffMap,
        Same,
        Different
    }

    public enum GardenDirection {

        Up,
        Down,
        Left,
        Right
    }
  
    internal class GardenManager {

        private List<GardenRegion> regions = new List<GardenRegion>();
        private GardenPlot[,] map;
        public int rowCount = 0;
        public int columnCount = 0;

        public GardenManager(string[] input) {

            this.rowCount = input.Length;
            this.columnCount = input[0].Length;
            this.map = new GardenPlot[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++) {
                for (int col = 0; col < columnCount; col++) {
                    this.map[row, col] = new GardenPlot(new Coordinate(row, col), input[row][col]);
                }
            }

            this.CalculateEdges();
            this.CalculateRegions();
        }

        private GardenPlot GetPlot(Coordinate coord) {

            if (coord == null) return null;

            return this.map[coord.row, coord.col];
        }

        private void MapOutRegion(Coordinate coord, GardenRegion region) {

            // add links back and forth.
            GardenPlot here = GetPlot(coord);

            // Have i already been here?
            if (here.region != null) {
                return;
            }

            region.plots.Add(here);
            here.region = region;

            if (!(here.edgeUp)) {
                this.MapOutRegion(FindNextCell(coord, GardenDirection.Up), region);
            }
            if (!(here.edgeDown)) {
                this.MapOutRegion(FindNextCell(coord, GardenDirection.Down), region);
            }
            if (!(here.edgeLeft)) {
                this.MapOutRegion(FindNextCell(coord, GardenDirection.Left), region);
            }
            if (!(here.edgeRight)) {
                this.MapOutRegion(FindNextCell(coord, GardenDirection.Right), region);
            }
        }


        private void CalculateRegions() {

            for (int row = 0; row < rowCount; row++) {
                for (int col = 0; col < columnCount; col++) {

                    Coordinate coord = new Coordinate(row, col);
                    GardenPlot here = GetPlot(coord);

                    if (here.region == null) {
                        GardenRegion region = new GardenRegion();
                        this.regions.Add(region);
                        this.MapOutRegion(coord, region);
                    }
                }
            }
        }

        private void CalculateEdges() {

            for (int row = 0; row < rowCount; row++) {
                for (int col = 0; col < columnCount; col++) {

                    Coordinate coord = new Coordinate(row, col);
                    GardenPlot here = GetPlot(coord);
                    GardenPlot gardenPlotUp = GetPlot(FindNextCell(coord, GardenDirection.Up));
                    GardenPlot gardenPlotDown = GetPlot(FindNextCell(coord, GardenDirection.Down));
                    GardenPlot gardenPlotLeft = GetPlot(FindNextCell(coord, GardenDirection.Left));
                    GardenPlot gardenPlotRight = GetPlot(FindNextCell(coord, GardenDirection.Right));

                    if (gardenPlotUp != null && (gardenPlotUp.vegetable == here.vegetable)) {
                        here.edgeUp = false;
                    }

                    if (gardenPlotDown != null && (gardenPlotDown.vegetable == here.vegetable)) {
                        here.edgeDown = false;
                    }

                    if (gardenPlotLeft != null && (gardenPlotLeft.vegetable == here.vegetable)) {
                        here.edgeLeft = false;
                    }

                    if (gardenPlotRight != null && (gardenPlotRight.vegetable == here.vegetable)) {
                        here.edgeRight = false;
                    }
                }
            }
        }

        private Coordinate FindNextCell(Coordinate start, GardenDirection direction) {

            int rowMod = 0;
            int colMod = 0;

            switch (direction) {

                case GardenDirection.Up:
                    rowMod = -1;
                    break;
                case GardenDirection.Down:
                    rowMod = 1;
                    break;
                case GardenDirection.Left:
                    colMod = -1;
                    break;
                case GardenDirection.Right:
                    colMod = 1;
                    break;

            }

            int newRow = start.row + rowMod;
            int newCol = start.col + colMod;

            Coordinate end = new Coordinate(newRow, newCol);
            if (!(LegalCell(end))) {
                return null;
            }

            return end;
        }


        private bool LegalCell(Coordinate cell) {

            int row = cell.row;
            int col = cell.col;

            return (row >= 0 && row < this.rowCount &&
                col >= 0 && col < this.columnCount);
        }

        // What is the total price of fencing all regions on your map
        public int TotalPriceOfFencing() {

            int cost = 0;

            foreach (GardenRegion region in this.regions) {
                cost += region.FencingCost();
            }

            return cost;

        }

        public int TotalPriceOfFencing2() {

            int cost = 0;

            foreach (GardenRegion region in this.regions) {
                cost += region.FencingCost2(this);
            }

            return cost;
        }
    }
}
