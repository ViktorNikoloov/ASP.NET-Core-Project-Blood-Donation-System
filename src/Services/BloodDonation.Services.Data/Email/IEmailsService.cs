namespace BloodDonation.Services.Data.Email
{
    using BloodDonation.Data.Models;
    using BloodDonation.Web.ViewModels.Appointment;

    public interface IEmailsService
    {
        string GenerateEmailAppointmentHtmlContent(AppointmentByIdViewModel appointmentAllInfo, string subject);

        string GenerateEmailApplicantResponseHtmlContent(ApplicationUser? model, string subject, string content, string email = "");
    }
}
