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
        Task<bool> AddPresence(StudentPresence presence);
    }
}
