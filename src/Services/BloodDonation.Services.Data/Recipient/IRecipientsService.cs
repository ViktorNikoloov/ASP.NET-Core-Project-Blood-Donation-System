namespace BloodDonation.Services.Data.Recipient
{
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;

    public interface IRecipientsService
    {
        Task CreateRecipientAsync(ApplicationUser user, string userId, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, Gender gender, BloodType bloodType, string imageUrl, string phoneNumber);

        ApplicationUser GetRecipientApplicationUser(string recipietId);

        GetRecipientByIdDto GetRecipientrById(string id);

        //Task UpdateCurrentLoggedInUserInfoAsync(string id, string firstName, string middleName, string lastName, string address, string description, string imageUrl);
    }
}
