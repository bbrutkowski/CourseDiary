using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task AddStudentAsync(Student student);
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentAsync(string email);
        Task<List<StudentInCourse>> GetMyCoursesAsync(int id);
        Task<bool> AddCourseRateAsync(CourseRate newRate);
        Task<List<Student>> GetAllStudentsInCourseAsync(int courseId);
    }
}
