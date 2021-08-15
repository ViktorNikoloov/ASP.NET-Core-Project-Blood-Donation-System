namespace BloodDonation.Services.Data.Administator
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Mapping;
    using BloodDonation.Web.ViewModels.Administration.Dashboard;

    using static BloodDonation.Common.GlobalConstants;

    public class AdministratorService : IAdministratorService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IRepository<QuestionAnswer> questionsRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmetsRepository;

        public AdministratorService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IRepository<QuestionAnswer> questionsRepository,
            IDeletableEntityRepository<Appointment> appointmetsRepository)
        {
            this.usersRepository = usersRepository;
            this.questionsRepository = questionsRepository;
            this.appointmetsRepository = appointmetsRepository;
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
                        Street = new Street
                        {
                            Name = "Липсва",
                        },
                    },
                },
                DonationCount = 0,
                ImageUrl = DefaulPicturetUrl, // Defaulf picture
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

        public AppointmentViewModel GetAppoinmentAllInfo(int id)
        {
            var currentAppointment = this.appointmetsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return new AppointmentViewModel
            {
                Id = id,
                RecipientFullName = $"{currentAppointment.Recipient.FirstName} {currentAppointment.Recipient.MiddleName} {currentAppointment.Recipient.LastName}",
                StartDate = currentAppointment.StartDate,
                DeadLine = currentAppointment.DeadLine,
                BloodType = currentAppointment.Recipient.BloodType,
                BloodBankCount = currentAppointment.BloodBankCount,
                HospitalName = currentAppointment.Hospital.HospitalName,
                HospitalWard = currentAppointment.Hospital.HospitalWardName,
                HospitalCity = currentAppointment.Hospital.Town.Name,
                SendingAddressInfo = currentAppointment.SendingAddressInfo,
                AdditionalInfo = currentAppointment.AdditionalInfo,
                ImageUrl = currentAppointment.Recipient.ImageUrl,
            };
        }

        public async Task ApproveAppointmentAsync(int id)
        {
            var currAppointment = this.GetCurrentAppointment(id);
            currAppointment.IsApproved = true;

            await this.appointmetsRepository.SaveChangesAsync();
        }

        public async Task RejectAppointmentAsync(int id)
        {
            var currAppointment = this.GetCurrentAppointment(id);

            this.appointmetsRepository.Delete(currAppointment);
            await this.appointmetsRepository.SaveChangesAsync();
        }

        private Appointment GetCurrentAppointment(int id)
        => this.appointmetsRepository.All().FirstOrDefault(x => x.Id == id);
    }
}
