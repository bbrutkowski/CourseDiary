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
        [Route("homework")]
        public async Task<bool> AddHomeworkResult([FromBody] HomeworkResults result)
        {
            return await _courseService.AddHomeworkResult(result);
        }

        [HttpPost]
        [Route("test")]
        public async Task<bool> AddTestResult([FromBody] TestResults testResult)
        {
            return await _courseService.AddTestResult(testResult);
        }
        
        [HttpPost]
        [Route("presence")]
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

        [HttpGet]
        [Route("{id}/presence")]
        public async Task<List<StudentPresence>> GetCourseStudentPresence([FromUri] int id)
        {
            return await _courseService.GetCourseStudentPresence(id);
        }

        [HttpGet]
        [Route("{id}/homework")]
        public async Task<List<HomeworkResults>> GetCourseHomeworkResults([FromUri] int id)
        {
            return await _courseService.GetCourseHomeworkResults(id);
        }

        [HttpGet]
        [Route("{id}test")]
        public async Task<List<TestResults>> GetCourseTestResults([FromUri] int id)
        {
            return await _courseService.GetCourseTestResults(id);
        }

        [HttpPost]
        [Route("results")]
        public async Task<bool> AddCourseResults([FromBody] CourseResults courseResults)
        {
            return await _courseService.AddCourseResults(courseResults);
        }

        [HttpGet]
        [Route("results/{courseId}")]
        public async Task<CourseResults> GetCourseResults([FromUri] int courseId)
        {
            return await _courseService.GetCourseResults(courseId);
        }
    }
}
