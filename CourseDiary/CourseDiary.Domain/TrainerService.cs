using CourseDiary.Domain.Interfaces;
using CourseDiary.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseDiary.Domain
{
    public interface ITrainerService
    {
        Task<bool> AddTrainer(Trainer trainer);
        Task<List<Trainer>> GetAllTrainers();
        Task<bool> CheckTrainerCredentials(string email, string trainerPassword);
        Task<Trainer> GetTrainer(int id);
    }

    public class TrainerService : ITrainerService
    {
        private ITrainerRepository _trainerRepository;

        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public async Task<List<Trainer>> GetAllTrainers()
        {
            return await _trainerRepository.GetAllTrainers();
        }
        public async Task<bool> AddTrainer(Trainer trainer)
        {
            return await _trainerRepository.AddTrainer(trainer);
        }

        public async Task<bool> CheckTrainerCredentials(string email, string trainerPassword)
        {
            Trainer trainer = await _trainerRepository.GetTrainerByEmail(email);
            var success = trainer != null && trainer.Password == trainerPassword;
            return success;
        }

        public async Task<Trainer> GetTrainer(int id)
        {
            return await _trainerRepository.GetTrainer(id);
        }

        public async Task<Trainer> GetTrainer(string trainerMail)
        {
            return await _trainerRepository.GetTrainerByEmail(trainerMail);
        }
    }
}
