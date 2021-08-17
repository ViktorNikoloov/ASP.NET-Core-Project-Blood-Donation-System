namespace BloodDonation.Services.Data.Recipient
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Recipient;

    public interface IRecipientsService
    {
        Task CreateRecipientAsync(ApplicationUser user, string userId, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, Gender gender, BloodType bloodType, string imageUrl, string phoneNumber);

        ApplicationUser GetRecipientApplicationUser(string recipietId);

        GetRecipientByIdDto GetRecipientrById(string id);

        Task UpdateCurrentLoggedInRecipientInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, string imageUrl);

        string GetRecipientIdByUserId(string userId);

        bool CheckRecipientExist(string userId);

        int GetAllAppointmentsApllyByRecipientCount(string recipientId);

        IEnumerable<AllAppointmentsInListViewModel> GetAll(string userId, int page, int itemsPerPage);

        string GetRecipientEmail(string userId);
    }
}
