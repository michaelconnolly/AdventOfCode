using System;
using System.Collections.ObjectModel;


namespace AdventOfCode2023 {

    internal enum  WinningHand {

        HighCard = 0,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    internal class CamelCardManager {

        Collection<CamelCardHand> hands = new Collection<CamelCardHand>();

        public CamelCardManager(string[] lines) {

            for (int i = 0; i < lines.Length; i++) {
                this.hands.Add(new CamelCardHand(i, lines[i]));
            }

        }

        public void print() {

            foreach (CamelCardHand hand in hands) {
                hand.print();
            }

        }

        public int MapCardToValue(char c, bool usePartTwoRules) {

            switch (c) {

                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    if (usePartTwoRules) { return 1; }
                    else { return 11; }
                case 'T':
                    return 10;
                default:
                    int value = Convert.ToInt32(c.ToString());
                    return value;
            }
        }

        public bool IsFirstSmallest(CamelCardHand hand1, CamelCardHand hand2, bool usePartTwoRules) {

            if (usePartTwoRules) {
                if (hand1.winningHand2 < hand2.winningHand2) {
                    return true;

                }
                else if (hand2.winningHand2 < hand1.winningHand2) {
                    return false;
                }
            }
            else {

                if (hand1.winningHand < hand2.winningHand) {
                    return true;

                }
                else if (hand2.winningHand < hand1.winningHand) {
                    return false;
                }
            }

            // Same hand, have to compare cards.
            for (int i=0; i<hand1.hand.Length; i++) {

                if (this.MapCardToValue(hand1.hand[i], usePartTwoRules) < this.MapCardToValue(hand2.hand[i], usePartTwoRules)) {
                    return true;
                }
                else if (this.MapCardToValue(hand1.hand[i], usePartTwoRules) > this.MapCardToValue(hand2.hand[i], usePartTwoRules)) {
                    return false;
                }
            }

            Console.WriteLine("ERROR! We should never get here.");
            throw new Exception();
        }

        // Entrypoint.
        public int GetTotalWinnings(bool usePartTwoRules=false) {

            int sum = 0;

            Collection<CamelCardHand> sortedHands = this.sortSmallestFirst(this.hands, usePartTwoRules);
            for (int i = 0; i < sortedHands.Count; i++) {
                sum += (sortedHands[i].bid * (i + 1));
            }

            return sum;
        }

        public Collection<CamelCardHand> sortSmallestFirst(Collection<CamelCardHand> unsorted, bool usePartTwoRules) {

            int size = unsorted.Count;
            CamelCardHand[] sorted = new CamelCardHand[size];

            foreach (CamelCardHand hand in unsorted) {

                for (int sortedIndex=0; sortedIndex<size; sortedIndex++) {
                    
                    // is this slot available?
                    if (sorted[sortedIndex] == null) {
                        sorted[sortedIndex] = hand;
                        break;
                    }

                    // is this slot bigger than me, and should get pushed down?
                    else if (this.IsFirstSmallest(hand, sorted[sortedIndex], usePartTwoRules)) {

                        // push everything down.
                        for (int pushIndex=(size-2); pushIndex>=sortedIndex; pushIndex--) {
                            sorted[pushIndex + 1] = sorted[pushIndex];
                        }
                        sorted[sortedIndex] = hand;
                        break;
                    }
                }
            }
            
            return new Collection<CamelCardHand>(sorted);
        }
    }
}
