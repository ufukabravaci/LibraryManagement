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

    public Author? GetAuthorById(int authorId)
    {
        // bulamazsak null dönmesi lazım. Yeni author nesnesi oluşturursak bulamama durumunda da bir nesne dönüyoruz.
        Author? author = null;
        try
        {
            string query = "SELECT * FROM Authors WHERE AuthorID = @AuthorID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@AuthorID", authorId);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                author = new Author
                {
                    AuthorId = Convert.ToInt32(reader["AuthorID"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString()
                };
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            System.Console.WriteLine("Veritabanı hatası: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return author;
    }

    // View kullanmaktadır. Vıew aşağıdaki gibidir.
    //     SELECT TOP 10 A.AuthorID, A.FirstName, A.LastName, COUNT(L.BookID) AS TotalLoans
    // FROM Authors A
    // INNER JOIN BookAuthor BA ON A.AuthorID = BA.AuthorID
    // INNER JOIN Loans L ON BA.BookID = L.BookID
    // GROUP BY A.AuthorID, A.FirstName, A.LastName
    // ORDER BY TotalLoans DESC;
    public List<AuthorLoanCount> GetMostLoaned10Authors()
    {
        List<AuthorLoanCount> authors = new();
        try
        {
            string query = "SELECT * FROM vw_MostLoanedTenBooks";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                AuthorLoanCount author = new();
                author.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                author.FirstName = reader["FirstName"].ToString();
                author.LastName = reader["LastName"].ToString();
                author.TotalLoans = Convert.ToInt32(reader["TotalLoans"]);
                authors.Add(author);
            }
            reader.Close();
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