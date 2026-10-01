# Real-World E-Commerce Application (.NET Core MVC)

A full-featured, real-world E-Commerce web application built using **ASP.NET Core MVC**, **Entity Framework Core**, and **ASP.NET Core Identity**. The application demonstrates modern web development practices, clean architecture, and robust e-commerce features.

---

## 🛠 Tech Stack & Key Technologies

* **Framework:** ASP.NET Core MVC (.NET)
* **Data Access & ORM:** Entity Framework Core & SQL Server
* **Authentication & Authorization:** ASP.NET Core Identity (Role-based Authorization)
* **Frontend:** Razor Views, HTML5, CSS3, Bootstrap, JavaScript / jQuery
* **Architecture:** Repository Pattern & N-Tier / Layered Architecture
* **Tools & Libraries:** AutoMapper, SweetAlert / Toastr notifications, Session & Cookie management

---

## ✨ Key Features & Functionality

* 🔐 **User Authentication & Roles:** Secure Registration, Login, and Role-Based Access Control (Admin, Customer, etc.) using ASP.NET Core Identity.
* 🛍 **Product & Category Management:** Full CRUD operations for products, categories, and cover types (Admin Panel).
* 🛒 **Shopping Cart System:** Dynamic shopping cart management with session handling and database persistence.
* 💳 **Checkout & Order Processing:** Complete order lifecycle management, status updates, and order summaries.
* 👤 **User Profile & Order History:** Customers can view their order history and order details.
* 🛡 **Data Validation & Security:** Client-side and server-side model validation, Anti-Forgery Tokens (CSRF protection).

---

## 🚀 Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/) / LocalDB

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/hadeelaljamal/ECommerce.git
```

2. Navigate to the project directory:
```
cd ECommerce
```

3. Update Database Credentials:
Check ⁠appsettings.json⁠ and update the ⁠DefaultConnection⁠ string to match your local SQL Server instance.

4. Apply EF Core Migrations & Seed Database:
```
dotnet ef database update
```

5. Run the Application:
```
dotnet run
```

6. Open your browser and navigate to ⁠https://localhost:5001⁠ or ⁠http://localhost:5000⁠.







