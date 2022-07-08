using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<bool> Add(Course course);
        Task<List<Course>> GetAllCoursesAsync();
        Task<bool> AddHomeworkResult(HomeworkResults result);
        Task<bool> AddTestResult(TestResults testResult);
        Task<bool> AddPresence(StudentPresence presence);
        Task<List<StudentPresence>> GetCourseStudentPresence(int courseId);
        Task<List<HomeworkResults>> GetCourseHomeworkResults(int courseId);
        Task<List<TestResults>> GetCourseTestResults(int courseId);
    }
}
