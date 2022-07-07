using CourseDiary.TrainerClient.Clients;
using CourseDiary.TrainerClient.Models;
using System;
using System.Collections.Generic;

namespace CourseDiary.TrainerClient
{
    public class TrainerClientActionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerClientLoginHandler _trainerClientLoginHandler;
        private readonly CourseWebApiClient _courseWebApiClient;
        private string _loggedUser = null;

        public TrainerClientActionHandler()
        {
            _cliHelper = new CliHelper();
            _trainerClientLoginHandler = new TrainerClientLoginHandler();
            _courseWebApiClient = new CourseWebApiClient();
        }
        public bool ProgramLoop(string loggedUser)
        {
            _loggedUser = loggedUser;
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

        private async void SelectActiveCourse()
        {
            var allCourses = await _courseWebApiClient.GetAllCourses();
            foreach (var course in allCourses)
            {
                Console.WriteLine($"{course.Name}");
            }
            var selectCourse = _cliHelper.GetStringFromUser("Enter name of course you want to assign");
            MenuForActivCourse(selectCourse);
        }

        private void MenuForActivCourse(string selectCourse)
        {
            var switchOption = _cliHelper.GetStringFromUser("What do you want to do?");
            var exit = false;
            while (!exit)
            {
                switch (switchOption)
                {
                    case "1":
                        ShowAcvivCourse();
                        break;
                    case "2":
                        AddCoursePresence();
                        break;
                    case "3":
                        AddHomeworkResults();
                        break;                       
                    case "4":
                        AddTestResultsAsync();
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
                        ProgramLoop(_loggedUser);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unsupported option");
                        break;
                }

            }
        }

        private async void AddTestResultsAsync()
        {
            TestResults newTestResult = new TestResults()
            {
                TestName = _cliHelper.GetStringFromUser("Enter test name"),
                FinishDate = _cliHelper.GetDateFromUser("Enter end date of test"),
                StudentId = _cliHelper.GetIntFromUser("Enter student id"),
                Result = _cliHelper.GetIntFromUser("Enter result of test (0-100)"),
            };
            if (newTestResult.Result > 100)
            {
                Console.WriteLine("Value cannot be higher than 100");
            }
            else
            {
                await _courseWebApiClient.AddTestResult(newTestResult);
            }
        }

        private async void AddHomeworkResults()
        {
            HomeworkResults newResults = new HomeworkResults()
            {
                HomeworkName = _cliHelper.GetStringFromUser("Enter homework name"),
                FinishDate = _cliHelper.GetDateFromUser("Enter homework deadline"),
                StudentId = _cliHelper.GetIntFromUser("Enter student Id"),
                Result = float.Parse(_cliHelper.GetStringFromUser("Enter homework result (0-200)")),
            };
            if (newResults.Result > 200)
            {
                Console.WriteLine("Value cannot be higher than 200");
            }
            else
            {
                await _courseWebApiClient.AddHomeworkResult(newResults);
            }

        }

        private void AddCoursePresence()
        {
            
        }

        private async void ShowAcvivCourse()
        {
            List<Course> getActiveCourses = new List<Course>();
            List<Course> allActiveCourses = new List<Course>();
            allActiveCourses = await _courseWebApiClient.GetAllActiveCourses(allActiveCourses);

            foreach(Course course in allActiveCourses)
            {
                Console.WriteLine($"Course name:  {course.Name} \n Trainer:  {course.Trainer.Name} {course.Trainer.Surname} \n Begin date: {course.BeginDate} \n");
            }
        }
    }
}