using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace AdventOfCode2021 {


    public class BingoFactory {


        public Collection<BingoCard> bingoCards = new Collection<BingoCard>();
        public string[] callableNumbers;
        public int heightOfBoard = Int32.MaxValue;
        public int lengthOfInputFile;


        public BingoFactory(string[] bingoData) {

            this.callableNumbers = bingoData[0].Split(',');
            this.lengthOfInputFile = bingoData.Length;

            // Do a pre-scan to determine the size of the bingo card, by looking for a break.
            for (int i = 2; i < lengthOfInputFile; i++) {
                if (bingoData[i] == "") {
                    this.heightOfBoard = i - 2;
                    break;
                }
            }

            // Suck in 'heightOfBoard' amount of rows. Each row is a string, which can be further
            // split into a array of strings.
            for (int i = 2; i < lengthOfInputFile; i += ((heightOfBoard + 1))) { // each card.

                string[,] boardData = new string[heightOfBoard, heightOfBoard];

                for (int j = 0; j < heightOfBoard; j++) { // each row.
                    string oneLine = bingoData[i + j];
                    string[] oneLineAsList = oneLine.Split(' ');
                    oneLineAsList = hackDiscardBlanks(oneLineAsList);
                    for (int k = 0; k < oneLineAsList.Length; k++) { // each cell.
                        boardData[j, k] = oneLineAsList[k];
                    }

                }

                bingoCards.Add(new BingoCard((bingoCards.Count + 1), boardData));
            }

            // Debug check.
            Console.WriteLine("Size of input array: " + this.lengthOfInputFile);
            Console.WriteLine("Number of bing cards: " + this.bingoCards.Count);
        }


        public int countOfOpenCards() {

            int count = 0;

            foreach (BingoCard card in this.bingoCards) {
                if (!(card.won)) {
                    count++;
                }
            }

            return count;
        }


        private string[] hackDiscardBlanks(string[] source) {

            int blankCount = 0;
            foreach (string line in source) {
                if (line == "") {
                    blankCount++;
                }
            }

            if (blankCount > 0) {
                string[] output = new string[source.Length - blankCount];
                int currentIndex = 0;
                foreach (string line in source) {
                    if (line != "") {
                        output[currentIndex] = line;
                        currentIndex++;

                    }
                }
                return output;
            }

            return source;
        }

    }
}
