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

                    string commandText = $"INSERT INTO [Courses] ([Name],[BeginDate],[TrainerId],[PresenceTreshold],[HomeworkTreshold],[TestTreshold]) VALUES (@Name, @BeginDate, @TrainerId, @PresenceTreshold, @HomeworkTreshold, @TestTreshold)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = course.Name;
                    command.Parameters.Add("@BeginDate", SqlDbType.DateTime2).Value = course.BeginDate;
                    command.Parameters.Add("@TrainerId", SqlDbType.Int).Value = course.Trainer;
                    command.Parameters.Add("@PresenceTreshold", SqlDbType.Float, 8).Value = course.PresenceTreshold;
                    command.Parameters.Add("@HomeworkTreshold", SqlDbType.Float, 8).Value = course.HomeworkTreshold;
                    command.Parameters.Add("@TestTreshold", SqlDbType.Float, 8).Value = course.TestTreshold;
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

        public async Task<List<Course>> GetAllCourses()
        {
            List<Course> courses = new List<Course>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"SELECT * FROM [Courses]";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                       Course course;

                        try
                        {
                            course = new Course
                            {
                                Name = dataReader["Name"].ToString(),                              
                            };
                            courses.Add(course);

                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return courses;
        }       
    }
}
