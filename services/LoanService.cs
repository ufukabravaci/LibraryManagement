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
            string query = "INSERT INTO Loans (BookID, MemberID, LoanDate, DueDate) VALUES (@BookID, @MemberID, @LoanDate, @DueDate);";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("BookID", loan.BookID);
            command.Parameters.AddWithValue("MemberID", loan.MemberID);
            command.Parameters.AddWithValue("LoanDate", loan.LoanDate);
            command.Parameters.AddWithValue("DueDate", loan.DueDate);
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
            string query = "UPDATE Loans SET BookID=@BookID, MemberID = @MemberID , LoanDate = @LoanDate, ReturnDate = @ReturnDate, DueDate = @DueDate WHERE LoanID = @LoanID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("BookID", loan.BookID);
            command.Parameters.AddWithValue("MemberID", loan.MemberID);
            command.Parameters.AddWithValue("LoanDate", loan.LoanDate);
            command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate == null ? DBNull.Value : loan.ReturnDate); // NUllsa null gönder değilse değerini gönder.
            command.Parameters.AddWithValue("DueDate", loan.DueDate);
            command.Parameters.AddWithValue("LoanID", loan.LoanID);
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
            string query = "SELECT LoanID, BookID, MemberID, LoanDate, ReturnDate, DueDate FROM Loans";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Loan loan = new();
                loan.LoanID = Convert.ToInt32(reader["LoanID"]);
                loan.BookID = Convert.ToInt32(reader["BookID"]);
                loan.MemberID = Convert.ToInt32(reader["MemberID"]);
                loan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                if (reader["ReturnDate"] != DBNull.Value)
                {
                    loan.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                }
                loan.DueDate = Convert.ToDateTime(reader["DueDate"]);
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

    public Loan GetLoanById(int loanId)
    {
        Loan loan = new();
        try
        {
            string query = "SELECT * FROM Loans WHERE LoanID = @LoanID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@LoanID", loanId);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                loan.LoanID = Convert.ToInt32(reader["LoanID"]);
                loan.BookID = Convert.ToInt32(reader["BookID"]);
                loan.MemberID = Convert.ToInt32(reader["MemberID"]);
                loan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                if (reader["ReturnDate"] != DBNull.Value)
                {
                    loan.ReturnDate = Convert.ToDateTime(reader["ReturnDate"]);
                }
                loan.DueDate = Convert.ToDateTime(reader["DueDate"]);
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
        return loan;
    }

    //Bu metot databasede oluşturduğum CalculateLoanDuration fonksiyonunu çağırıyor.
    public List<Loan> GetShortTermLoans()
    {
        List<Loan> loans = new();
        try
        {
            string query = "SELECT LoanID FROM Loans WHERE dbo.fn_CalculateLoanDuration(LoanDate, ReturnDate) < 7";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();

            List<int> loanIds = new();
            while (reader.Read())
            {
                loanIds.Add(reader.GetInt32(0));
            }
            reader.Close();

            foreach (int id in loanIds)
            {
                Loan loan = GetLoanById(id);
                loans.Add(loan);
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
        return loans;
    }
    public List<Loan> GetCurrentLoans()
    {
        List<Loan> loans = new();
        try
        {
            string query = "SELECT LoanID, BookID, MemberID,LoanDate,DueDate from Loans l WHERE l.ReturnDate IS NULL";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Loan loan = new();
                loan.LoanID = Convert.ToInt32(reader["LoanID"]);
                loan.BookID = Convert.ToInt32(reader["BookID"]);
                loan.MemberID = Convert.ToInt32(reader["MemberID"]);
                loan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                loan.DueDate = Convert.ToDateTime(reader["DueDate"]);
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