namespace BloodDonation.Services.Data.Recipient
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Services.Mapping;
    using BloodDonation.Web.ViewModels.Recipient;

    public class RecipientsService : IRecipientsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipient> recipientRepository;
        private readonly IDeletableEntityRepository<Donor> donorRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;
        private readonly IRepository<AppointmetsDonors> appointmentsDonorsRepository;

        public RecipientsService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Recipient> recipientRepository,
            IDeletableEntityRepository<Donor> donorRepository,
            IDeletableEntityRepository<Appointment> appointmentRepository,
            IRepository<AppointmetsDonors> appointmentsDonorsRepository)
        {
            this.userRepository = userRepository;
            this.recipientRepository = recipientRepository;
            this.donorRepository = donorRepository;
            this.appointmentRepository = appointmentRepository;
            this.appointmentsDonorsRepository = appointmentsDonorsRepository;
        }

        public ApplicationUser GetRecipientApplicationUser(string recipietId)
        {
            var instance = this.userRepository
                .All().FirstOrDefault(u => u.Recipient.Id == recipietId);

            return instance;
        }

        public async Task CreateRecipientAsync(ApplicationUser user, string userId, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, Gender gender, BloodType bloodType, string imageUrl, string phoneNumber)
        {
            var recipient = new Recipient
            {
                UserId = userId,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Gender = gender,
                ImageUrl = imageUrl,
                PhoneNumber = phoneNumber,
                BloodType = bloodType,
                Address = new Address
                {
                    Town = new Town
                    {
                        Name = cityName,
                        PostCode = postCode,
                        Street = new Street
                        {
                            Name = streetName,
                        },
                    },
                },
            };

            await this.recipientRepository.AddAsync(recipient);
            await this.recipientRepository.SaveChangesAsync();
        }

        public GetRecipientByIdDto GetRecipientrById(string id)
        {
            return this.recipientRepository
                .AllAsNoTracking()
                .Where(r => r.UserId == id)
                .Select(x => new GetRecipientByIdDto
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    ImageUrl = x.ImageUrl,
                    CityName = x.Address.Town.Name,
                    PostCode = x.Address.Town.PostCode,
                    Gender = x.Gender,
                    BloodType = x.BloodType,
                    StreetName = x.Address.Town.Street.Name,
                    Email = x.User.Email,
                }) //.To<GetRecipientByIdDto>()
                .FirstOrDefault();
        }

        public async Task UpdateCurrentLoggedInRecipientInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, string imageUrl)
        {
            var recipient = this.recipientRepository.All().FirstOrDefault(o => o.UserId == id);

            recipient.FirstName = firstName;
            recipient.MiddleName = middleName;
            recipient.LastName = lastName;
            recipient.Address = new Address
            {
                Town = new Town
                {
                    Name = cityName,
                    PostCode = postCode,
                    Street = new Street
                    {
                        Name = streetName,
                    },
                },
            };
            recipient.PhoneNumber = phoneNumber;
            recipient.ImageUrl = imageUrl;

            await this.recipientRepository.SaveChangesAsync();
        }

        public IEnumerable<AllAppointmentsInListViewModel> GetAll(string userId, int page, int itemsPerPage)
        {
            var recipientrId = this.GetRecipientIdByUserId(userId);
            var appointmentsApplyByRecipient = this.appointmentsDonorsRepository.All()
            .Where(x => x.Appointment.RecipientId == recipientrId)
            .OrderByDescending(x => x.Appointment.DeadLine)
            .Skip((page - 1) * itemsPerPage) // Pages formula
            .Take(itemsPerPage)
            .Select(x => new AllAppointmentsInListViewModel
            {
                Id = x.Appointment.Id,
                DonorName = $"{x.Donor.FirstName} {x.Donor.LastName}",
                StartDate = x.Appointment.StartDate,
                DeadLine = x.Appointment.DeadLine,
                BloodType = x.Donor.BloodType,
                DonorPhone = x.Donor.PhoneNumber,
                DonorEmail = x.Donor.User.Email,
            })
            .ToList();

            return appointmentsApplyByRecipient;
        }

        public string GetRecipientIdByUserId(string userId)
        => this.recipientRepository.All().FirstOrDefault(x => x.UserId == userId).Id;

        public bool CheckRecipientExist(string userId)
        => this.recipientRepository.All().Any(x => x.UserId == userId);

        public int GetAllAppointmentsApllyByRecipientCount(string recipientId)
        => this.appointmentRepository.AllAsNoTracking().Where(x => x.RecipientId == recipientId).Count();

        public string GetRecipientEmail(string userId)
        => this.GetRecipientrById(userId).Email;
    }
}
