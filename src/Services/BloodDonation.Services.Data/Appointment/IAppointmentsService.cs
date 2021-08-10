namespace BloodDonation.Services.Data.Appointment
{
    using System.Threading.Tasks;

    using BloodDonation.Web.ViewModels.Appointment;

    public interface IAppointmentsService
    {
        Task CreateAsync(AppointmentCreateInputModel model, string recipientId);
    }
}
