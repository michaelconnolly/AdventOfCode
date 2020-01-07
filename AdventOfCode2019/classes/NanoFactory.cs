using System;
using System.Collections.Generic;
using System.Text;

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

        //public Dictionary<string, int> PrimaryIngredientsRequired(string baseRecipeName, string finalRecipeName, int amountNeeded, Dictionary<string, int> totalIngredientsNeeded) {

        //    int oreRequired = 0;

        //    if (recipeName == "ORE") return amountNeeded;

        //    NanoFactoryRecipe recipe = this.FindRecipe(recipeName);
        //    double multiplier = amountNeeded / recipe.output.amount;
        //    foreach (NanoFactoryIngredient ingredient in recipe.inputs) {

        //        int amount = ingredient.amount;

        //        if (totalIngredientsNeeded.ContainsKey(ingredient.name)) {
        //            int newValue = totalIngredientsNeeded[ingredient.name] + ingredient.amount;
        //            totalIngredientsNeeded[ingredient.name] = newValue;
        //        }
        //        else {
        //            totalIngredientsNeeded[ingredient.name] = ingredient.amount;
        //        }


        //        int baseOreAmount = this.AmountOfOreRequired(ingredient.name, amount, totalIngredientsNeeded);
        //        oreRequired += Convert.ToInt32(Math.Ceiling(baseOreAmount * multiplier));
        //    }

        //    return oreRequired;

        //    //Console.WriteLine("Part 1:");
        //    //recipeFuel.PrintOut();
        //}

        public bool IsAtomicMaterial(string materialName) {

            return (materialName == "ORE");

        //    NanoFactoryRecipe materialRecipe = this.FindRecipe(materialName);

        //    return (materialName != "ORE" && materialRecipe.inputs.Count == 1 &&
        //        materialRecipe.inputs[0].name == atomicMaterialName);
        }

        public bool IsAPrimaryMaterial(string materialName, string atomicMaterialName) {

            NanoFactoryRecipe materialRecipe = this.FindRecipe(materialName);

            return (materialName != "ORE" && materialRecipe.inputs.Count == 1 && 
                materialRecipe.inputs[0].name == atomicMaterialName);
        }

        public void AddToCollection(string materialName, long amount, Dictionary<string, long>totalIngredientsNeeded, string label) {

            if (amount == 0) return;

            if (totalIngredientsNeeded.ContainsKey(materialName)) {
                long newValue = totalIngredientsNeeded[materialName] + amount;
                totalIngredientsNeeded[materialName] = newValue;
            }
            else {
                totalIngredientsNeeded[materialName] = amount;
            }

           //Console.WriteLine(label + ": " + amount + " " + materialName);
        }

        public NanoFactoryIngredient RemoveFromCollection(string materialName, long amount, 
            Dictionary<string, long> materialStore) {

            Program.Assert(materialStore.ContainsKey(materialName), "ERROR! tried to remove material we don't have.");
            Program.Assert((materialStore[materialName] >= amount), "ERROR! tried to remove material we don't have enough of.");


            // If this material isn't in the store at all, bail.
            //if (!(materialStore.ContainsKey(materialName))) {
            //    return new NanoFactoryIngredient(materialName, 0);
            //}

            // Take the right amount, or if you need more, at least take all we have. 
            //int amountToTake = ((amount <= materialStore[materialName]) ? amount : materialStore[materialName]);
            //materialStore[materialName] -= amountToTake;
            materialStore[materialName] -= amount;

            return new NanoFactoryIngredient(materialName, amount);
           // return new NanoFactoryIngredient(materialName, amountToTake);
    }

        private NanoFactoryIngredient CheckToSeeIfIHaveItAlready(string materialName, long amount, 
           Dictionary<string, long> materialStore) {

            // If this material isn't in the store at all, bail.
            if (!(materialStore.ContainsKey(materialName))) {
                return new NanoFactoryIngredient(materialName, 0);
            }

            // Take the right amount, or if you need more, at least take all we have. 
            long amountToTake = ((amount <= materialStore[materialName]) ? amount : materialStore[materialName]);
           // materialStore[materialName] -= amountToTake;

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

        public void MultiplyCollection(Dictionary<string, long> materialStore, long multiplier) {

            string[] keys = new string[materialStore.Keys.Count];
            materialStore.Keys.CopyTo(keys, 0);
            //for (int i=0; i<materialStore.Keys.Count; i++) {
            //    keys[i] = materialStore.Keys.CopyTo(;
            //}

            //string returnVal = "\tmaterial-bag: ";
            foreach (string key in keys) {
                long newValue = materialStore[key] * multiplier;
                materialStore[key] = newValue;

                //returnVal += key + ": " + materialStore[key] + ", ";
            }
            //Console.WriteLine(returnVal);

            //if (oreCost != null) {
            //    Console.WriteLine("\tOre Cost: " + oreCost);
            //}
        }


        //        public int ProduceMaterial(string materialName, int amount, Dictionary<string, int> leftoverIngredients, Dictionary<string,int> produced) {
        public long ProduceMaterial(string materialName, long amount, Dictionary<string, long> materialBag) {

            //int amountProduced;

            // Find the recipe.
            NanoFactoryRecipe recipeMaterial = this.FindRecipe(materialName);

            //int multiplier = (amountToGet / amount);

            // Is this already in the leftover ingredients?
            // int amountOnHand = this.RemoveFromCollection(materialName, amount, leftoverIngredients);
            NanoFactoryIngredient ingredientOnHand = this.CheckToSeeIfIHaveItAlready(materialName, amount, materialBag);


            // How much do we really have to make?
            // Remember that we might have to round up to take care of base needs for our recipe.
            long amountLeftToProduce = (amount - ingredientOnHand.amount);
            if (amountLeftToProduce == 0) {
                //this.AddToCollection(materialName, amount, produced, "produce");
                return 0;  // Cost zero ORE!
            }


            // I QUESTION THIS.
            //int amountLeftToGetMinimum = Program.TargetAmount(amountLeftToProduce, recipeMaterial.output.amount);

            //if (amountLeftToGetMinimum == 0) {
            //    this.AddToCollection(materialName, amount, produced, "produce");
            //    return 0;  // Cost zero ORE!
            //}




            // If this ingredient is primary, go ahead and make it with ORE.
            if (IsAPrimaryMaterial(materialName, "ORE")) {

                long oreCost = 0;
                long amountOfPrimaryProduced = 0;
                while (amountLeftToProduce > amountOfPrimaryProduced) {
                    Program.Assert(recipeMaterial.inputs[0].name == "ORE", "ERROR!");
                    Program.Assert(recipeMaterial.inputs.Count == 1, "ERROR!");
                    oreCost += recipeMaterial.inputs[0].amount;
                    amountOfPrimaryProduced += recipeMaterial.output.amount;
                }

                this.AddToCollection(materialName, amountOfPrimaryProduced, materialBag, "produce");
                return oreCost;
            }

            // If we have made it here, we need to produce a non-primary material.
            long totalOreCost = 0;
            //int howManyTimesDoRunRecipe = (int)Math.Ceiling((((double)amount) / (double)recipeMaterial.output.amount));
            int howManyTimesDoRunRecipe = (int)Math.Ceiling((((double)amountLeftToProduce) / (double)recipeMaterial.output.amount));

            foreach (NanoFactoryIngredient inputIngredient in recipeMaterial.inputs) {

                long amountPerRun = inputIngredient.amount;
                //int amountNeeded = howManyTimesDoRunRecipe * amountPerRun / recipeMaterial.output.amount;
                long amountNeeded = howManyTimesDoRunRecipe * amountPerRun;

                //NanoFactoryRecipe ingredientRecipe = this.FindRecipe(inputIngredient.name);
                //int amountNeeded = (int)Math.Ceiling((double)(inputIngredient.amount * amount / recipeMaterial.output.amount));
                //int amountNeeded = inputIngredient.amount * amount;

                totalOreCost += this.ProduceMaterial(inputIngredient.name, amountNeeded, materialBag);
                //NanoFactoryRecipe recipeIngredient = this.FindRecipe(inputIngredient.name);

                this.RemoveFromCollection(inputIngredient.name, amountNeeded, materialBag);
                // Calculate how much we really have to make, since sometimes we produce more than 1.
                // Look to see if that material is already in the leftoverIngredients.  If so, take from there.
                // If this ingredient is primary, go ahead and make it with ORE.
                // Call us recursively to make the rest.  Then pull out what we really need from what we made, putting
                // the rest in the leftoverIngredients pile.
            }

            // What did we make?
            long amountProduced = (howManyTimesDoRunRecipe * recipeMaterial.output.amount);
            this.AddToCollection(recipeMaterial.output.name, amountProduced, materialBag, "produce");

            //Console.WriteLine("Produced " + amountProduced + " " + materialName);

            // If we produced more than we needed, put in leftover bin.
            // int materialLeftOver = amountProduced - amount;
            // this.AddToCollection(materialName, materialLeftOver, leftoverIngredients, "leftover");
            //this.PrintCollection(materialBag, null);

            return totalOreCost;
        }

        

        //public void FindCompoundAndBasicIngredients(Dictionary<string, double> compoundIngredients,
        //    Dictionary<string, double> basicIngredients) {

        //    // Produce a list of all the primary ingredients, which are anything that can be directly
        //    // made from ORE.  If ORE itself is requested here, return.
        //    if (recipeName == "ORE") return;

            

        //    // Find the recipe.
        //    NanoFactoryRecipe recipe = this.FindRecipe(recipeName);

        //    // If the thing coming in is itself a basic ingredient, add myself to the basicIngredientsList.
        //    if (IsAPrimaryMaterial(recipeName, "ORE")) {
        //        this.AddToCollection(recipeName, amountNeeded, basicIngredients);
        //        return;
        //    }


        //    // We need to order as many as we need, which is often more than we need.
        //    int amountProduced = Program.TargetAmount(amountNeeded, recipe.output.amount);
        //    double multiplier = (amountProduced / recipe.output.amount);

        //    foreach (NanoFactoryIngredient ingredient in recipe.inputs) {
        //        double amount = ingredient.amount * multiplier;
        //        this.AddToCollection(ingredient.name, amount, compoundIngredients);
        //    }



        //    //        //int amount = ingredient.amount * multiplier;

        //    //        //if (IsAtomicMaterial(ingredient.name)) {
        //    //        //    if (amountNeeded > recipe.output.amount) {
        //    //        //        Console.WriteLine("ERROR!");
        //    //        //    }
        //    //        //    oreRequired += ingredient.amount;
        //    //        //}

        //    //        if (IsAPrimaryMaterial(ingredient.name, "ORE")) {
        //    //            //if (ingredient.name != "ORE") {

        //    //            this.AddToCollection(ingredient.name, amount, totalIngredientsNeeded);

        //    //            //if (totalIngredientsNeeded.ContainsKey(ingredient.name)) {
        //    //            //    int newValue = totalIngredientsNeeded[ingredient.name] + amount;
        //    //            //    totalIngredientsNeeded[ingredient.name] = newValue;
        //    //            //}
        //    //            //else {
        //    //            //    totalIngredientsNeeded[ingredient.name] = amount;
        //    //            //}
        //    //        }


        //    //        //int baseOreAmount = this.AmountOfOreRequired(ingredient.name, amount, totalIngredientsNeeded);
        //    //        else {
        //    //            this.FindPrimaryMaterials(ingredient.name, amount, totalIngredientsNeeded);
        //    //        }


        //    //        //oreRequired += Convert.ToInt32(baseOreAmount * multiplier);
        //    //    }

        //    //    //// Return the right amount of Ore if the inputted recipe is itself a primary.
        //    //    //if (IsAPrimaryMaterial(recipe.output.name, "ORE")) {
        //    //    //    if (amountNeeded > recipe.output.amount) {
        //    //    //        Console.WriteLine("ERROR!");
        //    //    //    }
        //    //    //    oreRequired += recipe.inputs[0].amount;
        //    //    //}

        //    //    //return oreRequired;

        //    //    //Console.WriteLine("Part 1:");
        //    //    //recipeFuel.PrintOut();
        //    //}


        //}

        public void FindPrimaryMaterials(string recipeName, long amountNeeded, Dictionary<string, long>totalIngredientsNeeded) {

            // Produce a list of all the primary ingredients, which are anything that can be directly
            // made from ORE.  If ORE itself is requested here, return.
            if (recipeName == "ORE") return;


            NanoFactoryRecipe recipe = this.FindRecipe(recipeName);

            if (IsAPrimaryMaterial(recipeName, "ORE")) {
                //int realAmountNeeded = (amountNeeded)
                this.AddToCollection(recipeName, amountNeeded, totalIngredientsNeeded, "primary");
                return; // 0;
            }

            // We need to order as many as we need, which is often more than we need.
            //int multiplier = Program.OrderEnough(amountNeeded, recipe.output.amount);
            long amountProduced = Program.TargetAmount(amountNeeded, recipe.output.amount);
            long multiplier = (amountProduced / recipe.output.amount);

            //double multiplier = amountNeeded / recipe.output.amount;
            foreach (NanoFactoryIngredient ingredient in recipe.inputs) {

                long amount = ingredient.amount * multiplier;

                //if (IsAtomicMaterial(ingredient.name)) {
                //    if (amountNeeded > recipe.output.amount) {
                //        Console.WriteLine("ERROR!");
                //    }
                //    oreRequired += ingredient.amount;
                //}

                if (IsAPrimaryMaterial(ingredient.name, "ORE")) {
                    //if (ingredient.name != "ORE") {

                    this.AddToCollection(ingredient.name, amount, totalIngredientsNeeded, "primary");

                    //if (totalIngredientsNeeded.ContainsKey(ingredient.name)) {
                    //    int newValue = totalIngredientsNeeded[ingredient.name] + amount;
                    //    totalIngredientsNeeded[ingredient.name] = newValue;
                    //}
                    //else {
                    //    totalIngredientsNeeded[ingredient.name] = amount;
                    //}
                }


                //int baseOreAmount = this.AmountOfOreRequired(ingredient.name, amount, totalIngredientsNeeded);
                else {
                    this.FindPrimaryMaterials(ingredient.name, amount, totalIngredientsNeeded);
                }


                //oreRequired += Convert.ToInt32(baseOreAmount * multiplier);
            }

            //// Return the right amount of Ore if the inputted recipe is itself a primary.
            //if (IsAPrimaryMaterial(recipe.output.name, "ORE")) {
            //    if (amountNeeded > recipe.output.amount) {
            //        Console.WriteLine("ERROR!");
            //    }
            //    oreRequired += recipe.inputs[0].amount;
            //}

            //return oreRequired;

            //Console.WriteLine("Part 1:");
            //recipeFuel.PrintOut();
        }
        
    }
}
