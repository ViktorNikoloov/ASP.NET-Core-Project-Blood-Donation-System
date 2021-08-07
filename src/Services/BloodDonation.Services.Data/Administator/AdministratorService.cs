namespace BloodDonation.Services.Data.Administator
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Mapping;

    public class AdministratorService : IAdministratorService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<QuestionAnswer> questionsRepository;

        public AdministratorService(IDeletableEntityRepository<ApplicationUser> usersRepository, IRepository<QuestionAnswer> questionsRepository)
        {
            this.usersRepository = usersRepository;
            this.questionsRepository = questionsRepository;
        }

        public async Task AddDonorAsync(string id)
        {
            var user = this.usersRepository.All().FirstOrDefault(u => u.Id == id);

            var donor = new Donor
            {
                FirstName = "Липсва",
                MiddleName = "Липсва",
                LastName = "Липсва",
                Gender = Gender.Unknown,
                BloodType = BloodType.Unknown,
                Address = new Address
                {
                    Town = new Town
                    {
                        Name = "Липсва",
                        PostCode = 0,
                        Street = new Street
                        {
                            Name = "Липсва",
                        },
                    },
                },
                DonationCount = 0,
                ImageUrl = "https://res.cloudinary.com/dvvbab0fs/image/upload/v1627247340/faoqwxe5cyxcadm0moks.jpg", // Defaulf picture
                PhoneNumber = user.PhoneNumber,
            };
            user.Donor = donor;

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
