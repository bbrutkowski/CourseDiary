create database [CourseDiaryDB];

Create Table [Trainers](
[Id] INT IDENTITY(1,1) PRIMARY KEY,
[Name] VARCHAR(255),
[Surname] VARCHAR(255),
[Email] VARCHAR(255) UNIQUE,
[Password] VARCHAR(255),
[DateOfBirth] VARCHAR(255)
);