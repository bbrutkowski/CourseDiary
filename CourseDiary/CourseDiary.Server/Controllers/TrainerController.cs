using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
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
        [Route("{id}")]
        public async Task<Trainer> GetTrainer(int id)
        {
            return await _trainerService.GetTrainer(id);
        }

        [HttpPost]
        [Route("credentials")]
        public async Task<bool> CheckTrainerCredentials([FromBody] string email, [FromBody] string password)
        {
            return await _trainerService.CheckTrainerCredentials(email, password);
        }   
    }
}
