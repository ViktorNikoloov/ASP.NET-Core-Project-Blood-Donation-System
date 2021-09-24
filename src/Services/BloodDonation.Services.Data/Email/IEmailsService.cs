namespace BloodDonation.Services.Data.Email
{
    using BloodDonation.Data.Models;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Appointment;

    public interface IEmailsService
    {
        string GenerateEmailAppointmentHtmlContent(AppointmentByIdViewModel appointmentAllInfo, string subject);

        string GenerateEmailDonorTakeAnAppointmentHtmlContent(GetDonorByIdDto model, string subject);

        string GenerateEmailApplicantResponseHtmlContent(ApplicationUser? model, string subject, string content, string email = "");

        string GenerateEmailAppoinmentResponseHtmlContent(ApplicationUser? model, string subject, string content, string email = "");

        string GenerateEmailDonorSendApplication(ApplicationUser user, string subject);

        string GenerateEmailRecipientNewRegistration(ApplicationUser user, string subject);
    }
}
