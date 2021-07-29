namespace BloodDonation.Services.Data.Administator
{
    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdministratorService : IAdministratorService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<QuestionAnswer> questionsRepository;

        public AdministratorService(IDeletableEntityRepository<ApplicationUser> usersRepository, IDeletableEntityRepository<QuestionAnswer> questionsRepository)
        {
            this.usersRepository = usersRepository;
            this.questionsRepository = questionsRepository;
        }

        public async Task AddDonorAsync(string id)
        {
            var user = this.usersRepository.All().FirstOrDefault(u => u.Id == id);

            var dogsitter = new Donor();
            user.Donor = dogsitter;
            user.Donor.PhoneNumber = user.PhoneNumber;

            await this.usersRepository.SaveChangesAsync();
        }

        public T ApplicantDetailsById<T>(string id)
        {
            var user = this.usersRepository.All()
                 .Where(u => u.Id == id)
                 .To<T>().FirstOrDefault();

            return user;
        }

        public async Task RemoveQuestionsAnswersFromUserAsync(string userId)
        {
            var questionAnswers = this.questionsRepository.All().Where(qa => qa.UserId == userId);

            foreach (var questionAnswer in questionAnswers)
            {
                this.questionsRepository.Delete(questionAnswer);
            }

            await this.questionsRepository.SaveChangesAsync();
        }
    }
}
