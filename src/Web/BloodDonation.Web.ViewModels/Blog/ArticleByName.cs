namespace BloodDonation.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    using BloodDonation.Services.Mapping;
    using Ganss.XSS;

    public class ArticleByName : IMapFrom<BloodDonation.Data.Models.Article>
    {
        public int Id { get; set; }

        public string UserUserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 60 ? this.Description?.Substring(0, 50) + "..." : this.Description;

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual IEnumerable<CommentsInArticleViewModel> Comments { get; set; }
    }
}
