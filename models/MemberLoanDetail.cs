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

    // Tüm parametreleri alan constructor
    public MemberLoanDetail(int memberID, int loanID, int bookID, DateTime loanDate, DateTime? returnDate, DateTime dueDate, string bookTitle, string authorFirstName, string authorLastName)
    {
        MemberID = memberID;
        LoanID = loanID;
        BookID = bookID;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        DueDate = dueDate;
        BookTitle = bookTitle;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
    }
}