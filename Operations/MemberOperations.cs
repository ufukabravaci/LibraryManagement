using LibraryManagement.Models;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;

namespace LibraryManagement.Operations;

public class MemberOperations
{
    private readonly MemberService _memberService;

    public MemberOperations(MemberService memberService)
    {
        _memberService = memberService;
    }

    public void MemberOperationsMenu()
    {
        bool isMemberMenuActive = true;

        while (isMemberMenuActive)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("          ÜYE İŞLEMLERİ          ");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Yeni Üye Ekle");
            Console.WriteLine("2 - Üye Bilgilerini Güncelle");
            Console.WriteLine("3 - Üye Sil");
            Console.WriteLine("4 - Tüm Üyeleri Listele");
            Console.WriteLine("5 - ID ile Üye Getir"); // procedure kullanır
            Console.WriteLine("6 - Aktif olarak kirada kitabı olan tüm üyeler"); // view kullanır
            Console.WriteLine("7 - Üyeye ait tüm kiralama işlemleri"); //view kullanır
            Console.WriteLine("8 - Üyenin tam adı"); //database fonksiyonu kullanır
            Console.WriteLine("9 - Ana menüye dön"); 
            Console.Write("Lütfen bir işlem seçin: ");

            string? memberMenuSelection = Console.ReadLine();

            switch (memberMenuSelection)
            {
                case "1":
                    AddNewMember();
                    break;
                case "2":
                    UpdateExistingMember();
                    break;
                case "3":
                    DeleteExistingMember();
                    break;
                case "4":
                    ListAllMembers();
                    break;
                case "5":
                    GetMemberDetailsById();
                    break;
                case "6":
                    GetMembersHasCurrentLoan();
                    break;
                case "7":
                    GetLoansByMemberId();
                    break;
                case "8":
                    GetMemberFullNameByID();
                    break;
                case "9":
                    isMemberMenuActive = false;
                    Console.WriteLine("Ana menüye dönülüyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
            Console.WriteLine();
        }
    }

    private void AddNewMember()
    {
        Console.WriteLine("Yeni Üye Kaydı");
        Console.Write("Adı: ");
        string? firstName = Console.ReadLine();
        Console.Write("Soyadı: ");
        string? lastName = Console.ReadLine();
        Console.Write("E-posta: ");
        string? email = Console.ReadLine();
        Console.Write("Telefon: ");
        string? phone = Console.ReadLine();
        //Null uyarısını engellemek için null kontrolü
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(phone))
        {
            Member newMember = new(firstName, lastName, email, phone);
            int result = _memberService.AddMember(newMember);

            if (result > 0)
            {
                Console.WriteLine("Yeni üye başarıyla kaydedildi.");
            }
            else
            {
                Console.WriteLine("Üye kaydı sırasında bir hata oluştu.");
            }
        }
        else
        {
            Console.WriteLine("Lütfen tüm alanları doldurun.");
        }
    }

    private void UpdateExistingMember()
    {
        System.Console.WriteLine("Üye Güncelleme");
        System.Console.WriteLine("Üye ID'si girin:");
        if(int.TryParse(Console.ReadLine(), out int memberId))
        {
            Member? existingMember = _memberService.GetMemberById(memberId);
            if (existingMember != null)
            {
                System.Console.WriteLine("Yeni üye adı:");
                string? firstName = Console.ReadLine();
                System.Console.WriteLine("Yeni üye soyadı:");
                string? lastName = Console.ReadLine();
                System.Console.WriteLine("Yeni üye e-posta:");
                string? email = Console.ReadLine();
                System.Console.WriteLine("Yeni üye telefon:");
                string? phone = Console.ReadLine();
                if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
                {
                    System.Console.WriteLine("Lütfen tüm alanları doldurun.");
                    return;
                } // Null uyarını engellemek için
                Member updatedMember = new Member(memberId, firstName, lastName, email, phone);
                int result = _memberService.UpdateMember(updatedMember);
                if (result > 0)
                {
                    System.Console.WriteLine("Üye bilgileri başarıyla güncellendi.");
                }
                else
                {
                    System.Console.WriteLine("Üye güncellenirken bir hata oluştu.");
                }
            }
            else
            {
                System.Console.WriteLine("Üye bulunamadı.");
            }
        }
    }

