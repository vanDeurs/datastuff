﻿using System;
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

        public static void DownloadCSV (List<dynamic> list)
        {
            StringBuilder csv = new StringBuilder();

            // If you want headers for your file
            var header = string.Format("\"{0}\",\"{1}\",\"{2}\"",
                                       "RegionName",
                                       "AverageCO2",
                                       "TotalCO2"
                                      );
            csv.AppendLine(header);

            foreach (var item in list)
            {
                Console.WriteLine("item: {0}", item);
                dynamic listResults = string.Format("\"{0}\",\"{1}\",\"{2}\"",
                                                  item.region,
                                                  item.averageCO2,
                                                  item.totalCO2
                                                 );
                csv.AppendLine(listResults);
            }
            string filePath = "../../../data.csv";
            File.WriteAllText(filePath, csv.ToString());

        }

        // Calculating methods

        public static void CO2PerIngredientPerRegion (List<Ingredient> ingredients, string Sverige)
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


    }
}
