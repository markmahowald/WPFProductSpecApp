-- Drop the existing database if it exists, to start fresh
DROP DATABASE IF EXISTS ProductSpecificationDB;

-- Create a new database
CREATE DATABASE ProductSpecificationDB;

-- Switch to the newly created database
USE ProductSpecificationDB;

-- Create the Products table
CREATE TABLE tbl_Products (
    ProductId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT, 
    Height DECIMAL(10, 2), 
    Width DECIMAL(10, 2),
    Unit VARCHAR(255), 
    SKU VARCHAR(255),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the Materials table
CREATE TABLE tbl_Materials (
    MaterialId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Height DECIMAL(10, 2), 
    Width DECIMAL(10, 2),
    Unit VARCHAR(255), 
    SKU VARCHAR(255),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the Molds table
CREATE TABLE tbl_Molds (
    MoldId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the Clients table
CREATE TABLE tbl_Clients (
    ClientId INT AUTO_INCREMENT PRIMARY KEY,
    ClientName VARCHAR(255) NOT NULL,
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the Branding table
CREATE TABLE tbl_Branding (
    BrandingId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT, 
    Type ENUM('CardboardSleve','BrandedBag', 'Shrinkwrap'), 
    ClientId INT,
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Automatically updates on changes
    FOREIGN KEY (ClientId) REFERENCES tbl_Clients(ClientId)
);

-- Create the ProductMaterials association table
CREATE TABLE tbl_ProductMaterials (
    ProductId INT,
    MaterialId INT,
    PRIMARY KEY (ProductId, MaterialId),
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId),
    FOREIGN KEY (MaterialId) REFERENCES tbl_Materials(MaterialId),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the ProductMolds association table
CREATE TABLE tbl_ProductMolds (
    ProductId INT,
    MoldId INT,
    PRIMARY KEY (ProductId, MoldId),
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId),
    FOREIGN KEY (MoldId) REFERENCES tbl_Molds(MoldId),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the ProductBranding association table
CREATE TABLE tbl_ProductBranding (
    ProductId INT,
    BrandingId INT,
    PRIMARY KEY (ProductId, BrandingId),
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId),
    FOREIGN KEY (BrandingId) REFERENCES tbl_Branding(BrandingId),
    UpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Automatically updates on changes
);

-- Create the ProductsHistory table to store historical changes
CREATE TABLE tbl_ProductsHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT,
    Name VARCHAR(255) NOT NULL,
    Description TEXT, 
    Height DECIMAL(10, 2),
    Width DECIMAL(10, 2),
    Unit VARCHAR(255),
    SKU VARCHAR(255),
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId)
);

-- Create the MaterialsHistory table to store historical changes
CREATE TABLE tbl_MaterialsHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    MaterialId INT,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Height DECIMAL(10, 2),
    Width DECIMAL(10, 2),
    Unit VARCHAR(255),
    SKU VARCHAR(255),
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaterialId) REFERENCES tbl_Materials(MaterialId)
);

-- Create the MoldsHistory table to store historical changes
CREATE TABLE tbl_MoldsHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    MoldId INT,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MoldId) REFERENCES tbl_Molds(MoldId)
);

-- Create the ClientsHistory table to store historical changes
CREATE TABLE tbl_ClientsHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    ClientId INT,
    ClientName VARCHAR(255) NOT NULL,
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ClientId) REFERENCES tbl_Clients(ClientId)
);

-- Create the BrandingHistory table to store historical changes
CREATE TABLE tbl_BrandingHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    BrandingId INT,
    Name VARCHAR(255) NOT NULL,
    Description TEXT, 
    Type ENUM('CardboardSleve','BrandedBag', 'Shrinkwrap'),
    ClientId INT,
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (BrandingId) REFERENCES tbl_Branding(BrandingId)
);

-- Create the ProductMaterialsHistory table to store historical changes
CREATE TABLE tbl_ProductMaterialsHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT,
    MaterialId INT,
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId),
    FOREIGN KEY (MaterialId) REFERENCES tbl_Materials(MaterialId)
);

