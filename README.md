# Employee Management API

This is a RESTful Web API project built with **ASP.NET Core**, **Entity Framework Core**, and **JWT Authentication**. It provides secure endpoints for user registration, login, and employee management.

---

## 🔧 Tech Stack

- **ASP.NET Core 8**
- **Entity Framework Core** (Database First)
- **SQL Server**
- **JWT Authentication**
- **Swagger (OpenAPI)**
- **Dependency Injection**

---

## 🚀 Features

- 🔐 User Registration & Login (with password hashing)
- 🔑 JWT Token-based Authentication
- 📋 Protected Routes with `[Authorize]`
- 🛠 Swagger UI with `Authorize 🔒` token button
- 🧩 Modular architecture for scalability

---

## 📁 Project Structure

EmployeeWithJWT/
│
├── Controllers/ # API controllers
├── Models/ # Entity Framework models
├── Interfaces/ # IRepository, IEmployeeRepository
├── Repositories/ # Generic & specific repositories
├── Helpers/ # JWT helper, password hashing
├── appsettings.json # Connection string & JWT config
└── Program.cs # App entry point + DI setup
