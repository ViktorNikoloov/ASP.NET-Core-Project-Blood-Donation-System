namespace BloodDonation.Services.Data.Donor
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Data.DTO;
    using BloodDonation.Web.ViewModels.Donor;

    public interface IDonorsService
    {
        GetDonorByIdDto GetDonorById(string id);

        Task FirstTimeDonorAddInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, Gender gender, BloodType bloodType, string imageUrl);

        Task UpdateSingInDonorInfoAsync(string id, string firstName, string middleName, string lastName, string cityName, string streetName, int? postCode, string phoneNumber, string imageUrl);

        string GetDonorEmailByUserId(string userId);

        DateTime GetLastTimeDonorDonaton(string userId);

        int GetDonorRemainingDaysToDonation(DateTime lastDonation);

        DateTime GetWhenDonorCouldDonateAgain(DateTime lastDonation);

        string GetDonorIdByUserId(string userId);

        public bool CheckDonorExist(string userId);

        public int GetAllAppointmentsTakeByDonorCount(string donorId);

        public IEnumerable<AllAppointmentsInListViewModel> GetAll(string userId, int page, int itemsPerPage);
    }
}
