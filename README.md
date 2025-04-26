# 📚 Library Management System

Bu proje, ADO.NET ile geliştirilmiş bir **konsol tabanlı kütüphane yönetim sistemidir**. Kitap, yazar, üye ve kiralama işlemlerini yönetmenizi sağlar.
CRUD işlemlerini ve gelişmiş sorgulama sistemlerini uygulamanıza olanak sağlar.

## 🛠️ Kullanılan Teknolojiler

- C#
- ADO.NET
- SQL Server
- .NET 9.0
- Konsol Uygulaması (Console App)
- Veritabanı işlemleri:ADO.NET ile bağlantı,Viewlar, User Defined functionlar,Stored Procedureler

## 📸 Ekran Görüntüleri

Ana menü görünümü:

![MainMenu](screenshots/MainMenu.png)

Kitap işlemleri ekranı:

![BookOperations](screenshots/BookOperations.png)

Yazar işlemleri ekranı:

![AuthorOperations](screenshots/AuthorOperations.png)

Kiralama işlemleri ekranı:

![LoanOperations](screenshots/LoanOperations.png)

Üye işlemleri ekranı:

![MemberOperations](screenshots/MemberOperations.png)

Database diagram:

![DatabaseDiagram](screenshots/DatabaseDiagram.png)

Database Structures:

![DatabaseStructures](screenshots/DatabaseStructures.png)


## 🗄️ Veritabanı Kurulumu

Projeyi kullanmadan önce `LibraryDB.sql` dosyasını çalıştırarak veritabanı yapısını oluşturmanız gerekmektedir.

Adımlar:
1. SQL Server Management Studio açın.
2. **İsmi tam olarak `LibraryManagement` olan bir veritabanı oluşturun.**
3. `Database/LibraryDB.sql` dosyasını açıp çalıştırın.
4. Proje içindeki bağlantı ayarı (`_connectionString`) doğrudan `LibraryManagement` veritabanına yönlendirildiği için ek bir değişiklik yapmanıza gerek yoktur.

> **Not:** Eğer farklı isimde veritabanı oluşturursanız, `DB.cs` dosyasında `_connectionString` içindeki `Database=...` kısmını güncellemeniz gerekir.

