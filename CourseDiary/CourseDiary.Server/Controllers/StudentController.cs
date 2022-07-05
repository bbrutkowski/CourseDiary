using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
using System.Web.Http;

namespace CourseDiary.Server.Controllers
{
    [RoutePrefix("api/v1/student")]
    public class StudentController : ApiController
    {
        private StudentService _studentService;

        public StudentController()
        {
            StudentRepository studentRepository = new StudentRepository();
            _studentService = new StudentService(studentRepository);
        }

        [HttpPost]
        [Route("")]
        public async void AddStudent([FromBody] Student student)
        {
            await _studentService.AddStudentAsync(student);
        }
    }
}
