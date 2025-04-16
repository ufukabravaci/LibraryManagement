using LibraryManagement.Utils;
using LibraryManagement.Services;
using LibraryManagement.Operations;

var db = new DB();
bool isMainMenuActive = true;

//Create services
var bookService = new BookService(db);
var authorService = new AuthorService(db);
//Create operations
var bookOperations = new BookOperations(bookService);
var authorOperations = new AuthorOperations(authorService);

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
            bookOperations.BookOperationsMenu();
            break;
        case "2":
            authorOperations.AuthorOperationsMenu();
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


void LoanOperationsMenu()
{
    Console.WriteLine("Kira İşlemleri Menüsü (Henüz implemente edilmedi)");
}

void MemberOperationsMenu()
{
    Console.WriteLine("Üye İşlemleri Menüsü (Henüz implemente edilmedi)");
}