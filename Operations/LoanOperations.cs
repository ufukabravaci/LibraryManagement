

using LibraryManagement.Services;

namespace LibraryManagement.Operations;

public class LoanOperations
{
    private readonly LoanService _loanService;
    public LoanOperations(LoanService loanService)
    {
        _loanService = loanService;
    }

    public void LoanOperationsMenu()
    {
        bool isLoanMenuActive = true;
        while (isLoanMenuActive)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("          KİRA İŞLEMLERİ          ");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Kitap Kiralama İşlemi");
            Console.WriteLine("2 - Kitap İade Alma İşlemi");
            Console.WriteLine("3 - Kirası gecikmiş kitaplar");
            Console.WriteLine("4 - Şu anda kirada olan tüm kitaplar");
            Console.WriteLine("5 - Üzerinde Aktif Kitap Olan Üyeler");
            Console.WriteLine("6 - Üyenin tüm kiralama geçmişi");
            Console.WriteLine("7 - En çok kiralanan 10 kitap");
            Console.WriteLine("8 - En çok kitap kiralayan 10 üye");
            Console.WriteLine("9 - Ana Menüye Dön");
            Console.Write("Lütfen bir işlem seçin: ");

            string? loanMenuSelection = Console.ReadLine();
            switch (loanMenuSelection)
            {
                case "1":
                    LendBook();
                    break;
                case "2":
                    ReturnBook();
                    break;
                case "3":
                    ListOverdueBooks();
                    break;
                case "4":
                    ListAllLoanedBooks();
                    break;
                case "5":
                    ListMembersWithActiveLoans();
                    break;
                case "6":
                    ListMemberLoanHistory();
                    break;
                case "7":
                    ListTop10MostLoanedBooks();
                    break;
                case "8":
                    ListTop10MembersWhoLoanedMost();
                    break;
                case "9":
                    isLoanMenuActive = false;
                    Console.WriteLine("Ana menüye dönülüyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }
    private void LendBook() { 
        
    }
    private void ReturnBook() { /* Kitap iade alma işlemini gerçekleştirir */ }
    private void ListOverdueBooks() { /* Kirası gecikmiş kitapları listeler */ }
    private void ListAllLoanedBooks() { /* Şu anda kirada olan tüm kitapları listeler */ }
    private void ListMembersWithActiveLoans() { /* Üzerinde aktif kitap olan üyeleri listeler */ }
    private void ListMemberLoanHistory() { /* Belirli bir üyenin tüm kiralama geçmişini listeler */ }
    private void ListTop10MostLoanedBooks() { /* En çok kiralanan 10 kitabı listeler */ }
    private void ListTop10MembersWhoLoanedMost() { /* En çok kitap kiralayan 10 üyeyi listeler */ }
}
