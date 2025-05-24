namespace LibraryManagement.Models;

public class AuthorLoanCount
{
    public int AuthorID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int TotalLoans { get; set; }
   
}