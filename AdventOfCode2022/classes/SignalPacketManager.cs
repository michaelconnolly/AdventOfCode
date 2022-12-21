using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2022 {
    public class SignalPacketManager {

        Collection<SignalPacketPair> signalPacketPairs = new Collection<SignalPacketPair>();

        public SignalPacketManager(string[] lines) {

            int packetPairCount = (lines.Length + 1) / 3;
            for (int i = 0; i < packetPairCount; i++) {

                string packetPair1 = lines[(i * 3) + 0];
                string packetPair2 = lines[(i * 3) + 1];
                this.signalPacketPairs.Add(new SignalPacketPair(packetPair1, packetPair2));
            }



        }

        public int QuestionOne() {

            int total = 0;

            // Walk through each pair, and if in right order, take it's index (+1) and add to total.
            for (int i = 0; i < this.signalPacketPairs.Count; i++) {

                ComparisonResult result = this.signalPacketPairs[i].IsInRightOrder();

                if (result == ComparisonResult.RightOrder) {
                    total = total + (i + 1);
                }

                // Debug Test.
                if (result == ComparisonResult.NotYetClear) {
                    // does any of the data get us to here?
                    int x = 5;
                }

            }

            return total;
        }

        public void Print() {

            Console.WriteLine("count of pairs: " + this.signalPacketPairs.Count);
            for (int i = 0; i < this.signalPacketPairs.Count; i++) {
                this.signalPacketPairs[i].Print(i + 1);
            }
        }

    }
}
