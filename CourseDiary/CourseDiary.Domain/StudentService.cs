using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain
{
    public interface IStudentService
    {
        Task AddStudentAsync(Student student);
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

        public bool CheckStudentCredentials(string email, string Password)
        {
            Student student = _studentRepository.GetStudentAsync(email).Result;
            var success = student != null && student.Password == Password;
            return success;
        }
    }
}
