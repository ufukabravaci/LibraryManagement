USE [master]
GO
/****** Object:  Database [LibraryManagement]    Script Date: 26.04.2025 10:48:36 ******/
CREATE DATABASE [LibraryManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibraryManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibraryManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LibraryManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryManagement] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LibraryManagement', N'ON'
GO
ALTER DATABASE [LibraryManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibraryManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibraryManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalculateLoanDuration]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CalculateLoanDuration] (@LoanDate DATETIME, @ReturnDate DATETIME)
RETURNS INT
AS
BEGIN
DECLARE @LoanDuration INT;
SET @LoanDuration = DATEDIFF(DAY, @LoanDate, @ReturnDate);
RETURN @LoanDuration
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetFullName]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_GetFullName] (@FirstName NVARCHAR(50), @LastName NVARCHAR(50))
RETURNS NVARCHAR(150)
AS
BEGIN
DECLARE @FullName NVARCHAR(150);
SET @FullName = @FirstName + ' ' + @LastName;
RETURN @FullName;
END;
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[PublishYear] [int] NULL,
	[ISBN] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookAuthor]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAuthor](
	[BookID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
 CONSTRAINT [PK_BookAuthor] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_BooksWithAuthors]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_BooksWithAuthors] AS
SELECT b.BookID BookID, b.Title Title, b.ISBN ISBN, b.PublishYear PublicationYear,
a.AuthorID AuthorID, a.FirstName AuthorName, a.LastName AuthorLastName
FROM Books b
INNER JOIN BookAuthor ba ON b.BookID = ba.BookID
INNER JOIN Authors a ON a.AuthorID = ba.AuthorID
GO
/****** Object:  Table [dbo].[Members]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loans](
	[LoanID] [int] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NOT NULL,
	[MemberID] [int] NOT NULL,
	[LoanDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_CurrentLoans]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CurrentLoans] AS
SELECT M.MemberID, M.FirstName MemberFirstName, 
M.LastName MemberLastName, L.LoanDate
FROM Members m
INNER JOIN Loans l ON m.MemberID = l.MemberID
INNER JOIN Books b ON b.BookID = l.BookID
WHERE l.ReturnDate > GETDATE();
GO
/****** Object:  View [dbo].[vw_CurrentLoansWithBookDetails]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CurrentLoansWithBookDetails] AS
SELECT M.MemberID, M.FirstName MemberFirstName, 
M.LastName MemberLastName, L.LoanDate,
b.BookID BookID, b.Title BookName,a.FirstName AuthorName
,a.LastName AuthorLastName
FROM Members m
INNER JOIN Loans l ON m.MemberID = l.MemberID
INNER JOIN Books b ON b.BookID = l.BookID
INNER JOIN BookAuthor ba ON ba.BookID = b.BookID
INNER JOIN Authors a ON a.AuthorID = ba.AuthorID
WHERE l.ReturnDate > GETDATE();
GO
/****** Object:  View [dbo].[vw_MostLoanedTenBooks]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_MostLoanedTenBooks]
AS
SELECT TOP 10 A.AuthorID, A.FirstName, A.LastName, COUNT(L.BookID) AS TotalLoans
FROM Authors A
INNER JOIN BookAuthor BA ON A.AuthorID = BA.AuthorID
INNER JOIN Loans L ON BA.BookID = L.BookID
GROUP BY A.AuthorID, A.FirstName, A.LastName
ORDER BY TotalLoans DESC;
GO
ALTER TABLE [dbo].[BookAuthor]  WITH CHECK ADD  CONSTRAINT [FK_BookAuthor_Authors] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([AuthorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookAuthor] CHECK CONSTRAINT [FK_BookAuthor_Authors]
GO
ALTER TABLE [dbo].[BookAuthor]  WITH CHECK ADD  CONSTRAINT [FK_BookAuthor_Books] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookAuthor] CHECK CONSTRAINT [FK_BookAuthor_Books]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [FK_Loans_Books] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [FK_Loans_Books]
GO
ALTER TABLE [dbo].[Loans]  WITH CHECK ADD  CONSTRAINT [FK_Loans_Members] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Members] ([MemberID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Loans] CHECK CONSTRAINT [FK_Loans_Members]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetBookDetails]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetBookDetails]
@BOOKID INT
AS
BEGIN
SELECT * FROM BOOKS B
WHERE B.BookID = @BOOKID
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoansByMember]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLoansByMember]
    @MEMBERID INT
AS
BEGIN
    SELECT
		L.MemberID,
        L.LoanID, 
        L.BookID, 
        L.LoanDate, 
        L.ReturnDate,
        L.DueDate,
        B.Title AS BookTitle, -- Kitap adı
        A.FirstName AS AuthorFirstName,
        A.LastName AS AuthorLastName
    FROM LOANS L
    INNER JOIN BOOKS B ON L.BookID = B.BookID
    INNER JOIN BookAuthor BA ON B.BookID = BA.BookID
    INNER JOIN Authors A ON BA.AuthorID = A.AuthorID
    WHERE @MEMBERID = L.MemberID
    ORDER BY L.LoanDate DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMemberDetails]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetMemberDetails]
@MemberID INT
AS
BEGIN
SELECT * FROM Members m
WHERE m.MemberID = @MemberID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPaginatedBooks]    Script Date: 26.04.2025 10:48:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetPaginatedBooks]
@PAGENUMBER INT = 1,
@PAGESIZE INT = 10
AS 
BEGIN
SELECT B.BookID BookId, b.Title BookName, b.ISBN ISBN, b.PublishYear PublicationYear FROM Books b 
ORDER BY b.BookID
OFFSET (@PAGENUMBER - 1) * @PAGESIZE ROWS
FETCH NEXT @PAGESIZE ROWS ONLY;
END;
GO
USE [master]
GO
ALTER DATABASE [LibraryManagement] SET  READ_WRITE 
GO
