using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CourseDiary.Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CourseDiaryDBConnectionString"].ConnectionString;

        public async Task AddStudentAsync(Student student)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [Students] ([Name], [Surname], [Email], [Password], [BirthDate]) VALUES (@Name, @Surname, @Email, @Password, @BirthDate)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = student.Name;
                    command.Parameters.Add("@Surname", SqlDbType.NVarChar, 255).Value = student.Surname;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = student.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = student.Password;
                    command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = student.BirthDate;
                    await command.ExecuteNonQueryAsync();

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            List<Student> students = new List<Student>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [Students]";
                    SqlCommand command = new SqlCommand(commandText, connection);

                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (dataReader.Read())
                    {
                        Student student = new Student();
                        student.Id = int.Parse(dataReader["Id"].ToString());
                        student.Name = dataReader["Name"].ToString();
                        student.Surname = dataReader["Surname"].ToString();
                        student.Password = dataReader["Password"].ToString();
                        student.Email = dataReader["Email"].ToString();
                        student.BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString());

                        students.Add(student);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                students = null;
            }
            return students;
        }

        public async Task<Student> GetStudentAsync(string email)
        {
            Student student = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"SELECT * FROM [Students] WHERE [Email] = @Email";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email;

                    SqlDataReader dataReader = command.ExecuteReaderAsync().Result;

                    await dataReader.ReadAsync();

                    student = new Student
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        Password = dataReader["Password"].ToString()
                    };

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                student = null;
            }

            return student;
        }

        public async Task<List<StudentInCourse>> GetMyCoursesAsync(int id)
        {
            List<StudentInCourse> courses = new List<StudentInCourse>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"
                        SELECT
                            [Students].[Id] AS [StudentId],
                            [Students].[Name],
                            [Students].[Surname],
                            [Courses].[Id] AS [CourseId],
                            [Courses].[Name] AS [CourseName],
                            [Courses].[State] AS [CourseState]
                        FROM[Students]
                        INNER JOIN[CourseStudents] ON [Students].[Id] = [CourseStudents].[StudentId]
                        INNER JOIN[Courses] ON [Courses].[Id] = [CourseStudents].[CourseId]
                        WHERE [Students].[Id] = @id; ";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        StudentInCourse student = new StudentInCourse();
                        student.StudentId = int.Parse(dataReader["StudentId"].ToString());
                        student.StudentName = dataReader["Name"].ToString();
                        student.StudentSurname = dataReader["Surname"].ToString();
                        student.CourseId = int.Parse(dataReader["CourseId"].ToString());
                        student.CourseName = dataReader["CourseName"].ToString();
                        student.CourseState = Enum.TryParse(dataReader["CourseState"].ToString(), out State cos) ? cos : student.CourseState;

                        courses.Add(student);
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

        public async Task<bool> AddCourseRateAsync(CourseRate newRate)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [CourseRates] ([CourseId], [Description], [ProgramRate], [TrainerRate], [ToolsRate]) VALUES (@CourseId, @Description, @ProgramRate, @TrainerRate, @ToolsRate)";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = newRate.CourseId;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = newRate.Description;
                    command.Parameters.Add("@ProgramRate", SqlDbType.Float, 8).Value = newRate.ProgramRate;
                    command.Parameters.Add("@TrainerRate", SqlDbType.Float, 8).Value = newRate.TrainerRate;
                    command.Parameters.Add("@ToolsRate", SqlDbType.Float, 8).Value = newRate.ToolsRate;
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    success = rowsAffected == 1;

                    //if(success)
                    //{
                    //    SendEmail(newRate);
                    //}

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

        //private void SendEmail(CourseRate newRate)
        //{
        //    string smtpServer;
        //    try
        //    {
        //        using (SmtpClient client = new SmtpClient(smtpServer))
        //        {
        //            string to = null;
                    
        //            MailMessage mail = new MailMessage();
        //            mail.From = new MailAddress("courseRate@course.com");
        //            mail.To.Add(to);

        //            mail.Subject = "new rating from student";
        //            mail.Body = $"CourseId:  {newRate.CourseId}  \nComments on course:  {newRate.Description}\n Program Rate: {newRate.ProgramRate}\n Trainer Rate {newRate.TrainerRate}\n Tools Rate: {newRate.ToolsRate} ";
        //            mail.IsBodyHtml = true;

        //            client.Send(mail);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
