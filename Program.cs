using LibraryManagement.Utils;
using System;
using LibraryManagement.Services;
using LibraryManagement.Models;

var db = new DB();
bool isMainMenuActive = true;

while (isMainMenuActive)
{
    Console.WriteLine("----------------------------------");
    Console.WriteLine("          YÖNETİM PANELİ          ");
    Console.WriteLine("----------------------------------");
    Console.WriteLine("1 - Kitap İşlemleri");
    Console.WriteLine("2 - Yazar İşlemleri");
    Console.WriteLine("3 - Kira İşlemleri");
    Console.WriteLine("4 - Üye İşlemleri");
    Console.WriteLine("5 - Çıkış");
    Console.Write("Lütfen bir işlem seçin: ");

    string? mainMenuSelection = Console.ReadLine();

    switch (mainMenuSelection)
    {
        case "1":
            BookOperationsMenu();
            break;
        case "2":
            AuthorOperationsMenu();
            break;
        case "3":
            LoanOperationsMenu();
            break;
        case "4":
            MemberOperationsMenu();
            break;
        case "5":
            isMainMenuActive = false;
            Console.WriteLine("Uygulamadan çıkılıyor...");
            break;
        default:
            Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
            break;
    }

    Console.WriteLine();
}

void BookOperationsMenu()
{
    bool isBookMenuActive = true;
    BookService bookService = new(db);

    while (isBookMenuActive)
    {
        Console.WriteLine("----------------------------------");
        Console.WriteLine("        KİTAP İŞLEMLERİ          ");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("1 - Kitap Ekle");
        Console.WriteLine("2 - Kitap Sil");
        Console.WriteLine("3 - Kitap Bilgilerini Güncelle");
        Console.WriteLine("4 - Bütün Kitapları Getir");
        Console.WriteLine("5 - ID ile Kitap Getir");
        Console.WriteLine("6 - Kitap İsmi ile Kitap Getir");
        Console.WriteLine("7 - Ana Menüye Dön");
        Console.Write("Lütfen bir işlem seçin: ");

        string? bookMenuSelection = Console.ReadLine();

        switch (bookMenuSelection)
        {
            case "1":
                AddBook(bookService);
                break;
            case "2":
                DeleteBook(bookService);
                break;
            case "3":
                UpdateBookInformation(bookService);
                break;
            case "4":
                GetAllBooks(bookService);
                break;
            case "5":
                GetBookById(bookService);
                break;
            case "6":
                GetBooksByTitle(bookService);
                break;
            case "7":
                isBookMenuActive = false;
                Console.WriteLine("Ana menüye dönülüyor...");
                break;
            default:
                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                break;
        }
        Console.WriteLine();
    }
}

void AddBook(BookService service)
{
    Console.WriteLine("Kitap Ekleme İşlemi");
    Console.Write("Kitap Adı: ");
    string? title = Console.ReadLine();
    Console.Write("ISBN: ");
    string? isbn = Console.ReadLine();
    Console.Write("Yayın Yılı: ");
    if (int.TryParse(Console.ReadLine(), out int publishYear))
    {
        Book newBook = new Book { Title = title, ISBN = isbn, PublishYear = publishYear };
        int result = service.AddBook(newBook);
        if (result > 0)
            Console.WriteLine("Kitap başarıyla eklendi.");
        else
            Console.WriteLine("Kitap eklenirken bir hata oluştu.");
    }
    else
    {
        Console.WriteLine("Geçersiz yayın yılı formatı.");
    }
}

void DeleteBook(BookService service)
{
    Console.WriteLine("Kitap Silme İşlemi");
    Console.Write("Silmek istediğiniz kitabın ID'sini girin: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        int result = service.DeleteBook(id);
        if (result > 0)
            Console.WriteLine($"ID: {id} olan kitap başarıyla silindi.");
        else
            Console.WriteLine($"ID: {id} olan kitap bulunamadı veya silinirken bir hata oluştu.");
    }
    else
    {
        Console.WriteLine("Geçersiz ID formatı.");
    }
}

