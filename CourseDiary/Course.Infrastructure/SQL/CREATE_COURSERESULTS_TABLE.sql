CREATE TABLE [CourseResults](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CourseId] INT FOREIGN KEY ([CourseId]) REFERENCES [Students]([Id]) NOT NULL
)

CREATE TABLE [StudentResults](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CourseResultsId] INT FOREIGN KEY ([CourseResults]) REFERENCES [CourseResults]([Id]) NOT NULL,
	[StudentId] INT FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id]) NOT NULL,
	[StudentPresencePercentage] FLOAT(8) NOT NULL,
	[StudentJustifiedAbsencePercentage] FLOAT(8) NOT NULL,
	[StudentHomeworkPercentage] FLOAT(8) NOT NULL,
	[StudentTestPercentage] FLOAT(8) NOT NULL,
	[FinalResult] VARCHAR(255) NOT NULL
)