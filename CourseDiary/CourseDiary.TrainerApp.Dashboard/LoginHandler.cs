using CourseDiary.Domain;
using CourseDiary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerApp.Dashboard
{
    internal class LoginHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerService _trainerService;

        public LoginHandler()
        {
            var trainerRepository = new TrainerRepository();

            _cliHelper = new CliHelper();
            _trainerService = new TrainerService(trainerRepository);
        }

        public string LoginLoop()
        {
            bool exit = false;
            string email = null;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("[Login] Choose action [Login, Exit]");
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

            bool correctCredentials = _trainerService.CheckTrainerCredentials(email, password);

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
