# 📄 BankAB – Secure Digital Banking Web App

Welcome to **BankAB**, a secure, modern, and data-rich digital banking platform built using **ASP.NET Core Razor Pages** and **Entity Framework Core**. This project was created for long-term educational and professional development as part of the .NET Full Stack program at KYH.

---
## 📚 Table of Contents
- [🚀 Project Overview](#-project-overview)
- [🌐 Live Website](#-live-website)
- [🧱 Tech Stack](#-tech-stack)
- [⚙️ Architecture & Patterns](#️-architecture--patterns)
- [🧩 Core Features](#-core-features)
  - [👤 Customers (CRUD)](#-customers-crud)
  - [💳 Accounts & Transactions (CRUD)](#-accounts--transactions-crud)
  - [📊 Statistics & Analytics](#-statistics--analytics)
  - [🔐 Identity & Authorization](#-identity--authorization)
  - [🎨 UI Enhancements](#-ui-enhancements)
- [🛠 Setup Instructions](#-setup-instructions)
- [🙌 Contributors](#-contributors)

## 🚀 Project Overview
BankAB simulates the operations of a full-service banking system, including customer management, account tracking, transactions, and administrative tools. It's designed for maintainability, scalability, and user-friendly interaction.

---

## 🌐 Live Website

🌐 Deployed on Azure:  
**https://nextgenbank-h2a2hxhqa4a2gbgx.swedencentral-01.azurewebsites.net/**

## 🧱 Tech Stack
- **ASP.NET Core Razor Pages** (.NET 9)
- **Entity Framework Core** (Code-First + Migrations)
- - **Dependency Injection** (Built-in .NET Core DI for services)
- **SQL Server** (local & cloud ready)
- **Bootstrap 5** – Based on the **FlexStart** template  
  (🌟 *Heavily customized for layout, responsiveness, and Razor integration*)
- **Font Awesome & Bootstrap Icons**
- **AOS (Animate On Scroll)** + **PureCounter.js**

---
## ⚙️ Architecture & Patterns

- **Separation of Concerns**: Pages, Services, Data Access Layers are separated
- **Dependency Injection**: Injected services (e.g., `ICustomerService`, `IAccountService`) for testable, clean code
- **Partial Views & Components**: For reusable layout sections like tables, stats, etc.


## 🧩 Core Features


### 👤 Customers (CRUD)
- Paginated customer list with search & sort
- View detailed profile with demographic & contact info
- Group customers by country, city, or gender
- Create/Edit/Delete customers with dropdowns (gender, country)

### 💳 Accounts & Transactions (CRUD)
- View all accounts, sortable & paginated
- View transactions by customer or account
- Perform deposit/withdrawals with validation
- AJAX-powered "Show More" button for dynamic loading

### 📊 Statistics & Analytics
- Homepage stats: total customers, accounts, balances by country
- Top 10 customers per country with country flag support

### 🔐 Identity & Authorization
- ASP.NET Core Identity for login & registration
- Custom login/register layout
- Admin-only page to manage application users

### 🎨 UI Enhancements
- Responsive layout with FlexStart template
- Scroll-to-top button
- Buttons with gradients, hover effects, and Bootstrap icons

---



## 🛠 Setup Instructions
1. Clone the repo: **https://github.com/LuxmiPalma/BankAB.git** 
2. Update `appsettings.json` with your SQL Server connection string.
3. Run migrations (if needed): `dotnet ef database update`
4. Launch with `dotnet run` or from Visual Studio.

---


## 🙌 Contributors
- 👩‍💻 **Luxmi Agnes Palma** – .Net Developer (Student)

---




> “NextGen Bank – Built to grow with your future.”
