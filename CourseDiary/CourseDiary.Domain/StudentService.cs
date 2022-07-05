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
        private IStudentRepository _studentRepository;
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
    }
}
