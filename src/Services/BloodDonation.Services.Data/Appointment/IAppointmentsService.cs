namespace BloodDonation.Services.Data.Appointment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonation.Web.ViewModels.Appointment;

    public interface IAppointmentsService
    {
        Task CreateAsync(AppointmentCreateInputModel model, string recipientId);

        IEnumerable<AppointmentInListViewModel> GetAll(int page, int itemsPerPage = 4);

        int GetCount();

        string GetRecipientIdByUserId(string userId);

        AppointmentByIdViewModel GetAppoinmentAllInfo(int id);

        Task TakeAppointmentByDonor(string id, int appointmentId);
    }
}
