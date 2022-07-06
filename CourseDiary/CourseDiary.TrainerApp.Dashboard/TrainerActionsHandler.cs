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
    internal class TrainerActionsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly CourseService _courseService;
        private Trainer _loggedTrainer =  null;
        private LoginHandler _loginHandler;

        public TrainerActionsHandler()
        {
            _cliHelper = new CliHelper();
            _courseService = new CourseService(new CourseRepository());
            _loggedTrainer = new Trainer();
            _loginHandler = new LoginHandler();
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
                        SelectCourse();
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
                        _loginHandler.LoginLoop();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }

            return exit;
        }

        public async void SelectCourse()
        {
            var allCourses = await _courseService.GetAllCourses();
            foreach (var course in allCourses)
            {
                Console.WriteLine($"{course.Name}");
            }
            var selectCourse = _cliHelper.GetStringFromUser("Enter name of course you want to assign");
            Course newCourse = new Course()
            {
                Name = selectCourse,
                Trainer = _loggedTrainer.Id,
            };
            var isUpdated = await _courseService.UpdateCourse(newCourse);
            if (isUpdated == true)
            {
                Console.WriteLine("Successfully assinging trainer to course");
            }
        }
    }
}
