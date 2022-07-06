using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                    SqlDataReader dataReader = command.ExecuteReaderAsync().Result;

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

                    string commandText = $"SELECT * FROM [Trainers] WHERE [Email] = @Email";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email;

                    SqlDataReader dataReader = command.ExecuteReaderAsync().Result;

                    await dataReader.ReadAsync();

                    student = new Student
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        Password = dataReader["UserPassword"].ToString()
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

    }
}
