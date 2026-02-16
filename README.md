# Logic Core - Dynamic Calculation Platform

A robust, extensible, and secure calculation platform designed for high-stakes operations. This project demonstrates a decoupled architecture where the client (Frontend) is completely agnostic to the business logic. All calculation rules are managed on the server, allowing for dynamic extension without client-side code changes or redeployments.

## 🚀 Key Features

* **Dynamic Formula Engine:** Utilizes **DynamicExpresso** to evaluate complex mathematical expressions stored in the database at runtime. The engine uses **Parameterization** to ensure strict separation between logic and data, preventing Code Injection attacks.
* **Smart Input Validation:** Features a database-driven validation system where each operation type can define its own **Regex rules** (e.g., allow only numbers for math operations). Invalid inputs are rejected with precise, user-friendly error messages before execution.
* **Multi-Type Support:** The dynamic engine seamlessly detects and processes both standard mathematical operations and string manipulations (like text concatenation) by intelligently parsing inputs at runtime.
* **Performance Optimization:** Database queries are optimized using **Composite Indexes** (`OperationKey` + `CreatedAt`) to ensure instant retrieval of historical logs even under heavy load.
* **Security & Resilience:** Implemented **Rate Limiting** middleware to prevent DDoS attacks and ensure fair usage of server resources.
* **Observability:** Integrated structured logging with **Serilog** (File/Console sinks) for advanced debugging and production-grade monitoring.
* **Audit & Analytics:** Integrated **Calculation Logs** that track every operation, providing real-time "Monthly Usage" statistics and "Recent History" per operation type.
* **Modern Frontend:** Built with **Angular 18**, utilizing **Standalone Components**, **Signals** for reactive state management, and the new **Control Flow**.
* **Robust Backend:** **.NET 8 Web API** following Clean Architecture principles and utilizing Dependency Injection.

## 🛠 Tech Stack

### Client Side (Frontend)
* **Framework:** Angular 18 (Standalone)
* **State Management:** Angular Signals
* **Styling:** SCSS (Flexbox/Grid)
* **Communication:** HttpClient with RxJS

### Server Side (Backend)
* **Framework:** .NET 8 Web API
* **Engine:** DynamicExpresso (Safe Expression Evaluation)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Code First)
* **Middleware:** Custom Exception Handling, Rate Limiting & CORS

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
cd Logic-Core-Server/Logic-Core-Server
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

**Tax Calculation**: `arg1 * 0.17  (Validation: ^-?\d+(\.\d+)?$)`

**Complex Interest**: `arg1 * Math.Pow(1 + arg2, 12)`
**String (Concatenation)**: `arg1 + arg2 (Input: "Hello", "World" -> Result: "HelloWorld")`