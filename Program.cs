using LibraryManagement.Utils;
using LibraryManagement.Services;
using LibraryManagement.Operations;

var db = new DB();
bool isMainMenuActive = true;

//Create services
var bookService = new BookService(db);
var authorService = new AuthorService(db);
var loanService = new LoanService(db);
var memberService = new MemberService(db);
//Create operations
var bookOperations = new BookOperations(bookService);
var authorOperations = new AuthorOperations(authorService);
var loanOperations = new LoanOperations(loanService, bookService, memberService);
var memberOperations = new MemberOperations(memberService);

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
            loanOperations.LoanOperationsMenu();
            break;
        case "4":
            memberOperations.MemberOperationsMenu();
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