using CourseDiary.TrainerClient.Clients;
using CourseDiary.TrainerClient.Models;
using System;

namespace CourseDiary.TrainerClient
{
    public class CourseManager
    {
        private CourseWebApiClient _courseWebApiClient;
        private CliHelper _cliHelper;
        private Trainer _loggedTrainer = null;

        public CourseManager()
        {
            _courseWebApiClient = new CourseWebApiClient();
            _cliHelper = new CliHelper();
            _loggedTrainer = new Trainer();
        }

        public void SetLoggedUser(Trainer loggedTrainer)
        {
            _loggedTrainer = loggedTrainer;
        }
        public async void SelectCourse()
        {
            var allCourses = await _courseWebApiClient.GetAllCourses();
            foreach (var course in allCourses)
            {
                Console.WriteLine($"{course.Name}");
            }
            var selectCourse = _cliHelper.GetStringFromUser("Enter the name of course you want to assign");
            Course newCourse = new Course
            {
                Name = selectCourse,
                Trainer = _loggedTrainer,
            };
        }
    }
}
