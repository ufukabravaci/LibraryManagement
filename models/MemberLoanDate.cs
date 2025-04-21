using System;
namespace LibraryManagement.Models;
public class MemberLoanDate
{
    public int MemberID { get; set; }
    public string? MemberFirstName { get; set; }
    public string? MemberLastName { get; set; }
    public DateTime LoanDate { get; set; }

    // Boş constructor
    public MemberLoanDate()
    {
    }

    // Tüm parametreleri alan constructor
    public MemberLoanDate(int memberID, string memberFirstName, string memberLastName, DateTime loanDate)
    {
        MemberID = memberID;
        MemberFirstName = memberFirstName;
        MemberLastName = memberLastName;
        LoanDate = loanDate;
    }
}