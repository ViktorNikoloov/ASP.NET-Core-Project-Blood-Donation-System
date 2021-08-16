namespace CronJobs
{
    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeletedAppointments
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentsRepository;

        public DeletedAppointments(IDeletableEntityRepository<Appointment> appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }

        public async Task Work()
        {
            var allAppointments = await this.appointmentsRepository
                .All()
                .ToListAsync();

            var expiredAppointments =
                 allAppointments
                 .Where(x =>
                 x.DeadLine.Subtract(DateTime.UtcNow).Days == 0)
                 .ToList();

            foreach (var offer in expiredAppointments)
            {
                this.appointmentsRepository.Delete(offer);
            }

            await this.appointmentsRepository.SaveChangesAsync();
        }
    }
}
