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
                string operation = _cliHelper.GetStringFromUser("Type number of operation: 1. Show my courses 2. Rate Your course 3. Logout");

                switch (operation)
                {
                    case "1":
                        ShowMyCourses(loggedUser);
                        break;
                    case "2":
                        RateYourCourse(loggedUser);
                        break;
                    case "3":
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

        private async void RateYourCourse(string loggedUser)
        {
            List<StudentInCourse> allCoursesForThisStudent = _studentViewWebApiClient.ShowMyCoursesAsync(loggedUser).Result;
            List<int> courseIds = new List<int>();

            if (allCoursesForThisStudent.Count == 0)
            {
                Console.WriteLine("You don't have course to rate");
            }
            else
            {
                
                foreach (StudentInCourse course in allCoursesForThisStudent)
                {
                    if (course.CourseState == State.Closed)
                    {
                        Console.WriteLine($"Course Id: {course.CourseId} -  Course name:  {course.CourseName} - State:  {course.CourseState}");
                        courseIds.Add(course.CourseId);
                    }
                }

                if(courseIds.Count == 0)
                {
                    Console.WriteLine("You don't have course to rate");
                }
                else
                {
                    int id;
                    do
                    {
                        id = _cliHelper.GetIntFromUser("Choose Id of course You want to rate: ");
                        if (!courseIds.Contains(id))
                        {
                            Console.WriteLine("You can't rate course with that id");
                        }
                    } while (!courseIds.Contains(id));

                    CourseRate newRate = new CourseRate()
                    {
                        CourseId = id,
                        Description = _cliHelper.GetStringFromUser("Rate this course in few words"),
                        ProgramRate = _cliHelper.GetRateFromUser("Rate course program (scale 1-10)"),
                        TrainerRate = _cliHelper.GetRateFromUser("Rate trainer (scale 1-10)"),
                        ToolsRate = _cliHelper.GetRateFromUser("Rate tools You used during the course (scale 1-10)")
                    };
                    var success = await _studentViewWebApiClient.AddCourseRateAsync(newRate);
                    string message = success
                        ? "rate added successfully"
                        : "error when added rate";
                    Console.WriteLine(message);
                }
            }
        }

        private async void ShowMyCourses(string loggedUser)
        {
            List<StudentInCourse> allCoursesForThisStudent = await _studentViewWebApiClient.ShowMyCoursesAsync(loggedUser);

            Console.WriteLine("Your courses: ");
            foreach (StudentInCourse course in allCoursesForThisStudent)
            {
                Console.WriteLine($"Course name:  {course.CourseName} - State:  {course.CourseState}");
            }
        }
    }
}
