﻿namespace BloodDonation.Services.Data.Donor
{
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;

    public class DonorsService : IDonorsService
    {
        private readonly IDeletableEntityRepository<Donor> donorRepository;

        public DonorsService(IDeletableEntityRepository<Donor> donorRepository)
        {
            this.donorRepository = donorRepository;
        }

        public async Task FirstTimeDonorAddInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, Gender gender, BloodType bloodType, string imageUrl)
        {
            var donor = this.donorRepository.All().FirstOrDefault(d => d.UserId == id);

            donor.FirstName = firstName;
            donor.MiddleName = middleName;
            donor.LastName = lastName;
            donor.Gender = gender;
            donor.BloodType = bloodType;
            donor.Address = new Address
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
            donor.PhoneNumber = phoneNumber;
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

        public async Task UpdateSingInDonorInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, string imageUrl)
        {
            var donor = this.donorRepository.All().FirstOrDefault(o => o.UserId == id);

            donor.FirstName = firstName;
            donor.MiddleName = middleName;
            donor.LastName = lastName;
            donor.Address = new Address
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
            donor.PhoneNumber = phoneNumber;
            donor.ImageUrl = imageUrl;

            await this.donorRepository.SaveChangesAsync();
        }
    }
}
