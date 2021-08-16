namespace BloodDonation.Services.Data.Appointment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Web.ViewModels.Appointment;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmetsRepository;
        private readonly IDeletableEntityRepository<Recipient> recipientRepository;
        private readonly IDeletableEntityRepository<Donor> donorRepository;
        private readonly IRepository<AppointmetsDonors> appointmentsDonorsRepository;

        public AppointmentsService(
            IDeletableEntityRepository<Appointment> appointmetsRepository,
            IDeletableEntityRepository<Recipient> recipientRepository,
            IDeletableEntityRepository<Donor> donorRepository,
            IRepository<AppointmetsDonors> appointmentsDonorsRepository)
        {
            this.appointmetsRepository = appointmetsRepository;
            this.recipientRepository = recipientRepository;
            this.donorRepository = donorRepository;
            this.appointmentsDonorsRepository = appointmentsDonorsRepository;
        }

        public async Task CreateAsync(AppointmentCreateInputModel model, string recipientId)
        {
            var appointment = new Appointment
            {
                RecipientId = recipientId,
                Hospital = new Hospital
                {
                    HospitalName = model.Hospital,
                    HospitalWardName = model.HospitalWard,
                    Town = new Town
                    {
                        Name = model.HospitalCity,
                        Street = new Street
                        {
                            Name = "Липсва",
                        },
                    },
                },
                BloodBankCount = model.BloodBankCount,
                StartDate = model.StartDate.Date.ToUniversalTime(),
                DeadLine = model.DeadLine.Date.ToUniversalTime(),
                SendingAddressInfo = model.SendingAddressInfo,
                AdditionalInfo = model.AdditionalInfo,
                IsApproved = false,
            };

            await this.appointmetsRepository.AddAsync(appointment);
            await this.appointmetsRepository.SaveChangesAsync();
        }

        public IEnumerable<AppointmentInListViewModel> GetAll(int page, int itemsPerPage, string userRole = "Approved")
        {
            if (userRole == "Approved")
            {
                var approvalAppointments = this.appointmetsRepository.AllAsNoTracking()
                .Where(x => x.DeadLine >= DateTime.UtcNow && x.BloodBankCount != 0 && x.IsApproved == true)
                .OrderBy(x => x.StartDate)
                .Skip((page - 1) * itemsPerPage) // Pages formula
                .Take(itemsPerPage)
                .Select(x => new AppointmentInListViewModel
                {
                    Id = x.Id,
                    BloodBankCount = x.BloodBankCount,
                    RecipientFirstName = x.Recipient.FirstName,
                    BloodType = x.Recipient.BloodType,
                    DeadLine = x.DeadLine,
                    ImageUrl = x.Recipient.ImageUrl,
                })
                .ToList();

                return approvalAppointments;
            }

            var notapprovalAppointments = this.appointmetsRepository.AllAsNoTracking()
                .Where(x => x.IsApproved == false)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage) // Pages formula
                .Take(itemsPerPage)
                .Select(x => new AppointmentInListViewModel
                {
                    Id = x.Id,
                    BloodBankCount = x.BloodBankCount,
                    RecipientFirstName = x.Recipient.FirstName,
                    BloodType = x.Recipient.BloodType,
                    AdditionalInfo = x.AdditionalInfo.Substring(0, 60) + "...",
                    DeadLine = x.DeadLine,
                    ImageUrl = x.Recipient.ImageUrl,
                })
                .ToList();

            return notapprovalAppointments;
        }

        public AppointmentByIdViewModel GetAppoinmentAllInfo(int id)
        {
            var currentAppointment = this.appointmetsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return new AppointmentByIdViewModel
            {
                Id = id,
                StartDate = currentAppointment.StartDate,
                DeadLine = currentAppointment.DeadLine,
                BloodType = currentAppointment.Recipient.BloodType,
                BloodBankCount = currentAppointment.BloodBankCount,
                RecipientFullName = $"{currentAppointment.Recipient.FirstName} {currentAppointment.Recipient.MiddleName} {currentAppointment.Recipient.LastName}",
                PhoneNumber = currentAppointment.Recipient.PhoneNumber,
                CityName = currentAppointment.Recipient.Address.Town.Name,
                Email = currentAppointment.Recipient.User.Email,
                HospitalName = currentAppointment.Hospital.HospitalName,
                HospitalWard = currentAppointment.Hospital.HospitalWardName,
                HospitalCity = currentAppointment.Hospital.Town.Name,
                SendingAddressInfo = currentAppointment.SendingAddressInfo,
                AdditionalInfo = currentAppointment.AdditionalInfo,
                ImageUrl = currentAppointment.Recipient.ImageUrl,
            };
        }

        public async Task TakeAppointmentByDonor(string userId, int appointmentId)
        {
            var currentDonorId = this.GetDonorIdByUserId(userId);
            var curruntDonor = this.donorRepository.All().FirstOrDefault(x => x.Id == currentDonorId);
            var currAppointment = this.appointmetsRepository.All().FirstOrDefault(x => x.Id == appointmentId);

            var appointmetsDonors = new AppointmetsDonors
            {
                DonorId = currentDonorId,
                AppointmentId = appointmentId,
            };

            curruntDonor.LastDonation = DateTime.UtcNow;
            curruntDonor.DonationCount += 1;

            currAppointment.BloodBankCount -= 1;

            await this.appointmentsDonorsRepository.AddAsync(appointmetsDonors);
            await this.appointmentsDonorsRepository.SaveChangesAsync();

            await this.appointmetsRepository.SaveChangesAsync();
            await this.donorRepository.SaveChangesAsync();
        }

        public int GetCount(bool isApproved)
        => this.appointmetsRepository.AllAsNoTracking().Where(x => x.DeadLine >= DateTime.Now && x.IsApproved == isApproved).Count();

        public string GetRecipientIdByUserId(string userId)
        => this.recipientRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId).Id;

        public string GetDonorIdByUserId(string userId)
        => this.donorRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId).Id;

        public bool IsDonorExistInDonorsAppointmetns(int appointmentId, string userId)
        {
            var donorId = this.donorRepository.All().FirstOrDefault(x => x.UserId == userId).Id;
            var isDonorExistInDonorsAppointmetns = this.appointmentsDonorsRepository.All().Where(a => a.AppointmentId == appointmentId)
                .ToList().FirstOrDefault(x => x.DonorId == donorId);

            if (isDonorExistInDonorsAppointmetns != null)
            {
                return true;
            }

            return false;
        }
    }
}
