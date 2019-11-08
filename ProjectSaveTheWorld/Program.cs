﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjectSaveTheWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            void loadJsonWithClass()
            {
                using (StreamReader r = new StreamReader("../../../json1.json"))
                {
                    string json = r.ReadToEnd();
                    List<Ingredient> ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(json);
                    Console.WriteLine("Length of ingredients: {0}", ingredients.Count);
                    foreach (Ingredient ingredient in ingredients)
                    {
                        ingredient.writeInformation();
                        foreach (Variation variation in ingredient.VARIATIONS)
                        {
                            Console.WriteLine("--------------------");
                            variation.writeInformation();
                            Console.WriteLine("--------------------");
                        }
                        Console.WriteLine("--------------------");
                        Thread.Sleep(100000);
                    }
                }
            }

            void loadJsonWithoutClass ()
            {
                using(StreamReader r = new StreamReader("../../../json1.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic array = JsonConvert.DeserializeObject(json);

                    double CO2SumPerKg = 0;
                    foreach (var ingredient in array)
                    {
                        Console.WriteLine("Ingredient: {0}", ingredient.Name);
                        var variations = ingredient.Variations;
                        foreach(var variation in variations)
                        {
                            if (variation.RegionName == "Sverige")
                            {
                                double co2 = variation.CO2;
                                CO2SumPerKg += co2;
                            }
                        }
                    }
                    Console.WriteLine("Total CO2 per kilo in swedish ingredients: {0}", CO2SumPerKg);
                }
            }
            //loadJsonWithoutClass();
            loadJsonWithClass();
        }
    }
}
