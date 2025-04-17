

using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Operations;

public class LoanOperations
{
    private readonly LoanService _loanService;
    private readonly BookService _bookService;
    private readonly MemberService _memberService;

    public LoanOperations(LoanService loanService, BookService bookService, MemberService memberService)
    {
        _loanService = loanService;
        _bookService = bookService;
        _memberService = memberService;
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
            Console.WriteLine("3 - Geri getirme tarihi gecikmiş kiralamalar ve tüm bilgileri");
            Console.WriteLine("4 - 1 Haftadan kısa süren kiralama işlemleri ve üye bilgileri");
            Console.WriteLine("5 - Şu anda kirada olan tüm kitaplar");
            Console.WriteLine("6 - Üzerinde Aktif Kitap Olan Üyeler");
            Console.WriteLine("7 - Üyenin tüm kiralama geçmişi kitap ve yazar bilgileriyle beraber");
            Console.WriteLine("8 - En çok kiralanan 10 kitap");
            Console.WriteLine("9 - En çok kitap kiralayan 10 üye");
            Console.WriteLine("10 - Ana Menüye Dön");
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
                    ListOverdueLoansWithDetails();
                    break;
                case "4":
                    ListLessThenOneWeekLoans();
                    break;
                case "5":
                    ListAllLoanedBooks();
                    break;
                case "6":
                    ListMembersWithActiveLoans();
                    break;
                case "7":
                    ListMemberLoanHistoryWithDetails();
                    break;
                case "8":
                    ListTop10MostLoanedBooks();
                    break;
                case "9":
                    ListTop10MembersWhoLoanedMost();
                    break;
                case "10":
                    isLoanMenuActive = false;
                    Console.WriteLine("Ana menüye dönülüyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }
    private void LendBook()
    {
        System.Console.WriteLine("Kitap kiralama işlemi");
        System.Console.WriteLine("Kitap adı: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Geçersiz kitap ID formatı.");
            return;
        }

        Console.Write("Üye ID: ");
        if (!int.TryParse(Console.ReadLine(), out int memberId))
        {
            Console.WriteLine("Geçersiz üye ID formatı.");
            return;
        }
        DateTime loanDate = DateTime.Now;
        DateTime dueDate = loanDate.AddMonths(1);

        Loan newLoan = new Loan
        {
            BookID = bookId,
            MemberID = memberId,
            LoanDate = loanDate,
            ReturnDate = null, // Henüz iade edilmedi
            DueDate = dueDate
        };

        int result = _loanService.AddLoan(newLoan);
        if (result > 0)
        {
            Console.WriteLine("Kitap başarıyla kiralandı.");
        }
        else
        {
            Console.WriteLine("Kitap kiralama işlemi başarısız oldu.");
        }

    }
    private void ReturnBook()
    {
        System.Console.WriteLine("Kitap iade alma işlemi");
        System.Console.WriteLine("LoanID: ");
        if (!int.TryParse(Console.ReadLine(), out int loanID))
        {
            Console.WriteLine("Geçersiz LoanID formatı.");
            return;
        }
        //Loan'ı bulacağız. Get ile getireceğiz.ReturnDateini güncelleyeceğiz
        try
        {
            Loan loan = _loanService.GetLoanById(loanID);
            if (loan != null)
            {
                if (loan.ReturnDate == null) // Daha önce iade edilmemişse
                {
                    loan.ReturnDate = DateTime.Now;
                    int result = _loanService.UpdateLoan(loan); // Loan'ı güncelle
                    if (result > 0)
                    {
                        Console.WriteLine("Kitap iadesi başarıyla tamamlandı.");
                    }
                    else
                    {
                        Console.WriteLine("Kitap iade işlemi veritabanında güncellenemedi.");
                    }
                }
                else
                {
                    Console.WriteLine("Bu kitap zaten iade edilmiş.");
                }
            }
            else
            {
                Console.WriteLine("Bu LoanID'ye sahip bir kayıt bulunamadı.");
            }

        }
        catch (SqlException ex)
        {
            Console.WriteLine("Kiralık kitap geri bırakılırken bir hata oluştu: " + ex.Message);
        }
    }

    // Buradan sonraki listeleme metotlarında tüm veriyi çekip
    // uygulama üzerinde filtreleme yapacağım. Bu çok verimli bir yöntem olmayabilir.
    // Fakat tüm filtrelemeler için service katmanında yeni metotlar yazıp
    // yeni queryler yazmak çok fazla vaktimi alacak.


    //Bu metot veritabanında oluşturulmuş bir fonksiyon kullanmaktadır.
    // (aslında memberservice kullanıyor. burada memberserviste fonksiyonu kullanılan metotu kullanıyoruz.)
    private void ListOverdueLoansWithDetails() //Return tarihi due tarihini geçmiş kitaplar
    {
        System.Console.WriteLine("Kira tarihi gecikmiş kitaplar ve üyeler");
        try
        {
            List<Loan> loans = _loanService.GetAllLoans();
            if (loans != null)
            {
                List<Loan> overdueLoans = new();
                Book book = new();
                string? fullName = "";
                foreach (var loan in loans)
                {
                    if (loan.ReturnDate == null && loan.DueDate < DateTime.Now) //return edilmemişse ve son tarihi şimdiden kçükse
                    {
                        book = _bookService.GetBookById(loan.BookID);
                        fullName = _memberService?.GetMemberFullNameByID(loan.MemberID);
                        overdueLoans.Add(loan);
                        Console.WriteLine($"LoanID: {loan.LoanID}, DueDate: {loan.DueDate}, MemberFullName: {fullName}, Title: {book.Title}");
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Kiralık Kitapları getirirken bir hata oluştu.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Kiralık Kitapları getirirken bir hata oluştu. " + ex.Message);
        }
    }

    // Bu metot veritabanında oluşturulmuş bir fonksiyon kullanmaktadır.
    private void ListLessThenOneWeekLoans()
    {
        System.Console.WriteLine("1 Haftadan kısa süren kiralama işlemleri ve üye bilgileri: ");
        try
        {
            var shortLoans = _loanService.GetShortTermLoans();
            foreach (var loan in shortLoans)
            {
                Book book = _bookService.GetBookById(loan.BookID);
                var duration = (loan.ReturnDate ?? DateTime.Now) - loan.LoanDate; //ReturnDate nullsa şimdiyi returndate yapıyoruz.
                Console.WriteLine($"LoanID: {loan.LoanID}, Duration: {duration.Days} gün, Üye: {_memberService.GetMemberFullNameByID(loan.MemberID)}, Kitap: {book.Title}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Kiralık Kitapları getirirken bir hata oluştu. " + ex.Message);
        }
    }
    private void ListAllLoanedBooks() // Şu anda kirada olan tüm kitapları listeler
    { }
    private void ListMembersWithActiveLoans() { /* Üzerinde aktif kitap olan üyeleri listeler */ }
    private void ListMemberLoanHistoryWithDetails() { /* Belirli bir üyenin tüm kiralama geçmişini listeler */ }
    private void ListTop10MostLoanedBooks() { /* En çok kiralanan 10 kitabı listeler */ }
    private void ListTop10MembersWhoLoanedMost() { /* En çok kitap kiralayan 10 üyeyi listeler */ }
}
