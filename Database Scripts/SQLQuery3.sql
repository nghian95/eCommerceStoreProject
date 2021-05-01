USE [eCommerceDB]

SELECT * FROM Products
SELECT * FROM Images
SELECT * FROM Users
SELECT * FROM Transactions

DROP TABLE Images

CREATE TABLE Transactions 
(
	TransactionID INT Identity(1,1) PRIMARY KEY,
    SKU VARCHAR(20),
    Quantity INT,
    TotalPrice DECIMAL (18,2),
    TransactionGroup INT,
    UserName VARCHAR(20),
    FOREIGN KEY (SKU) REFERENCES dbo.Products (SKU),
    FOREIGN KEY (UserName) REFERENCES dbo.Users (UserName)
)

ALTER TABLE Transactions 
    ADD Status INT;

INSERT INTO Images (ImageName, OriginalFormat, ImageFile, SKU) SELECT

      'Yellow Hoodie 03'
      ,'jpg'
      ,ImageFile
	  ,'CotnHD01-Y'

FROM OPENROWSET(BULK N'D:\Downloads\amir-soltani-X-0Dym22PgM-unsplash.jpg', SINGLE_BLOB) AS ImageSource(ImageFile);


DELETE FROM Transactions WHERE Sku IS NULL OR Quantity IS NULL OR TotalPrice is NULL
SELECT * FROM Transactions

SELECT * FROM Transactions