using System;
namespace LibraryManagement.Models;
public class MemberLoanDetail
{
    public int MemberID { get; set; }
    public int LoanID { get; set; }
    public int BookID { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Nullable DateTime
    public DateTime DueDate { get; set; }
    public string? BookTitle { get; set; }
    public string? AuthorFirstName { get; set; }
    public string? AuthorLastName { get; set; }

    // Boş constructor
    public MemberLoanDetail()
    {
        ReturnDate = null; // Hata oluşmasını engellemek amacıyla returnDate başlangıçta null 
    }

}