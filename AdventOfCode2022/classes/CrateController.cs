using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdventOfCode2022 {

    internal class CrateController {

        Collection<CrateStack> crateStacks = new Collection<CrateStack>();
        Collection<CrateCommand> commands = new Collection<CrateCommand>();

        public CrateController(string[] lines) {

            // find the delimiter.
            int delimiterRow = -1;
            for (int i = 0; i < lines.Length; i++)
                if (lines[i] == "") delimiterRow = i;
            if (delimiterRow == -1) throw new Exception();

            // how many columns?
            int columnCount = int.Parse(lines[delimiterRow - 1].Substring(lines[delimiterRow - 1].Length - 2, 1));

            // create stacks.
            for (int i = 0; i < columnCount; i++) {
                crateStacks.Add(new CrateStack());
            }
            
            // populate stacks.
            int lastCrateRow = delimiterRow - 2;
            for (int row = lastCrateRow; row >= 0; row--) {
                for (int j = 0; j < columnCount; j++) {
                    int currentColumn = (j * 4) + 1;
                    char currentCrate = lines[row][currentColumn];
                    if (currentCrate != ' ') crateStacks[j].Push(currentCrate); 
                }
            }

            // populate list of moves.
            int firstCommandRow = delimiterRow + 1;
            for (int i = firstCommandRow; i < lines.Length; i++) {
                this.commands.Add(new CrateCommand(lines[i]));
            }      
        }

        public void print() {

            Console.WriteLine("column count: " + this.crateStacks.Count);

            // stacks
            for (int i=0; i<this.crateStacks.Count; i++) {
                Console.Write("column " + (i + 1) + ": ");
                this.crateStacks[i].Print();
            }

            // commands.
            for (int i=0; i<this.commands.Count; i++) {
                Console.Write("command " + (i + 1) + ": ");
                this.commands[i].Print();
            }
        }

        public void ExecuteCommands() {

            foreach (CrateCommand command in this.commands) {

                //int count = command.count;
                for (int i = 0; i < command.count; i++) {
                    char crate = this.crateStacks[command.stackStart - 1].Pop();
                    this.crateStacks[command.stackEnd - 1].Push(crate);
                }
            }
        }

        public void ExecuteCommands2() {

            foreach (CrateCommand command in this.commands) {

                char[] crates = this.crateStacks[command.stackStart - 1].Pop(command.count);
                this.crateStacks[command.stackEnd - 1].Push(crates);
            }
        }

        public string TopCrates() {

            string output = "";

            foreach (CrateStack stack in this.crateStacks) {
                output += stack.crates.Last();
            }

            return output;
        }
    }
}
