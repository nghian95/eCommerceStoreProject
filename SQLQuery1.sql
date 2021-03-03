SELECT * FROM Products
SELECT * FROM Categories

INSERT INTO Categories Values('Shorts'), ('Socks'), ('Gloves'), ('Watches'), ('Sweaters'), ('Jackets'), ('Shoes'), ('Underwear')
INSERT INTO Categories Values('Hats')

ALTER TABLE Categories
ADD UNIQUE (Name)