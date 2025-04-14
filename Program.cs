using LibraryManagement.Utils;
using System;
using LibraryManagement.Services;
using LibraryManagement.Models;
var db = new DB();

Console.WriteLine("-----------------------------------------------------");
Console.WriteLine("---------------------BookService---------------------");
Console.WriteLine("-----------------------------------------------------");
BookService bookService = new(db);
Book book = new Book
{
    Title = "Yüzüklerin Efendisi Yüzük Kardeşliği",
    ISBN = "987",
    PublishYear = 2001
};
int result = 0;
System.Console.WriteLine("---------------------AddBook---------------------");
result = bookService.AddBook(book);
if(result > 0) {
    Console.WriteLine("Kitabınız başarıyla eklendi.");
}
System.Console.WriteLine("---------------------DeleteBook---------------------");
// bookService.DeleteBook(book); // Doğrudan kitap nesnesi kullanarak metotta title'ını kullandık.
System.Console.WriteLine("----------------------------------------------------");
System.Console.WriteLine("---------------------UpdateBook---------------------");
book.Title = "Yüzüklerin Efendisi İki Kule";
bookService.UpdateBook(book);
System.Console.WriteLine("----------------------------------------------------");
bookService.DeleteBook(book);


