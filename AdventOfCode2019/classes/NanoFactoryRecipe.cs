using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {

    public class NanoFactoryRecipe {

        public NanoFactoryIngredient output;
        public List<NanoFactoryIngredient> inputs = new List<NanoFactoryIngredient>();

        public NanoFactoryRecipe(string recipe) {

            this.ParseRecipe(recipe);

            //1 A, 2 B, 3 C => 2 D
           


        }

        public void ParseRecipe(string recipe) {

            // 1 A, 2 B, 3 C => 2 D
            string[] parts = recipe.Split("=>");
            string partsInput = parts[0];
            string partsOutput = parts[1];

            // output
            string[] partsOutput2 = partsOutput.Trim().Split(' ');
            this.output = new NanoFactoryIngredient(partsOutput2[1], Convert.ToInt32(partsOutput2[0]));

            // input
            string[] partsInput2 = partsInput.Split(',');
            foreach (string partsInput3 in partsInput2) {
                string[] partsInput4 = partsInput3.Trim().Split(' ');
                this.inputs.Add(new NanoFactoryIngredient(partsInput4[1], Convert.ToInt32(partsInput4[0])));
            }
        }

        public void PrintOut() {

            string returnVal = output.amount + " " + output.name + ": ";
            foreach (NanoFactoryIngredient input in this.inputs) {
                returnVal += input.amount + " " + input.name + ", ";
            }

            Console.WriteLine(returnVal);
        }
    }
}
