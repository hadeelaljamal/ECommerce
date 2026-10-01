# Full-Stack E-Commerce Web Application

A robust, full-stack e-commerce application built using **ASP.NET Core Web API** and **Angular**. This project implements modern software architecture patterns, clean coding standards, and essential e-commerce features including product management, basket handling, order processing, and secure checkout.

---

## 🛠️ Tech Stack & Key Technologies

### **Backend (.NET Core API)**
* **Framework:** ASP.NET Core Web API (C#)
* **Architecture:** Clean Architecture & Repository Pattern with Unit of Work
* **Database & ORM:** Entity Framework Core & SQL Server / SQLite
* **Caching & In-Memory Store:** Redis (for high-performance shopping cart management)
* **Authentication:** ASP.NET Core Identity & JWT (JSON Web Tokens)
* **Payment Integration:** Stripe API integration
* **Error Handling:** Global Error Handling Middleware & Standardized API Responses

### **Frontend (Angular)**
* **Framework:** Angular (TypeScript)
* **State & UI:** RxJS, Reactive Forms, Ngx-Bootstrap / Angular Material
* **HTTP Client:** Interceptors for JWT Tokens and Global Error Catching
* **Routing:** Lazy Loading Modules, Route Guards for Protected Endpoints

---

## ✨ Key Features & Functionality

* 🔐 **User Authentication & Authorization:** Secure registration and login using JWTs and ASP.NET Identity.
* 🛍️ **Product Catalog & Filtering:** Dynamic product listing with pagination, sorting, search, and filtering by brand or category.
* 🛒 **High-Performance Shopping Basket:** Redis-backed transient shopping cart for fast operations.
* 💳 **Checkout & Payment:** Seamless checkout process integrated with Stripe for secure online payments.
* 📦 **Order Management:** Detailed order processing, tracking, and user order history.
* 🛡️ **Robust Error Handling:** Custom middleware delivering standardized errors for client applications.

---

## 📐 Architectural Highlights

* **Specification Pattern:** Encapsulates query logic for clean, testable, and reusable database queries.
* **Repository & Unit of Work Patterns:** Decouples data access logic from business operations.
* **DTOs & AutoMapper:** Enforces data encapsulation between API endpoints and domain models.

---

## 🚀 Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download)
* [Node.js & npm](https://nodejs.org/)
* [Angular CLI](https://cli.angular.io/)
* [Redis](https://redis.io/) (via Docker or local service)

### Backend Setup
1. Clone the repository:
   ```bash
   git clone [https://github.com/your-username/your-repo-name.git](https://github.com/your-username/your-repo-name.git)
