using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Dynamic;
using System.IO;
using System.Web.Mvc;


namespace ProjectSaveTheWorld
{
    class Ingredient
    {
        object Name;
        int SLVID;
        object _id;
        double WeightedAvgCO2;
        int FoodCat1ID;
        string FoodCat1Name;
        int FoodCat2ID;
        string FoodCat2Name;
        int FoodCat3ID;
        string FoodCat3Name;
        List<Variation> Variations = new List<Variation>();

        public Ingredient(
            object Name,
            int SLVID,
            object _id,
            double WeightedAvgCO2,
            int FoodCat1ID,
            string FoodCat1Name,
            int FoodCat2ID,
            string FoodCat2Name,
            int FoodCat3ID,
            string FoodCat3Name,
            List<Variation> Variations
            )
        {
            this.Name = Name;
            this.SLVID = SLVID;
            this._id = _id;
            this.WeightedAvgCO2 = WeightedAvgCO2;
            this.FoodCat1ID = FoodCat1ID;
            this.FoodCat1Name = FoodCat1Name;
            this.FoodCat2ID = FoodCat2ID;
            this.FoodCat2Name = FoodCat2Name;
            this.FoodCat3ID = FoodCat3ID;
            this.FoodCat3Name = FoodCat3Name;
            this.Variations = Variations;
        }
        //Properties
        public object NAME
        {
            get { return this.Name; }
        }
        public List<Variation> VARIATIONS
        {
            get { return this.Variations; }
        }
        public bool includesVariation (string regionName)
        {
            foreach (Variation var in this.Variations)
            {
                if (var.REGIONNAME == regionName) {
                    return true;
                }
            }
            return false;
        }
        public void WriteInformation()
        {
            Console.WriteLine("Name: {0}\nSLVID: {1}\n_id: {2}\nAvgCO2: {3}\nFoodCat1ID: {4}\nFoodCat1Name: {5}\nFoodCat2ID: {6}\nFoodCat2Name: {7}\nFoodCat3ID: {8}\nFoodCat3Name: {9}", this.Name, this.SLVID, this._id, this.WeightedAvgCO2, this.FoodCat1ID, this.FoodCat1Name, this.FoodCat2ID, this.FoodCat2Name, this.FoodCat3ID, this.FoodCat3Name);
        }
        public static IEnumerable<string> uniqueRegions (List<Ingredient> ingredients)
        {
            List<String> variations = new List<String>();
            foreach (Ingredient ingredient in ingredients)
            {
                foreach (Variation variation in ingredient.VARIATIONS)
                {
                    variations.Add(variation.REGIONNAME);

                }
            }
            IEnumerable<string> distinctVariationRegions = variations.Distinct();
            return distinctVariationRegions;
        }

        public static IEnumerable<string> UniqueFoodCats(int catIndex, List<Ingredient> ingredients)
        {
            List<String> categories = new List<String>();
            foreach (Ingredient ingredient in ingredients)
            {
                switch (catIndex)
                {
                    case 1: categories.Add(ingredient.FoodCat1Name);
                        break;
                    case 2: categories.Add(ingredient.FoodCat2Name);
                        break;
                    case 3: categories.Add(ingredient.FoodCat3Name);
                        break;
                    default:
                        break;
                }
            }
            IEnumerable<string> distinctCategories = categories.Distinct();
            return distinctCategories;
        }

        public static void DownloadCSV (List<dynamic> list)
        {
            StringBuilder csv = new StringBuilder();

            // If you want headers for your file
            var header = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                                       "Region",
                                       "ConventionalAvg",
                                       "OrganicAvg",
                                       "CO2Difference",
                                       "OrganicHighest"
                                      );
            csv.AppendLine(header);

