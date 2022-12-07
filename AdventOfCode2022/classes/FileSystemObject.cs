using System.Collections.ObjectModel;

namespace AdventOfCode2022 {

    internal class FileSystemObject {

        private long _size = 0;
        public bool isDirectory;
        public string name;
        public Collection<FileSystemObject> children;
        public FileSystemObject parent;

        public long size {
            get {
                if (!this.isDirectory) return _size;

                long output = 0;
                foreach (FileSystemObject child in children) {
                    output += child.size;
                }
                return output;
            }
            set {
                this._size = value;
            }
        }

        public FileSystemObject(bool root=false) {

            this.children = new Collection<FileSystemObject>();

            if (root) {
                this.isDirectory = true;

                this.name = "";
                this.parent = null;
            }
        }

        public FileSystemObject createChildDir(string newName) {

            FileSystemObject child = new FileSystemObject();
            child.parent = this;
            child.isDirectory = true;
            child.name = newName;
            this.children.Add(child);

            return child;
        }

        public FileSystemObject createChildFile(string newName, string size) {

            FileSystemObject child = new FileSystemObject();
            child.parent = this;
            child.isDirectory = false;
            child.size = long.Parse(size);
            child.name = newName;
            this.children.Add(child);

            return child;
        }

        public FileSystemObject createChildObject(string input1, string input2) {

            if (input1 == "dir") {
                return this.createChildDir(input2);
            };

            return this.createChildFile(input2, input1);
        }

        public Collection<FileSystemObject> BiggestFolders() {

            Collection<FileSystemObject> biggestFolders = new Collection<FileSystemObject>();

            // if this is a file, return an empty list.
            if (!this.isDirectory) return biggestFolders;

            foreach (FileSystemObject child in this.children) {

                Collection<FileSystemObject> biggestChildFolders = child.BiggestFolders();

                foreach (FileSystemObject bigChildFolder in biggestChildFolders) {
                    biggestFolders.Add(bigChildFolder);
                }
            }

            if (this.size <= 100000) biggestFolders.Add(this);

            return biggestFolders;
        }

        public FileSystemObject BestFolderToDelete(long spaceNeeded) {

            // if this is a file, return an empty list.
            if (!this.isDirectory) return null;

            // If this folder is too small, no need to investigate further.
            if (this.size < spaceNeeded) return null;

            FileSystemObject bestFolder = this;
           
            foreach (FileSystemObject child in this.children) {

                FileSystemObject bestChildFolder = child.BestFolderToDelete(spaceNeeded);
                if ((bestChildFolder != null) && (bestChildFolder.size > spaceNeeded)) {

                    long bestDelta = bestFolder.size - spaceNeeded;
                    long childDelta = bestChildFolder.size - spaceNeeded;
                    if (childDelta < bestDelta) bestFolder = bestChildFolder;

                }
            }

            return bestFolder;
        }
    }
} 