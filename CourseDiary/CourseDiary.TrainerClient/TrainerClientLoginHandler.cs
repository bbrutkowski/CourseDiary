using CourseDiary.TrainerClient.Clients;
using System;

namespace CourseDiary.TrainerClient
{
    public class TrainerClientLoginHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerWebApiClient _trainerWebApiClient;
        public string LoginLoop()
        {
            bool exit = false;
            string email = null;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("Choose action [Login, Exit]");
                switch (operation)
                {
                    case "Login":
                        email = LoginUser();
                        exit = !string.IsNullOrEmpty(email);
                        break;
                    case "Exit":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }

            return email;
        }

        private string LoginUser()
        {
            string email = _cliHelper.GetStringFromUser("Add email");
            string password = _cliHelper.GetStringFromUser("Add pasword");

            bool correctCredentials = _trainerWebApiClient.CheckTrainerCredentials(email, password).Result;

            if (correctCredentials)
            {
                Console.WriteLine($"Logon successful. Hello {email}");
            }
            else
            {
                Console.WriteLine($"Login unsuccesful. Try again...");
                return null;
            }

            return email;
        }
    }
}
