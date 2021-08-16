namespace BloodDonation.Services.Data.Administator
{
    using System.Threading.Tasks;

    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Administration.Dashboard;

    public interface IAdministratorService
    {
        Т ApplicantDetailsById<Т>(string id);

        Task RemoveQuestionsAnswersFromUserAsync(string userId);

        Task AddDonorAsync(string id);

        AppointmentViewModel GetAppoinmentAllInfo(int id);

        Task ApproveAppointmentAsync(int id);

        Task RejectAppointmentAsync(int id);

        Task UpdateAppoinmentAsync(GetAppointmentById model);
    }
}
