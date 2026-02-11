# Logic Core - Dynamic Banking Calculation Platform

A robust, extensible, and secure calculation platform designed for banking operations.
This project demonstrates a decoupled architecture where the client (Frontend) is agnostic to the business logic residing on the server, allowing for dynamic extension of operations without client-side code changes.

## 🚀 Key Features

* **Dynamic Operation Loading:** Uses **Strategy Pattern** and **Reflection** to load calculation logic at runtime. New operations can be added to the backend without redeploying or modifying the frontend.
* **Modern Frontend:** Built with **Angular 18**, utilizing **Standalone Components**, **Signals** for reactive state management, and the new **Control Flow** syntax (`@if`, `@for`).
* **Robust Backend:** **.NET 8 Web API** with Clean Architecture principles.
* **Persistence:** SQL Server database integration using **Entity Framework Core (Code First)** for auditing and historical analysis (Bonus requirement).
* **Security:** Input validation and secure API endpoints.

## 🛠 Tech Stack

### Client Side (Frontend)
* **Framework:** Angular 18
* **Architecture:** Standalone Components (No Modules)
* **State Management:** Angular Signals
* **Styling:** SCSS with Flexbox/Grid
* **Communication:** HttpClient

### Server Side (Backend)
* **Framework:** .NET 8 Web API
* **Language:** C#
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Logging:** Serilog (Structured Logging)
* **Validation:** FluentValidation

## 🏗 Architecture & Design Patterns

The solution adheres to the **Open/Closed Principle**:
1.  **Strategy Pattern:** Each mathematical operation (Add, Subtract, etc.) is encapsulated in its own class implementing a common `ICalculationOperation` interface.
2.  **Factory/Reflection:** The server dynamically discovers all available operations at startup and exposes them via API.
3.  **Thin Client:** The Angular client fetches the metadata of available operations (Name, Key) and renders the UI dynamically. It does not contain hard-coded business logic.

## ⚙️ Getting Started

### Prerequisites
* Node.js (v18+)
* .NET 8 SDK
* SQL Server (LocalDB or Express)

### 1. Database Setup
The project uses EF Core Code First.
```bash
cd Logic-Core-Server
dotnet ef database update
cd Logic-Core-Server
dotnet run

cd Logic-Core-Client
npm install
ng serve --o

Usage
Select an operation from the dynamically loaded dropdown.

Enter values for Field A and Field B.

Click Calculate.

View the result along with historical usage statistics (Bonus Feature).