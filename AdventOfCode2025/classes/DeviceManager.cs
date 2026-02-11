using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2025 {

    internal class DeviceManager {

        public List<Device> devices = new List<Device>();
        public Device start = null;
        public Device dac = null;
        public Device fft = null;

        public DeviceManager(string[] input, string firstNodeName) {

            // initialize device list.
            foreach (string inputItem in input) {

                Device device = new Device(inputItem, this);
                this.devices.Add(device);

                // Find our special named ones.
                if (device.name == firstNodeName) {
                    this.start = device;
                }
                else if (device.name == "dac") {
                    this.dac = device;
                }
                else if (device.name == "fft") {
                    this.fft = device;
                }
            }

            // Add the final one, 'out'.
            this.devices.Add(new Device("out: ", this));

            if (this.start == null) {
                Debug.Assert(false, "failed to find start node.");
            }

            // Let's go in and fix up the device output lists.
            foreach (Device device in this.devices) {
                foreach (string output in device.outputs) {
                    Device deviceOutput = this.devices.Find(x => x.name == output);
                    if (deviceOutput == null) { int x = 5; }
                    device.outputDevices.Add(deviceOutput);
                }
            }
        }

        public void Print() {

            foreach (Device device in this.devices) {
                device.Print();
            }
        }

        public long FindPaths() {

            return this.start.FindPaths(new List<Device>(), "out");
        }

        private void ResetAllCachedValues() {

            foreach (Device device in this.devices) {
                device.value = -1;
            }
        }

        public long FindPaths2() {

            // If you don't have the important nodes, bail.
            if (this.start == null || this.dac == null || this.fft == null) {
                Console.WriteLine("Missing an important node for part two, bailing.");
                return -1;
            }

            // start - dac - fft - end
            long countStartToDac = this.start.FindPaths(new List<Device>(), "dac");
            this.ResetAllCachedValues();
            long countDacToFft = this.dac.FindPaths(new List<Device>(), "fft");
            this.ResetAllCachedValues();
            long countFftToEnd = this.fft.FindPaths(new List<Device>(), "out");
            this.ResetAllCachedValues();
            long countStartToDacToFftToEnd = countStartToDac * countDacToFft * countFftToEnd;

            long countStartToFft = this.start.FindPaths(new List<Device>(), "fft");
            this.ResetAllCachedValues();
            long countFftToDac = this.fft.FindPaths(new List<Device>(), "dac");
            this.ResetAllCachedValues();
            long countDacToEnd = this.dac.FindPaths(new List<Device>(), "out");
            this.ResetAllCachedValues();
            long countStartToFftToDacToEnd = countStartToFft * countFftToDac * countDacToEnd;

            return countStartToDacToFftToEnd + countStartToFftToDacToEnd;
        }
    }
}
