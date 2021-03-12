CREATE TABLE Users 
(
	UserName VARCHAR(20) CONSTRAINT pk_UserName PRIMARY KEY,
	Password VARCHAR(30) NOT NULL,
	Access INT NOT NULL,
	FirstName VARCHAR(25),
	LastName VARCHAR(25),
	Address VARCHAR(100),
	Phone INT,
	Email VARCHAR(50)
)

INSERT INTO Users(UserName, Password, Access) VALUES('admin', 'adminpw', 1), ('customer', 'custpw', 0)

CREATE TABLE [dbo].[Products] (
    [SKU]         VARCHAR (20)  NOT NULL,
    [Name]        VARCHAR (40)  NOT NULL,
    [Quantity]    INT           NULL,
    [Description] VARCHAR (250) NULL,
    [Price]       DECIMAL (18,2)  NULL,
    [CategoryID]  INT           NULL,
    [Sale]        FLOAT (53)    NULL,
    PRIMARY KEY CLUSTERED ([SKU] ASC),
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID])
);

INSERT INTO Products(SKU, Name) VALUES ('BLUEPOLOSHRT0001', 'Blue Activewear Polo Shirt')
INSERT INTO Products(SKU, Name, CategoryId) VALUES ('Polo01-R', 'Red Activewear Polo Shirt', 1)

CREATE TABLE Images 
(
	[ImageID] [int] IDENTITY (1,1) PRIMARY KEY,
	[ImageName] [varchar](40) NOT NULL,
	[OriginalFormat] [nvarchar](5) NOT NULL,
	[ImageFile] [varbinary](max) NOT NULL,
	[SKU] [varchar](20) NOT NULL
)

INSERT INTO Images (ImageName, OriginalFormat, ImageFile, SKU) SELECT

      'Yellow Hoodie 01'
      ,'jpg'
      ,ImageFile
	  ,'CotnHD01-Y'

FROM OPENROWSET(BULK N'D:\Downloads\Yellow Hoodie.jpg', SINGLE_BLOB) AS ImageSource(ImageFile);