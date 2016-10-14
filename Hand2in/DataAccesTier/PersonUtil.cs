using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
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
                var con = new SqlConnection(@"Data Source=I4DAB.ASE.AU.DK;Initial Catalog=E16I4DABH2Gr12; User ID=E16I4DABH2Gr12;Password=E16I4DABH2Gr12");
                //var con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hand2in;Integrated Security=True");
                con.Open();
                return con;
            }
        }


        public void insertPerson(ref Person person)
        {

            string commandstring = @"INSERT INTO [Adresse] (Bynavn,Husnummer,Postnummer,Vejnavn)
                                    OUTPUT INSERTED.AdresseID
                                    VALUES (@Bynavn,@Husnummer,@Postnummer,@Vejnavn)";
            long AdresseId;

            using (SqlCommand cmd = new SqlCommand(commandstring, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Bynavn", person.Adresse.Bynavn);
                cmd.Parameters.AddWithValue("@Husnummer", person.Adresse.Husnummer);
                cmd.Parameters.AddWithValue("@Postnummer", person.Adresse.Postummer);
                cmd.Parameters.AddWithValue("@Vejnavn", person.Adresse.Vejnavn);
                AdresseId = (long)cmd.ExecuteScalar();
            }


                // prepare command string using paramters in string and returning the given identity

                string insertStringParam = @"INSERT INTO [Person] (PersonNummer,Fornavn, Efternavn, Type, Mellemnavn,AdresseID)
                                                    VALUES (@PersonNummer,@Fornavn, @Efternavn, @Type, @Mellemnavn, @AdresseID)";

               using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@PersonNummer",person.PersonNummer);
                    cmd.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                    cmd.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                    cmd.Parameters.AddWithValue("@Type", person.Type);
                    cmd.Parameters.AddWithValue("@Mellemnavn", person.Mellemnavn);
                    cmd.Parameters.AddWithValue("@AdresseID", AdresseId);

                cmd.ExecuteNonQuery();
                }

            foreach (var item in person.Adresser)
            {
                var temp = item;
                InsertAdresse(ref temp,ref person);
            }

            foreach (var item in person.Telefoner)
            {
                var temp = item;
                InsertTelefon(ref temp, ref person);
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
                    tlf = new TelefonBinding
                    {
                        telefon = new Telefon(),
                        Type = (string) rdr["Type"]
                    };
                    tlf.telefon.TelefonNummer = (rdr["TelefonNummerID"].ToString());
                    telefons.Add(tlf);
                }
                return telefons;
            }
        }

        public List<AdresseBinding> getPersons_Adresse(ref Person person)
        {
            string selectToolboxAdresseString = @"SELECT *
                                                  FROM [erPaa] 
                                                  WHERE ([PersonNummer] = @PersonNummer)";
            using (var cmd = new SqlCommand(selectToolboxAdresseString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);

                rdr = cmd.ExecuteReader();
                List<AdresseBinding> adresses = new List<AdresseBinding>();

                AdresseBinding adr = null;
                while (rdr.Read())
                {
                    adr = new AdresseBinding
                    {
                        adresse = new Adresse(),
                        Type = (string) rdr["Type"]
                    }; 

                    adr.adresse.AdresseId = int.Parse(rdr["AdresseID"].ToString());
                    adresses.Add(adr);
                }

                foreach (var item in adresses)
                {
                    string getAdresse = "SELECT * FROM [Adresse] WHERE AdresseID = " + item.adresse.AdresseId;
                    using (SqlCommand cmd2 = new SqlCommand(getAdresse, OpenConnection))
                    {
                        SqlDataReader rdr2 = null;
                        rdr2 = cmd2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            item.adresse.Bynavn = (string) rdr2["Bynavn"];
                            item.adresse.Husnummer = (string) rdr2["Husnummer"];
                            item.adresse.Postummer = (int) rdr2["Postnummer "];
                            item.adresse.Vejnavn = (string) rdr2["Vejnavn"];
                        }
                    }
                }

                return adresses;
            }
        }

        public void InsertTelefon(ref TelefonBinding tlf, ref Person person)
        {
            string insertStringParam = @"INSERT INTO [Telefon] (TelefonNummerID)
                                                    VALUES  (@TelefonNummerID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.telefon.TelefonNummer);
                cmd.ExecuteNonQuery();
            }

            string insertStringParam1 = @"INSERT INTO [har] (TelefonNummerID, PersonNummer, Type)
                                                    VALUES  (@TelefonNummerID, @PersonNummer, @Type)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam1, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.telefon.TelefonNummer);
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);
                cmd.Parameters.AddWithValue("@Type", tlf.Type);
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


        public void InsertAdresse(ref AdresseBinding adressebinding, ref Person person)
        {

            string commandstring = @"INSERT INTO [Adresse] (Bynavn,Husnummer,Postnummer,Vejnavn)
                                    OUTPUT INSERTED.AdresseID
                                    VALUES (@Bynavn,@Husnummer,@Postnummer,@Vejnavn)";
            long AdresseId;

            using (SqlCommand cmd = new SqlCommand(commandstring, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Bynavn", adressebinding.adresse.Bynavn);
                cmd.Parameters.AddWithValue("@Husnummer", adressebinding.adresse.Husnummer);
                cmd.Parameters.AddWithValue("@Postnummer", adressebinding.adresse.Postummer);
                cmd.Parameters.AddWithValue("@Vejnavn", adressebinding.adresse.Vejnavn);
                AdresseId = (long)cmd.ExecuteScalar();
            }


            string commandstring2 = @"INSERT INTO [erPaa] (AdresseID, PersonNummer, Type)
                                    VALUES (@AdresseID,@PersonNummer,@Type)";

            using (SqlCommand cmd = new SqlCommand(commandstring2, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdresseID", AdresseId);
                cmd.Parameters.AddWithValue("@Type", adressebinding.Type);
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);
                cmd.ExecuteNonQuery();
            }
        }



        public void UpdateTelefonBinding(ref TelefonBinding tlf, ref Person person)
        {
            // prepare command string
            string updateString =
                @"UPDATE [har]
                        SET Type=@Type, PersonNummer = @PersonNummer
                        WHERE TelefonNummerID=@TelefonNummer";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Type", tlf.Type);
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.telefon.TelefonNummer);
                
                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCurrentTelefon(ref TelefonBinding tlf, ref Person person)
        {
            DeleteTelefon(tlf.telefon);

            string deleteString = @"DELETE FROM [har] WHERE (PersonNummer = @PersonNummer)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);

                var id = (int)cmd.ExecuteNonQuery();
                tlf = null;
            }

            

        }
        public void DeleteTelefon(Telefon tlf)
        {
            string deleteString = @"DELETE FROM [Telefon] WHERE (TelefonNummerID = @TelefonNummerID)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@TelefonNummerID", tlf.TelefonNummer);

                var id = (int)cmd.ExecuteNonQuery();
                tlf = null;
            }
        }

        public void UpdateAdresse(ref Adresse adr)
        {
            // prepare command string
            string updateString =
                @"UPDATE [Adresse]
                        SET Vejnavn = @Vejnavn,Husnummer = @Husnummer, Postnummer = @Postnummer, Bynavn = @Bynavn 
                        WHERE AdresseID=@AdresseID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Vejnavn", adr.Vejnavn);
                cmd.Parameters.AddWithValue("@Husnummer", adr.Husnummer);
                cmd.Parameters.AddWithValue("@Postnummer", adr.Postummer);
                cmd.Parameters.AddWithValue("@Bynavn", adr.Bynavn);
                cmd.Parameters.AddWithValue("@AdresseID", adr.AdresseId);
                
                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAdresseBinding(ref AdresseBinding adr, ref Person person)
        {
            // prepare command string
            string updateString =
                @"UPDATE [erPaa]
                        SET PersonNummer = @PersonNummer,Type = @Type
                        WHERE AdresseID=@AdresseID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@PersonNummer", person.PersonNummer);
                cmd.Parameters.AddWithValue("@Type", adr.Type);
                cmd.Parameters.AddWithValue("@AdresseID", adr.adresse.AdresseId);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAdresse(ref Adresse adr)
        {
            string deleteString = @"DELETE FROM [Adresse] WHERE (AdresseID = @AdresseID)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdresseID", adr.AdresseId);

                var id = (int)cmd.ExecuteNonQuery();
                adr = null;
            }
        }

        public void DeleteAdresseBinding(ref AdresseBinding adr)
        {
            string deleteString = @"DELETE FROM [erPaa] WHERE (AdresseID = @AdresseID)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AdresseID", adr.adresse.AdresseId);

                cmd.ExecuteNonQuery();
                adr = null;
            }
        }

        public void DeleteEverything()
        {
            string deleteString = @"DELETE FROM [Person] DELETE FROM [Telefon] DELETE FROM [Adresse] ";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

}
