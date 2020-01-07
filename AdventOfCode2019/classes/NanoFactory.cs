using System;
using System.Collections.Generic;


namespace AdventOfCode2019 {


    public class NanoFactory {

        public List<NanoFactoryRecipe> recipes = new List<NanoFactoryRecipe>();

        public NanoFactory(string[] recipesRaw) {

            foreach (string recipeRaw in recipesRaw) {
                recipes.Add(new NanoFactoryRecipe(recipeRaw));
            }
        }

        public NanoFactoryRecipe FindRecipe(string recipeName) {

            foreach (NanoFactoryRecipe recipe in this.recipes) {
                if (recipe.output.name == recipeName) {
                    return recipe;
                }
            }

            return null;
        }

        public bool IsAtomicMaterial(string materialName) {

            return (materialName == "ORE");
        }

        public bool IsAPrimaryMaterial(string materialName, string atomicMaterialName) {

            NanoFactoryRecipe materialRecipe = this.FindRecipe(materialName);

            return (materialName != "ORE" && materialRecipe.inputs.Count == 1 && 
                materialRecipe.inputs[0].name == atomicMaterialName);
        }

        public void AddToCollection(string materialName, long amount, Dictionary<string, long> totalIngredientsNeeded) {

            if (amount == 0) return;

            long newValue = totalIngredientsNeeded[materialName] + amount;
            totalIngredientsNeeded[materialName] = newValue;
        }

        public NanoFactoryIngredient RemoveFromCollection(string materialName, long amount,
            Dictionary<string, long> materialStore) {

            //Program.Assert(materialStore.ContainsKey(materialName), "ERROR! tried to remove material we don't have.");
            //Program.Assert((materialStore[materialName] >= amount), "ERROR! tried to remove material we don't have enough of.");

            // If this material isn't in the store at all, bail.
            //if (!(materialStore.ContainsKey(materialName))) {
            //    return new NanoFactoryIngredient(materialName, 0);
            //}

            // Take the right amount, or if you need more, at least take all we have. 
            //int amountToTake = ((amount <= materialStore[materialName]) ? amount : materialStore[materialName]);
            //materialStore[materialName] -= amountToTake;
            materialStore[materialName] -= amount;

            return new NanoFactoryIngredient(materialName, amount);
        }

        private NanoFactoryIngredient CheckToSeeIfIHaveItAlready(string materialName, long amount, 
           Dictionary<string, long> materialStore) {

            // If this material isn't in the store at all, bail.
            //if (!(materialStore.ContainsKey(materialName))) {
            //    return new NanoFactoryIngredient(materialName, 0);
            //}

            // Take the right amount, or if you need more, at least take all we have. 
            long amountWeHave = materialStore[materialName];
            long amountToTake = ((amount <= amountWeHave) ? amount : amountWeHave);

            return new NanoFactoryIngredient(materialName, amountToTake);
        }

        public void PrintCollection(Dictionary<string, long> materialStore, int? oreCost) {

            string returnVal = "\tmaterial-bag: ";
            foreach (string key in materialStore.Keys) {
                returnVal += key + ": " + materialStore[key] + ", ";
            }
            Console.WriteLine(returnVal);

            if (oreCost != null) {
                Console.WriteLine("\tOre Cost: " + oreCost);
            }
        }

        public long ProduceMaterial(string materialName, long amount, Dictionary<string, long> materialBag) {

            // Find the recipe.
            NanoFactoryRecipe recipeMaterial = this.FindRecipe(materialName);

            // Is this already in the leftover ingredients?
            NanoFactoryIngredient ingredientOnHand = this.CheckToSeeIfIHaveItAlready(materialName, amount, materialBag);
            long amountLeftToProduce = (amount - ingredientOnHand.amount);
            if (amountLeftToProduce == 0) {
                return 0;  // Cost zero ORE!
            }

            // If this ingredient is primary, go ahead and make it with ORE.
            if (IsAPrimaryMaterial(materialName, "ORE")) {

                long oreCost = 0;
                long amountOfPrimaryProduced = 0;
                while (amountLeftToProduce > amountOfPrimaryProduced) {
                    //Program.Assert(recipeMaterial.inputs[0].name == "ORE", "ERROR!");
                   // Program.Assert(recipeMaterial.inputs.Count == 1, "ERROR!");
                    oreCost += recipeMaterial.inputs[0].amount;
                    amountOfPrimaryProduced += recipeMaterial.output.amount;
                }

                this.AddToCollection(materialName, amountOfPrimaryProduced, materialBag);
                return oreCost;
            }

            // If we have made it here, we need to produce a non-primary material.
            long totalOreCost = 0;
            int howManyTimesDoRunRecipe = (int)Math.Ceiling((((double)amountLeftToProduce) / (double)recipeMaterial.output.amount));

            foreach (NanoFactoryIngredient inputIngredient in recipeMaterial.inputs) {

                long amountPerRun = inputIngredient.amount;
                long amountNeeded = howManyTimesDoRunRecipe * amountPerRun;
                totalOreCost += this.ProduceMaterial(inputIngredient.name, amountNeeded, materialBag);

                this.RemoveFromCollection(inputIngredient.name, amountNeeded, materialBag);
            }

            // What did we make?
            long amountProduced = (howManyTimesDoRunRecipe * recipeMaterial.output.amount);
            this.AddToCollection(recipeMaterial.output.name, amountProduced, materialBag);

            //Console.WriteLine("Produced " + amountProduced + " " + materialName);

            return totalOreCost;
        }
    }
}
