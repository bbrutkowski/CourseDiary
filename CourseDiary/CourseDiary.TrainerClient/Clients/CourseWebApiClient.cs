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
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course");


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

        public async Task<List<Course>> GetAllActiveCourses()
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course/active");

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

        public async Task<bool> AddHomeworkResult(HomeworkResults results)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(results), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/addhomework", content);

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

        public async Task<bool> AddPresence(StudentPresence presence)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(presence), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/addpresence", content);

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
