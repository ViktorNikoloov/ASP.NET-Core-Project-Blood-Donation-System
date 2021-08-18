namespace BloodDonation.Web.ViewModels.Article
{
    using BloodDonation.Services.Mapping;

    public class ArticleViewModel : IMapFrom<BloodDonation.Data.Models.Blog>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CommentCount { get; set; }

        public string Url => $"/Article/{this.Title.Replace(' ', '-')}";
    }
}
