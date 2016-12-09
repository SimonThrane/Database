using GDL.Model;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

namespace GDL
{
  class Program
  {
      static void Main(string[] args)
      {
          var db = new GDLContext();

          for (int i = 1; i < 2; i++)
          {
              var url = "http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/" + i + ".json";
              var container = ReadReadingContainerData(url);
              db.ReadingContainers.Add(container);
              db.SaveChanges();
          }

          var url2 = "http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/GFKSC002_original.txt";
          var container2 = ReadCharacteristicContainerData(url2);
          db.CharacteristicContainers.Add(container2);
          db.SaveChanges();



            while (true)
            {

                Console.Write("See sensor data for appartment: ");

                var key = Console.ReadLine();
                if (key != null)
                {
                    var appartmentId = int.Parse(key);

                    var appartmentData = db.ReadingContainers.OrderByDescending(o => o.timestamp).First();
                    var data = appartmentData.reading.FindAll(a => a.appartmentId == appartmentId).ToList();
                    var sensors = db.CharacteristicContainers.First();

                    Console.WriteLine("---");
                    Console.WriteLine();
                    Console.WriteLine("Timestamp: " + appartmentData.timestamp + " Version: " + appartmentData.version);

                    if (data.Count > 0)
                    {
                        foreach (var appartment in data)
                        {
                            var sensorData = sensors.sensorCharacteristic.First(d => d.sensorId == appartment.sensorId);
                            Console.WriteLine("SensorID: " + appartment.sensorId + " Value: " + appartment.value + " " + sensorData.unit + " " + sensorData.description);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No sensor info on appartment: " + appartmentId);
                    }
                }
                else
                {
                    Console.WriteLine("Bad input");
                }

                Console.WriteLine();
                Console.WriteLine("---");
                Console.WriteLine();
            }

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
