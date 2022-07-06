using System.Net.Http;

namespace CourseDiary.TrainerClient.Clients
{
    public class CourseWebApiClient
    {
        private readonly HttpClient _client;

        public CourseWebApiClient()
        {
            _client = new HttpClient();
        }

    }
}
