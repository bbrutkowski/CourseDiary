using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Interfaces
{
    public interface ICourseService
    {
        Task<bool> Add(Course course);
        Task<List<Course>> GetAllCourses();
        Task<bool> AddHomeworkResult(HomeworkResults result);
        Task<bool> AddTestResult(TestResults testResult);
        Task<bool> AddPresence(StudentPresence presence);
        Task<bool> CloseCourse(Course course);
        Task<List<StudentPresence>> GetCourseStudentPresence(int courseId);
        Task<List<HomeworkResults>> GetCourseHomeworkResults(int courseId);
        Task<List<TestResults>> GetCourseTestResults(int courseId);
        Task<bool> AddCourseResults(CourseResults courseResults);
        Task<CourseResults> GetCourseResults(int courseId);
    }
}
