using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdventOfCode2023 {

    internal class HashLenseBox {

        public Collection<HashLense> lenses = new Collection<HashLense>();
        public int id;

        public HashLenseBox( int id) {
        
            this.id = id;
        }
        public long FocusingPower() {

            // Multiple these three things together:
            // 1) One plus the box number of the lens in question.
            // 2) The slot number of the lens within the box: 1 for the first lens, 2 for the second lens, and so on.
            // 3) The focal length of the lens.

            long sum = 0;

            for (int i = 0; i < lenses.Count; i++) {
                int slotNumber = i + 1;
                sum += (1 + this.id) * slotNumber * (lenses[i].focalLength);
            }

            return sum;
        }
    }
}
