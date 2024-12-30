using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {


    internal class Stone {

        public long value = 0;
        public List<Stone> substones = new List<Stone>();

        public Stone(long initialValue) {

            this.value = initialValue;
        }

        public void Print() {

            if (this.substones.Count == 0) {
                Console.Write(this.value);
                Console.Write(" ");
            }
            else {
                foreach (Stone stone in this.substones) {
                    stone.Print();
                }
            }
        }

        //public long GetValue() {

        //    if (substones.Count > 0) {
        //        Debug.Assert(false, "we shouldn't have gotten here.");
        //        return -1;
        //    }
        //    else {
        //        return value;
        //    }
        //}

        public long GetCount() {

            if (this.substones.Count == 0) return 1;

            long count = 0;
            foreach (Stone stone in this.substones) {
                count += stone.GetCount();
            }
            return count;
        }

        public bool IsEvenAmountOfDigits() {

            //return (this.value % 2 == 0);
            string valueString = this.value.ToString();
            return (valueString.Length % 2 == 0);
        }

        // If the stone is engraved with a number that has an even number of digits,
        // it is replaced by two stones. The left half of the digits are engraved
        // on the new left stone, and the right half of the digits are engraved on
        // the new right stone. (The new numbers don't keep extra leading zeroes:
        // 1000 would become stones 10 and 0.)
        public long[] SplitValue() {

            string valueString = this.value.ToString();
            long[] output = new long[2];
            int sizeHalf = valueString.Length / 2;
            string val1 = valueString.Substring(0, sizeHalf);
            string val2 = valueString.Substring(sizeHalf);
            long val1_long = long.Parse(val1);
            long val2_long = long.Parse(val2);
            output[0] = val1_long;
            output[1] = val2_long;

            return output;
        }


        public void Blink2(int blinkCount) {



            // We are still storing raw value, not subdivided.
            for (int i = 0; i < blinkCount; i++) {

                // Have we had to subdivide this stone?  
                if (this.substones.Count > 0) {
                    Debug.Assert(this.value == -1);

                    foreach (Stone substone in this.substones) {

                        substone.Blink2(blinkCount - i);
                    }
                    return;
                }


                //Console.WriteLine("blink " + i);


                //for (int stoneIndex = 0; stoneIndex < this.substones.Count; stoneIndex++) {

                //    Stone stone = this.substones[stoneIndex];



                if (this.value == 0) {
                    this.value = 1;
                }
                else if (this.IsEvenAmountOfDigits()) {

                    long[] newValues = this.SplitValue();
                    this.substones.Add(new Stone(newValues[0]));
                    this.substones.Add(new Stone(newValues[1]));
                    this.value = -1;
                    // Stone newStone = new Stone(newValues[0]);
                    //Stone newStone = new Stone(newValues[0]);
                    //this.stones.Insert(stoneIndex, newStone);
                    //  stone.value = newValues[1];
                    //stoneIndex++;
                }
                else {
                    this.value = this.value * 2024;
                }
            }

        }

    }


}

 

