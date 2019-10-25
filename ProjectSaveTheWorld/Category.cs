using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSaveTheWorld
{
    class Category
    {
        string name;
        int id;
        List<Ingredient> ingredients = new List<Ingredient>();

        public Category (string namn, int id)
        {
            this.name = namn;
            this.id = id;
        }

    }
}
