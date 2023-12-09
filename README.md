
# EmployeeAPI - Employee Management Microservice

## Overview

EmployeeAPI is a robust and efficient microservice designed to perform CRUD (Create, Read, Update, Delete) operations on Employee entities. It is built using ASP.NET Core and utilizes a SQL Server database for data persistence. The service is tailored to handle high request-per-second (RPS) workloads and is suitable for managing large volumes of employee data.

## Features

-   **CRUD Operations**: Supports creating, reading, updating, and deleting employee records.
-   **Swagger Integration**: Includes Swagger for API documentation and testing.
-   **Department Classification**: Categorizes employees into departments such as DEVELOPMENT, QUALITY_ASSURANCE, TESTING, and DEVOPS.

## Getting Started

### Prerequisites

-   .NET 6 SDK
-   SQL Server

### Setup

1.  Clone the repository.
2.  Update the connection string in `appsettings.json` to point to your SQL Server instance.
3.  Run the application. This will start the web server and the application will be accessible locally.

### Building and Running

Use the following command to build and run the application:

shellCopy code

`dotnet run` 

## Architecture

### Core Components

-   `Program.cs`: Bootstrapping and configuration of the application.
-   `DatabaseContext.cs`: Entity Framework Core context for database interactions.
-   `EmployeeController.cs`: API controller for handling employee-related requests.

### Models

-   `Employee`: Represents the employee entity with properties like ID, firstname, lastName, email, and department.
-   `Department`: Enumeration representing various departments within the organization.

### Services

-   `IEmployeeService`: Interface defining the contract for employee services.
-   `EmployeeService`: Implementation of `IEmployeeService`, handling business logic for employee operations.

## API Endpoints

-   `GET /api/employee`: Retrieves all employees.
-   `POST /api/employee/add`: Adds a new employee.
-   `GET /api/employee/display`: Displays all employees.
-   `GET /api/employee/edit/{id}`: Retrieves an employee for editing.
-   `POST /api/employee/edit`: Updates an employee.
-   `POST /api/employee/delete/{id}`: Deletes an employee.

## Testing

Outline test cases for each API endpoint to ensure functionality and robustness. (Test implementation not provided in the codebase.)

## Notes

-   Ensure proper configuration of the database connection before running the application.
-   This application is configured for development environments. For production deployment, additional configuration and security measures are recommended.