# Logic Core - Dynamic Calculation Platform

A robust, extensible, and secure calculation platform designed for high-stakes operations. This project demonstrates a decoupled architecture where the client (Frontend) is completely agnostic to the business logic. All calculation rules are managed on the server, allowing for dynamic extension without client-side code changes or redeployments.

## 🚀 Key Features

* **Dynamic Formula Engine:** Utilizes DynamicExpresso to evaluate complex mathematical expressions stored in the database at runtime. This allows the to add new products (e.g., Tax, Interest, Commissions) by simply updating a database record.
* **Audit & Analytics (Bonus):** Integrated Calculation Logs that track every operation, providing real-time "Monthly Usage" statistics and "Recent History" per operation type.
* **Modern Frontend:** Built with Angular 18, utilizing Standalone Components, Signals for reactive state management, and the new Control Flow syntax (@if, @for).
* **Robust Backend:** .NET 8 Web API following Clean Architecture principles and utilizing Dependency Injection.
* **Persistence:** SQL Server database integration using Entity Framework Core (Code First) with automated migrations.

## 🛠 Tech Stack

### Client Side (Frontend)
* **Framework:** Angular 18 (Standalone)
* **State Management:** Angular Signals
* **Styling:** SCSS (Flexbox/Grid)
* **Communication:** HttpClient with RxJS

### Server Side (Backend)
* **Framework:** .NET 8 Web API
* **Engine:** DynamicExpresso (Expression Evaluation)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Middleware:** Custom Exception Handling & CORS

## 🏗 Architecture & Design Principles

The solution is a textbook implementation of the Open/Closed Principle:

* **Expression Evaluation Pattern:** Instead of hard-coded Strategy classes, the server acts as a dynamic evaluator. It fetches the formula $f(arg1, arg2)$ from the DB and computes the result for any given inputs.
* **Metadata-Driven UI:** The Angular client fetches available operations (Name, Key, Symbol) from the API and renders the UI dynamically.
* **Data Persistence & History:** Every calculation is persisted in the CalculationLogs table, enabling historical tracking and usage limits.

## ⚙️ Getting Started

### Prerequisites
* Node.js (v18+)
* .NET 8 SDK
* SQL Server (LocalDB)

### 1. Database Setup
* Update the connection string in `appsettings.json`  and run:
 ```bash
cd Logic-Core-Server
dotnet ef database update
```
### 2. Run the Backend
```bash
dotnet run 
```

### 3. Run the Frontend 
```bash
cd Logic-Core-Client
npm install
ng serve --o
```

### Dynamic Logic Example
The engine can evaluate any valid C# expression stored as a string:

**Add**: `arg1 + arg2`

**Tax Calculation**: `arg1 * 0.17`

**Complex Interest**: `arg1 * Math.Pow(1 + arg2, 12)`