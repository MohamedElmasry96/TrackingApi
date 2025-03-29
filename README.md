# Tracking API
A RESTful API for managing tracking orders, built with .NET 8, SQLite, and Swagger.

## Features
- Create, read, update, and delete tracking orders.
- Filter orders by client name, start date, and end date.
- Input validation and error handling.

## Technologies
- .NET 8
- Entity Framework Core (SQLite)
- Swagger for API documentation

## How to Run
1. Clone the repository.
2. Run `dotnet restore` to install dependencies.
3. Run `dotnet ef database update` to create the SQLite database.
4. Run `dotnet run` to start the API.
5. Open `https://localhost:5001` to access Swagger.