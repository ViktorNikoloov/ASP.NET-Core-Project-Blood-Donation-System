namespace BloodDonation.Web.ViewModels.Article
{
    using BloodDonation.Services.Mapping;

    public class ArticleViewModel : IMapFrom<BloodDonation.Data.Models.Article>
    {
        public string Title { get; set; }

        public string Discription { get; set; }

        public string ImageUrl { get; set; }

        public int CommentCount { get; set; }

        public string Url => $"/Blog/{this.Title.Replace(' ', '-')}";
    }
}
