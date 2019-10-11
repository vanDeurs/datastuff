using System;
using System.IO;
using System.Collections.Generic;

namespace ProjectSaveTheWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(@"C:\users/alexw/documents/gymnasiearbete/projectsavetheworld/data/klimato-ingredients-15.csv"))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Console.WriteLine("Line: " + line);
                }
            }
        }
    }
}
