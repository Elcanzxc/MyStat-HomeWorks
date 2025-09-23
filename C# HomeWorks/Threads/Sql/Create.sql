CREATE DATABASE UserStatistic

USE UserStatistic

CREATE TABLE UserMajorStatistic(
 [Id] INT PRIMARY KEY IDENTITY,
 [Username] NVARCHAR(255) NOT NULL,
 [AllTime] INT NOT NULL,
 [LastSeenDateTime] DATETIME2 NOT NULL
)

CREATE TABLE PageStats (
    [Id] INT IDENTITY PRIMARY KEY,  
    [Username] NVARCHAR(255) NOT NULL,  
    [PageName] NVARCHAR(255) NOT NULL,  
    [AllTime] INT NOT NULL,
    [UserMajorId] INT FOREIGN KEY REFERENCES UserMajorStatistic([Id])
)

CREATE TABLE ButtonClicks (
    [Id] INT IDENTITY PRIMARY KEY,  
    [Username] NVARCHAR(255) NOT NULL,   
    [Button] NVARCHAR(255) NOT NULL,     
    [ClickCount] INT NOT NULL,
    [UserMajorId] INT FOREIGN KEY REFERENCES UserMajorStatistic([Id])
)

SELECT * FROM UserMajorStatistic
SELECT * FROM PageStats