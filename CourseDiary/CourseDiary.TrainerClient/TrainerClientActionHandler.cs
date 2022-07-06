using CourseDiary.TrainerClient.Clients;
using CourseDiary.TrainerClient.Models;
using System;

namespace CourseDiary.TrainerClient
{
    internal class TrainerClientActionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerClientLoginHandler _trainerClientLoginHandler;
        private readonly CourseWebApiClient _courseWebApiClient;
        private Trainer _loggedTrainer;

        public TrainerClientActionHandler()
        {
            _cliHelper = new CliHelper();
            _courseWebApiClient = new CourseWebApiClient();
            _loggedTrainer = new Trainer();
        }

        public bool ProgramLoop(Trainer loggedTrainer)
        {
            loggedTrainer = _loggedTrainer;
            bool exit = false;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("");

                switch (operation)
                {
                    case "1":
                        SelectActiveCourse();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10":
                        _trainerClientLoginHandler.LoginLoop();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }

            return exit;
        }

        public async void SelectActiveCourse()
        {
            var allCourses = await _courseWebApiClient.GetAllCourses();
            foreach (var course in allCourses)
            {
                Console.WriteLine($"{course.Name}");
            }
            var selectCourse = _cliHelper.GetStringFromUser("Enter name of course you want to assign");
            MenuForActivCourse(selectCourse);
        }

        private void MenuForActivCourse(string selectedCourse)
        {
            var switchOption = _cliHelper.GetStringFromUser("What do you want to do?");
            var exit = false;
            while (!exit)
            {
                switch (switchOption)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10":
                        ProgramLoop(_loggedTrainer);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unsupported option");
                        break;
                }

            }
        }
    }
}
