# Customer Order API TEST (ASP.NET Core Web API - .NET 8)

## 🔧 Technologies Used
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (InMemory)
- AutoMapper
- Swagger

## 📦 Project Structure
- `/Controllers` – REST endpoints
- `/Entities` – Customer and Order models
- `/DTOs` – Data Transfer Objects
- `/Services` – Business logic
- `/Data` – DbContext and initial data

  
## Architecture
- **Controllers**: Handle HTTP requests and responses.
- **Services**: Business logic for customers and orders, defined via interfaces and implemented in the `Services` folder.
- **Data Layer**: Uses Entity Framework Core with an in-memory database for demo purposes.
- **DTOs**: Data Transfer Objects for input/output models.
- **AutoMapper**: Handles mapping between DTOs and entity models.
- **Serilog**: Provides structured logging to console and rolling log files.
- **Swagger**: API documentation and testing UI.

## Setup Instructions
1. **Clone the repository**
2. **Build the project**
3. **Run the project**
4. **Access the API**
   - Open your browser and navigate to (http://localhost:5150/swagger) to view and test the API endpoints using Swagger UI.

## Features

- CRUD operations for Customers and Orders.
- In-memory database for easy setup and testing.
- AutoMapper for clean DTO/entity mapping.
- Serilog for robust logging.
- API documentation with Swagger.

## Notes

- The database is seeded with initial data on startup.
- All dependencies are registered in `Program.cs`.
- Update the in-memory database to a persistent provider (e.g., SQLite, SQL Server) for production use.
