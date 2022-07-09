using CourseDiary.Domain.Interfaces;
using System.Configuration;
using System.Threading.Tasks;
using CourseDiary.Domain.Models;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace CourseDiary.Infrastructure
{   

    public class CourseRepository : ICourseRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CourseDiaryDBConnectionString"].ConnectionString;

        public async Task<bool> Add(Course course)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [Courses] ([Name],[BeginDate],[TrainerId],[PresenceTreshold],[HomeworkTreshold],[TestTreshold],[State]) VALUES (@Name, @BeginDate, @TrainerId, @PresenceTreshold, @HomeworkTreshold, @TestTreshold, @State)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = course.Name;
                    command.Parameters.Add("@BeginDate", SqlDbType.DateTime2).Value = course.BeginDate;
                    command.Parameters.Add("@TrainerId", SqlDbType.Int).Value = course.Trainer;
                    command.Parameters.Add("@PresenceTreshold", SqlDbType.Float, 8).Value = course.PresenceTreshold;
                    command.Parameters.Add("@HomeworkTreshold", SqlDbType.Float, 8).Value = course.HomeworkTreshold;
                    command.Parameters.Add("@TestTreshold", SqlDbType.Float, 8).Value = course.TestTreshold;
                    command.Parameters.Add("@State", SqlDbType.VarChar, 255).Value = course.State;

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    string getLastAddedCourseIdCommandSql = $"SELECT IDENT_CURRENT('Courses') AS [CourseId];";
                    SqlCommand getLastAddedCourseIdCommand = new SqlCommand(getLastAddedCourseIdCommandSql, connection);

                    SqlDataReader reader = await getLastAddedCourseIdCommand.ExecuteReaderAsync();
                    await reader.ReadAsync();

                    int courseId = int.Parse(reader["CourseId"].ToString());

                    reader.Close();

                    foreach (var student in course.Students)
                    {
                        commandText = $"INSERT INTO [CourseStudents] ([StudentId],[CourseId]) VALUES (@StudentId, @CourseId)";
                        command = new SqlCommand(commandText, connection);
                        command.Parameters.Add("@StudentId", SqlDbType.Int).Value = student.Id;
                        command.Parameters.Add("@CourseId", SqlDbType.Int).Value = courseId;

                        rowsAffected += await command.ExecuteNonQueryAsync();
                    }

                    success = rowsAffected >= 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }

        public async Task<bool> ClosingCourse(Course course)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandSql = "UPDATE Courses SET State = @State WHERE Name = @Name";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = course.Name;
                    command.Parameters.Add("@State", SqlDbType.VarChar, 255).Value = course.State;

                    if (command.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }


        public async Task<List<Course>> GetAllCoursesAsync()
        {
            List<Course> courses = new List<Course>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT [Courses].[Id] as [CourseId], [Courses].[Name] as [CourseName], [Courses].[BeginDate] as [CourseBeginDate], [Courses].[PresenceTreshold] as [CoursePresenceTreshold], [Courses].[HomeworkTreshold] as [CourseHomeworkTreshold], [Courses].[TestTreshold] as [CourseTestTreshold], [Courses].[State] as [CourseState], [Trainers].[Id] as [TrainerId], [Trainers].[Name] as [TrainerName], [Trainers].[Surname] as [TrainerSurname], [Trainers].[Email] as [TrainerEmail], [Trainers].[Password] as [TrainerPassword], [Trainers].[DateOfBirth] as [TrainerDateOfBirth] FROM [Courses] FULL OUTER JOIN [Trainers] ON [Trainers].[Id] = [TrainerId]";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Course course = new Course();
                        course.Id = int.Parse(dataReader["CourseId"].ToString());
                        course.Name = dataReader["CourseName"].ToString();
                        course.BeginDate = DateTime.Parse(dataReader["CourseBeginDate"].ToString());
                        course.Trainer = new Trainer
                        {
                            Id = int.Parse(dataReader["TrainerId"].ToString()),
                            Name = dataReader["TrainerName"].ToString(),
                            Surname = dataReader["TrainerSurname"].ToString(),
                            Email = dataReader["TrainerEmail"].ToString(),
                            Password = dataReader["TrainerPassword"].ToString(),
                            DateOfBirth = DateTime.Parse(dataReader["TrainerDateOfBirth"].ToString())
                        };
                        course.PresenceTreshold = double.Parse(dataReader["CoursePresenceTreshold"].ToString());
                        course.HomeworkTreshold = double.Parse(dataReader["CourseHomeworkTreshold"].ToString());
                        course.TestTreshold = double.Parse(dataReader["CourseTestTreshold"].ToString());
                        course.State = Enum.TryParse(dataReader["CourseState"].ToString(), out State cos) ? cos : course.State;

                        string studentCommandText = @"SELECT [Students].[Id] as [StudentId], [Students].[Name] as [StudentName], [Students].[Surname] as [StudentSurname], [Students].[Email] as [StudentEmail], [Students].[Password] as [StudentPassword], [Students].[BirthDate] as [StudentBirthDate] FROM [CourseStudents] RIGHT JOIN [Students] ON [CourseStudents].[StudentId] = [Students].[Id] WHERE [CourseStudents].[CourseId] = @Id";
                        SqlCommand studentCommand = new SqlCommand(studentCommandText, connection);
                        studentCommand.Parameters.Add("@Id", SqlDbType.Int).Value = course.Id;

                        SqlDataReader studentDataReader = await studentCommand.ExecuteReaderAsync();

                        List<Student> students = new List<Student>();
                        while (studentDataReader.Read())
                        {
                            Student student = new Student();
                            student.Id = int.Parse(dataReader["StudentId"].ToString());
                            student.Name = dataReader["StudentName"].ToString();
                            student.Surname = dataReader["StudentSurname"].ToString();
                            student.Password = dataReader["StudentPassword"].ToString();
                            student.Email = dataReader["StudentEmail"].ToString();
                            student.BirthDate = DateTime.Parse(dataReader["StudentBirthDate"].ToString());

                            students.Add(student);
                        }

                        courses.Add(course);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                courses = null;
            }

            return courses;
        }

        public async Task<bool> AddHomeworkResult(HomeworkResults result)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [HomeworkResults] ([HomeWorkName],[FinishDate],[StudentId],[Results] VALUES (@HomeWorkName, @FinishDate, @StudentId, @CourseId, @Results)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@HomeWorkName", SqlDbType.VarChar, 255).Value = result.HomeworkName;
                    command.Parameters.Add("@FinishDate", SqlDbType.Date).Value = result.FinishDate;
                    command.Parameters.Add("@StudentId", SqlDbType.Int).Value = result.StudentId;
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = result.CourseId;
                    command.Parameters.Add("@Results", SqlDbType.Float, 8).Value = result.Result;
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    success = rowsAffected == 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }


        public async Task<bool> AddPresence(StudentPresence presence)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = "INSERT INTO [StudentPresence] ([LessonDate],[CourseId],[StudentId],[Presence]) VALUES (@LessonDate, @CourseId, @StudentId, @Presence)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@LessonDate", SqlDbType.Date).Value = presence.LessonDate;
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = presence.CourseId;
                    command.Parameters.Add("@StudentId", SqlDbType.Int).Value = presence.StudentId;
                    command.Parameters.Add("@Presence", SqlDbType.NVarChar, 255).Value = presence.Presence;

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    success = rowsAffected == 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }

        public async Task<bool> AddTestResult(TestResults testResult)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [TestResults] ([TestName],[FinishDate],[StudentId],[Results] VALUES (@TestName, @FinishDate, @StudentId, @CourseId, @Results)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@TestName", SqlDbType.VarChar, 255).Value = testResult.TestName;
                    command.Parameters.Add("@FinishDate", SqlDbType.Date).Value = testResult.FinishDate;
                    command.Parameters.Add("@StudentId", SqlDbType.Int).Value = testResult.StudentId;
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = testResult.CourseId;
                    command.Parameters.Add("@Results", SqlDbType.Float, 8).Value = testResult.Result;
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    success = rowsAffected == 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }

        public async Task<List<CourseResult>> GetAllCourseResults(int id)
        {
            List<CourseResult> courseResults = new List<CourseResult>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT
	                                     [CourseStudents].[Id] AS [ID],
	                                     [CourseStudents].[CourseId],
	                                     [HomeworkResults].[StudentId],
	                                     [HomeworkResults].[HomeWorkName],	
	                                     [HomeworkResults].[Results],
	                                     [TestResults].[TestName],
	                                     [TestResults].[Results],
	                                     [Students].[Email]
	                                     FROM[CourseStudents], [HomeworkResults], [TestResults], [Students]
	                                     WHERE CourseId = '@CourseId'";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        CourseResult result = new CourseResult();
                        result.HomeworkResults = new HomeworkResults()
                        {
                            StudentId = int.Parse(dataReader["StudentId"].ToString()),
                            Result = float.Parse(dataReader["Result"].ToString()),
                        };
                        result.TestResults = new TestResults()
                        {
                            StudentId = int.Parse(dataReader["StudentId"].ToString()),
                            Result = float.Parse(dataReader["Result"].ToString()),
                        };
                        //result.StudentPresence.Student.Email = dataReader["Email"].ToString();                      

                        courseResults.Add(result);
                    }                
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                courseResults = null;
            }

            return courseResults;
        }

        public async Task<List<StudentPresence>> GetCourseStudentPresence(int courseId)
        {
            List<StudentPresence> studentPresences = new List<StudentPresence>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [StudentPresence] WHERE [StudentPresence].[CourseId] = @CourseId";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = courseId;

                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (dataReader.Read())
                    {
                        StudentPresence studentPresence = new StudentPresence();
                        studentPresence.LessonDate = DateTime.Parse(dataReader["LessonDate"].ToString());
                        studentPresence.StudentId = int.Parse(dataReader["StudentId"].ToString());
                        studentPresence.CourseId = int.Parse(dataReader["CourseId"].ToString());
                        Presence presence;
                        Enum.TryParse<Presence>(dataReader["CourseId"].ToString(), out presence);
                        studentPresence.Presence = presence;

                        studentPresences.Add(studentPresence);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                studentPresences = null;
            }

            return studentPresences;
        }

        public async Task<List<HomeworkResults>> GetCourseHomeworkResults(int courseId)
        {
            List<HomeworkResults> homeworkResults = new List<HomeworkResults>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [HomeworkResults] WHERE [HomeworkResults].[CourseId] = @CourseId";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = courseId;

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        HomeworkResults homeworkResult = new HomeworkResults();
                        homeworkResult.Id = int.Parse(dataReader["Id"].ToString());
                        homeworkResult.HomeworkName = dataReader["HomeworkName"].ToString();
                        homeworkResult.FinishDate = DateTime.Parse(dataReader["FinishDate"].ToString());
                        homeworkResult.StudentId = int.Parse(dataReader["StudentId"].ToString());
                        homeworkResult.Result = float.Parse(dataReader["Result"].ToString());

                        homeworkResults.Add(homeworkResult);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                homeworkResults = null;
            }

            return homeworkResults;
        }

        public async Task<List<TestResults>> GetCourseTestResults(int courseId)
        {
            List<TestResults> testResults = new List<TestResults>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [TestResults] WHERE [TestResults].[CourseId] = @CourseId";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = courseId;

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        TestResults testResult = new TestResults();
                        testResult.Id = int.Parse(dataReader["Id"].ToString());
                        testResult.TestName = dataReader["TestName"].ToString();
                        testResult.FinishDate = DateTime.Parse(dataReader["FinishDate"].ToString());
                        testResult.StudentId = int.Parse(dataReader["StudentId"].ToString());
                        testResult.Result = float.Parse(dataReader["Result"].ToString());

                        testResults.Add(testResult);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                testResults = null;
            }

            return testResults;
        }

        public async Task<Course> GetCourse(int id)
        {
            Course course = new Course();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT [Courses].[Id] as [CourseId], [Courses].[Name] as [CourseName], [Courses].[BeginDate] as [CourseBeginDate], [Courses].[PresenceTreshold] as [CoursePresenceTreshold], [Courses].[HomeworkTreshold] as [CourseHomeworkTreshold], [Courses].[TestTreshold] as [CourseTestTreshold], [Courses].[State] as [CourseState], [Trainers].[Id] as [TrainerId], [Trainers].[Name] as [TrainerName], [Trainers].[Surname] as [TrainerSurname], [Trainers].[Email] as [TrainerEmail], [Trainers].[Password] as [TrainerPassword], [Trainers].[DateOfBirth] as [TrainerDateOfBirth] FROM [Courses] FULL OUTER JOIN [Trainers] ON [Trainers].[Id] = [TrainerId] WHERE [Courses].[Id] = @Id";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        course.Id = int.Parse(dataReader["CourseId"].ToString());
                        course.Name = dataReader["CourseName"].ToString();
                        course.BeginDate = DateTime.Parse(dataReader["CourseBeginDate"].ToString());
                        course.Trainer = new Trainer
                        {
                            Id = int.Parse(dataReader["TrainerId"].ToString()),
                            Name = dataReader["TrainerName"].ToString(),
                            Surname = dataReader["TrainerSurname"].ToString(),
                            Email = dataReader["TrainerEmail"].ToString(),
                            Password = dataReader["TrainerPassword"].ToString(),
                            DateOfBirth = DateTime.Parse(dataReader["TrainerDateOfBirth"].ToString())
                        };
                        course.PresenceTreshold = double.Parse(dataReader["CoursePresenceTreshold"].ToString());
                        course.HomeworkTreshold = double.Parse(dataReader["CourseHomeworkTreshold"].ToString());
                        course.TestTreshold = double.Parse(dataReader["CourseTestTreshold"].ToString());
                        course.State = Enum.TryParse(dataReader["CourseState"].ToString(), out State cos) ? cos : course.State;

                        string studentCommandText = @"SELECT [Students].[Id] as [StudentId], [Students].[Name] as [StudentName], [Students].[Surname] as [StudentSurname], [Students].[Email] as [StudentEmail], [Students].[Password] as [StudentPassword], [Students].[BirthDate] as [StudentBirthDate] FROM [CourseStudents] RIGHT JOIN [Students] ON [CourseStudents].[StudentId] = [Students].[Id] WHERE [CourseStudents].[CourseId] = @Id";
                        SqlCommand studentCommand = new SqlCommand(studentCommandText, connection);
                        studentCommand.Parameters.Add("@Id", SqlDbType.Int).Value = course.Id;

                        SqlDataReader studentDataReader = await studentCommand.ExecuteReaderAsync();

                        List<Student> students = new List<Student>();
                        while (studentDataReader.Read())
                        {
                            Student student = new Student();
                            student.Id = int.Parse(dataReader["StudentId"].ToString());
                            student.Name = dataReader["StudentName"].ToString();
                            student.Surname = dataReader["StudentSurname"].ToString();
                            student.Password = dataReader["StudentPassword"].ToString();
                            student.Email = dataReader["StudentEmail"].ToString();
                            student.BirthDate = DateTime.Parse(dataReader["StudentBirthDate"].ToString());

                            students.Add(student);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                course = null;
            }

            return course;
        }

        public async Task<bool> AddCourseResults(CourseResults courseResults)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [CourseResults] ([CourseId]) VALUES (@CourseId)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Name", SqlDbType.Int).Value = courseResults.Course.Id;
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    string getLastAddedCourseIdCommandSql = $"SELECT IDENT_CURRENT('CourseResults') AS [CourseResultsId];";
                    SqlCommand getLastAddedCourseIdCommand = new SqlCommand(getLastAddedCourseIdCommandSql, connection);
                    SqlDataReader reader = await getLastAddedCourseIdCommand.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    int courseResultsId = int.Parse(reader["CourseResultsId"].ToString());

                    reader.Close();

                    foreach (var studentResult in courseResults.StudentResults)
                    {
                        commandText = "INSERT INTO [StudentResults] ([CourseResultsId], [StudentId], [StudentPresencePercentage], [StudentJustifiedAbsencePercentage], [StudentHomeworkPercentage], [StudentTestPercentage]) VALUES (@CourseResultsId, @StudentId, @StudentPresencePercentage, @StudentJustifiedAbsencePercentage, @StudentHomeworkPercentage, @StudentTestPercentage)";
                        command = new SqlCommand(commandText, connection);
                        command.Parameters.Add("@CourseResultsId", SqlDbType.Int).Value = courseResultsId;
                        command.Parameters.Add("@StudentId", SqlDbType.Int).Value = studentResult.Student.Id;
                        command.Parameters.Add("@StudentPresencePercentage", SqlDbType.Float, 8).Value = studentResult.StudentPresencePercentage;
                        command.Parameters.Add("@StudentJustifiedAbsencePercentage", SqlDbType.Float, 8).Value = studentResult.StudentJustifiedAbsencePercentage;
                        command.Parameters.Add("@StudentHomeworkPercentage", SqlDbType.Float, 8).Value = studentResult.StudentHomeworkPercentage;
                        command.Parameters.Add("@StudentTestPercentage", SqlDbType.Float, 8).Value = studentResult.StudentTestPercentage;

                        rowsAffected += await command.ExecuteNonQueryAsync();
                    }

                    success = rowsAffected >= 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }

        public async Task<CourseResults> GetCourseResults(int courseId)
        {
            CourseResults courseResults = new CourseResults();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [CourseResults] WHERE [CourseResults].[CourseId] = @CourseId";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = courseId;
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (dataReader.Read())
                    {
                        courseResults.Id = int.Parse(dataReader["Id"].ToString());
                    }

                    dataReader.Close();

                    commandText = "[Students].[Id] as [StudentId], [Students].[Name] as [StudentName], [Students].[Surname] as [StudentSurname], [Students].[Email] as [StudentEmail], [Students].[Password] as [StudentPassword], [Students].[BirthDate] as [StudentBirthDate], [StudentPresencePercentage], [StudentJustifiedAbsencePercentage], [StudentHomeworkPercentage], [StudentTestPercentage], [FinalResult] FROM [StudentResults] FULL OUTER JOIN [Students] ON [StudentResults].[StudentId] = [Students].[Id] WHERE [StudentResults].[CourseResultsId] = @CourseResultsId";
                    command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@CourseResultsId", SqlDbType.Int).Value = courseResults.Id;
                    dataReader = await command.ExecuteReaderAsync();

                    List<StudentResult> studentResults = new List<StudentResult>();
                    while (dataReader.Read())
                    {
                        StudentResult studentResult = new StudentResult();

                        Student student = new Student();
                        student.Id = int.Parse(dataReader["StudentId"].ToString());
                        student.Name = dataReader["StudentName"].ToString();
                        student.Surname = dataReader["StudentSurname"].ToString();
                        student.Password = dataReader["StudentPassword"].ToString();
                        student.Email = dataReader["StudentEmail"].ToString();
                        student.BirthDate = DateTime.Parse(dataReader["StudentBirthDate"].ToString());
                        
                        studentResult.Student = student;
                        studentResult.StudentPresencePercentage = float.Parse(dataReader["StudentPresencePercentage"].ToString());
                        studentResult.StudentJustifiedAbsencePercentage = float.Parse(dataReader["StudentJustifiedAbsencePercentage"].ToString());
                        studentResult.StudentHomeworkPercentage = float.Parse(dataReader["StudentHomeworkPercentage"].ToString());
                        studentResult.StudentTestPercentage = float.Parse(dataReader["StudentTestPercentage"].ToString());
                        FinalResult finalResult;
                        Enum.TryParse<FinalResult>(dataReader["FinalResult"].ToString(), out finalResult);
                        studentResult.FinalResult = finalResult;

                        studentResults.Add(studentResult);
                    }

                    courseResults.StudentResults = studentResults;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                courseResults = null;
            }

            return courseResults;
        }
    }
}
