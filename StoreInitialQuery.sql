CREATE DATABASE StoreDB;

ALTER DATABASE StoreDB
SET AUTO_CLOSE OFF;

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
    State NVARCHAR(50)
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
    UserId INT FOREIGN KEY REFERENCES UserAccount(Id) NOT NULL,
    Time DATETIME NOT NULL
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

Select * FROM UserAccount;
Select * FROM Product;
Select * FROM Store;
Select * FROM Orders;