-- Create the ProductMoldsHistory table to store historical changes
CREATE TABLE tbl_ProductMoldsHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT,
    MoldId INT,
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId),
    FOREIGN KEY (MoldId) REFERENCES tbl_Molds(MoldId)
);

-- Create the ProductBrandingHistory table to store historical changes
CREATE TABLE tbl_ProductBrandingHistory (
    HistoryId INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT,
    BrandingId INT,
    OperationType ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    OperationTimestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductId) REFERENCES tbl_Products(ProductId),
    FOREIGN KEY (BrandingId) REFERENCES tbl_Branding(BrandingId)
);

-- Set a different delimiter to handle the trigger definition
DELIMITER $$

-- Trigger for INSERT on Products
CREATE TRIGGER trg_products_insert
AFTER INSERT ON tbl_Products
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductsHistory (ProductId, Name, Description, Height, Width, Unit, SKU, OperationType)
    VALUES (NEW.ProductId, NEW.Name, NEW.Description, NEW.Height, NEW.Width, NEW.Unit, NEW.SKU, 'INSERT');
END$$

-- Trigger for INSERT on Materials
CREATE TRIGGER trg_materials_insert
AFTER INSERT ON tbl_Materials
FOR EACH ROW
BEGIN
    INSERT INTO tbl_MaterialsHistory (MaterialId, Name, Description, Height, Width, Unit, SKU, OperationType)
    VALUES (NEW.MaterialId, NEW.Name, NEW.Description, NEW.Height, NEW.Width, NEW.Unit, NEW.SKU, 'INSERT');
END$$

-- Trigger for INSERT on Molds
CREATE TRIGGER trg_molds_insert
AFTER INSERT ON tbl_Molds
FOR EACH ROW
BEGIN
    INSERT INTO tbl_MoldsHistory (MoldId, Name, Description, OperationType)
    VALUES (NEW.MoldId, NEW.Name, NEW.Description, 'INSERT');
END$$

-- Trigger for INSERT on Clients
CREATE TRIGGER trg_clients_insert
AFTER INSERT ON tbl_Clients
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ClientsHistory (ClientId, ClientName, OperationType)
    VALUES (NEW.ClientId, NEW.ClientName, 'INSERT');
END$$

-- Trigger for INSERT on Branding
CREATE TRIGGER trg_branding_insert
AFTER INSERT ON tbl_Branding
FOR EACH ROW
BEGIN
    INSERT INTO tbl_BrandingHistory (BrandingId, Name, Description, Type, ClientId, OperationType)
    VALUES (NEW.BrandingId, NEW.Name, NEW.Description, NEW.Type, NEW.ClientId, 'INSERT');
END$$

-- Trigger for INSERT on ProductMaterials
CREATE TRIGGER trg_productmaterials_insert
AFTER INSERT ON tbl_ProductMaterials
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductMaterialsHistory (ProductId, MaterialId, OperationType)
    VALUES (NEW.ProductId, NEW.MaterialId, 'INSERT');
END$$

-- Trigger for INSERT on ProductMolds
CREATE TRIGGER trg_productmolds_insert
AFTER INSERT ON tbl_ProductMolds
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductMoldsHistory (ProductId, MoldId, OperationType)
    VALUES (NEW.ProductId, NEW.MoldId, 'INSERT');
END$$

-- Trigger for INSERT on ProductBranding
CREATE TRIGGER trg_productbranding_insert
AFTER INSERT ON tbl_ProductBranding
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductBrandingHistory (ProductId, BrandingId, OperationType)
    VALUES (NEW.ProductId, NEW.BrandingId, 'INSERT');
END$$

-- Trigger for UPDATE on Products
CREATE TRIGGER trg_products_update
AFTER UPDATE ON tbl_Products
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductsHistory (ProductId, Name, Description, Height, Width, Unit, SKU, OperationType)
    VALUES (NEW.ProductId, NEW.Name, NEW.Description, NEW.Height, NEW.Width, NEW.Unit, NEW.SKU, 'UPDATE');
