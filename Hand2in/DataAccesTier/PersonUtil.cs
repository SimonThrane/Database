using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace DataAccesTier
//{
//    public class PersonDataUtil
//    {
//        private Person locPerson;
//        // Instantiate the connection
//        private SqlConnection conn;

//        public PersonDataUtil()
//        {
//            conn =
//                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hand2in;Integrated Security=True");
//        }

//        public void insertPerson(Person person)
//        {
//            try
//            {
//                // Open the connection
//                conn.Open();

                

//                // prepare command string using paramters in string and returning the given identity

//                string insertStringParam = @"INSERT INTO [Person] (Fornavn, Efternavn, Type, Fornavn, Mellemnavn, AdresseID)
//                                                    VALUES (@Data1, @Data2,@Data3,@Data4,@Data5,@Data6)";
                


//                using (SqlCommand cmd = new SqlCommand(insertStringParam, conn))
//                {
//                    // Get your parameters ready 
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1"; //Works even whit lower case "d"
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data6";
//                    cmd.Parameters["@Data1"].Value = person.PersonNummer; 
//                    cmd.Parameters["@Data2"].Value = person.Efternavn;
//                    cmd.Parameters["@Data3"].Value = person.Type;
//                    cmd.Parameters["@Data4"].Value = person.Fornavn;
//                    cmd.Parameters["@Data5"].Value = person.Mellemnavn;
//                    cmd.Parameters["@Data6"].Value = person.AdresseID;
                  
//                   cmd.ExecuteNonQuery(); 

//                    this.locPerson = person; //Make new Håndværker to currentHåndværker 

//                }

//            }
//            finally
//            {
//                // Close the connection
//                if (conn != null)
//                {
//                    conn.Close();
//                }
//            }
//        }

//        public void updateCurrentPerson(ref Person person)
//        {
//            // prepare command string
//            string updateString =
//               @"UPDATE Person
//                        SET PersonNummer= @PersonNummer, Efternavn = @Efternavn, Type = @Type, Fornavn = @Fornavn
//                        WHERE PersonNummer = @PersonNummer";

//            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
//            {
//                // Get your parameters ready 
//                cmd.Parameters.AddWithValue("@Ansættelsedato", hv.Ansættelsedato.Date);
//                cmd.Parameters.AddWithValue("@Fagområde", hv.Fagområde);
//                cmd.Parameters.AddWithValue("@Fornavn", hv.Fornavn);
//                cmd.Parameters.AddWithValue("@HåndværkerId", hv.HID);

//                var id = (int)cmd.ExecuteNonQuery();
//            }


//            try
//            {
//                // Open the connection
//                conn.Open();

//                // prepare command string
//                string updateString =
//                    @"UPDATE Person
//                        SET PersonNummer= @Data1, Efternavn = @Data2, Type = @Data3, Fornavn = @Data4
//                        WHERE PersonNummer = @Data5";
//                using (SqlCommand cmd = new SqlCommand(updateString, conn))
//                {
//                    // Get your parameters ready 
//                    cmd.Parameters.AddWithValue("@Data1", this.locPerson.Ansættelsedato.Date);
//                    //cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";
//                    //cmd.Parameters["@Data1"].Value = this.locPerson.Ansættelsedato.Date;                  
//                    cmd.Parameters["@Data2"].Value = this.locPerson.Efternavn;
//                    cmd.Parameters["@Data3"].Value = this.locPerson.Fagområde;
//                    cmd.Parameters["@Data4"].Value = this.locPerson.Fornavn;
//                    cmd.Parameters["@Data5"].Value = this.locPerson.HID;



//                    var id = (int) cmd.ExecuteNonQuery(); //Returns the numbers of tuples/records affected
//                }


//            }
//            finally
//            {
//                // Close the connection
//                if (conn != null)
//                {
//                    conn.Close();
//                }
//            }
//        }

//        public void deleteCurrentPerson()
//        {
//            try
//            {
//                // Open the connection
//                conn.Open();

//                // prepare command string
//                string deleteString =
//                   @"DELETE FROM Person
//                        WHERE (PersonNummer = @Data1)";
//                using (SqlCommand cmd = new SqlCommand(deleteString, conn))
//                {
//                    // Get your parameters ready 
//                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@data1";

//                    cmd.Parameters["@Data1"].Value = this.locPerson.PersonNummer;

//                    var id = (int)cmd.ExecuteNonQuery(); //Returns thenumber of tuple/record affected
//                    locPerson = null;

//                }
//            }
//            finally
//            {
//                // Close the connection
//                if (conn != null)
//                {
//                    conn.Close();
//                }
//            }

//        }



//    }

//    }
