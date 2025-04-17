using LibraryManagement.Utils;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Services;

public class BookService
{
    DB _db;
    public BookService(DB db)
    {
        _db = db;
    }

    public int AddBook(Book book)
    {
        int result = 0;
        try
        {
            string query = "INSERT INTO Books (Title, PublishYear, ISBN) VALUES (@Title, @PublishYear, @ISBN); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@PublishYear", book.PublishYear);
            command.Parameters.AddWithValue("@ISBN", book.ISBN);
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
    public int DeleteBook(int id)
    {
        int result = 0;
        try
        {
            string query = "DELETE FROM BOOKS WHERE BookID = @BookId";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@BookId", id);
            result = Convert.ToInt32(command.ExecuteScalar()); // id'yi aldık.
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
    public int UpdateBook(Book book)
    {
        int result = 0;
        try
        {
            string query = "UPDATE BOOKS SET Title = @Title, ISBN = @ISBN, PublishYear = @PublishYear WHERE BookID = @BookID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@ISBN", book.ISBN);
            command.Parameters.AddWithValue("@PublishYear", book.PublishYear);
            command.Parameters.AddWithValue("@BookID", book.BookId);
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
    public List<Book> GetAllBooks()
    {
        List<Book> books = new();
        try
        {
            string query = "SELECT * FROM Books";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Book book = new();
                book.BookId = Convert.ToInt32(reader["BookID"]);
                book.Title = reader["Title"].ToString();
                book.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                book.ISBN = reader["ISBN"].ToString();
                books.Add(book);
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
        return books;
    }

    public Book GetBookById(int bookId)
    {
        Book book = new();
        try
        {
            string query = "SELECT * FROM Books WHERE BookID = @BookID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@BookID", bookId);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                book.BookId = Convert.ToInt32(reader["BookID"]);
                book.Title = reader["Title"].ToString();
                book.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                book.ISBN = reader["ISBN"].ToString();
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
        return book;
    }

    public List<Book> GetBooksByTitle(string title)
    {
        List<Book> books = new List<Book>();
        try
        {
            string query = "SELECT * FROM Books WHERE Title LIKE @Title";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@Title", "%" + title + "%");
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Book book = new Book
                {
                    BookId = Convert.ToInt32(reader["BookID"]),
                    Title = reader["Title"].ToString(),
                    PublishYear = Convert.ToInt32(reader["PublishYear"]),
                    ISBN = reader["ISBN"].ToString()
                };
                books.Add(book);
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
        return books;
    }
}