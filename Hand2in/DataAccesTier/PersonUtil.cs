using System;
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
                        SET Telefon= @Telefon, AdresseID=@AdresseID Efternavn = @Efternavn, Type = @Type, Fornavn = @Fornavn, Mellemnavn=@Mellemnavn
                        WHERE PersonNummer = @PersonNummer";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Telefon", person.Telefon);
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

        public List<Telefon> getPersons_Telefon(ref Person person)
        {
            string selectToolboxTelefonString = @"SELECT *
                                                  FROM [har] 
                                                  WHERE ([PersonNummer] = @PersonNummer)";
            using (var cmd = new SqlCommand(selectToolboxTelefonString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);

                rdr = cmd.ExecuteReader();
                List<Telefon> telefons = new List<Telefon>();

                Telefon tlf = null;
                while (rdr.Read())
                {
                    tlf = new Telefon(); // 

                    tlf.TelefonNummer = (string) rdr["TelefonNummerID"];
                    tlf.Type= (string) rdr["Type"];
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





    }

}
