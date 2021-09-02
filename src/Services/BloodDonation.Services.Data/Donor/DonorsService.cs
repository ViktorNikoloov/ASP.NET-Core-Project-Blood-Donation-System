namespace BloodDonation.Services.Data.Donor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Common;
    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Donor;

    public class DonorsService : IDonorsService
    {
        private readonly IDeletableEntityRepository<Donor> donorRepository;
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;
        private readonly IRepository<AppointmentsDonors> appointmentsDonorsRepository;

        public DonorsService(
            IDeletableEntityRepository<Donor> donorRepository,
            IDeletableEntityRepository<Appointment> appointmentRepository,
            IRepository<AppointmentsDonors> appointmentsDonorsRepository)
        {
            this.donorRepository = donorRepository;
            this.appointmentRepository = appointmentRepository;
            this.appointmentsDonorsRepository = appointmentsDonorsRepository;
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
                    LastDonation = x.LastDonation,
                    CityName = x.Address.Town.Name,
                    PostCode = x.Address.Town.PostCode,
                    StreetName = x.Address.Town.Street.Name,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.User.Email,
                    ImageUrl = x.ImageUrl,
                }) //.To<GetDonorByIdDto>()
                .FirstOrDefault();
        }

        public string GetDonorEmailByUserId(string userId)
        {
            var curruntDonorId = this.GetDonorIdByUserId(userId);
            var currentDonor = this.donorRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            return currentDonor.User.Email;
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

        public IEnumerable<AllAppointmentsInListViewModel> GetAll(string userId, int page, int itemsPerPage)
        {
            var donorId = this.GetDonorIdByUserId(userId);
            var appointmentsTakeByDonor = this.appointmentsDonorsRepository.All()
            .Where(x => x.DonorId == donorId && x.Appointment.IsDeleted == false)
            .OrderByDescending(x => x.Appointment.DeadLine)
            .Skip((page - 1) * itemsPerPage) // Pages formula
            .Take(itemsPerPage)
            .Select(x => new AllAppointmentsInListViewModel
            {
                Id = x.AppointmentId,
                RecipientName = $"{x.Appointment.Recipient.FirstName} {x.Appointment.Recipient.LastName}",
                StartDate = x.Appointment.StartDate,
                DeadLine = x.Appointment.DeadLine,
                RecipientBloodType = x.Appointment.Recipient.BloodType,
                RecipientPhone = x.Appointment.Recipient.PhoneNumber,
                RecipientEmail = x.Appointment.Recipient.User.Email,
            })
            .ToList();

            return appointmentsTakeByDonor;
        }

        public int GetAllAppointmentsTakeByDonorCount(string donorId)
        => this.appointmentsDonorsRepository.AllAsNoTracking().Where(x => x.DonorId == donorId && x.Appointment.IsDeleted == false && x.Appointment.IsApproved == true).Count();

        public string GetDonorIdByUserId(string userId)
        => this.donorRepository.All().FirstOrDefault(x => x.UserId == userId).Id;

        public bool CheckDonorExist(string userId)
        => this.donorRepository.All().Any(x => x.UserId == userId);

        public DateTime GetLastTimeDonorDonaton(string userId)
        => this.GetDonorById(userId).LastDonation;

        public int GetDonorRemainingDaysToDonation(DateTime lastDonation)
        => lastDonation.AddDays(GlobalConstants.DonationMinimumPeriod).ToUniversalTime().Subtract(DateTime.UtcNow).Days;

        public DateTime GetWhenDonorCouldDonateAgain(DateTime lastDonation)
        => lastDonation.AddDays(GlobalConstants.DonationMinimumPeriod).Date;

    }
}
