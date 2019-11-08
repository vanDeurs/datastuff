using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSaveTheWorld
{
    class Variation
    {
        double Co2;
        bool Organic;
        int ID;
        int RegionID;
        string RegionName;

        public Variation(double CO2, bool Organic, int ID, int RegionID, string RegionName)
        {
            this.Co2 = CO2;
            this.Organic = Organic;
            this.ID = ID;
            this.RegionID = RegionID;
            this.RegionName = RegionName;
        }

        public string REGIONNAME
        {
            get { return this.RegionName; }
        }
        public double CO2
        {
            get { return this.Co2; }
        }
        public void writeInformation ()
        {
            Console.WriteLine("CO2: {0}", this.CO2);
            Console.WriteLine("Organic: {0}", this.Organic);
            Console.WriteLine("ID: {0}", this.ID);
            Console.WriteLine("RegionId: {0}", this.RegionID);
            Console.WriteLine("RegionName: {0}", this.RegionName);
        }
    }
}