END$$

-- Trigger for UPDATE on Materials
CREATE TRIGGER trg_materials_update
AFTER UPDATE ON tbl_Materials
FOR EACH ROW
BEGIN
    INSERT INTO tbl_MaterialsHistory (MaterialId, Name, Description, Height, Width, Unit, SKU, OperationType)
    VALUES (NEW.MaterialId, NEW.Name, NEW.Description, NEW.Height, NEW.Width, NEW.Unit, NEW.SKU, 'UPDATE');
END$$

-- Trigger for UPDATE on Molds
CREATE TRIGGER trg_molds_update
AFTER UPDATE ON tbl_Molds
FOR EACH ROW
BEGIN
    INSERT INTO tbl_MoldsHistory (MoldId, Name, Description, OperationType)
    VALUES (NEW.MoldId, NEW.Name, NEW.Description, 'UPDATE');
END$$

-- Trigger for UPDATE on Clients
CREATE TRIGGER trg_clients_update
AFTER UPDATE ON tbl_Clients
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ClientsHistory (ClientId, ClientName, OperationType)
    VALUES (NEW.ClientId, NEW.ClientName, 'UPDATE');
END$$

-- Trigger for UPDATE on Branding
CREATE TRIGGER trg_branding_update
AFTER UPDATE ON tbl_Branding
FOR EACH ROW
BEGIN
    INSERT INTO tbl_BrandingHistory (BrandingId, Name, Description, Type, ClientId, OperationType)
    VALUES (NEW.BrandingId, NEW.Name, NEW.Description, NEW.Type, NEW.ClientId, 'UPDATE');
END$$

-- Trigger for UPDATE on ProductMaterials
CREATE TRIGGER trg_productmaterials_update
AFTER UPDATE ON tbl_ProductMaterials
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductMaterialsHistory (ProductId, MaterialId, OperationType)
    VALUES (NEW.ProductId, NEW.MaterialId, 'UPDATE');
END$$

-- Trigger for UPDATE on ProductMolds
CREATE TRIGGER trg_productmolds_update
AFTER UPDATE ON tbl_ProductMolds
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductMoldsHistory (ProductId, MoldId, OperationType)
    VALUES (NEW.ProductId, NEW.MoldId, 'UPDATE');
END$$

-- Trigger for UPDATE on ProductBranding
CREATE TRIGGER trg_productbranding_update
AFTER UPDATE ON tbl_ProductBranding
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductBrandingHistory (ProductId, BrandingId, OperationType)
    VALUES (NEW.ProductId, NEW.BrandingId, 'UPDATE');
END$$

-- Trigger for DELETE on Products
CREATE TRIGGER trg_products_delete
AFTER DELETE ON tbl_Products
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductsHistory (ProductId, Name, Description, Height, Width, Unit, SKU, OperationType)
    VALUES (OLD.ProductId, OLD.Name, OLD.Description, OLD.Height, OLD.Width, OLD.Unit, OLD.SKU, 'DELETE');
END$$

-- Trigger for DELETE on Materials
CREATE TRIGGER trg_materials_delete
AFTER DELETE ON tbl_Materials
FOR EACH ROW
BEGIN
    INSERT INTO tbl_MaterialsHistory (MaterialId, Name, Description, Height, Width, Unit, SKU, OperationType)
    VALUES (OLD.MaterialId, OLD.Name, OLD.Description, OLD.Height, OLD.Width, OLD.Unit, OLD.SKU, 'DELETE');
END$$

-- Trigger for DELETE on Molds
CREATE TRIGGER trg_molds_delete
AFTER DELETE ON tbl_Molds
FOR EACH ROW
BEGIN
    INSERT INTO tbl_MoldsHistory (MoldId, Name, Description, OperationType)
    VALUES (OLD.MoldId, OLD.Name, OLD.Description, 'DELETE');
END$$

