using Microsoft.Data.SqlClient;

namespace LibraryManagement.Utils;

public class DB
{
    static string _connectionString = "Server=.; Database=LibraryManagement; Integrated Security=True; TrustServerCertificate=True;";
    SqlConnection _connection = new(_connectionString);

    public SqlConnection GetConnection()
    {
        try
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
                System.Console.WriteLine("Connection Opened.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        return _connection;
    }
    public void CloseConnection()
    {
        try {
          if(_connection.State == System.Data.ConnectionState.Open)
          {
            _connection.Close();
            System.Console.WriteLine("Connection Closed.");
          }
        } catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);

        }
    }
}