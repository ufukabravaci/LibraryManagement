using System;
namespace LibraryManagement.Models;
public class MemberLoanDate
{
    public int MemberID { get; set; }
    public string? MemberFirstName { get; set; }
    public string? MemberLastName { get; set; }
    public DateTime LoanDate { get; set; }

}