using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdventOfCode2023 {

    internal class CalibrationDocument {

        string[] lines;

        public CalibrationDocument(string[] lines) {
            this.lines = lines; 
        }


        private int GetCalibrationValue(string line, bool includeWrittenOut) {

            Collection<char> numbers = new Collection<char>();
            Collection<string> numbersWrittenOut = new Collection<string>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

            for (int i=0; i < line.Length; i++) { 
                char c = line.ToCharArray()[i];
      
                // do we have an initial match?
                bool match = c.ToString().IndexOfAny("0123456789".ToCharArray()) != -1;
                if (match) {
                    numbers.Add(c);
                }

                else if (includeWrittenOut)
                {
                    // see if we have the written-out version.
                    for (int j = 0; j < numbersWrittenOut.Count; j++) {
                        
                        string numberWrittenOut = numbersWrittenOut[j];

                        if (line.Substring(i, Math.Min(numberWrittenOut.Length, (line.Length - i))) == numberWrittenOut)
                        {
                           // i = i + numberWrittenOut.Length - 1;
                            numbers.Add((j+1).ToString()[0]);
                            break;
                        }
                    }
                }

            }

            if (numbers.Count <1)  {
                Console.WriteLine("ERROR: calibrationLine had less than 1 number.");
                throw new Exception();
            }

            string valString = numbers.First().ToString() + numbers.Last().ToString();
            int valInt = Convert.ToInt32(valString);
            Console.WriteLine(line + " - " + valInt.ToString());

            return valInt;
        }

        public int GetCalibrationValuesTotal(bool includeWrittenOut=false) {

            int total = 0;

            foreach (string line in this.lines)
            {
                int calibrationValue = GetCalibrationValue(line, includeWrittenOut);
                total += calibrationValue;
            }

            return total;
        }
    }
}
