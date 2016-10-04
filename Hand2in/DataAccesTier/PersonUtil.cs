﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesTier
{
    public class PersonDataUtil
    {
        public PersonDataUtil()
        {
            
        }

        private SqlConnection OpenConnection
        {
            get
            {
                var con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hand2in;Integrated Security=True");
                con.Open();
                return con;
            }
        }


        public void insertPerson(ref Person person)
        {
           
            // prepare command string using paramters in string and returning the given identity

                string insertStringParam = @"INSERT INTO [Person] (PersonNummer,Fornavn, Efternavn, Type, Mellemnavn, AdresseID)
                                                    VALUES (@PersonNummer,@Fornavn, @Efternavn, @Type, @Mellemnavn, @AdresseID)";


            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@PersonNummer",person.PersonNummer);
                    cmd.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                    cmd.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                    cmd.Parameters.AddWithValue("@Type", person.Type);
                    cmd.Parameters.AddWithValue("@Mellemnavn", person.Mellemnavn);
                    cmd.Parameters.AddWithValue("@AdresseID", person.Adresse);
                    
                    cmd.ExecuteNonQuery();
                }
            }

        public void updateCurrentPerson(ref Person person)
        {
            // prepare command string
            string updateString =
               @"UPDATE Person
                        SET AdresseID=@AdresseID Efternavn = @Efternavn, Type = @Type, Fornavn = @Fornavn, Mellemnavn=@Mellemnavn
                        WHERE PersonNummer = @PersonNummer";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                cmd.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);
                cmd.Parameters.AddWithValue("@Type", person.Type);
                cmd.Parameters.AddWithValue("@Mellemnavn", person.Mellemnavn);
                cmd.Parameters.AddWithValue("@AdresseID", person.Adresse);

                var id = (int)cmd.ExecuteNonQuery();
            }           
        }

        public void DeleteCurrentPerson(ref Person person)
        {
            string deleteString = @"DELETE FROM Person WHERE (PersonNummer = @PersonNummer)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);

                var id = (int)cmd.ExecuteNonQuery();
                person = null;
            }

        }

        public List<TelefonBinding> getPersons_Telefoner(ref Person person)
        {
            string selectToolboxTelefonString = @"SELECT *
                                                  FROM [har] 
                                                  WHERE ([PersonNummer] = @PersonNummer)";

            using (var cmd = new SqlCommand(selectToolboxTelefonString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);

                rdr = cmd.ExecuteReader();
                List<TelefonBinding> telefons = new List<TelefonBinding>();

                TelefonBinding tlf = null;
                while (rdr.Read())
                {
                    tlf = new TelefonBinding(); // 

                    tlf.telefon.TelefonNummer = (string) rdr["TelefonNummerID"];
                    tlf.Type = (string) rdr["Type"];
                    telefons.Add(tlf);
                }
                return telefons;
            }

        }

        public List<Adresse> getPersons_Adresse(ref Person person)
        {
            string selectToolboxAdresseString = @"SELECT *
                                                  FROM [erPaa] 
                                                  WHERE ([PersonNummer] = @PersonNummer)";
            using (var cmd = new SqlCommand(selectToolboxAdresseString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);

                rdr = cmd.ExecuteReader();
                List<Adresse> adresses = new List<Adresse>();

                Adresse adr = null;
                while (rdr.Read())
                {
                    adr = new Adresse(); // 

                    adr.Type = (string)rdr["Type"];
                    //adr.AdresseID = (string)rdr["AdresseID"];
                    adresses.Add(adr);
                }
                return adresses;
            }

        }

        public void addTelefon(ref Telefon tlf)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Telefon] (TelefonNummerID)
                                                    VALUES  (@TelefonNummerID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.TelefonNummer);

                cmd.ExecuteNonQuery();
            }
        }

        public void getTelefon(ref Telefon tlf)
        {
            // prepare command string using paramters in string and returning the given identity 

            string sqlcmd = @"SELECT * FROM [Telefon] WHERE (TelefonNummerID= TelefonNummerID)";

            using (SqlCommand cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.TelefonNummer);

                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    tlf.TelefonNummer = (string) rdr["TelefonNummerID"];
                }
            }
        }


        public void addTelefonToOwner(ref TelefonBinding tlf, ref Person person)
        {

            insertPerson(ref person);
            addTelefonBinding(ref tlf);

            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [har] (TelefonNummerID, PersonNummer, Type)
                                                    VALUES  (@TelefonNummerID, @PersonNummer, @Type)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.telefon.TelefonNummer);
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);
                cmd.Parameters.AddWithValue("@Type", tlf.Type);

                person.Telefoner.Add(tlf);

                cmd.ExecuteNonQuery();
            }
        }

        public void addTelefonBinding(ref TelefonBinding tlf)
        {
            // prepare command string using paramters in string and returning the given identity 
            string insertStringParam = @"INSERT INTO [Har] (TelefonNummerID, Type)
                                                    VALUES  (@TelefonNummerID, @Type)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.telefon.TelefonNummer);
                cmd.Parameters.AddWithValue("@Type", tlf.Type);
                cmd.ExecuteNonQuery();
            }
        }

        public void getTelefonOwner(ref TelefonBinding tlf, ref Person person)
        {
            // prepare command string using paramters in string and returning the given identity 
            string sqlcmd = @"SELECT * FROM [har] WHERE (TelefonNummerID= TelefonNummerID)";

            using (SqlCommand cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.telefon.TelefonNummer);


                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    tlf.telefon.TelefonNummer = (string)rdr["TelefonNummerID"];
                    tlf.Type = (string) rdr["Type"];
                    person.PersonNummer = (string) rdr["PersonNummer"];
                }
            }
        }

      

    }

}