-- Trigger for DELETE on Clients
CREATE TRIGGER trg_clients_delete
AFTER DELETE ON tbl_Clients
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ClientsHistory (ClientId, ClientName, OperationType)
    VALUES (OLD.ClientId, OLD.ClientName, 'DELETE');
END$$

-- Trigger for DELETE on Branding
CREATE TRIGGER trg_branding_delete
AFTER DELETE ON tbl_Branding
FOR EACH ROW
BEGIN
    INSERT INTO tbl_BrandingHistory (BrandingId, Name, Description, Type, ClientId, OperationType)
    VALUES (OLD.BrandingId, OLD.Name, OLD.Description, OLD.Type, OLD.ClientId, 'DELETE');
END$$

-- Trigger for DELETE on ProductMaterials
CREATE TRIGGER trg_productmaterials_delete
AFTER DELETE ON tbl_ProductMaterials
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductMaterialsHistory (ProductId, MaterialId, OperationType)
    VALUES (OLD.ProductId, OLD.MaterialId, 'DELETE');
END$$

-- Trigger for DELETE on ProductMolds
CREATE TRIGGER trg_productmolds_delete
AFTER DELETE ON tbl_ProductMolds
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductMoldsHistory (ProductId, MoldId, OperationType)
    VALUES (OLD.ProductId, OLD.MoldId, 'DELETE');
END$$

-- Trigger for DELETE on ProductBranding
CREATE TRIGGER trg_productbranding_delete
AFTER DELETE ON tbl_ProductBranding
FOR EACH ROW
BEGIN
    INSERT INTO tbl_ProductBrandingHistory (ProductId, BrandingId, OperationType)
    VALUES (OLD.ProductId, OLD.BrandingId, 'DELETE');
END$$

-- Reset the delimiter back to semicolon
DELIMITER ;

-- Inserting mock data so the app can run 
-- Insert mock data into Clients
INSERT INTO tbl_Clients (ClientName, UpdateDate) VALUES 
('Client A', NOW()),
('Client B', NOW()),
('Client C', NOW()),
('Client D', NOW()),
('Client E', NOW()),
('Client F', NOW()),
('Client G', NOW()),
('Client H', NOW()),
('Client I', NOW()),
('Client J', NOW());

-- Insert mock data into Branding
INSERT INTO tbl_Branding (Name, Description, Type, ClientId, UpdateDate) VALUES 
('Branding A', 'Description A', 'CardboardSleve', 1, NOW()),
('Branding B', 'Description B', 'BrandedBag', 2, NOW()),
('Branding C', 'Description C', 'Shrinkwrap', 3, NOW()),
('Branding D', 'Description D', 'CardboardSleve', 4, NOW()),
('Branding E', 'Description E', 'BrandedBag', 5, NOW()),
('Branding F', 'Description F', 'Shrinkwrap', 6, NOW()),
('Branding G', 'Description G', 'CardboardSleve', 7, NOW()),
('Branding H', 'Description H', 'BrandedBag', 8, NOW()),
('Branding I', 'Description I', 'Shrinkwrap', 9, NOW()),
('Branding J', 'Description J', 'CardboardSleve', 10, NOW());

-- Insert mock data into Materials
INSERT INTO tbl_Materials (Name, Description, Height, Width, Unit, SKU, UpdateDate) VALUES 
('Material A', 'Description A', 10.0, 5.0, 'cm', 'SKU-A', NOW()),
('Material B', 'Description B', 12.0, 6.0, 'cm', 'SKU-B', NOW()),
('Material C', 'Description C', 14.0, 7.0, 'cm', 'SKU-C', NOW()),
('Material D', 'Description D', 16.0, 8.0, 'cm', 'SKU-D', NOW()),
('Material E', 'Description E', 18.0, 9.0, 'cm', 'SKU-E', NOW()),
('Material F', 'Description F', 20.0, 10.0, 'cm', 'SKU-F', NOW()),
('Material G', 'Description G', 22.0, 11.0, 'cm', 'SKU-G', NOW()),
('Material H', 'Description H', 24.0, 12.0, 'cm', 'SKU-H', NOW()),
('Material I', 'Description I', 26.0, 13.0, 'cm', 'SKU-I', NOW()),
('Material J', 'Description J', 28.0, 14.0, 'cm', 'SKU-J', NOW());

