namespace BloodDonation.Data.Models
{
    using BloodDonation.Data.Common.Models;

    public class QuestionAnswer : BaseDeletableModel<int>
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }
    }
}
