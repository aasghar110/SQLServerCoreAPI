# SQLServerCoreAPI

SQL Server Toolkit for .NET Core

This toolkit provides a set of APIs for easy integration of SQL Server databases with .NET Core applications. Whether you need to retrieve data, execute stored procedures, or perform select and insert queries, this toolkit simplifies database interactions and enhances performance.

## Features

- **Retrieve Data**: Fetch data from SQL Server databases effortlessly.
- **Execute Stored Procedures**: Seamlessly execute stored procedures for complex operations.
- **Query Handling**: Execute select and insert queries with ease.
- **Efficient Performance**: Streamline database tasks to improve overall application performance.
- **Simple Integration**: User-friendly APIs for quick integration into .NET Core projects.

## Usage

1. Clone the repository to your local machine.
2. Install the necessary dependencies using NuGet Package Manager.
3. Configure the connection string in the `appsettings.json` file.
4. Start using the APIs in your .NET Core application for smooth database interactions.

## API Endpoints

- `/API/GetData`: Retrieve data from the database.
- `/API/InsertData`: Insert data into the database.
- `/API/ExecuteQuery`: Execute select and insert queries.

## Example

```csharp
// Retrieve data example
// Make a POST request to /API/GetData with JSON body containing stored procedure name, parameters, and values.

// Insert data example
// Make a POST request to /API/InsertData with JSON body containing stored procedure name, parameters, and values.

// Execute query example
// Make a POST request to /API/ExecuteQuery with JSON body containing the SQL query and query type.

Developed by Ali Asghar with ❤️

