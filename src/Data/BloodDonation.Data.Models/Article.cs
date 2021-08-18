namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;

    using BloodDonation.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Discription { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}
