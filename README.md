# 🛒 Mini E-Commerce

A full-stack e-commerce demo application built with **ASP.NET Core 8 Web API** (Backend) and **Blazor Server** (Frontend), following **Clean Architecture** principles.

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?logo=microsoftsqlserver)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [API Endpoints](#-api-endpoints)
- [Database Schema](#-database-schema)
- [Getting Started](#-getting-started)
- [License](#-license)

---

## 📖 Overview

**Mini E-Commerce** is a junior-level project demonstrating a clean, layered architecture for building RESTful APIs with a Blazor Server frontend. It covers the essential e-commerce operations: managing products, placing orders with automatic stock validation, and applying quantity-based discounts.

---

## ✨ Features

### Backend (Web API)

- ✅ **Product Management** — Create products and list them with server-side pagination
- ✅ **Order Processing** — Place orders with multiple items, automatic stock deduction, and validation
- ✅ **Quantity-Based Discounts** — Automatic discount calculation:
  - `5%` discount for orders with **2–4** total items
  - `10%` discount for orders with **5+** total items
- ✅ **Stock Validation** — Prevents orders exceeding available inventory
- ✅ **Swagger / OpenAPI** — Interactive API documentation in development mode
- ✅ **CORS Enabled** — Allows cross-origin requests from any frontend

### Frontend (Blazor Server)

- 🖥️ **Products Page** — Browse all products in a paginated table with stock badges
- ➕ **Create Product** — Form with validation to add new products
- 🛍️ **Create Order** — Dynamic order form with product dropdown, quantity inputs, and item management
- 🔍 **Order Lookup** — Search for an order by its GUID
- 📄 **Order Details** — Full order summary with items, subtotal, discount breakdown, and final total
- 🎨 **Responsive UI** — Built with Bootstrap 5 and Bootstrap Icons

---

## 🏗 Architecture

The backend follows **Clean Architecture** with four layers:
┌──────────────────────────────────────────────────┐
│                  Presentation                    │ 
│          (ASP.NET Core Web API Controllers)      │ 
├──────────────────────────────────────────────────┤ 
│                  Application                     │ 
│       (Services, DTOs, Interfaces)               │ 
├──────────────────────────────────────────────────┤ 
│                    Domain                        │ 
│            (Entities, Base Classes)              │ 
├──────────────────────────────────────────────────┤ 
│              Infrastructure                      │ 
│    (EF Core DbContext, Repositories, Migrations) │ 
└──────────────────────────────────────────────────┘

**Dependency Flow:** Presentation → Application → Domain ← Infrastructure

---

## 🛠 Tech Stack

| Layer          | Technology                          |
|----------------|-------------------------------------|
| **API**        | ASP.NET Core 8 Web API              |
| **Frontend**   | Blazor Server (.NET 8)              |
| **ORM**        | Entity Framework Core 8             |
| **Database**   | SQL Server                          |
| **Docs**       | Swagger / Swashbuckle               |
| **Language**   | C# 12                               |


---

## 🔌 API Endpoints

### Products

| Method | Endpoint               | Description              |
|--------|------------------------|--------------------------|
| `POST` | `/api/Products/Create` | Create a new product     |
| `GET`  | `/api/Products/GetAll`  | Get paginated product list |

#### Query Parameters for `GetAll`

| Parameter  | Type | Default | Description    |
|------------|------|---------|----------------|
| `page`     | int  | 1       | Page number    |
| `pageSize` | int  | 10      | Items per page |

#### Create Product — Request Body
{ "name": "Wireless Mouse", "price": 29.99, "availableQuantity": 100 }

### Orders

| Method | Endpoint                   | Description              |
|--------|----------------------------|--------------------------|
| `POST` | `/api/Orders/Create`       | Place a new order        |
| `GET`  | `/api/Orders/GetById/{id}` | Get order details by GUID |

#### Create Order — Request Body
{ "customerName": "Ahmed Mohamed", 
"customerEmail": "ahmed@example.com", 
"items": 
[ { "productId": "GUID-HERE", "quantity": 2 }, 
{ "productId": "GUID-HERE", "quantity": 3 } ] 
}


#### Order Response Example

{ "id": "...",
"customerName": "Ahmed Mohamed", 
"customerEmail": "ahmed@example.com", 
"createdAt": "2025-03-02T09:40:00Z", 
"totalItems": 5, "subTotal": 149.95,
"discountPercentage": 10, 
"discountAmount": 14.99,
"finalTotal": 134.96, 
"items": [ 
{ 
"id": "...", 
"productId": "...",
"productName": "Wireless Mouse", 
"quantity": 2, 
"unitPrice": 29.99,
"lineTotal": 59.98 
} 
] 
}


---

## 🗄 Database Schema
┌─────────────┐       ┌──────────────┐       ┌─────────────┐ 
│   Products  │       │  OrderItems  │       │   Orders    │ 
├─────────────┤       ├──────────────┤       ├─────────────┤ 
│ Id (PK)     │◄──────│ ProductId(FK)│       │ Id (PK)     │ 
│ Name        │       │ OrderId (FK) │──────►│ CustomerName│ 
│ Price       │       │ Quantity     │       │CustomerEmail│ 
│ AvailableQty│       │ UnitPrice    │       │ CreatedAt   │ 
└─────────────┘       │ LineTotal    │       │ TotalItems  │ 
                      └──────────────┘       │ SubTotal    │ 
                                             │ Discount %  │ 
                                             │ DiscountAmt │ 
                                             │ FinalTotal  │ 
                                             └─────────────┘

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB or full instance)

### 1. Clone the Repository
git clone https://github.com/engmos1/MiniE-Commerce.git cd MiniE-Commerce


### 2. Configure the Database

Update the connection string in `Backend/Mini E-Commerce (Junior Level)/appsettings.json`:
{ "ConnectionStrings": 
{ "DefaultConnection": "Server=YOUR_SERVER;Database=MiniECommerceDb;Trusted_Connection=True;TrustServerCertificate=True" } }


### 3. Apply Migrations
cd Backend dotnet ef database update --project 
"Infrastructure (Data Access)" --startup-project "Mini E-Commerce (Junior Level)"

### 4. Run the Backend API
cd "Backend/Mini E-Commerce (Junior Level)" dotnet run


The API will be available at `http://localhost:5038`.  
Swagger UI is accessible at `http://localhost:5038/swagger`.

### 5. Run the Blazor Frontend

Open a **new terminal**:
cd ECommerce.Web dotnet run


The frontend will be available at `https://localhost:60828`.

> **Note:** Make sure `ApiSettings:BaseUrl` in `ECommerce.Web/appsettings.json` matches your backend URL.

---

## 📜 License

This project is open source and available under the [MIT License](LICENSE).

---

<p align="center">Made with ❤️ using .NET 8 & Blazor</p>
