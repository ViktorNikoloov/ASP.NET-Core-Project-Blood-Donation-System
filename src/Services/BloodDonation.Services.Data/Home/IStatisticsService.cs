namespace BloodDonation.Services.Data.Home
{
    using System.Collections.Generic;

    using BloodDonation.Web.ViewModels.Home;

    public interface IStatisticsService
    {
        GetCountsViewModel GetCounts();

        IEnumerable<GetTopDonationsViewModel> GetTopDonations(int donationCount);
    }
}
