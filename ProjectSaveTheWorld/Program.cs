using System;
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
                using (StreamReader r = new StreamReader("E:\\Debug\\VipBat\\json1.json"))
                {
                    string json = r.ReadToEnd();
                    List<Ingredient> items = JsonConvert.DeserializeObject<List<Ingredient>>(json);
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
                    Console.WriteLine("CO2 sum: {0}", CO2SumPerKg);
                }
            }
            loadJsonWithoutClass();
        }
    }
}
