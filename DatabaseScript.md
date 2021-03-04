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
