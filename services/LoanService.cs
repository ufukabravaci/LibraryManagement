using LibraryManagement.Models;
using LibraryManagement.Utils;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Services;

public class LoanService
{
    DB _db;
    public LoanService(DB db)
    {
        _db = db;
    }

    public int AddLoan(Loan loan)
    {
        int result = 0;
        try
        {
            string query = "INSERT INTO Loans (BookID, MemberID, LoanDate, ReturnDate) VALUES (@BookID, @MemberID, @LoanDate, @ReturnDate);";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("BookID", loan.BookId);
            command.Parameters.AddWithValue("MemberID", loan.MemberId);
            command.Parameters.AddWithValue("LoanDate", loan.LoanDate);
            command.Parameters.AddWithValue("ReturnDate", loan.ReturnDate);
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

    public int UpdateLoan(Loan loan)
    {
        int result = 0;
        try
        {
            string query = "UPDATE Loans SET MemberID = @MemberID , LoanDate = @LoanDate, ReturnDate = @ReturnDate WHERE BookID = @BookID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("BookID", loan.BookId);
            command.Parameters.AddWithValue("MemberID", loan.MemberId);
            command.Parameters.AddWithValue("LoanDate", loan.LoanDate);
            command.Parameters.AddWithValue("ReturnDate", loan.ReturnDate);
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

    public int DeleteLoan(int id)
    {
        int result = 0;
        try
        {
            string query = "DELETE FROM LOANS WHERE  LoanID= @LoanID";
            SqlCommand command = new SqlCommand(query, _db.GetConnection());
            command.Parameters.AddWithValue("@LoanID", id);
            result = command.ExecuteNonQuery(); // etkilenen satır sayısını döndürür
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
    public List<Loan> GetAllLoans()
    {
        List<Loan> loans = new();
        try
        {
            string query = "SELECT LoanID, BookID, MemberID, LoanDate, ReturnDate FROM Loans";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Loan loan = new();
                loan.LoanId = Convert.ToInt32(reader["LoanID"]);
                loan.BookId = Convert.ToInt32(reader["BookID"]);
                loan.MemberId = Convert.ToInt32(reader["MemberID"]);
                loan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                loan.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                loans.Add(loan);
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
        return loans;
    }
}