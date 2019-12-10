using System;

namespace AdventOfCode2019 {

    public class ImageLayer {

        public string[] imageRows;

        public ImageLayer(string[] imageRows) {

            this.imageRows = imageRows;
        }

       public int HowMany(char charToFind) {

            int count = 0;

            foreach (string imageRow in this.imageRows){
                foreach (char imageCell in imageRow.ToCharArray()) {
                    if (imageCell == charToFind) count++;
                }
            }

            return count;
        }

        public void PrintOut() {

            //Console.WriteLine("Layer:");
            foreach (string imageRow in this.imageRows) {
                Console.WriteLine("\t" + imageRow);
            }
        }

        public void PrintOutJustWhite() {

            // 0 - black; 1 - white; 2 - transparent

            foreach (string imageRow in this.imageRows) {
                string justBlackRow = imageRow;
                justBlackRow = justBlackRow.Replace("0", " ");
                justBlackRow = justBlackRow.Replace("1", "X");
                justBlackRow = justBlackRow.Replace("2", " ");
                Console.WriteLine("\t" + justBlackRow);
            }
        }

    }
}
