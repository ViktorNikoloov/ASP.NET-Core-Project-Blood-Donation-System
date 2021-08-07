namespace BloodDonation.Services.Data.Donor
{
    using System.Threading.Tasks;

    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;

    public interface IDonorsService
    {
        GetDonorByIdDto GetDonorById(string id);

        Task FirstTimeDonorAddInfo(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, Gender gender, BloodType bloodType, string imageUrl);

        Task UpdateSingInDonorInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, string imageUrl);
    }
}
