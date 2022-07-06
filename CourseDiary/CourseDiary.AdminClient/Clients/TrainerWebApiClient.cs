using CourseDiary.AdminClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseDiary.AdminClient.Clients
{
    public class TrainerWebApiClient
    {
        private readonly HttpClient _client;

        public TrainerWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> AddTrainer(Trainer trainer)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(trainer), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync(@"http://localhost:9000/api/v1/trainer", content);

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

        public async Task<List<Trainer>> GetAllTrainers()
        {
            try
            {
                var responseBody = await _client.GetAsync(@"http://localhost:9000/api/v1/trainer");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Trainer>();
                }

                return JsonConvert.DeserializeObject<List<Trainer>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Trainer>();
            }
        }

        public async Task<Trainer> GetTrainer(int id)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"http://localhost:9000/api/v1/trainer/{id}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new Trainer();
                }

                return JsonConvert.DeserializeObject<Trainer>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Trainer();
            }
        }
    }
}
