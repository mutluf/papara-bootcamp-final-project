# 📦 MyProject

This project is a modern backend application built with **.NET 8**, leveraging **Entity Framework Core**, **RabbitMQ**, **Redis**, and **Hangfire**. It follows clean architecture principles and uses **MediatR** for implementing the CQRS pattern.

---

## 🚀 Technologies Used

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Redis** – Caching
- **RabbitMQ** – Asynchronous messaging
- **Hangfire** – Background job processing
- **MediatR** – CQRS implementation
- **Swagger (Swashbuckle)** – API documentation
- **AutoMapper** – Mapping between DTOs and entities
- **FluentValidation** – Input validation rules
- **Microsoft Identity** – Authentication and role-based authorization

---
📁 Project Structure
```bash
 DualPay/
 ├── Presentation/DualPay.API                  # Controllers, ActionFilters, FilterAttributes
 ├── Core/DualPay.Application     	       # Business rules, CQRS handlers, interfaces, ValidationBehaviors, Events, Mappings, DTOs, Validators
 ├── Infrastructure/DualPay.Infrastructure     # External service configurations (e.g., Redis Caching, RabbitMQ[Consumers, Publishers]), Worker Service for consumers
 ├── Infrastructure/DualPay.Persistence        # EF Core DbContext, migrations, repositories, Hangfire[Background services, scheduled tasks], data seeders
 ├── Core/DualPay.Domain                       # Entities, Enums
 ├── PaymentWorker                             # RabbitMQ - Messaging[Consumers, Publishers], Events

```
## 🧰 Prerequisites

Before running the project, make sure the following are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) – To run Redis and RabbitMQ
- Optional tools:
  - SQL Server Management Studio (SSMS)
  - Postman (for API testing)

---

## 🐳 Redis & RabbitMQ via Docker

Start Redis and RabbitMQ using Docker:

```bash
# Start Redis
docker run -d --name redis -p 6379:6379 redis

# Start RabbitMQ (with management UI)
docker run -d --hostname rabbit --name rabbitmq \
  -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
RabbitMQ AMQP: amqp://localhost:5672

RabbitMQ UI: http://localhost:15672
(Username: guest, Password: guest)


⚙️ Getting Started
1. Clone the repository
```bash
git clone https://github.com/mutluf/papara-bootcamp-final-project.git
cd papara-bootcamp-final-project
```
2. Set up the database
3. Apply EF Core migrations:
```bash
dotnet ef database update
```
If you make changes to models later:
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## 🧩 EF Core & Project Roles in Onion Architecture

In this solution, the project follows the **Onion Architecture** pattern. Therefore, responsibilities are distributed across different layers. When working with Entity Framework Core, it's important to understand the role of each project:

### 📌 `MyProject.Api` – **Startup Project (for running the app)**

- This is the **entry point** of the application.
- It contains `Program.cs` / `Startup.cs`, and is responsible for configuring and running the whole app.
- Always use this project when:
  - Running the application (`dotnet run`)
  - Debugging
  - Hosting your APIs

### 📌 `MyProject.Persistence` – **Migration Project (for EF Core)**

- This project contains:
  - `DbContext` implementation
  - Entity Framework Core configurations (via Fluent API)
  - `Migrations` folder
- You must use this project as the **startup project when adding or updating EF Core migrations**.

### ⚙️ EF Core Migration Commands – Proper Setup

If you face issues like the database not being created or tables not appearing, follow these steps:

1. **Set `MyProject.Persistence` as Startup Project** in your IDE (e.g., Rider or Visual Studio).
2. Open terminal and navigate to the solution root.
3. Run the following commands:

```bash
"C:\Program Files\dotnet\dotnet.exe" ef migrations add --project Infrastructure\DualPay.Persistence\DualPay.Persistence.csproj --startup-project Presentation\DualPay.API\DualPay.API.csproj --context DualPay.Persistence.Context.DualPayDbContext --configuration Debug Initial --output-dir Migrations
"C:\Program Files\dotnet\dotnet.exe" ef database update --project Infrastructure\DualPay.Persistence\DualPay.Persistence.csproj --startup-project Presentation\DualPay.API\DualPay.API.csproj --context DualPay.Persistence.Context.DualPayDbContext --configuration Debug 20250505175415_Initial
⚠️ Stored Procedures
Stored procedures are not automatically run by EF Core.
You need to execute the SQL files manually:

# In SQL Server Management Studio or Azure Data Studio
Open and run files in: 
```bash
https://github.com/mutluf/papara-bootcamp-final-project/blob/main/dual-pay-stored-procedures-for-reports.sql
```
3. Configure appsettings.json
```bash
"ConnectionStrings": {
    "MicrosoftSQL": "Server=your_server;Database=DualPayDb;User Id=your_user;Password=your_password;TrustServerCertificate=True;"
  },
  "Token": {
    "Audience": "www.mydualpay.com",
    "Issuer": "www.mydualpayapi.com",
    "SecurityKey": "Welcome to the final DualP show!"
  },
  "InitialAdmin": {
    "Email": "dpadmin@dualpay.com.tr",
    "Password": "DP2025!"
  },
  "InitialUser": {
    "Email": "dpuser@dualpay.com.tr",
    "Password": "DP2025!"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Port" : "5672",
    "UserName" : "guest",
    "Password" : "guest"
  },
  "Redis": {
    "Configuration": "localhost:6379",
    "InstanceName": "Reports_"
  }
```
📌 Updated Configuration Section for Worker Services
 appsettings.json for Worker Service
```bash
{
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest"
  }
}
```



## ⚠️🚨 IMPORTANT: START BOTH PROJECTS! 🚨⚠️

# ✅ YOU MUST RUN BOTH SERVICES FOR THE SYSTEM TO WORK PROPERLY:

### 🔹 1. `DualPay` – Main API project (Onion Architecture)
> Located in: `DualPay/Presentation/DualPay.API`

### 🔹 2. `PaymentWorker` – Background worker service (RabbitMQ, Hangfire, etc.)
> Located in: `Dualpay/PaymentWorker`

These two projects **must** be running **simultaneously** for payments, background tasks, and messaging to function correctly.