            // For downloading CO2PerIngredientPerRegion
            /*foreach (var item in list)
            {
                dynamic listResults = string.Format("\"{0}\",\"{1}\",\"{2}\"",
                                                  item.region,
                                                  item.averageCO2,
                                                  item.totalCO2
                                                 );
                csv.AppendLine(listResults);
            }*/
            foreach (var item in list)
            {
                dynamic listResults = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                                                    item.region,
                                                    item.CO2ConventionalAvg,
                                                    item.CO2OrganicAvg,
                                                    item.C02Difference,
                                                    item.OrganicHighest
                                                 );
                csv.AppendLine(listResults);
            }
            string filePath = "../../../data.csv";
            File.WriteAllText(filePath, csv.ToString());

        }

        // Calculating methods

        public static void CO2PerIngredientPerRegion (List<Ingredient> ingredients)
        {
            IEnumerable<string> regions = uniqueRegions(ingredients);
            List <dynamic> regionObjects = new List<dynamic>();
            foreach (string region in regions)
            {
                dynamic regionObject = new ExpandoObject();
                regionObject.region = region;
                regionObject.variations = new List<Variation>();
                regionObject.averageCO2 = 0;
                regionObject.totalCO2 = 0;

                foreach (Ingredient ingredient in ingredients)
                {
                    foreach (Variation variation in ingredient.VARIATIONS)
                    {
                        if (variation.REGIONNAME == region)
                        {
                            regionObject.variations.Add(variation);
                            regionObject.totalCO2 += variation.CO2;
                        }
                    }
                }
                regionObject.averageCO2 = regionObject.totalCO2 / regionObject.variations.Count;
                regionObjects.Add(regionObject);
            }
            foreach (dynamic regionObject in regionObjects)
            {
                Console.WriteLine("Region: {0}", regionObject.region);
                Console.WriteLine("Genomsnittligt {0} gram CO2e per gram av ingrediens", regionObject.averageCO2);
                Console.WriteLine("Antal ingredienser i beräkningarna: {0}", regionObject.variations.Count);
                Console.WriteLine("---------------------------");
            }

            bool downloadCSVFile = true;
            if (downloadCSVFile)
            {
                DownloadCSV(regionObjects);
            }
        }
        // catIndex avgör om man ska jämföra FoodCat 1, 2 eller 3 
        public static void CO2PerIngredientPerFoodCat(int catIndex, List<Ingredient> ingredients)
        {
            IEnumerable<string> foodCats = UniqueFoodCats(catIndex, ingredients);
            List<dynamic> catObjects = new List<dynamic>();
            foreach (string cat in foodCats)
            {
                dynamic catObject = new ExpandoObject();
                catObject.category = cat;
                catObject.index = catIndex;
                catObject.averageCO2 = 0;
                catObject.totalCO2 = 0;
                catObject.ingredients = new List<Ingredient>();

                foreach (Ingredient ingredient in ingredients)
                {
                    switch (catIndex)
                    {
                        case 1:
                            if (ingredient.FoodCat1Name == cat)
                            {
                                catObject.totalCO2 += ingredient.WeightedAvgCO2;
                                catObject.ingredients.Add(ingredient);
                            }
                            break;
                        case 2:
                            if (ingredient.FoodCat2Name == cat)
                            {
                                catObject.totalCO2 += ingredient.WeightedAvgCO2;
                                catObject.ingredients.Add(ingredient);
                            }
                            break;
                        case 3:
                            if (ingredient.FoodCat3Name == cat)
                            {
                                catObject.totalCO2 += ingredient.WeightedAvgCO2;
                                catObject.ingredients.Add(ingredient);
                            }
                            break;
                    }
                }
                catObject.averageCO2 = catObject.totalCO2 / catObject.ingredients.Count;
                catObjects.Add(catObject);
            }
            foreach (dynamic catObject in catObjects)
            {
                Console.WriteLine("Kategori: {0}", catObject.category);
                Console.WriteLine("Index: {0}", catObject.index);
                Console.WriteLine("Genomsnittligt {0} gram CO2e per gram av ingrediens", catObject.averageCO2);
                Console.WriteLine("Antal ingredienser i beräkningarna: {0}", catObject.ingredients.Count);
                Console.WriteLine("---------------------------");
            }

            bool downloadCSVFile = false;
            if (downloadCSVFile)
            {
                DownloadCSV(catObjects);
            }
        }
        public static void CO2InsideMeatCategory(List<Ingredient> ingredients)
        {
            // This method returns all ingredients which have "Kött" as their FoodCat1Name
            List<dynamic> variations = new List<dynamic>();

            dynamic meatData = new ExpandoObject();
            meatData.averageCO2OrganicVariations = 0;
            meatData.averageCO2ConventionalVariations = 0;
            meatData.organicVariations = new List<Variation>();
            meatData.conventionalVariations = new List<Variation>();
            meatData.totalVariations = new List<Variation>();
            meatData.averageCO2Total = 0;
            meatData.meatIngredients = 0;

            meatData.meatIngredientsCO2 = 0;
            meatData.totalIngredients = new List<Ingredient>();

            foreach (Ingredient ingredient in ingredients)
            {
                // ID 6 is for meat
                if (ingredient.FoodCat1ID == 6)
                {
                    meatData.meatIngredientsCO2 += ingredient.WeightedAvgCO2;
                    meatData.totalIngredients.Add(ingredient);
                    foreach (Variation variation in ingredient.VARIATIONS)
                    {
                        dynamic variationObject = new ExpandoObject();
                        variationObject.CO2 = variation.CO2;
                        variationObject.Organic = variation.ORGANIC;
                        variationObject.RegionName = variation.REGIONNAME;
                        variations.Add(variationObject);

                        meatData.totalVariations.Add(variation);
                        meatData.averageCO2Total += variation.CO2;
                        if (variation.ORGANIC)
                        {
                            meatData.organicVariations.Add(variation);
                            meatData.averageCO2OrganicVariations += variation.CO2;
                        } else
                        {
                            meatData.conventionalVariations.Add(variation);
                            meatData.averageCO2ConventionalVariations += variation.CO2;
                        }
                    }
                }
            }
            if (meatData.organicVariations.Count < 1 && meatData.conventionalVariations.Count < 1)
            {
                Console.WriteLine("meatData count is less than one. Stop here.");
                return;
            }

            meatData.meatIngredientsCO2 = meatData.meatIngredientsCO2 / meatData.totalIngredients.Count;
            Console.WriteLine("Genomsnitt kött baserat på avg CO2: {0}", meatData.meatIngredientsCO2);

            Console.WriteLine("Antal kött: {0}", meatData.meatIngredients);

            meatData.averageCO2Total = meatData.averageCO2Total / meatData.totalVariations.Count;

            Console.WriteLine("Amount of meat: {0}", meatData.totalVariations.Count);
            Console.WriteLine("Average CO2 for meat: {0}", meatData.averageCO2Total);

            meatData.averageCO2OrganicVariations = meatData.averageCO2OrganicVariations / meatData.organicVariations.Count;
            meatData.averageCO2ConventionalVariations = meatData.averageCO2ConventionalVariations / meatData.conventionalVariations.Count;

            Console.WriteLine("Amount of organic meats: {0}", meatData.organicVariations.Count);
            Console.WriteLine("Average CO2 for organic meats: {0}", meatData.averageCO2OrganicVariations);
            Console.WriteLine("Amount of organic meats: {0}", meatData.conventionalVariations.Count);
            Console.WriteLine("Average CO2 for conventional meats: {0}", meatData.averageCO2ConventionalVariations);

            bool downloadCSVFile = true;
            if (downloadCSVFile)
            {
                DownloadCSV(variations);
            }
        }

        public static void CO2ForIngredientPerRegion(List<Ingredient> ingredients, int FoodCat1ID)
        {
            // This method compares the CO2e for meat per regions
            IEnumerable<string> regions = uniqueRegions(ingredients);
            List<dynamic> regionObjects = new List<dynamic>();

            foreach (string region in regions)
            {
                dynamic regionObject = new ExpandoObject();
                regionObject.region = region;
                regionObject.meatVariations = new List<Variation>();
                regionObject.averageCO2PerMeatVariation = 0;

                foreach (Ingredient ingredient in ingredients)
                {
                    if (ingredient.FoodCat1ID == FoodCat1ID)
                    {
                        foreach (Variation variation in ingredient.VARIATIONS)
                        {
                            if (variation.REGIONNAME == region)
                            {
                                regionObject.meatVariations.Add(variation);
                                regionObject.averageCO2PerMeatVariation += variation.CO2;
                            }
                        }
                    }
                }
                if (regionObject.meatVariations.Count < 1) { continue; }

                regionObject.averageCO2PerMeatVariation = regionObject.averageCO2PerMeatVariation / regionObject.meatVariations.Count;
                regionObjects.Add(regionObject);
            }

            foreach (dynamic regionObject in regionObjects)
            {
                Console.WriteLine("Region: {0}", regionObject.region);
                Console.WriteLine("Avg CO2: {0}", regionObject.averageCO2PerMeatVariation);
                Console.WriteLine("Antal variationer: {0}", regionObject.meatVariations.Count);
                Console.WriteLine("---------------------------");
            }

            bool downloadCSVFile = true;
            if (downloadCSVFile)
            {
                DownloadCSV(regionObjects);
            }
        }
        public static void ProductionMethods(List<Ingredient> ingredients)
        {
            double CO2ConventionalAvg = 0;
            double CO2OrganicAvg = 0;
            List<Variation> organics = new List<Variation>();
            List<Variation> conventionals = new List<Variation>();

            foreach (Ingredient ingredient in ingredients)
            {
                foreach (Variation variation in ingredient.VARIATIONS)
                {
                    if (variation.ORGANIC == true)
                    {
                        organics.Add(variation);
                        CO2OrganicAvg += variation.CO2;
                    } 
                    else
                    {
                        conventionals.Add(variation);
                        CO2ConventionalAvg += variation.CO2;
                    }
                }
            }
            CO2ConventionalAvg = CO2ConventionalAvg / conventionals.Count;
            CO2OrganicAvg = CO2OrganicAvg / organics.Count;
            Console.WriteLine("Amount of organics: {0}", organics.Count);
            Console.WriteLine("Amount of conventionals: {0}", conventionals.Count);
            Console.WriteLine("Organic CO2 AVG: {0}", CO2OrganicAvg);
            Console.WriteLine("Conventional CO2 AVG: {0}", CO2ConventionalAvg);
        }
        public static void ProductionMethodsPerCountry(List<Ingredient> ingredients)
        {
            IEnumerable<string> regions = uniqueRegions(ingredients);
            List <dynamic> regionObjects = new List<dynamic>();

            foreach ( string region in regions)
	        {
                dynamic regionObject = new ExpandoObject();
                regionObject.region = region;
                regionObject.CO2ConventionalAvg = 0;
                regionObject.CO2OrganicAvg = 0;
                regionObject.C02Difference = 0;
                regionObject.OrganicHighest = true;
                regionObject.organics = new List<Variation>();
                regionObject.conventionals = new List<Variation>();
                foreach (Ingredient ingredient in ingredients)
                {
                    foreach (Variation variation in ingredient.VARIATIONS)
                    {
                        if (variation.REGIONNAME == regionObject.region)
                        {
                            if (variation.ORGANIC)
                            {
                                regionObject.CO2OrganicAvg += variation.CO2;
                                regionObject.organics.Add(variation);
                            }
                            else
                            {
                                regionObject.CO2ConventionalAvg += variation.CO2;
                                regionObject.conventionals.Add(variation);
                            }
                        }
                            

                    }
                }

                if (regionObject.organics.Count != 0)
                {
                    regionObject.CO2OrganicAvg = regionObject.CO2OrganicAvg / regionObject.organics.Count;
                }
                if (regionObject.conventionals.Count != 0)
                {
                    regionObject.CO2ConventionalAvg = regionObject.CO2ConventionalAvg / regionObject.conventionals.Count;
                }

                if (regionObject.CO2ConventionalAvg >= regionObject.CO2OrganicAvg)
                {
                   regionObject.CO2Difference = regionObject.CO2ConventionalAvg - regionObject.CO2OrganicAvg;
                    regionObject.OrganicHighest = false;
                }
                if(regionObject.CO2ConventionalAvg < regionObject.CO2OrganicAvg)
                {
                    regionObject.CO2Difference = regionObject.CO2OrganicAvg - regionObject.CO2ConventionalAvg;
                }
                regionObjects.Add(regionObject);
	        }
            foreach (dynamic regiono in regionObjects)
            {
                Console.WriteLine(regiono.CO2Difference);
            }
            DownloadCSV(regionObjects);
        }

    }
}