void UpdateBookInformation(BookService service)
{
    Console.WriteLine("Kitap Bilgilerini Güncelleme İşlemi");
    Console.Write("Güncellemek istediğiniz kitabın ID'sini girin: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Console.Write("Yeni Kitap Adı: ");
        string? newTitle = Console.ReadLine();
        Console.Write("Yeni ISBN: ");
        string? newIsbn = Console.ReadLine();
        Console.Write("Yeni Yayın Yılı: ");
        if (int.TryParse(Console.ReadLine(), out int newPublishYear))
        {
            Book updatedBook = new Book { BookId = id, Title = newTitle, ISBN = newIsbn, PublishYear = newPublishYear };
            int result = service.UpdateBook(updatedBook);
            if (result > 0)
                Console.WriteLine($"ID: {id} olan kitap başarıyla güncellendi.");
            else
                Console.WriteLine($"ID: {id} olan kitap bulunamadı veya güncellenirken bir hata oluştu.");
        }
        else
        {
            Console.WriteLine("Geçersiz yayın yılı formatı.");
        }
    }
    else
    {
        Console.WriteLine("Geçersiz ID formatı.");
    }
}

void GetAllBooks(BookService service)
{
    Console.WriteLine("Bütün Kitaplar:");
    List<Book> books = service.GetAllBooks();
    if (books.Count > 0)
    {
        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.BookId}, Adı: {book.Title}, ISBN: {book.ISBN}, Yayın Yılı: {book.PublishYear}");
        }
    }
    else
    {
        Console.WriteLine("Kayıtlı kitap bulunmamaktadır.");
    }
}

void GetBookById(BookService service)
{
    Console.WriteLine("ID ile Kitap Getirme İşlemi");
    Console.Write("Getirmek istediğiniz kitabın ID'sini girin: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Book book = service.GetBookById(id);
        if (book != null)
        {
            Console.WriteLine($"ID: {book.BookId}, Adı: {book.Title}, ISBN: {book.ISBN}, Yayın Yılı: {book.PublishYear}");
        }
        else
        {
            Console.WriteLine($"Belirtilen ID'ye sahip kitap bulunamadı.");
        }
    }
    else
    {
        Console.WriteLine("Geçersiz ID formatı.");
    }
}

void GetBooksByTitle(BookService service)
{
    Console.WriteLine("Kitap İsmi ile Kitap Getirme İşlemi");
    Console.Write("Getirmek istediğiniz kitabın ismini girin: ");
    string? bookTitle = Console.ReadLine();
    if (bookTitle != null)
    {
        List<Book> foundBooks = service.GetBooksByTitle(bookTitle);
        if (foundBooks.Count > 0)
        {
            foreach (var book in foundBooks)
            {
                Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, ISBN: {book.ISBN}, Publish Year: {book.PublishYear}");
            }
        }
        else
        {
            Console.WriteLine($"'{bookTitle}' isminde kitap bulunamadı.");
        }
    }
}

void AuthorOperationsMenu()
{
    bool isAuthorMenuActive = true;
    AuthorService authorService = new(db);

    while (isAuthorMenuActive)
    {
        Console.WriteLine("----------------------------------");
        Console.WriteLine("        YAZAR İŞLEMLERİ          ");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("1 - Yazar Ekle");
        Console.WriteLine("2 - Yazar Sil");
        Console.WriteLine("3 - Yazar Bilgilerini Güncelle");
        Console.WriteLine("4 - Bütün Yazarları Getir");
        Console.WriteLine("5 - ID ile Yazar Getir");
        Console.WriteLine("6 - Ana Menüye Dön");
        Console.Write("Lütfen bir işlem seçin: ");

        string? authorMenuSelection = Console.ReadLine();

        switch (authorMenuSelection)
        {
            case "1":
                AddAuthor(authorService);
                break;
            case "2":
                DeleteAuthor(authorService);
                break;
            case "3":
                UpdateAuthorInformation(authorService);
                break;
            case "4":
                GetAllAuthors(authorService);
                break;
            case "5":
                GetAuthorById(authorService);
                break;
            case "6":
                isAuthorMenuActive = false;
                Console.WriteLine("Ana menüye dönülüyor...");
                break;
            default:
                Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                break;
        }
        Console.WriteLine();
    }
}

