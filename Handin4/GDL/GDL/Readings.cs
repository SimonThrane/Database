using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GDL.Model
{
    public class Readings : List<Reading>
    {
        public DataTable GetReadingsAsDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ReadingId", typeof(int));
            table.Columns.Add("sensorId",typeof(int));
            table.Columns.Add("appartmentId", typeof(int));
            table.Columns.Add("value", typeof(float));
            table.Columns.Add("timestamp", typeof(DateTime));

            foreach (var item in this)
            {
                table.Rows.Add(item.ReadingId, item.sensorId, item.appartmentId, item.value, item.timestamp);
            }
            return table;
        }

        public static void AddItemsToDatabase(Readings items)
        {
            // construct sql connection and sql command objects.
            using (SqlConnection sqlcon = new SqlConnection("Data Source=LAPTOP-CIPK7L70\\SQLEXPRESS;Initial Catalog=GDL.Model.GDLContext;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True"))
            {
                using (SqlCommand cmd = new SqlCommand("InsertData", sqlcon))
                {
                    // add the table-valued-parameter. 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Readings", SqlDbType.Structured).Value = items.GetReadingsAsDataTable();
                    // execute
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}