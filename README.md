# 📚 Library Management System

A RESTful API built with **ASP.NET Core 8** following **Clean Architecture** principles.

---

## 🏗️ Architecture

The solution is divided into 4 layers:

```
LibraryManagement.sln
├── LibraryManagement.API           → Controllers, Program.cs
├── LibraryManagement.Application   → CQRS (Commands/Queries), DTOs, MediatR, JwtService
├── LibraryManagement.Domain        → Entities, Enums
└── LibraryManagement.Infrastructure → EF Core, DbContext, Configurations
```

### Design Patterns Used
- **Clean Architecture** - Separation of concerns across layers
- **CQRS with MediatR** - Commands and Queries are separated
- **Repository Pattern** - Via EF Core DbContext abstraction (IAppDbContext)

---

## 🛠️ Tech Stack

| Technology | Version |
|---|---|
| ASP.NET Core | 8.0 |
| Entity Framework Core | 8.0 |
| SQL Server | LocalDB / SQL Server |
| MediatR | 12.2.0 |
| JWT Authentication | 8.0.0 |
| Swagger (Swashbuckle) | 6.6.2 |
| FluentValidation | 11.9.0 |

---

## 🗂️ Database Schema

### Entities

| Entity | Description |
|---|---|
| **Book** | Books with metadata (ISBN, Edition, Summary, CoverImage, Status) |
| **Author** | Book authors (Many-to-Many with Books) |
| **Publisher** | Book publishers (One-to-Many with Books) |
| **Category** | Hierarchical categories (self-referencing) |
| **Member** | Library members (borrowers) |
| **SystemUser** | Staff with role-based access (Admin/Librarian/Staff) |
| **BorrowingTransaction** | Borrowing and return records |
| **UserActivityLog** | System user activity tracking |

### Relationships
- Book ↔ Author: **Many-to-Many** (via BookAuthor)
- Book ↔ Category: **Many-to-Many** (via BookCategory)
- Book → Publisher: **Many-to-One**
- Member → BorrowingTransaction: **One-to-Many**
- SystemUser → BorrowingTransaction: **One-to-Many**
- Category → Category: **Self-referencing** (Parent/Child)

---

## 🔐 Authentication & Authorization

JWT Bearer Token authentication with 3 roles:

| Role | Permissions |
|---|---|
| **Administrator** | Full access to all endpoints |
| **Librarian** | Can manage books, members, and borrowing |
| **Staff** | Read-only access |

### Getting a Token
```
POST /api/Auth/login
{
  "email": "admin@library.com",
  "password": "Admin@123"
}
```

Use the token in the Authorization header:
```
Authorization: Bearer {token}
```

---

## 📡 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | /api/Auth/login | Login and get JWT token | ❌ |

### Books
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/Books | Get all books | All |
| GET | /api/Books/{id} | Get book by ID | All |
| GET | /api/Books/search | Search by name/author/category | All |
| GET | /api/Books/status/{status} | Get books by status | All |
| POST | /api/Books | Create a book | Admin/Librarian |
| PUT | /api/Books/{id} | Update a book | Admin/Librarian |
| DELETE | /api/Books/{id} | Delete a book | Admin |

### Authors
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/Authors | Get all authors | All |
| GET | /api/Authors/{id} | Get author by ID | All |
| POST | /api/Authors | Create an author | Admin/Librarian |
| PUT | /api/Authors/{id} | Update an author | Admin/Librarian |
| DELETE | /api/Authors/{id} | Delete an author | Admin |

### Publishers
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/Publishers | Get all publishers | All |
| GET | /api/Publishers/{id} | Get publisher by ID | All |
| POST | /api/Publishers | Create a publisher | Admin/Librarian |
| PUT | /api/Publishers/{id} | Update a publisher | Admin/Librarian |
| DELETE | /api/Publishers/{id} | Delete a publisher | Admin |

