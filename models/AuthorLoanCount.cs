namespace LibraryManagement.Models;

public class AuthorLoanCount
{
    public int AuthorID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int TotalLoans { get; set; }

    public AuthorLoanCount()
    {
    }

    public AuthorLoanCount(int authorID, string firstName, string lastName, int totalLoans)
    {
        AuthorID = authorID;
        FirstName = firstName;
        LastName = lastName;
        TotalLoans = totalLoans;
    }
}