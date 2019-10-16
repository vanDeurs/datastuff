using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace ProjectSaveTheWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StreamReader reader = new StreamReader(File.OpenRead("C:\\users/alexw/documents/gymnasiearbete/projectsavetheworld/data/klimato-ingredients-15.csv"));
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
                    Console.WriteLine("Values {0}: {1}", values[0], values[4]);
                    Thread.Sleep(500);
                    }
                }
            string[] firstlistA = listA.ToArray();
            string[] firstlistB = listB.ToArray();
            string[] firstlistC = listC.ToArray();
            string[] firstlistD = listD.ToArray();
        }
    }
}
