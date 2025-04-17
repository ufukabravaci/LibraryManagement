namespace LibraryManagement.Models;

using System;

public class Loan
{
    public int LoanID { get; set; }
    public int BookID { get; set; }
    public int MemberID { get; set; }
    public DateTime LoanDate { get; set; } //Kiralama zamanı oluşturulacak tarih
    public DateTime? ReturnDate { get; set; } // ReturnDate kitap iade edildiğinde girilecek tarih. O tarihe kadar null.

    public DateTime DueDate {get; set;} //Kitabın iade edilmesi gereken tarih. LoanDate +1 Ay

    public Loan() 
    {
    }

    // Loan kaydederken id'siz ctor
    public Loan(int bookId, int memberId, DateTime loanDate, DateTime? returnDate, DateTime dueDate)
    {
        BookID = bookId;
        MemberID = memberId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        DueDate = dueDate;
    }

    // Loan okurken id'li ctor
    public Loan(int loanId, int bookId, int memberId, DateTime loanDate, DateTime? returnDate, DateTime dueDate)
    {
        LoanID = loanId;
        BookID = bookId;
        MemberID = memberId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        DueDate = dueDate;
    }
}