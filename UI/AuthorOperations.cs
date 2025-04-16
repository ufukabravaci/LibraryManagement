using System;
using System.Collections.Generic;
using LibraryManagement.Services;
using LibraryManagement.Models;

namespace LibraryManagement.UI;

public class AuthorOperations
{
    private readonly AuthorService _authorService;

    public AuthorOperations(AuthorService authorService)
    {
        _authorService = authorService;
    }

    public void AuthorOperationsMenu()
    {
        bool isAuthorMenuActive = true;

        while (isAuthorMenuActive)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("          YAZAR İŞLEMLERİ          ");
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
                    AddAuthor();
                    break;
                case "2":
                    DeleteAuthor();
                    break;
                case "3":
                    UpdateAuthorInformation();
                    break;
                case "4":
                    GetAllAuthors();
                    break;
                case "5":
                    GetAuthorById();
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

    private void AddAuthor()
    {
        Console.WriteLine("Yazar Ekleme İşlemi");
        Console.Write("Yazar Adı: ");
        string? firstName = Console.ReadLine();
        Console.Write("Yazar Soyadı: ");
        string? lastName = Console.ReadLine();
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
            Author newAuthor = new Author { FirstName = firstName, LastName = lastName };
            int result = _authorService.AddAuthor(newAuthor);
            if (result > 0)
                Console.WriteLine("Yazar başarıyla eklendi.");
            else
                Console.WriteLine("Yazar eklenirken bir hata oluştu.");
        }
        else
        {
            Console.WriteLine("Yazar adı ve soyadı boş olamaz.");
        }
    }

    private void DeleteAuthor()
    {
        Console.WriteLine("Yazar Silme İşlemi");
        Console.Write("Silmek istediğiniz yazarın ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            int result = _authorService.DeleteAuthor(id);
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

    private void UpdateAuthorInformation()
    {
        Console.WriteLine("Yazar Bilgilerini Güncelleme İşlemi");
        Console.Write("Güncellemek istediğiniz yazarın ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Yeni Yazar Adı: ");
            string? newFirstName = Console.ReadLine();
            Console.Write("Yeni Yazar Soyadı: ");
            string? newLastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newFirstName) && !string.IsNullOrEmpty(newLastName))
            {
                Author updatedAuthor = new Author { AuthorId = id, FirstName = newFirstName, LastName = newLastName };
                int result = _authorService.UpdateAuthor(updatedAuthor);
                if (result > 0)
                    Console.WriteLine($"ID: {id} olan yazar başarıyla güncellendi.");
                else
                    Console.WriteLine($"ID: {id} olan yazar bulunamadı veya güncellenirken bir hata oluştu.");
            }
            else
            {
                Console.WriteLine("Yazar adı ve soyadı boş olamaz.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID formatı.");
        }
    }

    private void GetAllAuthors()
    {
        Console.WriteLine("Bütün Yazarlar:");
        List<Author> authors = _authorService.GetAllAuthors();
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

    private void GetAuthorById()
    {
        Console.WriteLine("ID ile Yazar Getirme İşlemi");
        Console.Write("Getirmek istediğiniz yazarın ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Author? author = _authorService.GetAuthorById(id);
            if (author != null)
            {
                Console.WriteLine($"ID: {author.AuthorId}, Ad: {author.FirstName}, Soyad: {author.LastName}");
            }
            else
            {
                Console.WriteLine("Belirtilen ID'ye sahip yazar bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID formatı.");
        }
    }
}