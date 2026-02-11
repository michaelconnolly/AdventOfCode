using System;
using System.Collections.Generic;

namespace AdventOfCode2025 {

    internal class Device {

        private DeviceManager deviceManager;
        public List<string> outputs = new List<string>();
        public List<Device> outputDevices = new List<Device>();
        public string name;
        public long value = -1;

        public Device(string input, DeviceManager deviceManager) {

            this.deviceManager = deviceManager; 

            // sample format: "ccc: ddd eee fff"
            string[] parts = input.Split(':');
            this.name = parts[0];

            string[] outputsAsString = parts[1].Substring(1).Split(' ');
            foreach (string output in outputsAsString) {
                if (output.Trim() != "") {
                    outputs.Add(output);
                }
            }
        }

        public void Print() {

            Console.Write(this.name + ": ");
            foreach (string output in this.outputs) {
                Console.Write(output + ", ");
            }
            Console.WriteLine();
        }

        public long FindPaths2(List<Device> history, string outString) {

            long successfulPaths = 0;

            // do we have a cache for this?
            if (this.value != -1) {
                return this.value;
            }

            // Prepare a new history stack, if necessary.
            List<Device> newHistory = new List<Device>();
            foreach (Device historyDevice in history) {
                newHistory.Add(historyDevice);
            }
            newHistory.Add(this);

            // For each output, calculate the amount of successful paths out.
            foreach (Device outputDevice in this.outputDevices) {

                if (outputDevice.name == outString) {
                    successfulPaths++;
                }
                else if (history.Contains(outputDevice)) {
                    successfulPaths += 0;
                }
                else {
                    successfulPaths += outputDevice.FindPaths2(newHistory, outString);
                }
            }

            // cache this.
            this.value = successfulPaths;

            return successfulPaths;
        }

        //public int FindPaths(List<Device> history) {

        //    int successfulPaths = 0;

        //    // Prepare a new history stack, if necessary.
        //    List<Device> newHistory = new List<Device>();
        //    foreach (Device historyDevice in history) {
        //        newHistory.Add(historyDevice);
        //    }
        //    newHistory.Add(this);

        //    // For each output, calculate the amount of successful paths out.
        //    foreach (Device outputDevice in this.outputDevices) {

        //        if (outputDevice.name == "out") {
        //            successfulPaths++;
        //        }
        //        else if (history.Contains(outputDevice)) {
        //            successfulPaths += 0;
        //        }
        //        else {
        //            successfulPaths += outputDevice.FindPaths(newHistory);
        //        }
        //    }

        //    return successfulPaths;
        //}
    }
}
