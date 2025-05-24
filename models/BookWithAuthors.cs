namespace LibraryManagement.Models;

public class BookWithAuthors
{
    public int BookID { get; set; }
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public int PublicationYear { get; set; }
    public int AuthorID { get; set; }
    public string? AuthorName { get; set; }
    public string? AuthorLastName { get; set; }

}