namespace BloodDonation.Services.Data.Home
{
    using System.Collections.Generic;
    using System.Linq;

    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Web.ViewModels.Home;

    public class StatisticsService : IStatisticsService
    {
        private readonly IDeletableEntityRepository<Donor> donorsRespository;
        private readonly IDeletableEntityRepository<Recipient> recipientsRepository;

        public StatisticsService(
            IDeletableEntityRepository<Donor> donorsRespository,
            IDeletableEntityRepository<Recipient> recipientsRepository)
        {
            this.donorsRespository = donorsRespository;
            this.recipientsRepository = recipientsRepository;
        }

        public GetCountsViewModel GetCounts()
        {
            var data = new GetCountsViewModel
            {
                DonorsCount = this.donorsRespository.AllAsNoTrackingWithDeleted().Count(),
                RecipientsCount = this.recipientsRepository.AllAsNoTrackingWithDeleted().Count(),
            };

            return data;
        }

        public IEnumerable<GetTopDonationsViewModel> GetTopDonations(int donationCount)
        {

            var topDonations = this.donorsRespository.AllAsNoTrackingWithDeleted()
                .Select(x => new GetTopDonationsViewModel
                {
                    FirstName = x.FirstName,
                    DonationCount = x.DonationCount,
                    LastDonation = x.LastDonation,
                })
                .OrderByDescending(x => x.DonationCount)
                .ThenBy(x => x.LastDonation)
                .Take(donationCount)
                .ToList();

            return topDonations;
        }
    }
}
