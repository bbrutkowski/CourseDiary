CREATE TABLE [Courses](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(255) NOT NULL,
	[BeginDate] DATE NOT NULL,
	[TrainerId] INT FOREIGN KEY ([TrainerId]) REFERENCES [Trainers]([Id]),
	[PresenceTreshold] FLOAT(8) NOT NULL,
	[HomeworkTreshold] FLOAT(8) NOT NULL,
	[TestTreshold] FLOAT(8) NOT NULL
);

CREATE TABLE [CourseStudents](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CourseId] INT FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]),
	[StudentId] INT FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id])
)