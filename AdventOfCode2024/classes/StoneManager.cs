using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024 {



    internal class StoneManager {

        List<Stone> stones = new List<Stone>();


        public StoneManager(string input) {

            string[] parts = input.Split(' ');
            foreach (string part in parts) {
                long partNumber = long.Parse(part);
                this.stones.Add(new Stone(partNumber));
            }
        }


        public long GetStoneCount() {

            long count = 0;
            foreach (Stone stone in this.stones) {
                count += stone.GetCount();
            }
            return count;

          
        }

        public void Print() {

            foreach (Stone stone in this.stones) {
                stone.Print();
            }

            Console.WriteLine();
        }


   

        public void Blink(int blinkCount) {

            for (int i = 0; i < blinkCount; i++) {

                Console.WriteLine("blink " + i);
               // this.Print();

                for (int stoneIndex = 0; stoneIndex < this.stones.Count; stoneIndex++) {

                    Stone stone = this.stones[stoneIndex];

                    if (stone.value == 0) {
                        stone.value = 1;
                    }
                    else if (stone.IsEvenAmountOfDigits()) {

                        long[] newValues = stone.SplitValue();
                        Stone newStone = new Stone(newValues[0]);
                        this.stones.Insert(stoneIndex, newStone);
                        stone.value = newValues[1];
                        stoneIndex++;
                    }
                    else {
                        stone.value = stone.value * 2024;
                    }
                }

            }

        }


        public void Blink2(int blinkCount) {

          //  for (int i = 0; i < blinkCount; i++) {

                //Console.WriteLine("blink " + i);

                for (int stoneIndex = 0; stoneIndex < this.stones.Count; stoneIndex++) {
                    
                    
                    Stone stone = this.stones[stoneIndex];
                    stone.Blink2(blinkCount);

                    //if (stone.value == 0) {
                    //    stone.value = 1;
                    //}
                    //else if (stone.IsEvenAmountOfDigits()) {

                    //    long[] newValues = stone.SplitValue();
                    //    Stone newStone = new Stone(newValues[0]);
                    //    this.stones.Insert(stoneIndex, newStone);
                    //    stone.value = newValues[1];
                    //    stoneIndex++;
                    //}
                    //else {
                    //    stone.value = stone.value * 2024;
                    //}
                }

           // }

        }

    }
}
