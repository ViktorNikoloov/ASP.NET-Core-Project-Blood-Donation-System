namespace BloodDonation.Services.Data.Recipient
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Services.Mapping;

    public class RecipientsService : IRecipientsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipient> recipientRepository;
        private readonly IDeletableEntityRepository<Donor> donorRepository;

        public RecipientsService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Recipient> recipientRepository,
            IDeletableEntityRepository<Donor> donorRepository)
        {
            this.userRepository = userRepository;
            this.recipientRepository = recipientRepository;
            this.donorRepository = donorRepository;
        }

        public ApplicationUser GetRecipientApplicationUser(string recipietId)
        {
            var instance = this.userRepository
                .All().FirstOrDefault(u => u.Recipient.Id == recipietId);

            return instance;
        }

        public async Task CreateRecipientAsync(ApplicationUser user, string userId, string firstName, string middleName, string lastName, string cityName, string streetName, int postCode, Gender gender, BloodType bloodType, string imageUrl, string phoneNumber)
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
                }) //.To<GetRecipientByIdDto>()
                .FirstOrDefault();
        }

        public async Task UpdateCurrentLoggedInUserInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int postCode, string phoneNumber, string imageUrl)
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
    }
}