    private void DeleteExistingMember()
    {
        Console.WriteLine("Üye Silme");
        Console.Write("Silinecek Üye ID: ");
        if (int.TryParse(Console.ReadLine(), out int memberId))
        {
            Member? memberToDelete = _memberService.GetMemberById(memberId);
            if (memberToDelete != null)
            {
                int result = _memberService.DeleteMember(memberId);
                if (result > 0)
                {
                    Console.WriteLine("Üye başarıyla silindi.");
                }
                else
                {
                    Console.WriteLine("Üye silinirken bir hata oluştu.");
                }
            }
            else
            {
                Console.WriteLine("Silinecek üye bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("ID sayısal bir değer olmalıdır.");
        }
    }
    private void ListAllMembers()
    {
        Console.WriteLine("Tüm Üyeler");
        List<Member> members = _memberService.GetAllMembers();
        if (members.Count > 0)
        {
            foreach (var member in members)
            {
                Console.WriteLine($"ID: {member.MemberId}, Adı: {member.FirstName}, Soyadı: {member.LastName}, E-posta: {member.Email}, Telefon: {member.Phone}, Kayıt Tarihi: {member.RegistrationDate}");
            }
        }
        else
        {
            Console.WriteLine("Kayıtlı üye bulunmamaktadır.");
        }
    }

    private void GetMemberDetailsById() //Procedure kullanır.
    {
        Console.WriteLine("Üye Detayları");
        Console.Write("Görüntülenecek Üye ID: ");
        if (int.TryParse(Console.ReadLine(), out int memberId))
        {
            Member? member = _memberService.GetMemberById(memberId);
            if (member != null)
            {
                Console.WriteLine($"ID: {member.MemberId}, Adı: {member.FirstName}, Soyadı :{member.LastName}, E-posta: {member.Email},Telefon: {member.Phone}, Kayıt Tarihi: {member.RegistrationDate}");
            }
            else
            {
                Console.WriteLine($"Belirtilen ID ({memberId}) ile bir üye bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID formatı.");
        }
    }

    private void GetMembersHasCurrentLoan() //View kullanır
    {
        Console.WriteLine("Aktif Olarak Kirada Kitabı Olan Üyeler");
        List<MemberLoanDate> members = _memberService.GetMembersHasCurrentLoan();
        if (members.Count > 0)
        {
            foreach (var member in members)
            {
                Console.WriteLine($"ID: {member.MemberID}, Adı: {member.MemberFirstName}, Soyadı: {member.MemberLastName}, Kiralama Tarihi: {member.LoanDate}");
            }
        }
        else
        {
            Console.WriteLine("Aktif olarak kirada kitabı olan üye bulunmamaktadır.");
        }
    }
    private void GetLoansByMemberId() //Procedure kullanır.
    {
        Console.WriteLine("Üyeye Ait Tüm Kiralama İşlemleri");
        Console.Write("Üye ID: ");
        if (int.TryParse(Console.ReadLine(), out int memberId))
        {
            List<MemberLoanDetail> loans = _memberService.GetLoansByMemberId(memberId);
            if (loans.Count > 0)
            {
                foreach (var loan in loans)
                {
                    Console.WriteLine($"Üye ID: {loan.MemberID},LoanID: {loan.LoanID} ,Kitap ID: {loan.BookID}, Kiralama Tarihi: {loan.LoanDate}, İade Tarihi: {loan.ReturnDate}, Son Tarih: {loan.DueDate}, Kitap Adı: {loan.BookTitle}, Yazar Adı: {loan.AuthorFirstName} {loan.AuthorLastName}");
                }
            }
            else
            {
                Console.WriteLine($"Belirtilen ID ({memberId}) ile bir üye bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID formatı.");
        }
    }

    private void GetMemberFullNameByID() //Database'de tanımladığım bir fonksiyonu kullanır.
    {
        Console.WriteLine("Üye Adı ve Soyadı");
        Console.Write("Görüntülenecek Üye ID: ");
        if (int.TryParse(Console.ReadLine(), out int memberId))
        {
            string fullName = _memberService.GetMemberFullNameByID(memberId);
            if (!string.IsNullOrEmpty(fullName))
            {
                Console.WriteLine($"Üye Adı ve Soyadı: {fullName}");
            }
            else
            {
                Console.WriteLine($"Belirtilen ID ({memberId}) ile bir üye bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID formatı.");
        }
    }
}