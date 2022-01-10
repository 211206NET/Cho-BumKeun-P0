CREATE DATABASE StoreDB;

-- turning off super annoying feature ("Auto Close") that gets enabled by default
ALTER DATABASE StoreDB
SET AUTO_CLOSE OFF;

-- Changing to use RRDB db instead of master
USE StoreDB;

CREATE TABLE UserAccount(
    Id INT PRIMARY KEY IDENTITY(1, 1),
    Username NVARCHAR(450) NOT NULL UNIQUE,
    Password NVARCHAR(100)
);

CREATE TABLE Store(
    Id INT PRIMARY KEY IDENTITY(1, 1),
    Name NVARCHAR(450) NOT NULL UNIQUE,
    City NVARCHAR(100),
    State NVARCHAR(50),
    --Zip INT NOT NULL
);

CREATE TABLE Product(
    Id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(450) NOT NULL UNIQUE,
    Price DECIMAL NOT NULL,
    Developer NVARCHAR(100),
    Inventory INT NOT NULL
);

CREATE TABLE Orders(
    Id INT PRIMARY KEY IDENTITY(1, 1),
    StoreId INT FOREIGN KEY REFERENCES Store(Id) NOT NULL,
    StoreName NVARCHAR(450) FOREIGN KEY REFERENCES Store(Name),
    ProductId INT FOREIGN KEY REFERENCES Product(Id) NOT NULL,
    ProductName NVARCHAR(450) FOREIGN KEY REFERENCES Product(Title),
    Quantity INT NOT NULL,
    TotalPrice DECIMAL NOT NULL,
    UserId INT FOREIGN KEY REFERENCES UserAccount(Id) NOT NULL
);

DROP TABLE UserAccount;
DROP TABLE Product;
DROP TABLE Store;
DROP TABLE Orders;

INSERT INTO Store (Name, City, State) VALUES
('Shelby Creek Vapor', 'Utica', 'MI'),
('Broadway Shops Vapor', 'Columbia', 'MO'),
('Bedford Grove Vapor', 'Bedford', 'NH'),
('Market Square Vapor', 'Rochester', 'NY');

INSERT INTO Product (Title, Price, Developer, Inventory) VALUES
('Halo Infinite', 59.99, '343 Industries', 100),
('Monster Hunter Rise', 59.99, 'CAPCOM', 100),
('Raft', 19.99, 'Redbeet Interactive', 100),
('Stardew Valley', 14.99, 'ConcernedApe', 100);

-- Inserting data is part of DML sublanguage, and we use INSERT keyword
INSERT INTO Restaurant (Name, City, State) VALUES
('Salt and Straw', 'Portland', 'OR'),
('Pita House', 'Battle Ground', 'WA'),
('Del Taco', 'LA', 'CA');

INSERT INTO Review (RestaurantId, Rating, Note) VALUES
(1,5, 'AMAZING ICE CREAM');

Select * FROM UserAccount;
Select * FROM Product;
Select * FROM Store;
Select * FROM Orders;

INSERT INTO Review (RestaurantId, Rating, Note) VALUES (1,5, 'AMAZING ICE CREAM');

-- Deletes anything with 'Taco' in the name
Delete FROM Restaurant WHERE Name LIKE '%Taco%';
-- Deletes based on Id
Delete FROM UserAccount WHERE Id = 2;

Update Product
SET Inventory = '100'-Quantity
WHERE Id = 'Halo Infinite';

SELECT * FROM Restaurant WHERE Name LIKE '' OR City LIKE '' OR State LIKE '';