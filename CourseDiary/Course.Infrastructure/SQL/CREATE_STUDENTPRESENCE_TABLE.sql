CREATE TABLE [StudentPresence](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[LessonDate] DATE NOT NULL,
	[StudentId] INT FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id]),
	[CourseId] INT FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]),
	[Presence] VARCHAR(255) NOT NULL
)