namespace BloodDonation.Data.Models
{
    using BloodDonation.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        public string RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }

        public int Score { get; set; }

        public string SentBy { get; set; }
    }
}