void AddAuthor(AuthorService service)
{
    Console.WriteLine("Yazar Ekleme İşlemi");
    Console.Write("Yazar Adı: ");
    string? firstName = Console.ReadLine();
    Console.Write("Yazar Soyadı: ");
    string? lastName = Console.ReadLine();
    Author newAuthor = new Author { FirstName = firstName, LastName = lastName };
    int result = service.AddAuthor(newAuthor);
    if (result > 0)
        Console.WriteLine("Yazar başarıyla eklendi.");
    else
        Console.WriteLine("Yazar eklenirken bir hata oluştu.");
}

void DeleteAuthor(AuthorService service)
{
    Console.WriteLine("Yazar Silme İşlemi");
    Console.Write("Silmek istediğiniz yazarın ID'sini girin: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        int result = service.DeleteAuthor(id);
        if (result > 0)
            Console.WriteLine($"ID: {id} olan yazar başarıyla silindi.");
        else
            Console.WriteLine($"ID: {id} olan yazar bulunamadı veya silinirken bir hata oluştu.");
    }
    else
    {
        Console.WriteLine("Geçersiz ID formatı.");
    }
}

void UpdateAuthorInformation(AuthorService service)
{
    Console.WriteLine("Yazar Bilgilerini Güncelleme İşlemi");
    Console.Write("Güncellemek istediğiniz yazarın ID'sini girin: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Console.Write("Yeni Yazar Adı: ");
        string? newFirstName = Console.ReadLine();
        Console.Write("Yeni Yazar Soyadı: ");
        string? newLastName = Console.ReadLine();
        Author updatedAuthor = new Author { AuthorId = id, FirstName = newFirstName, LastName = newLastName };
        int result = service.UpdateAuthor(updatedAuthor);
        if (result > 0)
            Console.WriteLine($"ID: {id} olan yazar başarıyla güncellendi.");
        else
            Console.WriteLine($"ID: {id} olan yazar bulunamadı veya güncellenirken bir hata oluştu.");
    }
    else
    {
        Console.WriteLine("Geçersiz ID formatı.");
    }
}

void GetAllAuthors(AuthorService service)
{
    Console.WriteLine("Bütün Yazarlar:");
    List<Author> authors = service.GetAllAuthors();
    if (authors.Count > 0)
    {
        foreach (var author in authors)
        {
            Console.WriteLine($"ID: {author.AuthorId}, Ad: {author.FirstName}, Soyad: {author.LastName}");
        }
    }
    else
    {
        Console.WriteLine("Kayıtlı yazar bulunmamaktadır.");
    }
}

void GetAuthorById(AuthorService service)
{
    Console.WriteLine("ID ile Yazar Getirme İşlemi");
    Console.Write("Getirmek istediğiniz yazarın ID'sini girin: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        // Eğer AuthorService'de ID ile yazar getirme metodu varsa onu kullanın
        // Author author = service.GetAuthorById(id);
        // if (author != null)
        // {
        //     Console.WriteLine($"ID: {author.AuthorId}, Ad: {author.FirstName}, Soyad: {author.LastName}");
        // }
        // else
        // {
        //     Console.WriteLine("Belirtilen ID'ye sahip yazar bulunamadı.");
        // }
        Console.WriteLine("Bu metot henüz implemente edilmedi.");
    }
    else
    {
        Console.WriteLine("Geçersiz ID formatı.");
    }
}

void LoanOperationsMenu()
{
    Console.WriteLine("Kira İşlemleri Menüsü (Henüz implemente edilmedi)");
}

void MemberOperationsMenu()
{
    Console.WriteLine("Üye İşlemleri Menüsü (Henüz implemente edilmedi)");
}