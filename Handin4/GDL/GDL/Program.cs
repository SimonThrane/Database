using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GDL.Model;
using Newtonsoft.Json;

namespace GDL
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new GDLContext();

            //var readings = new Readings
            //{
            //    new Reading {appartmentId = 4, sensorId = 20, timestamp = DateTime.Now, value = 20},
            //    new Reading {appartmentId = 4, sensorId = 20, timestamp = DateTime.Now, value = 20},
            //    new Reading {appartmentId = 4, sensorId = 20, timestamp = DateTime.Now, value = 20},
            //    new Reading {appartmentId = 4, sensorId = 20, timestamp = DateTime.Now, value = 20},
            //    new Reading {appartmentId = 4, sensorId = 20, timestamp = DateTime.Now, value = 20}
            //};


            //Readings.AddItemsToDatabase(readings);


            for (int i = 1; i < 2; i++)
            {
                var url = "http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/" + i + ".json";
                var container = ReadReadingContainerData(url);
                db.ReadingContainers.Add(container);
                db.SaveChanges();
                Console.WriteLine("Item "+ i +" added");
            }

            //var url = "http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/GFKSC002_original.txt";
            //var container = ReadCharacteristicContainerData(url);
            //db.CharacteristicContainers.Add(container);
            //    db.SaveChanges();

            Console.WriteLine("Done.. Press any key...");
            Console.ReadKey();
        }

        private static CharacteristicContainer ReadCharacteristicContainerData(string url)
        {
            var container = new CharacteristicContainer();
            using (var wc = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    jsonData = wc.DownloadString(url);
                }
                catch (Exception)
                {
                    // ignored
                }
                if (!string.IsNullOrEmpty(jsonData))
                    container = JsonConvert.DeserializeObject<CharacteristicContainer>(jsonData);
            }
            return container;
        }
        private static ReadingContainer ReadReadingContainerData(string url)
        {
            var container = new ReadingContainer();
            using (var wc = new WebClient())
            {
                var jsonData = string.Empty;
                try
                {
                    jsonData = wc.DownloadString(url);
                }
                catch (Exception)
                {
                    // ignored
                }
                if (!string.IsNullOrEmpty(jsonData))
                    container = JsonConvert.DeserializeObject<ReadingContainer>(jsonData);
            }
            return container;
        }

    }


}
