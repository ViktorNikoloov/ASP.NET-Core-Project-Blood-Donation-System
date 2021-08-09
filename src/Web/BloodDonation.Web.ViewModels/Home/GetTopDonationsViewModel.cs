namespace BloodDonation.Web.ViewModels.Home
{
    using System;

    public class GetTopDonationsViewModel
    {
        public string FirstName { get; set; }

        public int DonationCount { get; set; }

        public DateTime LastDonation { get; set; }
    }
}
