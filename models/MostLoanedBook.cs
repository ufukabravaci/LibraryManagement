namespace LibraryManagement.Models;

public class MostLoanedBook
{
    public int BookId { get; set; }
    public string? Title { get; set; }
    public int LoanCount { get; set; }
    public string? Author { get; set; }

}
