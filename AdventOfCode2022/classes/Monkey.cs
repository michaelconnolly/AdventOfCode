using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2022 {

    public class Monkey {

        private MonkeyManager monkeyManager;
        public int id;
        public Collection<MonkeyItem> monkeyItems = new Collection<MonkeyItem>();
        public string factor1;
        public string factorOperator;
        public string factor2;
        public string testOperator;
        public long testFactor;
        public int ifTrueMonkeyThrow;
        public int ifFalseMonkeyThrow;
        public int inspectionCount;

        public Monkey(MonkeyManager monkeyManager, int id, string[] startItems, string[] operationParts, string[] testParts,
            int destinationIfTrue, int destinationIfFalse) {

            this.monkeyManager = monkeyManager;
            this.inspectionCount = 0;

            this.id = id;

            foreach (string startItem in startItems) {
                this.monkeyItems.Add(new MonkeyItem(long.Parse(startItem)));
            }

            this.factor1 = operationParts[0];
            this.factorOperator = operationParts[1];
            this.factor2 = operationParts[2];

            this.testFactor = long.Parse(testParts[2]);
            this.testOperator = testParts[0];

            this.ifTrueMonkeyThrow = destinationIfTrue;
            this.ifFalseMonkeyThrow = destinationIfFalse;

            //string[] operationParts = input[i + 2].Substring(17).Split(" ");
            //string[] testParts = input[i + 3].Substring(8).Split(" ");
            //int destinationIfTrue = int.Parse(input[i + 4].Substring(30));
            //int destinationIfFalse = int.Parse(input[i + 5].Substring(31));
        }


        public void Print() {

            Console.Write("monkey " + this.id + ": inspection count = " + this.inspectionCount + ", items: ");
            foreach (MonkeyItem item in this.monkeyItems) {
                Console.Write(item.worryLevel + ", ");
            }
            Console.WriteLine("");
        }

        public void Catch(MonkeyItem monkeyItem) {

            this.monkeyItems.Add(monkeyItem);
        }

        private long InspectValue(string value, long oldValue) {

            switch (value) {

                case "old":
                    return oldValue;

                default:
                    return long.Parse(value);
            }
        }

        public void Inspect(MonkeyItem monkeyItem, bool TheSecondWay) {

            // Which way am i doing my math?
            //long worryLevel = (TheSecondWay ? monkeyItem.worryLevel2 : monkeyItem.worryLevel);
            long worryLevel = monkeyItem.worryLevel;

            long factor1 = this.InspectValue(this.factor1, worryLevel);
            long factor2 = this.InspectValue(this.factor2, worryLevel);
            long newValue; 
       
            switch (this.factorOperator) {

                case "*":

                    long factor1remainder = factor1 % testFactor;
                    long factor2remainder = factor2 % testFactor;
               
                    if (TheSecondWay) {
                        //newValue = (factor1remainder * factor2) % testFactor;
                        newValue = (factor1remainder * factor2) + factor2remainder;
                        newValue = (factor1remainder + factor2remainder) + testFactor;

                    }
                    else {
                        try {
                            newValue = checked(factor1 * factor2);
                        }
                        catch (OverflowException e) {
                            throw new Exception();
                        }
                    }

                    break;

                case "+":

                    try {
                        //newValue = checked(BigInteger.Add(factor1, factor2));
                        newValue = checked(factor1 + factor2);
                        //newValue2 = checked(factor1_2 + factor2_2);
                        break;
                    }
                    catch (OverflowException e) {
                        throw new Exception();
                    }


                default:
                    throw new Exception();

            }


          
            monkeyItem.worryLevel = newValue;
           // monkeyItem.worryLevel2 = newValue2;

            return;
        }


        public bool Test(MonkeyItem monkeyItem, bool TheSecondWay) {

            // I only know how to do one thing.
            if (this.testOperator != "divisible") throw new Exception();

            // Which way am i doing my math?
            //long worryLevel = (TheSecondWay ? monkeyItem.worryLevel2 : monkeyItem.worryLevel);
            long worryLevel = monkeyItem.worryLevel;

            long remainder = worryLevel % this.testFactor;

            // Test Code.
            //long remainder2 = monkeyItem.worryLevel2 % this.testFactor;
            //if (remainder != remainder2) {
            //    int x = 5;
            //}

            return (remainder == 0);
        }



        public void InspectTestThrow(bool theSecondWay) {

            foreach (MonkeyItem monkeyItem in this.monkeyItems) {

                // Inspection stage.
                //monkeyItem.worryLevel = this.Inspect(monkeyItem);
                this.Inspect(monkeyItem, theSecondWay);
                this.inspectionCount++;

                // Boredom and Relief stage.
                if (!theSecondWay) {
                    decimal divideByThree = (monkeyItem.worryLevel) / 3;
                    long divideByThreeEven = System.Convert.ToInt32(Math.Ceiling((decimal)divideByThree));
                    monkeyItem.worryLevel = divideByThreeEven;
                   // monkeyItem.worryLevel2 = divideByThreeEven;
                }

                // Test and Throw stage.
                if (this.Test(monkeyItem, theSecondWay)) {
                    Monkey monkey = (Monkey)this.monkeyManager.monkeys[this.ifTrueMonkeyThrow];
                    monkey.Catch(monkeyItem);
                }
                else {
                    Monkey monkey = (Monkey)this.monkeyManager.monkeys[this.ifFalseMonkeyThrow];
                    monkey.Catch(monkeyItem);
                }
            }

            this.monkeyItems.Clear();
        }
    }
}
