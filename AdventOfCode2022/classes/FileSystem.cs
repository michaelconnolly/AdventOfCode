using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace AdventOfCode2022 {

    internal class FileSystem {

        private string[] initialCommands;
        public FileSystemObject root;

        public FileSystem(string[] lines) {

            this.initialCommands = lines;
            this.root = new FileSystemObject(true);
        }

        private FileSystemObject ParseChangeDir(FileSystemObject current, string dirName) {

            // Hop to root if that is the dir name, or parent if that is requested.
            if (dirName == "/") return this.root;
            if (dirName == "..") return current.parent;

            // if it already exists, move to it.
            foreach (FileSystemObject child in current.children) {
                if (child.name == dirName) { return child; }
            }

            // if it doesn't exist, create it and return it.
            //FileSystemObject newChild = current.createChildDir(dirName);
            //return newChild;
            throw new Exception();
        }


        public void ParseCommands() {

            FileSystemObject current = root;

            // foreach (string command in initialCommands) {
            for (int i = 0; i < initialCommands.Length; i++) {

                string command = this.initialCommands[i];
                string[] parts = command.Split(" ");

                if (parts[0] == "$") {

                    switch (parts[1]) {

                        case "cd":
                            current = this.ParseChangeDir(current, parts[2]);
                            break;

                        case "ls":

                            bool foundNextCommand = false;
                            while (!foundNextCommand) {
                                int nextRow = i + 1;
                                if (nextRow >= (this.initialCommands.Length)) {
                                    foundNextCommand = true;
                                }
                                else {
                                    string[] partsForListing = this.initialCommands[nextRow].Split(" ");
                                    if (partsForListing[0] == "$") {
                                        foundNextCommand = true;
                                    }
                                    else {
                                        current.createChildObject(partsForListing[0], partsForListing[1]);
                                        i++;
                                    }
                                }
                            }


                            break;

                        default:
                            throw new Exception();

                    }
                }
            }
        }
       
        
        // To begin, find all of the directories with a total size of at most 100000, then calculate the sum of their total sizes.In the example
       // above, these directories are a and e; the sum of their total sizes is 95437 (94853 + 584). (As in this example, this process 
       // can count files more than once!)
       // Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?
        public long QuestionOne() {

            long total = 0;
            Collection<FileSystemObject> biggestFolders = root.BiggestFolders();

            foreach (FileSystemObject bigFolder in biggestFolders) {
                total += bigFolder.size;
            }

            return total;
        }

        // The total disk space available to the filesystem is 70000000. To run the update, you need unused space of at least 30000000.
        // You need to find a directory you can delete that will free up enough space to run the update.
        // Find the smallest directory that, if deleted, would free up enough space on the filesystem to run the update.
        // What is the total size of that directory?
        public long QuestionTwo() {

            long diskSpaceAvailable = 70000000 - root.size;
            long diskSpaceNeeded = 30000000 - diskSpaceAvailable;

            FileSystemObject bestFolder = root.BestFolderToDelete(diskSpaceNeeded);
            return bestFolder.size;
        }
    }
}
