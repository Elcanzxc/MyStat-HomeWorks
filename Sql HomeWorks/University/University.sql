CREATE DATABASE University

USE University



-- Task № - 14
CREATE TABLE Teachers(
   [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
   [Name] NVARCHAR(MAX) NOT NULL CHECK(LEN(Name) > 2) DEFAULT('UNKNOWN'),
   [Surname] NVARCHAR(MAX) NOT NULL CHECK(LEN(Surname) > 2) DEFAULT('UNKNOWN')
)




-- Task № - 1
CREATE TABLE Assistants(
   [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
   [TeacherId] INT NOT NULL FOREIGN KEY REFERENCES Teachers([Id]) 
)


-- Task № - 2
CREATE TABLE Curators(
  [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  [TeacherId] INT NOT NULL FOREIGN KEY REFERENCES Teachers([Id]) 
)



-- Task № - 3
CREATE TABLE Deans(
  [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  [TeacherId] INT NOT NULL FOREIGN KEY REFERENCES Teachers([Id]) 
)



-- Task № - 5
CREATE TABLE Faculties(
   [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
   [Building] INT NOT NULL CHECK([Building]BETWEEN 1 AND 5),
   [Name] NVARCHAR(100) NOT NULL UNIQUE DEFAULT('UNKNOWN'),
   [DeanId] INT NOT NULL FOREIGN KEY REFERENCES Deans([Id])
)

-- Task № - 9
CREATE TABLE Heads(
  [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
  [TeacherId] INT NOT NULL FOREIGN KEY REFERENCES Teachers([Id])
)


-- Task № - 4
CREATE TABLE Departments(
   [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
   [Building] INT NOT NULL CHECK([Building]BETWEEN 1 AND 5),
   [Name] NVARCHAR(100) NOT NULL UNIQUE DEFAULT('UNKNOWN'),
   [FacultyId] INT NOT NULL FOREIGN KEY REFERENCES Faculties([Id]),
   [HeadId] INT NOT NULL FOREIGN KEY REFERENCES Heads([Id])
)


-- Task № - 6
CREATE TABLE Groups(
 [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
 [Name] NVARCHAR(10) NOT NULL UNIQUE DEFAULT('UNKNOWN'),
 [Year] INT NOT NULL CHECK([YEAR] BETWEEN 1 AND 5),
 [DepartmentId] INT NOT NULL FOREIGN KEY REFERENCES Departments([Id])
)

-- Task № - 7
CREATE TABLE GroupsCurators(
    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [CuratorId] INT NOT NULL FOREIGN KEY REFERENCES Curators([Id]),
    [GroupId] INT NOT NULL FOREIGN KEY REFERENCES Groups([Id])
)

-- Task № - 13
CREATE TABLE Subjects(
    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL UNIQUE DEFAULT('UNKNOWN'),
)



-- Task № - 11
CREATE TABLE Lectures(
    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [SubjectId] INT NOT NULL FOREIGN KEY REFERENCES Subjects([Id]),
    [TeacherId] INT NOT NULL FOREIGN KEY REFERENCES Teachers([Id])
)

-- Task № - 8
CREATE TABLE GroupsLectures(
    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [GroupId] INT NOT NULL FOREIGN KEY REFERENCES Groups([Id]),
    [LectureId] INT NOT NULL FOREIGN KEY REFERENCES Lectures([Id])
)

-- Task № - 10
CREATE TABLE LectureRooms(
    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [Building] INT NOT NULL CHECK([Building]BETWEEN 1 AND 5),
    [Name] NVARCHAR(10) NOT NULL UNIQUE DEFAULT('UNKNOWN'),
)

-- Task № - 12
CREATE TABLE Schedules(
    [Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [Class] INT NOT NULL CHECK([Class]BETWEEN 1 AND 8),
    [DayOfWeek] INT NOT NULL CHECK([DayOfWeek]BETWEEN 1 AND 7),
    [Week] INT NOT NULL CHECK([Week]BETWEEN 1 AND 52),
    [LectureId] INT NOT NULL FOREIGN KEY REFERENCES Lectures([Id]),
    [LectureRoomId] INT NOT NULL FOREIGN KEY REFERENCES LectureRooms([Id]),
)



DROP TABLE Schedules;
DROP TABLE GroupsLectures;
DROP TABLE LectureRooms;
DROP TABLE Lectures;
DROP TABLE Subjects;
DROP TABLE GroupsCurators;
DROP TABLE Groups;
DROP TABLE Departments;
DROP TABLE Heads;
DROP TABLE Faculties;
DROP TABLE Deans;
DROP TABLE Curators;
DROP TABLE Assistants;
DROP TABLE Teachers;


INSERT INTO Teachers ([Name], [Surname]) VALUES
('Edward', 'Hopper'),
('Alex', 'Carmack'),
('Alice', 'Smith'),
('Bob', 'Johnson'),
('Charlie', 'Brown'),
('Diana', 'Miller'),
('Eve', 'Davis'),
('Frank', 'Garcia'),
('Grace', 'Rodriguez'),
('Henry', 'Martinez'),
('Ivy', 'Hernandez'),
('Jack', 'Lopez'),
('Karen', 'Gonzalez'),
('Liam', 'Wilson'),
('Mia', 'Anderson'),
('Noah', 'Thomas'),
('Olivia', 'Jackson'),
('Peter', 'White'),
('Quinn', 'Harris'),
('Rachel', 'Martin'),
('Sam', 'Thompson'),
('Tina', 'Moore'),
('Uma', 'Young'),
('Victor', 'King'),
('Wendy', 'Wright'),
('Xavier', 'Scott'),
('Yara', 'Green'),
('Zack', 'Baker'),
('Anna', 'Adams'),
('Ben', 'Hall');

-- Инсерты которая нейросеть сделала под задачу, не ChatGPT , другой

INSERT INTO Deans ([TeacherId]) VALUES
((SELECT Id FROM Teachers WHERE Name = 'Alice' AND Surname = 'Smith')), 
((SELECT Id FROM Teachers WHERE Name = 'Bob' AND Surname = 'Johnson')); 



INSERT INTO Heads ([TeacherId]) VALUES
((SELECT Id FROM Teachers WHERE Name = 'Charlie' AND Surname = 'Brown')), 
((SELECT Id FROM Teachers WHERE Name = 'Diana' AND Surname = 'Miller')), 
((SELECT Id FROM Teachers WHERE Name = 'Eve' AND Surname = 'Davis'));    


INSERT INTO Curators ([TeacherId]) VALUES
((SELECT Id FROM Teachers WHERE Name = 'Frank' AND Surname = 'Garcia')), 
((SELECT Id FROM Teachers WHERE Name = 'Grace' AND Surname = 'Rodriguez')),
((SELECT Id FROM Teachers WHERE Name = 'Henry' AND Surname = 'Martinez'));  


INSERT INTO Assistants ([TeacherId]) VALUES
((SELECT Id FROM Teachers WHERE Name = 'Ivy' AND Surname = 'Hernandez')), 
((SELECT Id FROM Teachers WHERE Name = 'Jack' AND Surname = 'Lopez')),   
((SELECT Id FROM Teachers WHERE Name = 'Karen' AND Surname = 'Gonzalez'));  


INSERT INTO Faculties ([Building], [Name], [DeanId]) VALUES
(1, 'Computer Science', (SELECT Id FROM Deans WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Alice' AND Surname = 'Smith'))),
(2, 'Engineering', (SELECT Id FROM Deans WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Bob' AND Surname = 'Johnson'))),
(3, 'Humanities', (SELECT Id FROM Deans WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Alice' AND Surname = 'Smith'))); 


INSERT INTO Departments ([Building], [Name], [FacultyId], [HeadId]) VALUES
(1, 'Software Development', (SELECT Id FROM Faculties WHERE Name = 'Computer Science'), (SELECT Id FROM Heads WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Charlie' AND Surname = 'Brown'))),
(1, 'Data Science', (SELECT Id FROM Faculties WHERE Name = 'Computer Science'), (SELECT Id FROM Heads WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Diana' AND Surname = 'Miller'))),
(2, 'Mechanical Engineering', (SELECT Id FROM Faculties WHERE Name = 'Engineering'), (SELECT Id FROM Heads WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Eve' AND Surname = 'Davis'))),
(3, 'Linguistics', (SELECT Id FROM Faculties WHERE Name = 'Humanities'), (SELECT Id FROM Heads WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Charlie' AND Surname = 'Brown')));



INSERT INTO Groups ([Name], [Year], [DepartmentId]) VALUES
('F505', 5, (SELECT Id FROM Departments WHERE Name = 'Software Development')), 
('CS101', 1, (SELECT Id FROM Departments WHERE Name = 'Data Science')),
('CS202', 2, (SELECT Id FROM Departments WHERE Name = 'Software Development')),
('EN303', 3, (SELECT Id FROM Departments WHERE Name = 'Mechanical Engineering')),
('HU404', 4, (SELECT Id FROM Departments WHERE Name = 'Linguistics')),
('SD501', 5, (SELECT Id FROM Departments WHERE Name = 'Software Development')),
('DS502', 5, (SELECT Id FROM Departments WHERE Name = 'Data Science')), 
('ME101', 1, (SELECT Id FROM Departments WHERE Name = 'Mechanical Engineering')),
('SD102', 1, (SELECT Id FROM Departments WHERE Name = 'Software Development'));


INSERT INTO GroupsCurators ([CuratorId], [GroupId]) VALUES
((SELECT Id FROM Curators WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Frank' AND Surname = 'Garcia')), (SELECT Id FROM Groups WHERE Name = 'F505')),
((SELECT Id FROM Curators WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Grace' AND Surname = 'Rodriguez')), (SELECT Id FROM Groups WHERE Name = 'CS101')),
((SELECT Id FROM Curators WHERE TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Henry' AND Surname = 'Martinez')), (SELECT Id FROM Groups WHERE Name = 'EN303'));


INSERT INTO Subjects ([Name]) VALUES
('Database Systems'),
('Web Development'),
('Algorithms and Data Structures'),
('Machine Learning'),
('Calculus'),
('Physics'),
('Literature'),
('SQL Basics'),
('Advanced Programming'),
('Operating Systems');


INSERT INTO Lectures ([SubjectId], [TeacherId]) VALUES

((SELECT Id FROM Subjects WHERE Name = 'Database Systems'), (SELECT Id FROM Teachers WHERE Name = 'Edward' AND Surname = 'Hopper')),
((SELECT Id FROM Subjects WHERE Name = 'SQL Basics'), (SELECT Id FROM Teachers WHERE Name = 'Edward' AND Surname = 'Hopper')),

((SELECT Id FROM Subjects WHERE Name = 'Advanced Programming'), (SELECT Id FROM Teachers WHERE Name = 'Alex' AND Surname = 'Carmack')),
((SELECT Id FROM Subjects WHERE Name = 'Operating Systems'), (SELECT Id FROM Teachers WHERE Name = 'Alex' AND Surname = 'Carmack')),

((SELECT Id FROM Subjects WHERE Name = 'Web Development'), (SELECT Id FROM Teachers WHERE Name = 'Liam' AND Surname = 'Wilson')),
((SELECT Id FROM Subjects WHERE Name = 'Algorithms and Data Structures'), (SELECT Id FROM Teachers WHERE Name = 'Mia' AND Surname = 'Anderson')),
((SELECT Id FROM Subjects WHERE Name = 'Machine Learning'), (SELECT Id FROM Teachers WHERE Name = 'Noah' AND Surname = 'Thomas')),
((SELECT Id FROM Subjects WHERE Name = 'Calculus'), (SELECT Id FROM Teachers WHERE Name = 'Olivia' AND Surname = 'Jackson')),
((SELECT Id FROM Subjects WHERE Name = 'Physics'), (SELECT Id FROM Teachers WHERE Name = 'Peter' AND Surname = 'White')),
((SELECT Id FROM Subjects WHERE Name = 'Literature'), (SELECT Id FROM Teachers WHERE Name = 'Quinn' AND Surname = 'Harris')),
((SELECT Id FROM Subjects WHERE Name = 'Database Systems'), (SELECT Id FROM Teachers WHERE Name = 'Ivy' AND Surname = 'Hernandez')); 


INSERT INTO GroupsLectures ([GroupId], [LectureId]) VALUES
((SELECT Id FROM Groups WHERE Name = 'F505'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Database Systems') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Edward' AND Surname = 'Hopper'))),
((SELECT Id FROM Groups WHERE Name = 'F505'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'SQL Basics') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Edward' AND Surname = 'Hopper'))),
((SELECT Id FROM Groups WHERE Name = 'F505'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Database Systems') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Ivy' AND Surname = 'Hernandez'))),
((SELECT Id FROM Groups WHERE Name = 'SD501'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Advanced Programming') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Alex' AND Surname = 'Carmack'))),
((SELECT Id FROM Groups WHERE Name = 'DS502'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Operating Systems') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Alex' AND Surname = 'Carmack'))),
((SELECT Id FROM Groups WHERE Name = 'CS101'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Calculus') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Olivia' AND Surname = 'Jackson'))),
((SELECT Id FROM Groups WHERE Name = 'CS202'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Web Development') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Liam' AND Surname = 'Wilson'))),
((SELECT Id FROM Groups WHERE Name = 'EN303'), (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Physics') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Peter' AND Surname = 'White')));



INSERT INTO LectureRooms ([Building], [Name]) VALUES
(1, 'A311'), 
(1, 'A104'), 
(2, 'B201'),
(3, 'C305'),
(1, 'A101'),
(4, 'D402'),
(5, 'E503');



INSERT INTO Schedules ([Class], [DayOfWeek], [Week], [LectureId], [LectureRoomId]) VALUES
(1, 1, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Database Systems') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Edward' AND Surname = 'Hopper')), (SELECT Id FROM LectureRooms WHERE Name = 'A311')),
(2, 1, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'SQL Basics') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Edward' AND Surname = 'Hopper')), (SELECT Id FROM LectureRooms WHERE Name = 'A311')),
(3, 2, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Web Development') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Liam' AND Surname = 'Wilson')), (SELECT Id FROM LectureRooms WHERE Name = 'A104')),
(4, 2, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Algorithms and Data Structures') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Mia' AND Surname = 'Anderson')), (SELECT Id FROM LectureRooms WHERE Name = 'A104')),
(1, 1, 2, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Machine Learning') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Noah' AND Surname = 'Thomas')), (SELECT Id FROM LectureRooms WHERE Name = 'B201')),
(2, 1, 3, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Calculus') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Olivia' AND Surname = 'Jackson')), (SELECT Id FROM LectureRooms WHERE Name = 'C305')),
(3, 3, 2, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Physics') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Peter' AND Surname = 'White')), (SELECT Id FROM LectureRooms WHERE Name = 'B201')),
(1, 4, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Literature') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Quinn' AND Surname = 'Harris')), (SELECT Id FROM LectureRooms WHERE Name = 'A101')),
(5, 1, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Advanced Programming') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Alex' AND Surname = 'Carmack')), (SELECT Id FROM LectureRooms WHERE Name = 'D402')),
(6, 1, 1, (SELECT Id FROM Lectures WHERE SubjectId = (SELECT Id FROM Subjects WHERE Name = 'Operating Systems') AND TeacherId = (SELECT Id FROM Teachers WHERE Name = 'Alex' AND Surname = 'Carmack')), (SELECT Id FROM LectureRooms WHERE Name = 'E503'));


















-- 1 (Edward 2 раза потому что он читает в этой аудитории 2 разных лекций!)
SELECT LR.Name
FROM Teachers T
JOIN Lectures L ON L.TeacherId = T.Id
JOIN Schedules S ON S.LectureId = L.Id
JOIN LectureRooms LR ON LR.Id = S.LectureRoomId 
WHERE T.Name = 'Edward' AND T.Surname = 'Hopper'
 


 -- 2
SELECT T.Surname 
FROM Assistants A
JOIN Teachers T ON A.TeacherId = T.Id
JOIN Lectures L ON L.TeacherId = T.Id
JOIN GroupsLectures GL ON GL.LectureId = L.Id
JOIN Groups G ON GL.GroupId = G.Id
WHERE G.Name = 'F505'


-- 3 
SELECT S.Name
FROM Subjects  S
JOIN Lectures  L ON S.Id = L.SubjectId
JOIN Teachers  T ON L.TeacherId = T.Id
JOIN GroupsLectures  GL ON L.Id = GL.LectureId
JOIN Groups  G ON GL.GroupId = G.Id
WHERE T.Name = 'Alex' AND T.Surname = 'Carmack' AND G.Year = 5;


-- 4 
SELECT T.Surname
FROM Teachers T
WHERE T.Id NOT IN (
  SELECT L.TeacherId
  FROM Lectures  L
  JOIN Schedules S ON L.Id = S.LectureId
  WHERE S.DayOfWeek = 1
)

-- 5
SELECT LR.Name , LR.Building
FROM LectureRooms LR
WHERE LR.Id NOT IN (
   SELECT S.LectureRoomId 
   FROM Schedules S
   WHERE S.DayOfWeek = 3 AND S.Week = 2 AND S.Class = 3
)



-- 6
SELECT T.Name, T.Surname
FROM Teachers T
WHERE T.Id IN (
    SELECT L.TeacherId
    FROM Lectures  L
    JOIN GroupsLectures  GL ON L.Id = GL.LectureId
    JOIN Groups  G ON GL.GroupId = G.Id
    JOIN Departments  D ON G.DepartmentId = D.Id
    JOIN Faculties  F ON D.FacultyId = F.Id
    WHERE F.Name = 'Computer Science'
)
AND T.Id NOT IN(
  SELECT C.TeacherId
  FROM Curators C
  JOIN GroupsCurators GC ON GC.CuratorId = C.Id
  JOIN Groups G ON GC.GroupId = G.Id
  JOIN Departments D ON D.Id = G.DepartmentId
  WHERE D.Name = 'Software Development'
)


-- 7
SELECT F.Building
FROM Faculties F
UNION
SELECT D.Building 
FROM Departments D
UNION
SELECT LR.Building 
FROM LectureRooms LR


-- 8

SELECT T.Name
FROM Teachers T
JOIN Deans D ON D.TeacherId = T.Id
UNION 
SELECT T.Name
FROM Teachers T
JOIN Heads H ON H.TeacherId = T.Id
UNION 
SELECT DISTINCT T.Name
FROM Teachers T
UNION 
SELECT T.Name
FROM Teachers T
JOIN Curators C ON C.TeacherId = T.Id



-- 9  ( В задании говорится про корпус номер 6 , хотя корпуса могут быть от 1 до 5 , думаю опечатка, и взял корпус номер 1 для примера)
SELECT DISTINCT S.DayOfWeek
FROM Schedules  S
JOIN LectureRooms  LR ON S.LectureRoomId = LR.Id
WHERE LR.Name IN ('A311', 'A104') AND LR.Building = 1;


--  10
SELECT Year , COUNT(Id) 'Groups Number'
FROM Groups
GROUP BY Year
ORDER BY Year;


-- 11 
SELECT T.Name, T.Surname, COUNT(L.Id) ' Assistants Number '
FROM Teachers  T
JOIN Assistants  A ON T.Id = A.TeacherId
LEFT JOIN Lectures  L ON T.Id = L.TeacherId
GROUP BY T.Name, T.Surname
ORDER BY COUNT(L.Id) DESC;


-- 12
SELECT G.Name, COUNT(GL.LectureId) 'Lectures Number'
FROM Groups G
JOIN GroupsLectures GL ON G.Id = GL.GroupId
GROUP BY G.Name
ORDER BY COUNT(GL.LectureId) DESC;