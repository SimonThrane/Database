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
                {new AdresseBinding {Type = "Sommerhus",adresse = new Adresse {AdresseId = 02,Bynavn = "Odder", Husnummer = "1",Postummer = 8620,Vejnavn = "Saksildvej"}} },
                {new AdresseBinding {Type = "Sommerhus",adresse = new Adresse {AdresseId = 03,Bynavn = "Odder", Husnummer = "2",Postummer = 8620,Vejnavn = "Saksildvej"}} }
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
                        AdresseId = 1,
                        Bynavn = "Aarhus N",
                        Husnummer = "37",
                        Postummer = 8200,
                        Vejnavn = "Gøteborg Alle"
                    },
                Adresser = Ejendomme,
                Efternavn = "Madsen",
                Fornavn = "Mads",
                PersonNummer = "1234123412",
                Telefoner = telefoner
            };


            List<AdresseBinding> Ejendomme2 = new List<AdresseBinding>
            {
                {new AdresseBinding {Type = "Lejlighed",adresse = new Adresse {AdresseId = 05,Bynavn = "Horsens", Husnummer = "1",Postummer = 8620,Vejnavn = "Bildbjergvej"}} },
                {new AdresseBinding {Type = "Sommerhus",adresse = new Adresse {AdresseId = 06,Bynavn = "København", Husnummer = "2",Postummer = 8620,Vejnavn = "Hovedgaden"}} }
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
                        AdresseId = 04,
                        Bynavn = "Aarhus N",
                        Husnummer = "37",
                        Postummer = 8200,
                        Vejnavn = "Finlandsgade"
                    },
                Adresser = Ejendomme,
                Efternavn = "Madsen",
                Fornavn = "Mads",
                PersonNummer = "1234123413",
                Telefoner = telefoner
            };

            PersonDataUtil db = new PersonDataUtil();

            db.insertPerson(ref p1);
            db.insertPerson(ref p2);

            Adresse nyAdresse = new Adresse
            {
                AdresseId = 10,
                Bynavn = "København",
                Husnummer = "1C",
                Postummer = 4000,
                Vejnavn = "Lillevej"
            };

            

            //db.InsertAdresse(nyAdresse,"Sommerhus",p1};

        }
    }
}