### Categories
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/Categories | Get all categories | All |
| GET | /api/Categories/{id} | Get category by ID | All |
| POST | /api/Categories | Create a category | Admin/Librarian |
| PUT | /api/Categories/{id} | Update a category | Admin/Librarian |
| DELETE | /api/Categories/{id} | Delete a category | Admin |

### Members
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/Members | Get all members | All |
| GET | /api/Members/{id} | Get member by ID | All |
| POST | /api/Members | Create a member | Admin/Librarian |
| PUT | /api/Members/{id} | Update a member | Admin/Librarian |
| DELETE | /api/Members/{id} | Delete a member | Admin |

### System Users
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/SystemUsers | Get all users | Admin |
| GET | /api/SystemUsers/{id} | Get user by ID | Admin |
| POST | /api/SystemUsers | Create a user | Admin |
| PUT | /api/SystemUsers/{id} | Update a user | Admin |
| DELETE | /api/SystemUsers/{id} | Delete a user | Admin |

### Borrowing Transactions
| Method | Endpoint | Description | Role |
|---|---|---|---|
| GET | /api/BorrowingTransactions | Get all transactions | All |
| GET | /api/BorrowingTransactions/{id} | Get transaction by ID | All |
| POST | /api/BorrowingTransactions | Borrow a book | Admin/Librarian |
| PUT | /api/BorrowingTransactions/{id}/return | Return a book | Admin/Librarian |

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server or LocalDB
- Visual Studio 2022 or VS Code

### Setup

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/LibraryManagement.git
cd LibraryManagement
```

2. **Update connection string** in `LibraryManagement.API/appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LibraryManagementDb;Integrated Security=True;TrustServerCertificate=True;"
}
```

3. **Run migrations**
```bash
dotnet ef database update --project LibraryManagement.Infrastructure --startup-project LibraryManagement.API
```

4. **Run the application**
```bash
dotnet run --project LibraryManagement.API
```

5. **Open Swagger UI**
```
https://localhost:7134/swagger
```

### Default Users

| Email | Password | Role |
|---|---|---|
| admin@library.com | Admin@123 | Administrator |
| librarian@library.com | Admin@123 | Librarian |
| staff@library.com | Admin@123 | Staff |

---

## 📁 Project Structure

```
LibraryManagement/
├── LibraryManagement.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── BooksController.cs
│   │   ├── AuthorsController.cs
│   │   ├── PublishersController.cs
│   │   ├── CategoriesController.cs
│   │   ├── MembersController.cs
│   │   ├── SystemUsersController.cs
│   │   └── BorrowingTransactionsController.cs
│   ├── appsettings.json
│   └── Program.cs
├── LibraryManagement.Application/
│   ├── Data/
│   │   └── IAppDbContext.cs
│   ├── DTOs/
│   ├── Features/
│   │   ├── Auth/
│   │   ├── Books/
│   │   ├── Authors/
│   │   ├── Publishers/
│   │   ├── Categories/
│   │   ├── Members/
│   │   ├── SystemUsers/
│   │   └── BorrowingTransactions/
│   ├── Services/
│   │   └── JwtService.cs
│   └── DependencyInjection.cs
├── LibraryManagement.Domain/
│   ├── Entities/
│   └── Enums/
└── LibraryManagement.Infrastructure/
    ├── Data/
    │   ├── AppDbContext.cs
    │   └── Configurations/
    └── DependencyInjection.cs
```

---

## 🎯 Design Decisions

1. **Clean Architecture** - Each layer has a single responsibility and dependencies flow inward only.
2. **CQRS with MediatR** - Separates read and write operations for better scalability and maintainability.
3. **IAppDbContext Interface** - Abstracts the DbContext to avoid circular dependencies between layers.
4. **PasswordHasher** - Uses ASP.NET Core's built-in `PasswordHasher` for secure password storage instead of third-party libraries.
5. **Hierarchical Categories** - Self-referencing Category entity supports unlimited nesting levels.
6. **Book Status** - Automatically updated when a book is borrowed or returned.
