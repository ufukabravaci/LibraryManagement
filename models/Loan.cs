namespace LibraryManagement.Models;

using System;

public class Loan
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; } // ReturnDate null olabilir

    public Loan() 
    {
    }

    // Loan kaydederken id'siz ctor
    public Loan(int bookId, int memberId, DateTime loanDate, DateTime? returnDate)
    {
        BookId = bookId;
        MemberId = memberId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
    }

    // Loan okurken id'li ctor
    public Loan(int loanId, int bookId, int memberId, DateTime loanDate, DateTime? returnDate)
    {
        LoanId = loanId;
        BookId = bookId;
        MemberId = memberId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
    }
}