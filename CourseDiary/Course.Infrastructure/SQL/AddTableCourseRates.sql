Create Table [CourseRates](
[Id] INT IDENTITY(1,1) PRIMARY KEY,
[CourseId] INT FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]),
[Description] VARCHAR(255),
[ProgramRate] FLOAT,
[TrainerRate] FLOAT,
[ToolsRate] FLOAT
);