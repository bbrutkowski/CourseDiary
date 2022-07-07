using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet]
        [Route("")]
        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentService.GetAllStudentsAsync();
        }

        [HttpPost]
        [Route("credentials")]
        public async Task<bool> CheckStudentCredentials([FromBody] StudentCredentials studentCredentials)
        {
            return await _studentService.CheckStudentCredentialsAsync(studentCredentials.Login, studentCredentials.Password);
        }

        [HttpGet]
        [Route("myCourses")]
        public async Task<List<StudentInCourse>> ShowMyCoursesAsync([FromBody] string email)
        {
            return await _studentService.ShowMyCoursesAsync(email);
        }
    }
}
