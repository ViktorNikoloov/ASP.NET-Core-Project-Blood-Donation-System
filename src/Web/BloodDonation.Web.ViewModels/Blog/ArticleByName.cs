﻿namespace BloodDonation.Web.ViewModels.Article
{
    using System;
    using System.Collections.Generic;

    using BloodDonation.Services.Mapping;

    public class ArticleByName : IMapFrom<BloodDonation.Data.Models.Blog>
    {
        public int Id { get; set; }

        public string UserUserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual IEnumerable<CommentsInArticleViewModel> Comments { get; set; }
    }
}
