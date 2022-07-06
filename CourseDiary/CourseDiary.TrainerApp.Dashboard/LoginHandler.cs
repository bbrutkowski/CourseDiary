using CourseDiary.Domain;
using CourseDiary.Domain.Models;
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
                            break;
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

            var newUser = _trainerService.GetTrainer(email);

            return newUser;

        }
    }
}
