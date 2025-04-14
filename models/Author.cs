namespace LibraryManagement.Models;
public class Author
{
    public int AuthorId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public Author()
    {
    }

    // Yazar eklerken id'siz
    public Author(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    // id'li
    public Author(int authorId, string firstName, string lastName)
    {
        AuthorId = authorId;
        FirstName = firstName;
        LastName = lastName;
    }
}