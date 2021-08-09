namespace BloodDonation.Web.ViewModels.Home
{
    public class GetCountsViewModel
    {
        public int DonorsCount { get; set; }

        public int RecipientsCount { get; set; }

        public int AllUsers
            => this.DonorsCount + this.RecipientsCount;
    }
}
