namespace BloodDonation.Services.Data.Appointment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Appointment;

    public interface IAppointmentsService
    {
        Task CreateAsync(AppointmentCreateInputModel model, string recipientId);

        IEnumerable<AppointmentInListViewModel> GetAll(int page, int itemsPerPage = 4, string userRole = "Approved");

        int GetCount(bool isApproved);

        string GetRecipientIdByUserId(string userId);

        AppointmentByIdViewModel GetAppoinmentAllInfo(int id);

        Task TakeAppointmentByDonor(string id, int appointmentId);

        bool IsDonorExistInDonorsAppointmetns(int appointmentId, string userId);

        GetAppointmentById GetCurrentAppointment(int id);

        string GetRecipientEmailByAppointmentId(int id);
    }
}
