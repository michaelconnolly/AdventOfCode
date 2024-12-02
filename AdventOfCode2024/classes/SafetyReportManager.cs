using System;
using System.Collections.Generic;


namespace AdventOfCode2024 {

    internal class SafetyReportManager {

        List<SafetyReport> SafetyReports = new List<SafetyReport>();

        public SafetyReportManager(string[] lines) {

            foreach (string line in lines) {
                this.SafetyReports.Add(new SafetyReport(line));
            }
        }

        public int SafeCount() {

            int count = 0;

            foreach (SafetyReport report in SafetyReports) {
                if (report.IsSafe(report.levels)) {
                    count++;
                }
            }

            return count;
        }

        public int SafeCountWithDampener() {

            int count = 0;

            foreach (SafetyReport report in SafetyReports) {
                if (report.IsSafeWithDampener()) {
                    count++;
                }
                Console.WriteLine(report.Print());
            }

            return count;
        }
    }
}
