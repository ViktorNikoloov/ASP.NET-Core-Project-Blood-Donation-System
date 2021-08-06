namespace BloodDonation.Services.Data.Donor
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Data.DTO;

    public class DonorsService : IDonorsService
    {
        private readonly IDeletableEntityRepository<Donor> donorRepository;

        public DonorsService(IDeletableEntityRepository<Donor> donorRepository)
        {
            this.donorRepository = donorRepository;
        }

        public async Task AddDonorInfo(string userId, string firstName, string middleName, string lastName, string townName, int postCode, string streetName, int number, string imageUrl)
        {
            var donor = this.donorRepository.All().Where(d => d.UserId == userId).FirstOrDefault();

            donor.FirstName = firstName;
            donor.MiddleName = middleName;
            donor.LastName = lastName;
            donor.Address = new Address
            {
                Town = new Town
                {
                    Name = townName,
                    PostCode = postCode,
                    Street = new Street
                    {
                        Name = streetName,
                    },
                },
            };
            donor.ImageUrl = imageUrl;

            await this.donorRepository.SaveChangesAsync();
        }

        public GetDonorByIdDto GetDonorById(string id)
        {
            return this.donorRepository
                .AllAsNoTracking()
                .Where(r => r.UserId == id)
                .Select(x => new GetDonorByIdDto
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    BloodType = x.BloodType,
                    CityName = x.Address.Town.Name,
                    PostCode = x.Address.Town.PostCode,
                    StreetName = x.Address.Town.Street.Name,
                    PhoneNumber = x.PhoneNumber,
                    ImageUrl = x.ImageUrl,
                }) //.To<GetRecipientByIdDto>()
                .FirstOrDefault();
        }
    }
}
