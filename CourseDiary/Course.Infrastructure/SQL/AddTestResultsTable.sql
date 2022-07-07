
  Create Table [TestResults](
[Id] INT IDENTITY(1,1) PRIMARY KEY,
[TestName] VARCHAR(255),
[FinishDate] DATE,
[StudentId] INT FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id]),
[Results] FLOAT CHECK (RESULTS<200) 
);