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
        [Route("addhomework")]
        public async Task<bool> AddHomeworkResult([FromBody] HomeworkResults result)
        {
            return await _courseService.AddHomeworkResult(result);
        }

        [HttpPost]
        [Route("closeCourse")]
        public async Task<bool> CloseCourse([FromBody]Course course)
        {
            return await _courseService.CloseCourse(course);
        }

        [HttpPost]
        [Route("AddTestResult")]
        public async Task<bool> AddTestResult([FromBody] TestResults testResult)
        {
            return await _courseService.AddTestResult(testResult);
        }

        [Route("addpresence")]
        public async Task<bool> AddPresence([FromBody] StudentPresence presence)
        {
            return await _courseService.AddPresence(presence);
        }
        
        [HttpGet]
        [Route("active")]
        public async Task<List<Course>> GetAllActiveCourses()
        {
            return await _courseService.GetActiveCoursesAsync();
        }
    }
}
