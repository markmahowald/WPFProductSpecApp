# Product Specification App

## Overview

This is a WPF application designed to manage product specifications, including details about products, materials, molds, and branding. The application utilizes a MySQL database with Entity Framework Core for data access. The project follows a DB-first approach, with a data layer that wraps database entities with business objects to encapsulate business logic.

## Features

- Manage Products, Materials, Molds, and Branding.
- Track historical changes for all entities using triggers and history tables.
- Business objects encapsulate business logic and validation.
- Responsive UI with WPF.
- Entity Framework Core for database access.

## Technologies Used

- **WPF**: Front-end UI framework for building desktop applications.
- **MySQL**: Relational database for storing product specification data.
- **Entity Framework Core**: ORM for interacting with the MySQL database.
- **.NET Core**: Platform for building and running the application.
- **Pomelo.EntityFrameworkCore.MySql**: MySQL provider for Entity Framework Core.

## Database Schema

The database schema consists of the following tables:

- `tbl_Products`
- `tbl_Materials`
- `tbl_Molds`
- `tbl_Clients`
- `tbl_Branding`
- Association tables for many-to-many relationships.
- History tables for tracking changes.

## Getting Started

### Prerequisites

- .NET Core SDK
- MySQL Server

### Setup

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/ProductSpecificationApp.git
    ```

2. **Create the database**:
    - Use the SQL script provided in the project to create the database and tables.

3. **Install dependencies**:
    ```bash
    dotnet restore
    ```

4. **Build the project**:
    ```bash
    dotnet build
    ```

5. **Run the application**:
    ```bash
    dotnet run
    ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contributing

Contributions are welcome! Please create a pull request with your changes.

