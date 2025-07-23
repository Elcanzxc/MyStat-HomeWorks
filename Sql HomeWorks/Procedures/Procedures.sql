CREATE DATABASE Vehicle

USE Vehicle


CREATE TABLE Cars
(
   [Id] INT PRIMARY KEY IDENTITY(1,1),
   [Name] NVARCHAR(MAX) NOT NULL,
   [CreationDate] DATETIME2 NOT NULL,
   [Price] MONEY NOT NULL
)


INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Ford Mustang', '2020-01-15 10:00:00', 35000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Chevrolet Camaro', '2019-05-20 14:30:00', 33000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Toyota Corolla', '2018-07-10 08:45:00', 18000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Honda Civic', '2021-03-22 12:00:00', 20000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'BMW 3 Series', '2020-11-05 09:15:00', 41000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Audi A4', '2019-09-18 17:30:00', 39000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Mercedes-Benz C-Class', '2017-12-01 11:00:00', 42000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Nissan Altima', '2021-06-30 15:20:00', 24000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Volkswagen Golf', '2018-04-12 13:50:00', 21000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Hyundai Elantra', '2019-02-28 10:10:00', 19000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Kia Optima', '2020-08-14 16:40:00', 22000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Subaru Impreza', '2017-11-23 14:00:00', 21000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Mazda 3', '2018-09-09 09:30:00', 20000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Jaguar XE', '2021-01-17 18:00:00', 45000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Ford Focus', '2019-10-05 07:20:00', 19000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Tesla Model 3', '2020-12-25 20:00:00', 48000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Chevrolet Malibu', '2018-06-08 11:45:00', 23000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Honda Accord', '2017-08-29 12:30:00', 25000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'Audi Q5', '2019-03-13 14:50:00', 47000);
INSERT INTO Cars (Name, CreationDate, Price) VALUES (N'BMW X5', '2021-07-04 16:10:00', 60000);



-- 1
CREATE OR ALTER FUNCTION dbo.GetCarsCount
(
)
RETURNS INT
AS
 BEGIN 
   
    RETURN 
	( 
	SELECT COUNT(*)
	FROM Cars 
	)
 END

 SELECT dbo.GetCarsCount()


 -- 2
CREATE OR ALTER FUNCTION dbo.GetMaxPrice
(
)
RETURNS MONEY
AS
 BEGIN
    RETURN 
	(
	SELECT Max(Price)
	FROM Cars
	)
 END

SELECT dbo.GetMaxPrice()


--3
CREATE OR ALTER FUNCTION dbo.GetMinCreationDate
(
)
RETURNS DATETIME2
AS
 BEGIN
    RETURN
	(
	 SELECT Min(CreationDate)
	 FROM Cars
	)
 END

SELECT dbo.GetMinCreationDate()




-- 1
CREATE OR ALTER PROCEDURE dbo.CreateProduct
   @Name NVARCHAR(MAX) ,
   @CreationDate DATETIME2 ,
   @Price MONEY 
AS
BEGIN
	INSERT INTO Cars
	VALUES (@Name,@CreationDate,@Price)
END

EXEC dbo.CreateProduct N'Lada 2107', '1985-01-01' ,85000




-- 2
CREATE OR ALTER PROCEDURE dbo.ReadAllCars
AS
BEGIN
  SELECT * FROM CARS
END

EXEC dbo.ReadAllCars



--3
CREATE OR ALTER PROCEDURE dbo.ReadCarById
  @Value INT
AS
BEGIN
  SELECT * FROM Cars WHERE Cars.Id = @Value
END

EXEC dbo.ReadCarById 15

-- 4
CREATE OR ALTER PROCEDURE dbo.UpdateCar
  @Id INT,
  @Name NVARCHAR(MAX),
  @Datetime DATETIME2,
  @Price MONEY
AS
BEGIN
  UPDATE Cars
  SET Name = @Name ,CreationDate = @Datetime , Price = @Price
  WHERE Id = @Id
END

EXEC dbo.UpdateCar 21 , N'Lada 2107' , '1985-01-01' , 15000


-- 5
CREATE OR ALTER PROCEDURE dbo.DeleteAllCars
AS
BEGIN
  DELETE Cars
END

EXEC dbo.DeleteAllCars


-- 6
CREATE OR ALTER PROCEDURE dbo.DeleteCarById
  @Id INT
AS
BEGIN
  DELETE Cars WHERE Cars.Id =  @Id
END

EXEC dbo.DeleteCarById 14