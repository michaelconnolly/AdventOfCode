using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2022 {

    public enum ComparisonResult {

        RightOrder,
        WrongOrder,
        NotYetClear
    }


    public class SignalPacketPair {

        public SignalPacketPart signalPacket1;
        public SignalPacketPart signalPacket2;

        public SignalPacketPair(string signalPacket1, string signalPacket2) {

            int x = 5;

            if (signalPacket2 == "[[2,1,[[4,3,9,0],9,3,3,[2,5,8]]],[[[8,5,3,3,1],9,[0]]],[[],[[],9,2],[[1,8,9,1],0,10,[6,1]],9,10],[[[3,3,4,9],5],[4,[]],[0,0,6],5],[1]]") {
                int y = 5;
            }

            Collection<SignalPacketPart> parts1 = new Collection<SignalPacketPart>();
            Collection<SignalPacketPart> parts2 = new Collection<SignalPacketPart>();

            signalPacket1 = signalPacket1.Substring(1, signalPacket1.Length - 2);
            signalPacket2 = signalPacket2.Substring(1, signalPacket2.Length - 2);


            this.ProcessList(signalPacket1, parts1);
            this.ProcessList(signalPacket2, parts2);

            this.signalPacket1 = new SignalPacketPart(true, parts1);
            this.signalPacket2 = new SignalPacketPart(true, parts2);


            //  int endBracketIndex1 = this.FindEndBracket(listToProcess);

            //string listRaw = listToProcess.Substring(i + 1, (endBracketIndex - (i + 1)));

            //Collection<SignalPacketPart> subParts = new Collection<SignalPacketPart>();
            //this.ProcessList(listRaw, subParts);



            //this.signalPacket1 = new SignalPacketPart(true, this.ProcessList(signalPacket1));
            //this.signalPacket2 = new SignalPacketPart(true, this.ProcessList(signalPacket2));
        }


        public void Print(int id) {

            if (!(this.signalPacket1.isList)) throw new Exception();
            if (!(this.signalPacket2.isList)) throw new Exception();

            Console.WriteLine("pair id: " + id);
            Console.WriteLine("packet 1 length: " + (this.signalPacket1.parts.Count));
            Console.WriteLine("packet 2 length: " + (this.signalPacket2.parts.Count));
            Console.WriteLine("");

        }

        private int FindEndBracket(string input) {

            // First char better be '[';
            if (input[0] != '[') throw new Exception();
            int startBracketCount = 1;

            for (int i = 1; i < input.Length; i++) {

                if (input[i] == ']') {

                    startBracketCount--;
                    if (startBracketCount == 0) {
                        return i;
                    }
                }
                else if (input[i] == '[') {
                    startBracketCount++;
                }
            }

            // If we got here we are screwed.
            throw new Exception();

        }


        private SignalPacketPart ConvertIntToList(int value) {


            Collection<SignalPacketPart> parts = new Collection<SignalPacketPart>();
            parts.Add(new SignalPacketPart(false, value));

            SignalPacketPart part = new SignalPacketPart(true, parts);

            return part;
        }

        private void ProcessList(string listInput, Collection<SignalPacketPart> parts) {

            //Collection<SignalPacketPart> signalPacketParts = new Collection<SignalPacketPart>();
            //
            int i = 0;
            //Collection<int> openBrackets = new Collection<int>();
            string listToProcess = listInput;
            string currentRawSubstring = "";

            // Assume a set of parts.  Can be empty.  Can contain an integer or a list.
            while (listToProcess != "") {

                char currentChar = listToProcess[i];

                if (currentChar == '[') {

                    //int endBracketIndex = listToProcess.LastIndexOf(']');
                    int endBracketIndex = this.FindEndBracket(listToProcess);

                    string listRaw = listToProcess.Substring(i + 1, (endBracketIndex - (i + 1)));

                    Collection<SignalPacketPart> subParts = new Collection<SignalPacketPart>();
                    this.ProcessList(listRaw, subParts);
                    parts.Add(new SignalPacketPart(true, subParts));

                    if ((listToProcess.Length - 1) == endBracketIndex) listToProcess = "";
                    else listToProcess = listToProcess.Substring(endBracketIndex + 2);

                    i = 0;
                }

                else if ((currentChar == ',') || (i == listToProcess.Length - 1)) {

                    int value;
                    if (currentRawSubstring.Length > 0) {
                        value = int.Parse(currentRawSubstring);
                    }
                    else {
                        value = int.Parse(listToProcess);
                    }
                    parts.Add(new SignalPacketPart(false, value));

                    listToProcess = listToProcess.Substring(i + 1);
                    currentRawSubstring = "";
                    i = 0;
                }

                else {
                    currentRawSubstring += currentChar;
                    i++;
                }

                //char currentChar = 
            }

            return;
        }


        public ComparisonResult IsInRightOrder() {

            return this.IsInRightOrder(this.signalPacket1, this.signalPacket2);
        }

        private ComparisonResult IsInRightOrder(SignalPacketPart packetOne, SignalPacketPart packetTwo) {

            // Better both be lists.
            if (!(packetOne.isList && packetTwo.isList)) throw new Exception();

            // Walk the list.
            for (int i = 0; i < packetOne.parts.Count; i++) {

                // If the right packet runs out of parts, return false.
                if (packetTwo.parts.Count == i) return ComparisonResult.WrongOrder;

                // Are both integers?
                else if (!(packetOne.parts[i].isList) && !(packetTwo.parts[i].isList)) {

                    int value1 = packetOne.parts[i].value;
                    int value2 = packetTwo.parts[i].value;
                    if (value1 > value2) return ComparisonResult.WrongOrder;
                    else if (value1 < value2) return ComparisonResult.RightOrder;
                }

                // Are both lists?
                else if (packetOne.parts[i].isList && packetTwo.parts[i].isList) {

                    ComparisonResult result = this.IsInRightOrder(packetOne.parts[i], packetTwo.parts[i]);
                    if (result != ComparisonResult.NotYetClear) return result;
                }

                else if (packetOne.parts[i].isList) {

                    SignalPacketPart partList = this.ConvertIntToList(packetTwo.parts[i].value);

                    ComparisonResult result = this.IsInRightOrder(packetOne.parts[i], partList);
                    if (result != ComparisonResult.NotYetClear) return result;
                }

                else if (packetTwo.parts[i].isList) {

                    SignalPacketPart partList = this.ConvertIntToList(packetOne.parts[i].value);

                    ComparisonResult result = this.IsInRightOrder(partList, packetTwo.parts[i]);
                    if (result != ComparisonResult.NotYetClear) return result;
                }

                else throw new Exception();
            }

            // If the left packet runs out of parts first, we are in the right order.
            if (packetTwo.parts.Count > packetOne.parts.Count) return ComparisonResult.RightOrder;

            return ComparisonResult.NotYetClear;
        }
    }
}