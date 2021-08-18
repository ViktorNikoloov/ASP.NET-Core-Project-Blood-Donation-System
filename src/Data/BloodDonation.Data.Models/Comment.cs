namespace BloodDonation.Data.Models
{
    using BloodDonation.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public virtual Blog Article { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
