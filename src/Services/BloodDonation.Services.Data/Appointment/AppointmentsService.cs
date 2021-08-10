namespace BloodDonation.Services.Data.Appointment
{
    using System.Threading.Tasks;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Appointment;

    using static BloodDonation.Web.Infrastructure.ClaimsPrincipalExtension;

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
                StartDate = model.StartDate,
                DeadLine = model.DeadLine,
                SendingAddressInfo = model.SendingAddressInfo,
                AdditionalInfo = model.AdditionalInfo,
                IsApproved = false,
            };

            await this.appointmetsRepository.AddAsync(appointment);
            await this.appointmetsRepository.SaveChangesAsync();
        }
    }
}
