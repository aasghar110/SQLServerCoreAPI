# ASP.NET Core Web API Project

This project is an ASP.NET Core Web API application that provides endpoints for interacting with a database. It includes functionality for retrieving data, inserting data, and executing SQL queries.

## Project Structure

The project consists of the following components:

- **Controllers**: Contains API controllers that define the endpoints for various operations.
- **Models**: Contains classes representing request models for different API operations.
- **appsettings.json**: Configuration file for database connection strings.

## APIs

1. **GetData API**: Endpoint for executing stored procedures and retrieving data.

   - Endpoint: `/API/GetData`
   - HTTP Method: `POST`
   - Request Body:
     ```json
     {
         "SPName": "NameOfStoredProcedure",
         "Parameters": ["Param1", "Param2"],
         "Values": ["Value1", "Value2"]
     }
     ```
   - Response: JSON array containing retrieved data.

2. **InsertData API**: Endpoint for inserting data into the database using stored procedures.

   - Endpoint: `/API/InsertData`
   - HTTP Method: `POST`
   - Request Body:
     ```json
     {
         "SPName": "NameOfStoredProcedure",
         "Parameters": ["Param1", "Param2"],
         "Values": ["Value1", "Value2"]
     }
     ```
   - Response: JSON object indicating success or failure.

3. **ExecuteQuery API**: Endpoint for executing SQL queries on the database.

   - Endpoint: `/API/ExecuteQuery`
   - HTTP Method: `POST`
   - Request Body:
     ```json
     {
         "Query": "SQL Query",
         "QueryType": 0 // 0 for SELECT 1 for INSERT
     }
     ```
   - Response: JSON array containing query results or a success/error message.

## Configuration

Database connection strings are stored in the `appsettings.json` file. Ensure to configure these connection strings appropriately before running the application.

## Dependencies

- **Microsoft.AspNetCore.Mvc**: Provides MVC framework for building APIs.
- **Microsoft.Extensions.Configuration**: Provides configuration capabilities.
- **System.Data.SqlClient**: SQL Server data provider for database interactions.

## Running the Application

1. Clone the repository.
2. Configure database connection strings in `appsettings.json`.
3. Build and run the application.
4. Access APIs using appropriate endpoints and request payloads.

Developed with ❤️ by Ali Asghar
