using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
using System;

namespace CourseDiary.TrainerApp.Dashboard
{
    public class CourseManager
    {
        private CourseService _courseService;
        private CliHelper _cliHelper;
        private Trainer _loggedTrainer = null;

        public CourseManager()
        {
            _courseService = new CourseService(new CourseRepository());
            _cliHelper = new CliHelper();
            _loggedTrainer = new Trainer();
        }

        public void SetLoggedUser(Trainer loggedTrainer)
        {
            _loggedTrainer = loggedTrainer;
        }
        public async void SelectCourse()
        {
            var allCourses = await _courseService.GetAllCourses();
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
            var isUpdated = await _courseService.UpdateCourse(newCourse);
            if (isUpdated == true)
            {
                Console.WriteLine("Successfully assigning trainer to course");
            }
        }
    }
}
