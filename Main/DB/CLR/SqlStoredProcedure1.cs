using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace DB.CLR
{
    public partial class StoredProcedures
    {
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void SqlStoredProcedure1 ()
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;Initial Catalog=DB;Integrated Security=SSPI;Enlist=false"))
            {
            }
        }

        [SqlProcedure()]
        public static void SelectData()
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;Initial Catalog=DB;Integrated Security=SSPI;Enlist=false"))
            {
                SqlCommand command = new SqlCommand();
            
                command.CommandText =
                    "Select 'Hello World!'";

                command.Connection = conn;

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
