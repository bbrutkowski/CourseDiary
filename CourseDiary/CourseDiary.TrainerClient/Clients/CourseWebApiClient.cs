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

        public async Task<bool> CloseTheCourse(Course course)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(course), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/closeCourse", content);

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

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/homework", content);

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
        
        public async Task<bool> AddTestResult(TestResults testResults)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(testResults), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/test", content);

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

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/presence", content);

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

        //public async Task<List<CourseResult>> GetCourseResults(int id)
        //{
        //    try
        //    {
        //        var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course/courseResults/{id}");

        //        var result = await responseBody.Content.ReadAsStringAsync();

        //        if (!responseBody.IsSuccessStatusCode)
        //        {
        //            return new List<CourseResult>();
        //        }

        //        return JsonConvert.DeserializeObject<List<CourseResult>>(result);
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Console.WriteLine("\nException Caught!");
        //        Console.WriteLine("Message :{0} ", e.Message);
        //        return new List<CourseResult>();
        //    }
        //}

        public async Task<List<StudentPresence>> GetCourseStudentPresence(int id)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course/{id}/presence");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<StudentPresence>();
                }

                return JsonConvert.DeserializeObject<List<StudentPresence>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<StudentPresence>();
            }
        }

        public async Task<List<HomeworkResults>> GetCourseHomeworkResults(int id)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course/{id}/homework");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<HomeworkResults>();
                }

                return JsonConvert.DeserializeObject<List<HomeworkResults>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<HomeworkResults>();
            }
        }

        public async Task<List<TestResults>> GetCourseTestResults(int id)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course/{id}/test");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<TestResults>();
                }

                return JsonConvert.DeserializeObject<List<TestResults>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<TestResults>();
            }
        }

        public async Task<bool> AddCourseResults(CourseResults courseResults)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(courseResults), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/course/results", content);

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

        public async Task<CourseResults> GetCourseResults(int courseId)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/course/results/{courseId}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new CourseResults();
                }

                return JsonConvert.DeserializeObject<CourseResults>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new CourseResults();
            }
        }
    }
}