-- Insert mock data into Molds
INSERT INTO tbl_Molds (Name, Description, UpdateDate) VALUES 
('Mold A', 'Description A', NOW()),
('Mold B', 'Description B', NOW()),
('Mold C', 'Description C', NOW()),
('Mold D', 'Description D', NOW()),
('Mold E', 'Description E', NOW()),
('Mold F', 'Description F', NOW()),
('Mold G', 'Description G', NOW()),
('Mold H', 'Description H', NOW()),
('Mold I', 'Description I', NOW()),
('Mold J', 'Description J', NOW());

-- Insert mock data into Products
INSERT INTO tbl_Products (Name, Description, Height, Width, Unit, SKU, UpdateDate) VALUES 
('Product A', 'Description A', 30.0, 15.0, 'cm', 'SKU-PA', NOW()),
('Product B', 'Description B', 32.0, 16.0, 'cm', 'SKU-PB', NOW()),
('Product C', 'Description C', 34.0, 17.0, 'cm', 'SKU-PC', NOW()),
('Product D', 'Description D', 36.0, 18.0, 'cm', 'SKU-PD', NOW()),
('Product E', 'Description E', 38.0, 19.0, 'cm', 'SKU-PE', NOW()),
('Product F', 'Description F', 40.0, 20.0, 'cm', 'SKU-PF', NOW()),
('Product G', 'Description G', 42.0, 21.0, 'cm', 'SKU-PG', NOW()),
('Product H', 'Description H', 44.0, 22.0, 'cm', 'SKU-PH', NOW()),
('Product I', 'Description I', 46.0, 23.0, 'cm', 'SKU-PI', NOW()),
('Product J', 'Description J', 48.0, 24.0, 'cm', 'SKU-PJ', NOW());

-- Insert mock data into ProductMaterials associations
INSERT INTO tbl_ProductMaterials (ProductId, MaterialId, UpdateDate) VALUES 
(1, 1, NOW()),
(2, 2, NOW()),
(3, 3, NOW()),
(4, 4, NOW()),
(5, 5, NOW()),
(6, 6, NOW()),
(7, 7, NOW()),
(8, 8, NOW()),
(9, 9, NOW()),
(10, 10, NOW());

-- Insert mock data into ProductMolds associations
INSERT INTO tbl_ProductMolds (ProductId, MoldId, UpdateDate) VALUES 
(1, 1, NOW()),
(2, 2, NOW()),
(3, 3, NOW()),
(4, 4, NOW()),
(5, 5, NOW()),
(6, 6, NOW()),
(7, 7, NOW()),
(8, 8, NOW()),
(9, 9, NOW()),
(10, 10, NOW());

-- Insert mock data into ProductBranding associations
INSERT INTO tbl_ProductBranding (ProductId, BrandingId, UpdateDate) VALUES 
(1, 1, NOW()),
(2, 2, NOW()),
(3, 3, NOW()),
(4, 4, NOW()),
(5, 5, NOW()),
(6, 6, NOW()),
(7, 7, NOW()),
(8, 8, NOW()),
(9, 9, NOW()),
(10, 10, NOW());

-- selecting all data to ensure success

select * from tbl_Products ; 
select * from tbl_Materials ;
select * from tbl_Molds ; 
select * from tbl_Clients ; 
select * from tbl_Branding ; 
select * from tbl_ProductMaterials ; 
select * from tbl_ProductMolds ; 
select * from tbl_ProductBranding ; 
select * from tbl_ProductsHistory ; 
select * from tbl_MaterialsHistory ; 
select * from tbl_MoldsHistory ; 
select * from tbl_ClientsHistory ; 
select * from tbl_BrandingHistory ; 
select * from tbl_ProductMaterialsHistory ; 
select * from tbl_ProductMoldsHistory ; 
select * from tbl_ProductBrandingHistory ; 
