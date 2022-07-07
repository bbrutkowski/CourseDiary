using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseDiary.Server.Controllers
{
    [RoutePrefix("api/v1/course")]
    public class CourseController : ApiController
    {
        private CourseService _courseService;

        public CourseController()
        {
            CourseRepository courseRepository = new CourseRepository();
            _courseService = new CourseService(courseRepository);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseService.GetAllCourses();
        }

        [HttpPost]
        [Route("")]
        public async void AddCourse([FromBody] Course course)
        {
            await _courseService.Add(course);
        }

        [HttpPost]
        [Route("AddHomeWworkResult")]
        public async Task<bool> AddHomeworkResult([FromBody] HomeworkResults result)
        {
            return await _courseService.AddHomeworkResult(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<Course>> GetAllActiveCourses()
        {
            return await _courseService.GetActiveCoursesAsync();
        }
    }
}
