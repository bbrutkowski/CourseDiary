using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.AdminClient
{
    public class CourseDiaryActionHelper
    {
        private readonly StudentService _studentService;
        private readonly CliHelper _cliHelper;

        public CourseDiaryActionHelper()
        {
            var studentRepository = new StudentRepository();

            _studentService = new StudentService(studentRepository);
            _cliHelper = new CliHelper();
        }
    }
}
