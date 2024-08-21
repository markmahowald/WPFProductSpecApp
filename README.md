Product Specification WPF Application
=====================================

Welcome to the **Product Specification WPF Application**. This project showcases a complete implementation of a WPF application built with modern development practices, including the MVVM pattern, Entity Framework Core for data access, and the MVVM Community Toolkit for messaging and command handling.

Table of Contents
-----------------

*   [Introduction](#introduction)
    
*   [Features](#features)
    
*   [Architecture](#architecture)
    
    *   [Database Structure](#database-structure)
        
    *   [Data Layer](#data-layer)
        
    *   [Business Objects](#business-objects)
        
    *   [WPF MVVM Implementation](#wpf-mvvm-implementation)
        
    *   [Messaging System](#messaging-system)
        
*   [Installation](#installation)
    
*   [Usage](#usage)
    
*   [Contributing](#contributing)
    
*   [License](#license)
    

Introduction
------------

This application is designed to manage and edit product specifications, including details for products, materials, molds, clients, and branding. It serves as an example of how to build a robust WPF application using best practices and modern tools.

Features
--------

*   **CRUD Operations**: Create, read, update, and delete operations for Products, Materials, Molds, Clients, and Branding.
    
*   **MVVM Pattern**: A clean separation of concerns with the Model-View-ViewModel pattern.
    
*   **Entity Framework Core**: Database access with MySQL, using a DB-first approach.
    
*   **Messaging System**: Notifications and updates across the application using the MVVM Community Toolkit.
    

Architecture
------------

### Database Structure

The application utilizes a MySQL database with several key tables, including:

*   tbl\_Products: Stores product details.
    
*   tbl\_Materials: Stores material details.
    
*   tbl\_Molds: Stores mold details.
    
*   tbl\_Clients: Stores client information.
    
*   tbl\_Branding: Stores branding information.
    
*   **Association Tables**: (tbl\_ProductMaterials, tbl\_ProductMolds, tbl\_ProductBranding) manage many-to-many relationships.
    

Historical tables and triggers are implemented to track changes, ensuring data integrity and traceability.

### Data Layer

The data layer is built with Entity Framework Core and follows the Repository pattern, providing a clear abstraction over data access.

*   **ProductSpecificationDbContext**: Manages the database context and entity sets.
    
*   **Repositories**: Implemented for each entity, such as ProductRepository, MaterialRepository, providing methods like GetAll, GetById, Add, and Update.
    

### Business Objects

Business objects represent domain models with additional logic beyond the database entities. They handle validation, business rules, and updates.

```
public class Product : BusinessObject
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Height { get; set; }
    public decimal? Width { get; set; }
    public string? Unit { get; set; }
    public string? SKU { get; set; }
    public DateTime? UpdateDate { get; set; }
    // Additional logic...
}
```

### WPF MVVM Implementation

The application’s front-end is built using WPF and adheres strictly to the MVVM pattern.

*   **Main Window**: A sidebar with buttons allows navigation between different entities. Each button loads a corresponding DataGrid populated with data.
    
*   **Display Views**: Grids display entity data with an "Edit" button for each item.
    
*   **Edit Views**: Provide detailed forms for editing entities, with full data binding and validation.
```
<Button Content="Edit" 
        Command="{Binding DataContext.EditProductCommand, RelativeSource={RelativeSource AncestorType=UserControl}}"
        CommandParameter="{Binding}" />
```

### Messaging System

The MVVM Community Toolkit’s messaging system is used to send notifications when data is saved, ensuring that the UI stays in sync with the database.

*   **SaveSuccessfulMessage**: Sent after an entity is successfully saved, carrying the object type and ID.
    
*   **Subscribers**: Display view models listen for this message and refresh their data accordingly.

```
WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Product), FocusedProduct.ProductId));
```

Installation
------------

1.  **Clone the Repository**:
2.  **Set Up the Database**: Create the MySQL database using the provided schema.
3.   **Configure Connection String**: Update the connection string in ProductSpecificationDbContext.
4.   **Restore NuGet Packages**: bash dotnet restore
5.   **Run the Application**: Via Visual Studio or Using dotnet run.

Usage
------------    
*   **Viewing Data**: Use the sidebar buttons to load different entities into the DataGrid.
    
*   **Editing Data**: Click the "Edit" button to open a new window for detailed editing.
    
*   **Saving Changes**: Save changes to refresh the UI automatically using the messaging system.
    
*   Contributing: Contributions are welcome! Please fork the repository, make your changes, and create a pull request. Ensure that your code adheres to the existing style and includes appropriate tests.
    
*   License: This project is licensed under the MIT License. See the LICENSE file for details