using SinglyLinkedLists.Facades.SqlDatabase.Contracts;
using System.Configuration;
using System.Data.SqlClient;

namespace SinglyLinkedLists.Facades.SqlDatabase
{
    public class SqlDatabase : ISqlDatabase
    {
        private string _connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SinglyLinkedLists;Integrated Security=true";

        public void AddNode(string nodeName)
        {
           // SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "insert into dbo.Nodes(name) Values (@name)";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@name", nodeName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}