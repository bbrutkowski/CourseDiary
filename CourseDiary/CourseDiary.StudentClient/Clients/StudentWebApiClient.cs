using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.StudentClient.Clients
{
    public class StudentWebApiClient
    {
        private readonly HttpClient _client;

        public StudentWebApiClient()
        {
            _client = new HttpClient();
        }


    }
}
