using CourseDiary.TrainerClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseDiary.TrainerClient.Clients
{
    public class CourseWebApiClient
    {
        private readonly HttpClient _client;

        public CourseWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<List<Course>> GetAllCourses()
        {
            try
            {
                var responseBody = await _client.GetAsync(@"http://localhost:9000/api/v1/course");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Course>();
                }

                return JsonConvert.DeserializeObject<List<Course>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Course>();
            }
        }
    }
}
