using LibraryManagement.Models;
using LibraryManagement.Utils;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Services;

public class AuthorService
{
    DB _db;
    public AuthorService(DB db)
    {
        _db = db;
    }

    public int AddAuthor(Author author)
    {
        int result = 0;
        try
        {
            string query = "INSERT INTO Authors (FirstName, LastName) VALUES (@FirstName, @LastName);";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@FirstName", author.FirstName);
            command.Parameters.AddWithValue("@LastName", author.LastName);
            result = command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            System.Console.WriteLine("Veritabanı hatası: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return result;
    }
    public int UpdateAuthor(Author author)
    {
        int result = 0;
        try
        {
            string query = "UPDATE AUTHORS SET FirstName = @FirstName, LastName = @LastName WHERE AuthorID = @AuthorID;";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@FirstName", author.FirstName);
            command.Parameters.AddWithValue("@LastName", author.LastName);
            command.Parameters.AddWithValue("@AuthorID", author.AuthorId);
            result = command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            System.Console.WriteLine("Veritabanı hatası: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return result;
    }

    public int DeleteAuthor(int authorId)
    {
        int result = 0;
        try
        {
            string query = "DELETE FROM AUTHORS WHERE AuthorID = @authorID;";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@authorID", authorId);
            result = command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            System.Console.WriteLine("Veritabanı hatası: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return result;
    }

    public List<Author> GetAllAuthors()
    {
        List<Author> authors = new();
        try
        {
            string query = "SELECT * FROM AUTHORS";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Author author = new();
                author.AuthorId = Convert.ToInt32(reader["AuthorID"]);
                author.FirstName = reader["FirstName"].ToString();
                author.LastName = reader["LastName"].ToString();
                authors.Add(author);
            }
        }
        catch (SqlException ex)
        {
            System.Console.WriteLine("Veritabanı hatası: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return authors;
    }

}