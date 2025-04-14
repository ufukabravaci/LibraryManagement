namespace LibraryManagement.Services;
public class Book
{
    public int BookId { get; set; }
    public string? Title { get; set; } 
    public string? ISBN { get; set; }
    public int PublishYear { get; set; }

    public Book()
    {
    }

    // Yeni kayıt eklerken kullanmak amacıyla id bulunmayan constructor
    public Book(string title, string isbn, int publishYear)
    {
        Title = title;
        ISBN = isbn;
        PublishYear = publishYear;
    }

    // Veri tabanından gelen veriyi okumak için veya update için lazım olan id'li ctor.
    public Book(int bookId, string title, string isbn, int publishYear, string genre)
    {
        BookId = bookId;
        Title = title;
        ISBN = isbn;
        PublishYear = publishYear;
    }
}