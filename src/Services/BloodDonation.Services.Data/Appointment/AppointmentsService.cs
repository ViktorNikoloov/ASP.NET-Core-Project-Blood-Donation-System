namespace BloodDonation.Services.Data.Appointment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Web.ViewModels.Appointment;


    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmetsRepository;

        public AppointmentsService(IDeletableEntityRepository<Appointment> appointmetsRepository)
        {
            this.appointmetsRepository = appointmetsRepository;
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

        public IEnumerable<AppointmentInListViewModel> GetAll(int page, int itemsPerPage = 6)
        {
            var appointment = this.appointmetsRepository.AllAsNoTracking()
                .Where(x => x.DeadLine >= DateTime.UtcNow)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
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

            return appointment;
        }

        public int GetCount()
        {
            return this.appointmetsRepository.AllAsNoTracking().Count();
        }
    }
}
