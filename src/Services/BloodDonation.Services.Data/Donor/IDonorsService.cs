namespace BloodDonation.Services.Data.Donor
{
    using System.Threading.Tasks;

    using BloodDonation.Services.Data.DTO;

    public interface IDonorsService
    {
        Task AddDonorInfo(string userId, string firstName, string middleName, string lastName, string townName, int postCode, string streetName, string imageUrl);

        GetDonorByIdDto GetDonorById(string id);
    }
}
