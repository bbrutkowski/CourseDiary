using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
using CourseDiary.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseDiary.Server.Controllers
{
    [RoutePrefix("api/v1/trainer")]
    public class TrainerController : ApiController
    {
        private TrainerService _trainerService;

        public TrainerController()
        {
            TrainerRepository trainerRepository = new TrainerRepository();
            _trainerService = new TrainerService(trainerRepository);
        }

        [HttpPost]
        [Route("")]
        public async void AddTrainer([FromBody] Trainer trainer)
        {
            await _trainerService.AddTrainer(trainer);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<Trainer>> GetAllTrainers()
        {
            return await _trainerService.GetAllTrainers();
        }

        [HttpGet]
        [Route("byId/{id}")]
        public async Task<Trainer> GetTrainer(int id)
        {
            return await _trainerService.GetTrainer(id);
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<Trainer> GetTrainer(string email)
        {
            return await _trainerService.GetTrainer(email);
        }

        [HttpPost]
        [Route("credentials")]
        public async Task<bool> CheckTrainerCredentials([FromBody] TrainerCredentials credentials)
        {
            return await _trainerService.CheckTrainerCredentials(credentials.Email, credentials.Password);
        }   
    }
}
