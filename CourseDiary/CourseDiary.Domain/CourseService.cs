using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CourseDiary.Domain
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<bool> Add(Course course)
        {
            if(course.Students.Count < 5 || course.Students.Count > 20)
            {
                throw new Exception("The amount of students must be between 5 and 20");
            }
            else if(course.Trainer == null)
            {
                throw new Exception("Trainer must be assaigned");
            }
            DateTime dt = DateTime.ParseExact(course.BeginDate.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            course.BeginDate = dt;
            course.State = State.Open;
            return await _courseRepository.Add(course);
        }

        public async Task<List<Course>> GetActiveCoursesAsync(List<Course> courses)
        {
            courses = await _courseRepository.GetAllCoursesAsync();
            List<Course> activeCourses = new List<Course>();

            activeCourses = courses
                .Where(x=>x.State == State.Open)
                .ToList();

            //todo: to trzeba dorzucić do clienta
            //foreach(Course course in activeCourses)
            //{
            //    Console.WriteLine($"Course name:  {course.Name} \n Trainer:  {course.Trainer.Name} {course.Trainer.Surname} \n Begin date: {course.BeginDate} \n");
            //}

            return activeCourses;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }        

    }
}
