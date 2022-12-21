using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventOfCode2022 {
    public class SignalPacketPart {

        public bool isList = false;
        //public object content;
        public int value = -1;
        public Collection<SignalPacketPart> parts = null;

        public SignalPacketPart(bool isList, object content) {

            this.isList = isList;
            if (this.isList) {
                parts = (Collection<SignalPacketPart>)content;
            }
            else {
                value = (int)content;
            }
        }
    }
}
