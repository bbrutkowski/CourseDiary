using CourseDiary.TrainerClient.Clients;
using CourseDiary.TrainerClient.Models;
using System;

namespace CourseDiary.TrainerClient
{
    public class TrainerClientLoginHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerWebApiClient _trainerWebApiClient;

        public TrainerClientLoginHandler()
        {
            _cliHelper = new CliHelper();
            _trainerWebApiClient = new TrainerWebApiClient();
        }
        public Trainer LoginLoop()
        {
            bool exit = false;
            Trainer trainer = null;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("[Login] Choose action [Login, Exit]");
                switch (operation)
                {
                    case "Login":
                        trainer = LoginUser();
                        if (trainer != null)
                        {
                            exit = true;                          
                        }
                        break;
                    case "Exit":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }

            return trainer;
        }

        private Trainer LoginUser()
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

            var newUser = _trainerWebApiClient.GetTrainerByEmail(email).Result;

            return newUser;
        }
    }
}
