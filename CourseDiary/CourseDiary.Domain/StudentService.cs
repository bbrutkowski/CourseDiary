using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseDiary.Domain
{
    public interface IStudentService
    {
        Task AddStudentAsync(Student student);
        Task<List<StudentInCourse>> ShowMyCoursesAsync(string email);
        Task<bool> CheckStudentCredentialsAsync(string email, string password);
        Task<List<Student>> GetAllStudentsAsync();
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _studentRepository.AddStudentAsync(student);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<bool> CheckStudentCredentialsAsync(string email, string password)
        {
            Student student = await _studentRepository.GetStudentAsync(email);
            var success = student != null && student.Password == password;
            return success;
        }

        public async Task<List<StudentInCourse>> ShowMyCoursesAsync(string email)
        {
            var student = await _studentRepository.GetStudentAsync(email);

            return await _studentRepository.GetMyCoursesAsync(student.Id);
        }
    }
}
