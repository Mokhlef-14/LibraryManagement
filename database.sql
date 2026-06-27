-- =============================================
-- Library Management System - Database Script
-- =============================================

USE LibraryManagementDb;

-- =============================================
-- SCHEMA
-- =============================================

CREATE TABLE Publishers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Address NVARCHAR(500),
    Phone NVARCHAR(50),
    Email NVARCHAR(200),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);

CREATE TABLE Authors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Bio NVARCHAR(1000),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);

CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ParentCategoryId INT,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (ParentCategoryId) REFERENCES Categories(Id)
);

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(300) NOT NULL,
    ISBN NVARCHAR(20) NOT NULL,
    Edition NVARCHAR(50),
    Summary NVARCHAR(2000),
    CoverImageUrl NVARCHAR(500),
    PublicationYear INT NOT NULL,
    Language NVARCHAR(50),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Available',
    PublisherId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (PublisherId) REFERENCES Publishers(Id)
);

CREATE TABLE BookAuthors (
    BookId INT NOT NULL,
    AuthorId INT NOT NULL,
    PRIMARY KEY (BookId, AuthorId),
    FOREIGN KEY (BookId) REFERENCES Books(Id),
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id)
);

CREATE TABLE BookCategories (
    BookId INT NOT NULL,
    CategoryId INT NOT NULL,
    PRIMARY KEY (BookId, CategoryId),
    FOREIGN KEY (BookId) REFERENCES Books(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

CREATE TABLE Members (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Address NVARCHAR(500),
    MembershipStartDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    MembershipEndDate DATETIME2,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);

CREATE TABLE SystemUsers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    LastLoginAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);

CREATE TABLE BorrowingTransactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    MemberId INT NOT NULL,
    SystemUserId INT NOT NULL,
    BorrowDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    DueDate DATETIME2 NOT NULL,
    ReturnDate DATETIME2,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
    Notes NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (BookId) REFERENCES Books(Id),
    FOREIGN KEY (MemberId) REFERENCES Members(Id),
    FOREIGN KEY (SystemUserId) REFERENCES SystemUsers(Id)
);

CREATE TABLE UserActivityLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SystemUserId INT NOT NULL,
    Action NVARCHAR(100) NOT NULL,
    Details NVARCHAR(500),
    IpAddress NVARCHAR(50),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (SystemUserId) REFERENCES SystemUsers(Id)
);

-- =============================================
-- SAMPLE DATA
-- =============================================

-- SystemUsers (Password = Admin@123)
INSERT INTO SystemUsers (FirstName, LastName, Email, PasswordHash, Role, IsActive, CreatedAt)
VALUES 
('Admin', 'User', 'admin@library.com', 'AQAAAAIAAYagAAAAEK+FtcXHnm8zcS2tuzWpj/xeHmA8bqLtni5XzZ2j5v9/LiIrMZ72ADanLaOiWwa4mw==', 'Administrator', 1, GETUTCDATE()),
('John', 'Librarian', 'librarian@library.com', 'AQAAAAIAAYagAAAAEK+FtcXHnm8zcS2tuzWpj/xeHmA8bqLtni5XzZ2j5v9/LiIrMZ72ADanLaOiWwa4mw==', 'Librarian', 1, GETUTCDATE()),
('Jane', 'Staff', 'staff@library.com', 'AQAAAAIAAYagAAAAEK+FtcXHnm8zcS2tuzWpj/xeHmA8bqLtni5XzZ2j5v9/LiIrMZ72ADanLaOiWwa4mw==', 'Staff', 1, GETUTCDATE());

-- Publishers
INSERT INTO Publishers (Name, Address, Phone, Email, CreatedAt)
VALUES 
('Dar Al Shorouk', 'Cairo, Egypt', '0201234567', 'info@shorouk.com', GETUTCDATE()),
('Penguin Books', 'London, UK', '+441234567', 'info@penguin.com', GETUTCDATE()),
('Oxford Press', 'Oxford, UK', '+441234568', 'info@oxford.com', GETUTCDATE());

-- Authors
INSERT INTO Authors (FirstName, LastName, Bio, CreatedAt)
VALUES 
('Naguib', 'Mahfouz', 'Egyptian Nobel Prize winning author', GETUTCDATE()),
('George', 'Orwell', 'English novelist and essayist', GETUTCDATE()),
('Fyodor', 'Dostoevsky', 'Russian novelist', GETUTCDATE());

-- Categories
INSERT INTO Categories (Name, Description, ParentCategoryId, CreatedAt)
VALUES 
('Fiction', 'Fictional literature', NULL, GETUTCDATE()),
('Non-Fiction', 'Non-fictional literature', NULL, GETUTCDATE()),
('Novel', 'Long fictional narrative', 1, GETUTCDATE()),
('Classic', 'Classic literature', 1, GETUTCDATE());

-- Books
INSERT INTO Books (Title, ISBN, Edition, Summary, PublicationYear, Language, Status, PublisherId, CreatedAt)
VALUES 
('Cairo Trilogy', '978-977-09-0965-0', '1st', 'Epic saga of a Cairo family', 1956, 'Arabic', 'Available', 1, GETUTCDATE()),
('1984', '978-0-452-28423-4', '1st', 'Dystopian novel', 1949, 'English', 'Available', 2, GETUTCDATE()),
('Crime and Punishment', '978-0-14-044913-6', '2nd', 'Psychological novel', 1866, 'English', 'Available', 3, GETUTCDATE());

-- BookAuthors
INSERT INTO BookAuthors (BookId, AuthorId)
VALUES (1, 1), (2, 2), (3, 3);

-- BookCategories
INSERT INTO BookCategories (BookId, CategoryId)
VALUES (1, 3), (1, 4), (2, 3), (2, 4), (3, 3), (3, 4);

-- Members
INSERT INTO Members (FirstName, LastName, Email, Phone, Address, MembershipStartDate, IsActive, CreatedAt)
VALUES 
('Ahmed', 'Ali', 'ahmed@email.com', '0201111111', 'Cairo, Egypt', GETUTCDATE(), 1, GETUTCDATE()),
('Sara', 'Mohamed', 'sara@email.com', '0202222222', 'Alexandria, Egypt', GETUTCDATE(), 1, GETUTCDATE());
