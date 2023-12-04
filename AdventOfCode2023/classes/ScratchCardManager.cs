using System;
using System.Collections.ObjectModel;

namespace AdventOfCode2023 {
    internal class ScratchCardManager {

        Collection<ScratchCard> scratchCards = new Collection<ScratchCard>();

        public ScratchCardManager(string[] lines) { 
        
            foreach (string line in lines) {
                this.scratchCards.Add(new ScratchCard(line));
            }
        
            this.CalculateCopies();
        }

        public int GetSumOfCardsPoints() {

            int sum = 0;

            foreach (ScratchCard card in this.scratchCards) {
                sum += card.points;
            }

            return sum;
        }

        public int GetSumOfCardsAfterCopying() {

            int sum = 0;

            foreach (ScratchCard card in this.scratchCards) {
                sum += card.copies;
            }
            return sum;
        }

        private void CalculateCopies() {

            for (int i = 0; i < this.scratchCards.Count; i++) {

                // Get the ID for the current card.
                ScratchCard card = this.scratchCards[i];
                int id = card.id;
                if (id != (i + 1)) throw new Exception();

                // Figure out if we need to create copies of downwind cards.
                int countOfDownwindCards = card.matchCount;
                int countOfCopiesPerDownwindCard = card.copies;
                
                if (countOfDownwindCards > 0) {

                    for (int j = 0; j < countOfDownwindCards; j++) {
                        this.scratchCards[j + i + 1].copies += countOfCopiesPerDownwindCard;
                    }
                }
            }
        }
    }
}
