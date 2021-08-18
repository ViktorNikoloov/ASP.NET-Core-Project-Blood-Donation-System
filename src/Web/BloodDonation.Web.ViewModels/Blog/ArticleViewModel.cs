namespace BloodDonation.Web.ViewModels.Blog
{
    using System;

    using BloodDonation.Services.Mapping;

    public class ArticleViewModel : IMapFrom<BloodDonation.Data.Models.Article>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 60 ? this.Description?.Substring(0, 50) + "..." : this.Description;

        public string ImageUrl { get; set; }

        public int CommentCount { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Url => $"/Article/{this.Title.Replace(' ', '-')}";
    }
}
