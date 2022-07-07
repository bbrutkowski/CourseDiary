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
        Task<bool> AddPresence(StudentPresence presence);
    }
}
