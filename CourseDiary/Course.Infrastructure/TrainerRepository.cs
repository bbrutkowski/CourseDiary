using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CourseDiary.Infrastructure
{
    public class TrainerRepository : ITrainerRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CourseDiaryDBConnectionString"].ConnectionString;

        public async Task<bool> AddTrainer(Trainer trainer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [Trainers] ([Name], [Surname], [Email], [Password], [DateOfBirth]) " +
                        "VALUES (@Name, @Surname, @Email, @Password, @DateOfBirth)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = trainer.Name;
                    command.Parameters.Add("@Surname", SqlDbType.VarChar, 255).Value = trainer.Surname;
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = trainer.Email;
                    command.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = trainer.Password;
                    command.Parameters.Add("@DateOfBirth", SqlDbType.VarChar, 255).Value = trainer.DateOfBirth;

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

        public Trainer GetTrainer(string email)
        {
            Trainer trainer = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = $"SELECT * FROM [Trainers] WHERE [Email] = @Email";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email;

                    SqlDataReader dataReader = command.ExecuteReader();

                    dataReader.Read();

                    trainer = new Trainer
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
                trainer = null;
            }

            return trainer;
        }
    }
}
