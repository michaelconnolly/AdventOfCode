using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023 {

    internal class SpringMap {

        public Collection<SpringRow> springRows = new Collection<SpringRow>();

        public SpringMap(string[] lines) {

            foreach (var line in lines) {
                this.springRows.Add(new SpringRow(line));
            }
        }

        public void print() {

            foreach (SpringRow springRow in this.springRows) {
                springRow.print();
            }
        }

        public long GetSumOfEachRowsComboOptions() {

            long sum = 0;

          

            return sum;
        }
    }
}
