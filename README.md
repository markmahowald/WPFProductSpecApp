* * * * *

Product Specification WPF Application
=====================================

This WPF application is designed to manage and edit product specifications, including products, materials, molds, clients, and branding. The application follows the MVVM (Model-View-ViewModel) pattern, leverages Entity Framework Core for data access, and uses the MVVM Community Toolkit for messaging and command handling.

Table of Contents
-----------------

-   [Overview](#overview)
-   [Database Structure](#database-structure)
-   [Data Layer](#data-layer)
-   [Business Objects](#business-objects)
-   [WPF MVVM Implementation](#wpf-mvvm-implementation)
-   [Messaging System](#messaging-system)
-   [Installation](#installation)
-   [Usage](#usage)
-   [Contributing](#contributing)
-   [License](#license)

Overview
--------

The Product Specification WPF Application is designed to manage various entities related to product specifications. The key features include:

-   CRUD operations for Products, Materials, Molds, Clients, and Branding.
-   Use of Entity Framework Core with MySQL as the database.
-   MVVM pattern implementation with the Community Toolkit.
-   Messaging system to notify components of data changes.
-   Editable views for each entity with validation and data binding.

Database Structure
------------------

The application uses a MySQL database with the following tables:

-   `tbl_Products`: Stores product details.
-   `tbl_Materials`: Stores material details.
-   `tbl_Molds`: Stores mold details.
-   `tbl_Clients`: Stores client information.
-   `tbl_Branding`: Stores branding information.
-   Association tables: `tbl_ProductMaterials`, `tbl_ProductMolds`, `tbl_ProductBranding` for many-to-many relationships between products and other entities.

History tables and triggers are also included to keep track of changes to the data.

### Database Schema

sql* * * * *

Product Specification WPF Application
=====================================

This WPF application is designed to manage and edit product specifications, including products, materials, molds, clients, and branding. The application follows the MVVM (Model-View-ViewModel) pattern, leverages Entity Framework Core for data access, and uses the MVVM Community Toolkit for messaging and command handling.

Table of Contents
-----------------

-   [Overview](#overview)
-   [Database Structure](#database-structure)
-   [Data Layer](#data-layer)
-   [Business Objects](#business-objects)
-   [WPF MVVM Implementation](#wpf-mvvm-implementation)
-   [Messaging System](#messaging-system)
-   [Installation](#installation)
-   [Usage](#usage)
-   [Contributing](#contributing)
-   [License](#license)

Overview
--------

The Product Specification WPF Application is designed to manage various entities related to product specifications. The key features include:

-   CRUD operations for Products, Materials, Molds, Clients, and Branding.
-   Use of Entity Framework Core with MySQL as the database.
-   MVVM pattern implementation with the Community Toolkit.
-   Messaging system to notify components of data changes.
-   Editable views for each entity with validation and data binding.

Database Structure
------------------

The application uses a MySQL database with the following tables:

-   `tbl_Products`: Stores product details.
-   `tbl_Materials`: Stores material details.
-   `tbl_Molds`: Stores mold details.
-   `tbl_Clients`: Stores client information.
-   `tbl_Branding`: Stores branding information.
-   Association tables: `tbl_ProductMaterials`, `tbl_ProductMolds`, `tbl_ProductBranding` for many-to-many relationships between products and other entities.

History tables and triggers are also included to keep track of changes to the data.

### Database Schema

sql