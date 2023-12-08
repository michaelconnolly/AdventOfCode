using System;
using System.Collections.Generic;

namespace AdventOfCode2023 {

    internal class CamelCardHand {

        int id;
        public string hand;
        public int bid;
        public WinningHand winningHand;
        public WinningHand winningHand2;
  
        public CamelCardHand(int id, string line) {

            string[] parts = line.Split(" ");
            hand = parts[0];
            bid = Convert.ToInt32(parts[1]);

            this.id = id;
            this.winningHand = this.CalculateWinningHand(false);
            this.winningHand2 = this.CalculateWinningHand(true);
        }

        private WinningHand CalculateWinningHand(bool usePartTwoRules=false) {

            Dictionary<char, int> cards = new Dictionary<char, int>();
            int jokerCount = 0;

            // count how many we have of each card.
            foreach (char card in this.hand.ToCharArray()) {
                if (cards.ContainsKey(card)) cards[card]++;
                else cards.Add(card, 1);
            }

            if (usePartTwoRules) {
                if (cards.ContainsKey('J'))
                    jokerCount = cards['J'];
            }

            bool foundThreeOfAKind = false;
            int countOfPairs = 0;
            WinningHand winningHand = WinningHand.HighCard;

            foreach (char card in cards.Keys) {

                // If either of these hit, call it a day and bail.
                if (cards[card] == 5) {
                    winningHand = WinningHand.FiveOfAKind;
                    break;
                }
                else if (cards[card] == 4) {
                    winningHand = WinningHand.FourOfAKind;
                    break;
                }

                if (cards[card] == 3) {
                    foundThreeOfAKind = true;
                }
                if (cards[card] == 2) {
                    countOfPairs++;
                }
            }

            if (foundThreeOfAKind && countOfPairs > 0) {
                winningHand = WinningHand.FullHouse;
            }
            else if (foundThreeOfAKind) {
                winningHand = WinningHand.ThreeOfAKind;
            }
            else if (countOfPairs > 1) {
                winningHand = WinningHand.TwoPair;
            }
            else if (countOfPairs == 1) {
                winningHand = WinningHand.OnePair;
            }
 
            if (jokerCount >0) {

                switch (winningHand) {

                    case WinningHand.FiveOfAKind:
                    case WinningHand.FourOfAKind:
                        return WinningHand.FiveOfAKind;

                    case WinningHand.FullHouse:
                        return WinningHand.FiveOfAKind;

                    case WinningHand.ThreeOfAKind:

                        if (jokerCount == 1) return WinningHand.FourOfAKind;
                        else if (jokerCount == 2) return WinningHand.FiveOfAKind;
                        else return WinningHand.FourOfAKind;

                    case WinningHand.TwoPair:

                        if (jokerCount == 1) return WinningHand.FullHouse;
                        else return WinningHand.FourOfAKind;

                    case WinningHand.OnePair:
                        if (jokerCount == 1) return WinningHand.ThreeOfAKind;
                        else if (jokerCount == 2) return WinningHand.ThreeOfAKind;
                        else return WinningHand.ThreeOfAKind;

                    case WinningHand.HighCard:
                        if (jokerCount == 1) return WinningHand.OnePair;
                        else if (jokerCount == 2) return WinningHand.ThreeOfAKind;
                        else if (jokerCount == 3) return WinningHand.FourOfAKind;
                        else return WinningHand.FiveOfAKind;
                }
            }

            return winningHand;
        }

        public void print() {

            Console.WriteLine("hand " + this.id + ": " + this.hand + "; bid: " + this.bid +
                "; winning hand: " + this.winningHand + "; winning hand 2: " +
                this.winningHand2); 
        }
    }
}
