using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
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
    }
}
