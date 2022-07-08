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

                    success = rowsAffected > 1;

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


        public async Task<List<Course>> GetAllCoursesAsync()
        {
            List<Course> courses = new List<Course>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [Courses]";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Course course = new Course();
                        course.Id = int.Parse(dataReader["Id"].ToString());
                        course.Name = dataReader["Name"].ToString();
                        course.BeginDate = DateTime.Parse(dataReader["BeginDate"].ToString());
                        course.Trainer = new Trainer
                        {
                            Id = int.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString())
                        };
                        course.PresenceTreshold = double.Parse(dataReader["TestTreshold"].ToString());
                        course.HomeworkTreshold = double.Parse(dataReader["TestTreshold"].ToString());
                        course.TestTreshold = double.Parse(dataReader["TestTreshold"].ToString());
                        course.State = Enum.TryParse(dataReader["State"].ToString(), out State cos) ? cos : course.State;

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

        public async Task<List<StudentPresence>> GetCourseStudentPresence(int courseId)
        {
            List<StudentPresence> studentPresences = new List<StudentPresence>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [StudentPresence]";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        StudentPresence studentPresence = new StudentPresence();
                        studentPresence.LessonDate = DateTime.Parse(dataReader["LessonDate"].ToString());
                        studentPresence.StudentId = int.Parse(dataReader["StudentId"].ToString());
                        studentPresence.CourseId = int.Parse(dataReader["CourseId"].ToString());
                        Enum.TryParse<Presence>(dataReader["CourseId"].ToString(), out studentPresence.Presence);

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

                    string commandText = @"SELECT * FROM [HomeworkResults]";
                    SqlCommand command = new SqlCommand(commandText, connection);
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

                    string commandText = @"SELECT * FROM [TestResults]";
                    SqlCommand command = new SqlCommand(commandText, connection);
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
    }
}
