using System;
using System.Collections.Generic;


namespace AdventOfCode2025 {

    internal class ProductIdChecker {

        private List<ProductIdRange> productIdRanges = new List<ProductIdRange>();
      

        public ProductIdChecker(string input) {

            string[] rawRanges = input.Split(',');

            foreach (string rawRange in rawRanges) {
                int separator = rawRange.IndexOf('-');
                long start = long.Parse(rawRange.Substring(0, separator));
                long end = long.Parse(rawRange.Substring(separator + 1));

                productIdRanges.Add(new ProductIdRange(start, end));
            }
        }

        public long GetTotalInvalidIdSum() {
            long total = 0;
            foreach (ProductIdRange range in productIdRanges) {
                total += range.GetInvalidIdSum();
            }
            return total;
        }

        public long GetTotalInvalidId2Sum() {
            long total = 0;
            foreach (ProductIdRange range in productIdRanges) {
                total += range.GetInvalidId2Sum();
            }
            return total;
        }

        public void Print() {

            foreach (ProductIdRange range in productIdRanges) {
                range.Print();
            }
        }
    }
}
