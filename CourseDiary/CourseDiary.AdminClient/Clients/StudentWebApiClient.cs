using CourseDiary.AdminClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseDiary.AdminClient.Clients
{
    public class StudentWebApiClient
    {
        private readonly HttpClient _client;

        public StudentWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> AddStudent(Student student)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(student), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/student", content);

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

        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                var responseBody = await _client.GetAsync(@"http://localhost:9000/api/v1/student");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Student>();
                }

                return JsonConvert.DeserializeObject<List<Student>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Student>();
            }
        }
    }
}
