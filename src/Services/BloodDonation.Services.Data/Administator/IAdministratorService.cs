namespace BloodDonation.Services.Data.Administator
{
    using System.Threading.Tasks;

    public interface IAdministratorService
    {
        Т ApplicantDetailsById<Т>(string id);

        Task RemoveQuestionsAnswersFromUserAsync(string userId);

        Task AddDonorAsync(string id);
    }
}
