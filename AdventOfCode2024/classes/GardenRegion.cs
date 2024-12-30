using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AdventOfCode2024.classes {

    internal class GardenRegion {

        public List<GardenPlot> plots = new List<GardenPlot>();

        public GardenRegion() {

        }

        public int CountOfSides(GardenManager gardenManager) {

            int gridsize = gardenManager.rowCount * 2;
            Debug.Assert(gardenManager.rowCount == gardenManager.columnCount);

            // Create my storage for tracking sides.
            // A list of rows, that represent the horizontal lines above and below each line. 
            // What we store: the column id's that have an edge on this row line. 
            List<int>[] hasSideRow = new List<int>[gridsize];
            for(int i = 0; i < gridsize; i++) {
                hasSideRow[i] = new List<int>();
            }
            // A list of cols, that represent the vertical lines left and right of each line. 
            // What we store: the row id's that have an edge on this col line. 
            List<int>[] hasSideCol = new List<int>[gridsize];
            for (int i = 0; i < gridsize; i++) {
                hasSideCol[i] = new List<int>();
            }

            // Map what we know about the included garden plots to 
            // store where lines should be.  
            for (int i = 0; i < this.plots.Count; i ++) { // (GardenPlot plot in plots) {

                GardenPlot plot = this.plots[i];
                int currentRow = plot.coordinate.row * 2;
                int currentCol = plot.coordinate.col * 2;

                if (plot.edgeUp) {
                    hasSideRow[currentRow].Add(plot.coordinate.col);
                }
                if (plot.edgeDown) {
                    hasSideRow[currentRow + 1].Add(plot.coordinate.col);
                }
                if (plot.edgeLeft) {
                    hasSideCol[currentCol].Add(plot.coordinate.row);
                }
                if (plot.edgeRight) {
                    hasSideCol[currentCol + 1].Add(plot.coordinate.row);
                }
            }

            int sideCount = 0;
            int gaps;
            int start;

            // For each List<int> we store
            for (int i = 0; i < hasSideRow.Length; i++) {
                gaps = 0;
                start = -1;
                hasSideRow[i].Sort();
                if (hasSideRow[i].Count != 0) {
                    for (int j = 0; j < hasSideRow[i].Count; j++) {
                        if (start == -1) start = hasSideRow[i][j];
                        else {
                            int end = hasSideRow[i][j];
                            if (end != start + 1) {
                                gaps++;
                            }
                            start = end;
                        }
                    }

                    sideCount += gaps + 1;
                }
            }

            for (int i = 0; i < hasSideCol.Length; i++) {
                gaps = 0;
                start = -1;
                hasSideCol[i].Sort();
                if (hasSideCol[i].Count != 0) {
                    for (int j = 0; j < hasSideCol[i].Count; j++) {
                        if (start == -1) start = hasSideCol[i][j];
                        else {
                            int end = hasSideCol[i][j];
                            if (end != start + 1) {
                                gaps++;
                            }
                            start = end;
                        }
                    }
                    sideCount += gaps + 1;
                }
            }

            return sideCount;
        }

            //the price of fence required for a region is found by multiplying that
            // region's area by its perimeter.
            public int FencingCost() {

            int area = this.plots.Count;
            int perimeter = 0;

            foreach (GardenPlot plot in this.plots) {
                perimeter += plot.CountOfFenceEdges();
            }

            return (area * perimeter);
        }

        //the price of fence required for a region is found by multiplying that
        // region's area by its perimeter.
        public int FencingCost2(GardenManager gardenManager) {

            int area = this.plots.Count;
            int sides = this.CountOfSides(gardenManager);

            return (area * sides);
        }
    }
}