namespace BloodDonation.Web.ViewModels.Article
{
    using System;

    using BloodDonation.Data.Models;
    using BloodDonation.Services.Mapping;

    public class CommentsInArticleViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
