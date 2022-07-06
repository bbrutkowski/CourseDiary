using CourseDiary.TrainerClient.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseDiary.TrainerClient.Clients
{
    public class TrainerWebApiClient
    {
        private readonly HttpClient _client;

        public TrainerWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> CheckTrainerCredentials(string login, string password)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new TrainerCredentials { Login = login, Password = password }), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/trainer/credentials", content);

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
