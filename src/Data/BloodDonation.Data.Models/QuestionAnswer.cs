namespace BloodDonation.Data.Models
{
    using BloodDonation.Data.Common.Models;

    public class QuestionAnswer : BaseModel<int>
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
