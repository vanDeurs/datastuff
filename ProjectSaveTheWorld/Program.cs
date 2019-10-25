using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

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

                    foreach (var item in array)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("{0}", item);
                        
                    }
                }
            }
            loadJsonWithoutClass();

            /*string json = @"{'_id':{'$oid':'5b0083866fd2dcf4a031b1c6'},'FoodCat1ID':7,'FoodCat1Name':'Livsmedelsrätter och ingredienser','FoodCat2ID':161,'FoodCat2Name':'Soppor','FoodCat3ID':1213,'FoodCat3Name':'Soppa','SLVID':904,'Name':{'sv':'Ärtsoppa vegetarisk','en':'Pea soup vegetarian','no':'Ertesuppe vegetarisk'},'text':'Ärtsoppa vegetarisk','Energi':0.78,'Kolhydrater':0.1204,'Fett':0.0032,'Protein':0.0512,'Järn':1.4e-05,'Vitamin_D':0,'Folat':'8.3e-08','Fibrer':0.026699999999999998,'Primary':true,'NewName':'','Class':5,'Variations':[{'_id':{'$oid':'5b7bd6c84a40085c0920772d'},'text':{'no':'Sverige. konvensjonell','en':'Sweden. Conventional','sv':{'no':'Europa. unntatt de nordiske landene. konvensjonell','en':'Europe. except the Nordic countries. Conventional','sv':'Sverige. Konventionell'}},'RegionName':'Sverige','RegionID':5,'ID':3,'Organic':false,'CO2':4.26},{'_id':{'$oid':'5b7bd6c84a40085c0920772c'},'text':{'no':'Sverige. konvensjonell','en':'Sweden. Conventional','sv':{'no':'Sverige. konvensjonell','en':'Sweden. Conventional','sv':'Sverige. Konventionell'}},'RegionName':'Sverige','RegionID':5,'ID':2,'Organic':false,'CO2':0.12},{'_id':{'$oid':'5b7bd6c84a40085c0920772b'},'text':{'no':'Annen opprinnelse. økologiske','en':'Other Origin. Ecological','sv':{'no':'Annen opprinnelse. økologiske','en':'Other Origin. Ecological','sv':'Övrigt Ursprung. Ekologisk'}},'RegionName':'Övrigt','RegionID':-1,'ID':1,'Organic':true,'CO2':0.12},{'_id':{'$oid':'5b7bd6c84a40085c0920772a'},'text':{'no':'Annen opprinnelse. Konvensjonelle.','en':'Other Origin. Conventional.','sv':'Övrigt Ursprung. Konventionell. '},'RegionName':'Övrigt','RegionID':-1,'ID':0,'Organic':true,'CO2':0.12}],'__v':0,'DuplicateId':null,'Index':2047,'OriginalName':'Ärtsoppa vegetarisk','WeightedAvgCO2':2.19}";
            string json2 = @"{'_id':{'$oid':'5b0083866fd2dcf4a031b1c6'},'FoodCat1ID':7}";

            JObject jo = JObject.Parse(json2);
            jo.Property("_id").Remove();
            json2 = jo.ToString();

            Console.WriteLine("JSON2: " + json2);


            Ingredient ingredient = JsonConvert.DeserializeObject<Ingredient>(json);

            ingredient.writeInformation();*/
            /*StreamReader reader = new StreamReader(File.OpenRead("C:\\users/alexw/documents/gymnasiearbete/projectsavetheworld/data/klimato-ingredients-15.csv"));
            List<string> listA = new List<String>();
            List<string> listB = new List<String>();
            List<string> listC = new List<String>();
            List<string> listD = new List<String>();
            //string vara1, vara2, vara3, vara4;
            while (!reader.EndOfStream)
                {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                    {
                    string[] values = line.Split(',');
                    if (values.Length >= 4)
                        {
                        listA.Add(values[0]);
                        listB.Add(values[1]);
                        listC.Add(values[2]);
                        listD.Add(values[3]);
                        }
                    Console.WriteLine("Values {0}: {1}", values[7], values[8]);
                    Thread.Sleep(500);
                    }
                }
            string[] firstlistA = listA.ToArray();
            string[] firstlistB = listB.ToArray();
            string[] firstlistC = listC.ToArray();
            string[] firstlistD = listD.ToArray();
            */
        }
    }
}
