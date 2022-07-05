using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
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

        [HttpPost]
        [Route("")]
        public async void AddCourse([FromBody] Course course)
        {
            await _courseService.Add(course);
        }
    }
}
