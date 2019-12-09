using System;

namespace AdventOfCode2019 {
    public class Image {

        ImageLayer[] layers;
        int imageWidth, imageHeight;

        public Image(string content, int imageWidth, int imageHeight) {

            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;

            // Let's double check that the content length is what we would expect.
            int contentLength = content.Length;
            if (contentLength % (imageWidth * imageHeight) != 0) {
                Console.WriteLine("Bad length of content! Found: " + contentLength.ToString() + "; expected: multiple of " + (imageWidth * imageHeight).ToString());
            }

            int layerCount = (contentLength / (imageWidth * imageHeight));
            layers = new ImageLayer[layerCount];
            for (int layerId = 0; layerId < layerCount; layerId++) {
                string[] imageRows = new string[imageHeight];
                for (int i = 0; i < imageHeight; i++) {
                    int startingPosition = (layerId * imageWidth * imageHeight) + (i * imageWidth);
                    string rowContent = content.Substring(startingPosition, imageWidth);
                    imageRows.SetValue(rowContent, i);
                }
                layers.SetValue(new ImageLayer(imageRows), layerId);
            }

        }

        public ImageLayer WhichImageHasLessOf(char target) {
         
            // Part 1.  Which layer has the the most zero's in it's data?
            ImageLayer layerWithMostZeros = null;
            int lowestCountOfZeros = int.MaxValue;
            int lowestIndex = int.MaxValue;

            for (int i = 0; i < layers.Length; i++) {

                int countOfZeros = layers[i].HowMany(target);

                if (layerWithMostZeros == null || countOfZeros < lowestCountOfZeros) {
                    lowestCountOfZeros = countOfZeros;
                    layerWithMostZeros = layers[i];
                    lowestIndex = i;
                }
            }

            return this.layers[lowestIndex];
        }

        public void PrintOut() {

            // Testing.
            for (int i = 0; i < layers.Length; i++) {
                Console.WriteLine("Layer " + i + ": Zeros: " + layers[i].HowMany('0'));
                layers[i].PrintOut();
            }
        }

        public ImageLayer VisibleLayer() {

            string[] visibleRows = new string[this.imageHeight];

            for (int rowId = 0; rowId < this.imageHeight; rowId++) {

                string visibleRow = "";

                for (int columnId = 0; columnId < this.imageWidth; columnId++) {

                    // 0 - black; 1 - white; 2 - transparent
                    char colorThatShows = '2';
                    for (int layerId = 0; layerId < this.layers.Length; layerId++) {

                        ImageLayer layer = this.layers[layerId];
                        string imageRow = layer.imageRows[rowId];
                        char currentColor = imageRow.ToCharArray()[columnId];

                        if (currentColor == '0' || currentColor == '1') {
                            colorThatShows = currentColor;
                            break;
                        }
                    }

                    visibleRow += colorThatShows;
                }

                visibleRows[rowId] = visibleRow.ToString();
            }

            return new ImageLayer(visibleRows);
        }
    }
}
