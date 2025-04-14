namespace LibraryManagement.Models;

public class Member
{
    public int MemberId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime RegistrationDate { get; set; }

    public Member()
    {
        RegistrationDate = DateTime.Now; // Varsayılan kayıt tarihi olarak şimdi
    }

    // id'siz yeni üye kaydı için ctor
    public Member(string firstName, string lastName, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        RegistrationDate = DateTime.Now;
    }

    // id'li veri okuma için ctor
    public Member(int memberId, string firstName, string lastName, string email, string phone, DateTime registrationDate)
    {
        MemberId = memberId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        RegistrationDate = registrationDate;
    }
}