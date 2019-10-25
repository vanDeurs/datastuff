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

        public Ingredient ()
        {

        }
    }
}
