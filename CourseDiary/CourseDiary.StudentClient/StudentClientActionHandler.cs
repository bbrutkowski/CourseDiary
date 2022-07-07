using CourseDiary.StudentClient.Clients;
using CourseDiary.StudentClient.Models;
using System;
using System.Collections.Generic;

namespace CourseDiary.StudentClient
{
    public class StudentClientActionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly StudentClientLoginHandler _studentClientLoginHandler;
        private readonly StudentViewWebApiClient _studentViewWebApiClient;
        private string _loggedUser;

        public StudentClientActionHandler()
        {
            _cliHelper = new CliHelper();
            _studentClientLoginHandler = new StudentClientLoginHandler();
            _studentViewWebApiClient = new StudentViewWebApiClient();
        }

        public bool ProgramLoop(string loggedUser)
        {
            _loggedUser = loggedUser;
            bool exit = false;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("1. Show my course");

                switch (operation)
                {
                    case "1":
                        ShowMyCourses(loggedUser);
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
                        _studentClientLoginHandler.LoginLoop();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }
            return exit;
        }

        private async void ShowMyCourses(string loggedUser)
        {
            List<StudentInCourse> allActiveCourses = await _studentViewWebApiClient.ShowMyCoursesAsync(loggedUser);

            foreach (StudentInCourse course in allActiveCourses)
            {
                Console.WriteLine($"Course name:  {course.CourseName} - State:  {course.CourseState}");
            }
        }
    }
}
