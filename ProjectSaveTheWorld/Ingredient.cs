using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSaveTheWorld
{
    class Ingredient
    {
        string name;
        int slvid;
        string mongoId;
        double weightedAvgCO2;
        Category foodCat1;
        Category foodCat2;
        Category foodCat3;
        List<Variation> variations = new List<Variation>();

        public Ingredient(string name, int slvid)
        {
            this.name = name;
            this.slvid = slvid;
        }
        public string Name
        {
            get { return this.name; }
        }
        public void writeInformation ()
        {
            Console.WriteLine("Name: {0}, SLVID: {1}, MongoId: {2}", this.name, this.slvid, this.mongoId);
        }

    }
}
