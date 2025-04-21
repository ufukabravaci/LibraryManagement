using System;
using System.Collections.Generic;
using LibraryManagement.Services;
using LibraryManagement.Models;
using System.IO.Pipelines;

namespace LibraryManagement.Operations;

public class BookOperations
{
    private readonly BookService _bookService;

    public BookOperations(BookService bookService)
    {
        _bookService = bookService;
    }

    public void BookOperationsMenu()
    {
        bool isBookMenuActive = true;

        while (isBookMenuActive)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("          KİTAP İŞLEMLERİ          ");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Kitap Ekle");
            Console.WriteLine("2 - Kitap Sil");
            Console.WriteLine("3 - Kitap Bilgilerini Güncelle");
            Console.WriteLine("4 - Bütün Kitapları Getir");
            Console.WriteLine("5 - ID ile Kitap Getir"); //Procedure kullanır.
            Console.WriteLine("6 - Kitap İsmi ile Kitap Getir");
            Console.WriteLine("7 - Tüm Kitaplar ve Yazarlarını Getir"); //View Kullanır.
            Console.WriteLine("8 - En çok kiralanmış 10 kitap"); //Database'de tanımlı bir fonksyion kullanıyor.
            Console.WriteLine("9 - Ana Menüye Dön");
            Console.Write("Lütfen bir işlem seçin: ");

            string? bookMenuSelection = Console.ReadLine();

            switch (bookMenuSelection)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    DeleteBook();
                    break;
                case "3":
                    UpdateBookInformation();
                    break;
                case "4":
                    GetAllBooks();
                    break;
                case "5":
                    GetBookById();
                    break;
                case "6":
                    GetBooksByTitle();
                    break;
                case "7":
                    GetBooksWithAuthors();
                    break;
                case "8":
                    GetMostLoaned10Books();
                    break;
                case "9":
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

    private void AddBook()
    {
        System.Console.WriteLine("Kitap Ekleme İşlemi");
        System.Console.WriteLine("Kitap Adı:");
        string? title = Console.ReadLine();
        System.Console.WriteLine("ISBN: ");
        string? isbn = Console.ReadLine();
        System.Console.WriteLine("Yayın yılı:");
        if (int.TryParse(Console.ReadLine(), out int publishYear))
        {
            Book book = new Book
            {
                Title = title,
                ISBN = isbn,
                PublishYear = publishYear
            };
            int result = _bookService.AddBook(book); // başarılıysa result>0
            if (result > 0)
                System.Console.WriteLine($"{title} başarıyla kütüphaneye eklendi.");
            else
                System.Console.WriteLine("Kitap Eklenirken hata oluştu. Lütfen tekrar deneyin.");
        }
    }

    private void DeleteBook()
    {
        System.Console.WriteLine("Kitap Silme İşlemi");
        System.Console.WriteLine("Lütfen silinecek kitap için geçerli bir ID bilgisi girin.");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            int result = _bookService.DeleteBook(id);
            if (result > 0) System.Console.WriteLine($"ID: {id} olan kitap başarıyla silindi.");
            else System.Console.WriteLine($"ID: {id} olan kitap bulunamadı veya silinirken bir hata oluştu.");
        }
        else
        {
            System.Console.WriteLine("Id değeri sayısal bir değer olmalıdır.");
        }
    }

