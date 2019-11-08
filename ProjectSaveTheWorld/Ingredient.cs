using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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
        public void writeInformation()
        {
            Console.WriteLine("Name: {0}\nSLVID: {1}\n_id: {2}\nAvgCO2: {3}\nFoodCat1ID: {4}\nFoodCat1Name: {5}\nFoodCat2ID: {6}\nFoodCat2Name: {7}\nFoodCat3ID: {8}\nFoodCat3Name: {9}", this.Name, this.SLVID, this._id, this.WeightedAvgCO2, this.FoodCat1ID, this.FoodCat1Name, this.FoodCat2ID, this.FoodCat2Name, this.FoodCat3ID, this.FoodCat3Name);
        }

    }
}
