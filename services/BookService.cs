using System.IO.Pipelines;
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
            System.Console.WriteLine("Error :" + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return result;
    }
    public int DeleteBook(Book book)
    {
        int result = 0;
        try
        {
            string query = "DELETE FROM BOOKS WHERE BookID = @BookId";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@BookId", book.BookId);
            result = Convert.ToInt32(command.ExecuteScalar()); // id'yi aldık.
            System.Console.WriteLine($"{book.Title} isimli kitap kütüphaneden silindi.");
        }
        catch(SqlException ex)
        {
            System.Console.WriteLine($"{book.Title} Eklenirken bir hata oluştu. " + ex.Message);
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
            System.Console.WriteLine($"{book.Title} güncellendi.");
        }
        catch (SqlException ex)
        {
            System.Console.WriteLine($"{book.Title} güncellenemedi. Error: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }
        return result;
    }

}