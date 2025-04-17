using LibraryManagement.Models;
using LibraryManagement.Utils;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Services;

public class MemberService
{
    DB _db;
    public MemberService(DB db)
    {
        _db = db;
    }

    public int AddMember(Member member)
    {
        int result = 0;
        try
        {
            string query = "INSERT INTO MEMBERS (FirstName, LastName, Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone);";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("FirstName", member.FirstName);
            command.Parameters.AddWithValue("LastName", member.LastName);
            command.Parameters.AddWithValue("Email", member.Email);
            command.Parameters.AddWithValue("Phone", member.Phone);
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

    public int UpdateMember(Member member)
    {
        int result = 0;
        try
        {
            string query = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone WHERE MemberID = @MemberID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("FirstName", member.FirstName);
            command.Parameters.AddWithValue("LastName", member.LastName);
            command.Parameters.AddWithValue("Email", member.Email);
            command.Parameters.AddWithValue("Phone", member.Phone);
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

    public int DeleteMember(int id)
    {
        int result = 0;
        try
        {
            string query = "DELETE FROM MEMBERS WHERE MemberID= @MemberID";
            SqlCommand command = new SqlCommand(query, _db.GetConnection());
            command.Parameters.AddWithValue("@MemberID", id);
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
    public List<Member> GetAllMembers()
    {
        List<Member> members = new();
        try
        {
            string query = "SELECT * FROM Members";
            SqlCommand command = new(query, _db.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Member member = new();
                member.MemberId = Convert.ToInt32(reader["MemberID"]);
                member.FirstName = reader["FirstName"].ToString();
                member.LastName = reader["LastName"].ToString();
                member.Email = reader["Email"].ToString();
                member.Phone = reader["Phone"].ToString();
                members.Add(member);
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
        return members;
    }

    public Member GetMemberById(int memberID)
    {
        Member member = new();
        try
        {
            string query = "SELECT * FROM Members WHERE MemberID = @MemberID";
            SqlCommand command = new(query, _db.GetConnection());
            command.Parameters.AddWithValue("@MemberID", memberID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                member.MemberId = Convert.ToInt32(reader["MemberID"]);
                member.FirstName = reader["FirstName"].ToString();
                member.LastName = reader["LastName"].ToString();
                member.Email = reader["Email"].ToString();
                member.Phone = reader["Phone"].ToString();
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
        return member;
    }

    // Bu metot database'de tanımladığım bir fonksiyonu kullanmaktadır.
    public string GetMemberFullNameByID(int memberID)
    {
        string? fullName = "";
        try
        {
            string query = "SELECT dbo.fn_GetFullName(FirstName, LastName) fullName FROM Members WHERE MemberID = @MemberID";
            SqlCommand command = new SqlCommand(query, _db.GetConnection());
            command.Parameters.AddWithValue("@MemberID", memberID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                fullName = reader["fullName"].ToString();
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
        return fullName !=null ? fullName : "";
    }
}