    private void UpdateBookInformation()
    {
        System.Console.WriteLine("Kitap Bilgilerini Güncelleme İşlemi");
        System.Console.WriteLine("Güncellemek istediğiniz kitabın ID'sini girin.");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            System.Console.WriteLine("Yeni Kitap Adı:");
            string? title = Console.ReadLine();
            System.Console.WriteLine("Yeni ISBN");
            string? isbn = Console.ReadLine();
            System.Console.WriteLine("Yeni Yayın Yılı: ");
            if (int.TryParse(Console.ReadLine(), out int publishYear))
            {
                Book newBook = new Book
                {
                    BookId = id,
                    Title = title,
                    ISBN = isbn,
                    PublishYear = publishYear
                };
                int result = _bookService.UpdateBook(newBook);
                if (result > 0)
                    Console.WriteLine($"ID: {id} olan kitap başarıyla güncellendi.");
                else
                    Console.WriteLine($"ID: {id} olan kitap bulunamadı veya güncellenirken bir hata oluştu.");
            }
            else
            {
                System.Console.WriteLine("Yayın yılı sayısal bir değer olmalıdır.");
            }
        }
        else
        {
            System.Console.WriteLine("ID değeri sayısal bir değer olmalıdır.");
        }
    }

    private void GetAllBooks()
    {
        System.Console.WriteLine("Bütün kitaplar:");
        List<Book> books = _bookService.GetAllBooks();
        if (books.Count > 0)
        {
            foreach (var book in books)
            {
                System.Console.WriteLine($"ID: {book.BookId}, Adı: {book.Title}, ISBN: {book.ISBN}, Yayın Yılı: {book.PublishYear}");
            }
        }
        else
        {
            System.Console.WriteLine("Kayıtlı kitap bulunamamaktadır.");
        }
    }

    //Bir procedure kullanır.
    private void GetBookById()
    {
        System.Console.WriteLine("ID ile kitap getirme işlemi");
        System.Console.WriteLine("Getirmek istediğiniz kitabın ID'sini getirin.");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Book book = _bookService.GetBookById(id);
            if (book != null)
            {
                System.Console.WriteLine($"ID: {book.BookId}, Adı: {book.Title}, ISBN: {book.ISBN}, Yayın Yılı: {book.PublishYear}");
            }
            else
            {
                System.Console.WriteLine($"{id} değerine sahip bir kitap bulunamadı.");
            }
        }
        else
        {
            System.Console.WriteLine("ID değeri sayısal bir değer olmalıdır.");
        }
    }

    private void GetBooksByTitle()
    {
        System.Console.WriteLine("Kitap ismi ile Kitap Getirme İşlemi");
        System.Console.WriteLine("Getirmek istediğiniz kitabın ismini giriniz.");
        string? bookTitle = Console.ReadLine();
        if (bookTitle != null)
        {
            List<Book> books = _bookService.GetBooksByTitle(bookTitle);
            if (books.Count > 0)
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, ISBN: {book.ISBN}, Publish Year: {book.PublishYear}");
                }
            }
            else
            {
                System.Console.WriteLine($"'{bookTitle}' isminde kitap bulunamadı.");
            }
        }
        else System.Console.WriteLine("Geçerli bir kitap ismi giriniz.");
    }

    //Bu metot bir database'de tanımlı bir view kulanıyor. Bu view kitap ve yazar bilgilerini birleştiriyor.
    //Bir kitabın birden fazla yazarı olabilir bu sebeple bir kitap için birden fazla sonuç dönebilir.
    //Son eklediğim kitaplar bu sebeple birden fazla dönüyor.
    private void GetBooksWithAuthors()
    {
        System.Console.WriteLine("Kitaplar ve Yazarları Tablosu");
        List<BookWithAuthors> books = _bookService.GetBooksWithAuthors();
        if (books.Count > 0)
        {
            foreach (var book in books)
            {
                System.Console.WriteLine($"ID: {book.BookID}, Adı: {book.Title}, ISBN: {book.ISBN}, Yayın Yılı: {book.PublicationYear}, Yazar Adı: {book.AuthorName}, Yazar Soyadı: {book.AuthorLastName}");
            }
        }
        else
        {
            System.Console.WriteLine("Kayıtlı kitap bulunmamaktadır.");
        }
    }

    private void GetMostLoaned10Books()
    {
        var books = _bookService.Get10MostLoanedBooks();

        Console.WriteLine("En Çok Kiralanan 10 Kitap:\n");

        foreach (var book in books)
        {
            Console.WriteLine($"Kitap ID: {book.BookId}, Başlık  : {book.Title}, Yazar   : {book.Author}, Kiralanma Sayısı: {book.LoanCount}");
        }
    }
}