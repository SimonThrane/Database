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
            CharacteristicContainer container=new CharacteristicContainer();
            var url = "http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/GFKSC002_original.txt";

            using (WebClient wc = new WebClient())
            {
                string jsonData=String.Empty;
                try
                {
                    jsonData=wc.DownloadString(url);
                }
                catch (Exception) { }
                if (!string.IsNullOrEmpty(jsonData))
                    container=JsonConvert.DeserializeObject<CharacteristicContainer>(jsonData);

                db.CharacteristicContainers.Add(container);
                db.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
