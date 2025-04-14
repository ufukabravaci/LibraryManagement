namespace LibraryManagement.Models;

public class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }

    public BookAuthor(int bookId, int authorId)
    {
        BookId = bookId;
        AuthorId = authorId;
    }
    public BookAuthor()
    {
    }
}