using CourseDiary.Domain;
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
            _cliHelper = new CliHelper();
        }

        public string LoginLoop()
        {
            bool exit = false;
            string loggedUser = null;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("[Login] Choose action [Login, Exit]");
                switch (operation)
                {
                    case "Login":
                        loggedUser = LoginUser();
                        exit = !string.IsNullOrEmpty(loggedUser);
                        break;
                    case "Exit":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }

            return loggedUser;
        }

        private string LoginUser()
        {
            string username = _cliHelper.GetStringFromUser("Add username");
            string password = _cliHelper.GetStringFromUser("Add pasword");

            bool correctCredentials = _trainerService.CheckUserCredentials(username, password);

            if (correctCredentials)
            {
                Console.WriteLine($"Logon successful. Hello {username}");
            }
            else
            {
                Console.WriteLine($"Login unsuccesful. Try again...");
                return null;
            }

            return username;
        }
    }
}
