using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {

    internal class LocationLists {

        int[] list1 = null;
        int[] list2 = null;
        int[] list1_sorted = null;
        int[] list2_sorted = null;
        int[] distance = null;
        int[] similarity = null;
        Dictionary<int, int> list2_count = new Dictionary<int, int>();

        public LocationLists(string[] lines) {

            this.list1 = new int[lines.Length];
            this.list2 = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++) {
                string[] chars = lines[i].Split(' ');
                list1[i] = Convert.ToInt32(chars[0]);
                list2[i] = Convert.ToInt32(chars[3]);
            }

            // initialize them.
            this.list1_sorted = new int[list1.Length];
            this.list2_sorted = new int[list2.Length];
            this.distance = new int[list1.Length];
            this.similarity = new int[list1.Length];

            // copy them.
            Array.Copy(this.list1, this.list1_sorted, list1.Length);
            Array.Copy(this.list2, this.list2_sorted, list2.Length);

            // sort them;
            Array.Sort(this.list1_sorted);
            Array.Sort(this.list2_sorted);

            // Question One: Calculate the list of differences between the pairs.
            for (int i = 0; i < list1_sorted.Length; i++) {

                this.distance[i] = Math.Abs(list1_sorted[i] - list2_sorted[i]);
            }

            // Question Two: Calculate the list of similarities between the pairs.
            for (int i = 0; i < this.list1.Length; i++) {

                int current = this.list1[i];

                if (!(this.list2_count.ContainsKey(current))) {

                    int count = 0;
                    for (int j = 0; j < list2.Length; j++) {
                        if (list2[j] == current) {
                            count++;
                        }
                    }

                    this.list2_count[current] = (count);
                }
            }
        }


        public int SimilaritySum() {

            int sum = 0;

            foreach (int i in this.list1) {
                sum += (i * list2_count[i]);
            }

            return sum;
        }

        public int DistanceSum() {

            int sum = 0;

            foreach (int value in this.distance) {
                sum += value;
            }

            return sum;
        }
    }
}
