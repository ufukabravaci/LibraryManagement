using System.Data;
using LibraryManagement.Models;
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
            result = command.ExecuteNonQuery(); // id'yi aldık.
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

    //Bu metot Procedure kullanarak kitap bilgilerini getirir.
    public Book GetBookById(int bookId)
    {
        Book book = new();
        try
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = "sp_GetBookDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = _db.GetConnection()
            };
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
                Book book = new Book //veri okumak için oluşturduğumuz id'li ctor kullandık
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

    //Bu metot yazdığım uygulamadaki en karışık query'i kullanıyor.
    //Database'de tanımlı olan bir fonksiyonu kullanıyor.
    //Veriyi karşılayabilmek için yeni bir model oluşturdum.
    public List<MostLoanedBook> Get10MostLoanedBooks()
    {
        List<MostLoanedBook> books = new();
        try
        {
            string query = @"
        SELECT TOP 10 
            l.BookID, 
            COUNT(l.BookID) AS LoanCount, 
            b.Title, 
            b.PublishYear,
            b.ISBN,
            dbo.fn_GetFullName(a.FirstName, a.LastName) AS Author
        FROM Loans l
        INNER JOIN Books b ON l.BookID = b.BookID
        INNER JOIN BookAuthor ba ON b.BookID = ba.BookID
        INNER JOIN Authors a ON ba.AuthorID = a.AuthorID
        GROUP BY 
            l.BookID, 
            b.Title, 
            b.PublishYear,
            b.ISBN,
            a.FirstName, 
            a.LastName
        ORDER BY LoanCount DESC";

            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MostLoanedBook book = new();
                book.BookId = Convert.ToInt32(reader["BookID"]);
                book.Title = reader["Title"].ToString();
                book.LoanCount = Convert.ToInt32(reader["LoanCount"]);
                book.Author = reader["Author"].ToString();
                books.Add(book);
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Veritabanı hatası: " + ex.Message);
        }
        finally
        {
            _db.CloseConnection();
        }

        return books;
    }

    // BU metot database'de tanımlı bir view kullanıyor. Bu view kitap ve yazar bilgilerini birleştiriyor
    public List<BookWithAuthors> GetBooksWithAuthors()
    {
        List<BookWithAuthors> books = new();
        try
        {
            string query = "SELECT * FROM vw_BooksWithAuthors";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                BookWithAuthors book = new();
                book.BookID = Convert.ToInt32(reader["BookID"]);
                book.Title = reader["Title"].ToString();
                book.ISBN = reader["ISBN"].ToString();
                book.PublicationYear = Convert.ToInt32(reader["PublicationYear"]);
                book.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                book.AuthorName = reader["AuthorName"].ToString();
                book.AuthorLastName = reader["AuthorLastName"].ToString();
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