using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesTier;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            List<AdresseBinding> Ejendomme = new List<AdresseBinding>
            {
                {new AdresseBinding {Type = "Sommerhus",adresse = new Adresse {Bynavn = "Odder", Husnummer = "1",Postummer = 8620,Vejnavn = "Saksildvej"}} },
                {new AdresseBinding {Type = "Sommerhus",adresse = new Adresse {Bynavn = "Odder", Husnummer = "2",Postummer = 8620,Vejnavn = "Saksildvej"}} }
            };

            List<TelefonBinding> telefoner = new List<TelefonBinding>
            {
                {new TelefonBinding {Type = "Mobil",telefon = new Telefon {TelefonNummer = "11111111"}} },
                {new TelefonBinding {Type = "Mobil",telefon = new Telefon {TelefonNummer = "22222222"}} }
            };
            Person p1 = new Person
            {
                Adresse =
                    new Adresse
                    {
                        Bynavn = "Aarhus N",
                        Husnummer = "37",
                        Postummer = 8200,
                        Vejnavn = "Gøteborg Alle"
                    },
                Adresser = Ejendomme,
                Efternavn = "Madsen",
                Mellemnavn = "Jacob",
                Fornavn = "Mads",
                PersonNummer = "1234123412",
                Telefoner = telefoner,
                Type = "Ven"
            };


            List<AdresseBinding> Ejendomme2 = new List<AdresseBinding>
            {
                {new AdresseBinding {Type = "Lejlighed",adresse = new Adresse {Bynavn = "Horsens", Husnummer = "1",Postummer = 8620,Vejnavn = "Bildbjergvej"}} },
                {new AdresseBinding {Type = "Sommerhus",adresse = new Adresse {Bynavn = "København", Husnummer = "2",Postummer = 8620,Vejnavn = "Hovedgaden"}} }
            };

            List<TelefonBinding> telefoner2 = new List<TelefonBinding>
            {
                {new TelefonBinding {Type = "Mobil",telefon = new Telefon {TelefonNummer = "33333333"}} },
                {new TelefonBinding {Type = "Mobil",telefon = new Telefon {TelefonNummer = "44444444"}} }
            };
            Person p2 = new Person
            {
                Adresse =
                    new Adresse
                    {
                        Bynavn = "Aarhus N",
                        Husnummer = "37",
                        Postummer = 8200,
                        Vejnavn = "Finlandsgade"
                    },
                Adresser = Ejendomme2,
                Efternavn = "Madsen",
                Mellemnavn = "",
                Fornavn = "Mads",
                PersonNummer = "1234123413",
                Telefoner = telefoner2,
                Type = "Familie"
            };

            PersonDataUtil db = new PersonDataUtil();

            //DELETE Everything in DB

            db.DeleteEverything();

            //CREATE

            db.insertPerson(ref p1);
            db.insertPerson(ref p2);

            //READ

            List<AdresseBinding> adresser = db.getPersons_Adresse(ref p1);

            foreach (var adresse in adresser)
            {
                Console.WriteLine(p1.Fornavn + " Bor på:" + adresse.adresse.Vejnavn + " nr. " + adresse.adresse.Husnummer
                    + ", " + adresse.adresse.Bynavn + " " + adresse.adresse.Postummer);
            }
            Console.ReadKey();

            //READ Telephones

            List<TelefonBinding> telephones = db.getPersons_Telefoner(ref p1);

            foreach (var telefon in telephones)
            {
                Console.WriteLine(p1.Fornavn + " Har en: " + telefon.Type + " med nr. "+ telefon.telefon.TelefonNummer);
            }
            Console.ReadKey();

            //UPDATE

            adresser.First().adresse.Husnummer = "107";

            Adresse temp = adresser.First().adresse;

            db.UpdateAdresse(ref temp);

            adresser = db.getPersons_Adresse(ref p1);

            foreach (var adresse in adresser)
            {
                Console.WriteLine(p1.Fornavn + " Bor på:" + adresse.adresse.Vejnavn + " nr. " + adresse.adresse.Husnummer
                    + ", " + adresse.adresse.Bynavn + " " + adresse.adresse.Postummer);
            }


            //DELETE

            Adresse deleteAdress = adresser.Last().adresse;

            db.DeleteAdresse(ref deleteAdress);

            adresser = db.getPersons_Adresse(ref p1);

            foreach (var adresse in adresser)
            {
                Console.WriteLine(p1.Fornavn + " Bor på:" + adresse.adresse.Vejnavn + " nr. " + adresse.adresse.Husnummer
                    + ", " + adresse.adresse.Bynavn + " " + adresse.adresse.Postummer);
            }

            Console.ReadKey();

        }
    }
}
