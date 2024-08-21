Product Specification WPF Application

This WPF application is designed to manage and edit product specifications, including products, materials, molds, clients, and branding. The application follows the MVVM (Model-View-ViewModel) pattern, leverages Entity Framework Core for data access, and uses the MVVM Community Toolkit for messaging and command handling.
Table of Contents

    Overview
    Database Structure
    Data Layer
    Business Objects
    WPF MVVM Implementation
    Messaging System
    Installation
    Usage
    Contributing
    License

Overview

The Product Specification WPF Application is designed to manage various entities related to product specifications. The key features include:

    CRUD operations for Products, Materials, Molds, Clients, and Branding.
    Use of Entity Framework Core with MySQL as the database.
    MVVM pattern implementation with the Community Toolkit.
    Messaging system to notify components of data changes.
    Editable views for each entity with validation and data binding.

Database Structure

The application uses a MySQL database with the following tables:

    tbl_Products: Stores product details.
    tbl_Materials: Stores material details.
    tbl_Molds: Stores mold details.
    tbl_Clients: Stores client information.
    tbl_Branding: Stores branding information.
    Association tables: tbl_ProductMaterials, tbl_ProductMolds, tbl_ProductBranding for many-to-many relationships between products and other entities.

History tables and triggers are also included to keep track of changes to the data.
Database Schema

sql

CREATE TABLE tbl_Products (
    ProductId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Height DECIMAL(10, 2),
    Width DECIMAL(10, 2),
    Unit VARCHAR(255),
    SKU VARCHAR(255),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE tbl_Materials (
    MaterialId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Height DECIMAL(10, 2),
    Width DECIMAL(10, 2),
    Unit VARCHAR(255),
    SKU VARCHAR(255),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Additional tables...

Data Layer

The data layer is built using Entity Framework Core with the following components:

    DbContext: ProductSpecificationDbContext is used to manage the database context and entity sets.
    Repositories: Repositories provide data access methods for each entity, such as ProductRepository, MaterialRepository, etc.

Repository Pattern

The repository pattern is implemented to encapsulate the data access logic and provide a clear separation of concerns.

csharp

public class ProductRepository : IProductSpecAppRepo
{
    private readonly ProductSpecificationDbContext _context;

    public ProductRepository(ProductSpecificationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.TblProducts.Select(p => new Product(_context, p)).ToList();
    }

    public bool SaveChanges()
    {
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during save: {ex.Message}");
            return false;
        }
    }

    // Other methods...
}

Business Objects

Business objects represent the domain models used in the application. These objects wrap the Entity Framework objects and include additional logic for validation and updates.

csharp

public class Product : BusinessObject
{
    private TblProduct tblProduct;
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? Height { get; set; }
    public decimal? Width { get; set; }
    public string? Unit { get; set; }
    public string? SKU { get; set; }
    public DateTime? UpdateDate { get; set; }

    public Product(TblProduct tblProduct, ProductSpecificationDbContext context)
    {
        this.tblProduct = tblProduct;
        this.Context = context;
        // Initialization logic...
    }

    public void UpdateDBObject(TblProduct tblProduct)
    {
        // Reflection-based update logic...
    }
}

WPF MVVM Implementation

The WPF front-end uses the MVVM pattern. Each entity has a DisplayView and an EditView:

    Display Views: Show data in a grid format with an "Edit" button for each item.
    Edit Views: Provide a form for editing individual entities.

Main Window

The main window contains a sidebar with buttons for each entity type (Products, Materials, etc.). Clicking a button displays the corresponding data in a DataGrid. Each DataGrid has an "Edit" button that opens a new window for editing the selected item.
View Models

Each view model is responsible for the logic behind the view. For example:

csharp

public class ProductDisplayViewModel : ObservableObject
{
    private readonly IProductSpecAppRepo _repository;

    public ProductDisplayViewModel(IProductSpecAppRepo repository)
    {
        _repository = repository;
        LoadProducts();

        WeakReferenceMessenger.Default.Register<SaveSuccessfulMessage>(this, (r, m) =>
        {
            if (m.Value.ObjectType == typeof(Product))
            {
                LoadProducts();
            }
        });
    }

    public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

    private void LoadProducts()
    {
        Products.Clear();
        foreach (var product in _repository.GetAllProducts())
        {
            Products.Add(product);
        }
    }
}

Messaging System

The application uses the MVVM Community Toolkit's messaging system to notify components of data changes:

    Messages: A SaveSuccessfulMessage is sent after an entity is saved successfully.
    Subscribers: Display view models subscribe to this message and refresh their data when the message is received.

csharp

public class SaveSuccessfulMessage : ValueChangedMessage<(Type ObjectType, int ObjectId)>
{
    public SaveSuccessfulMessage(Type objectType, int objectId) : base((objectType, objectId)) { }
}

Installation

    Clone the repository.
    Set up the MySQL database using the provided schema.
    Update the connection string in ProductSpecificationDbContext.
    Restore NuGet packages using dotnet restore.
    Run the application from Visual Studio or via dotnet run.

Usage

    Viewing Data: Use the sidebar buttons to view data for Products, Materials, Molds, Clients, and Branding.
    Editing Data: Click the "Edit" button next to any item to open the edit window.
    Saving Changes: After editing, click "Save" to persist changes and automatically refresh the data in the main window.

Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes.
License

This project is licensed under the MIT License. See the LICENSE file for more details.