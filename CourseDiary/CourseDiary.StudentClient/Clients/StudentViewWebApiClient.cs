using CourseDiary.StudentClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.StudentClient.Clients
{
    public class StudentViewWebApiClient
    {
        private readonly HttpClient _client;

        public StudentViewWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<List<StudentInCourse>> ShowMyCoursesAsync(string email)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/student/myCourses");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<StudentInCourse>();
                }

                return JsonConvert.DeserializeObject<List<StudentInCourse>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<StudentInCourse>();
            }
        }

        public async Task<bool> CheckStudentCredentialsAsync(string email, string password)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new StudentCredentials { Login = email, Password = password }), Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/student/credentials", content);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return false;
                }

                return bool.Parse(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }
        }
    }
}
