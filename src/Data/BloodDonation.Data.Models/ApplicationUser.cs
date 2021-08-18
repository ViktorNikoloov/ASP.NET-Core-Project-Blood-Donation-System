// ReSharper disable VirtualMemberCallInConstructor
namespace BloodDonation.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonation.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            // Application questions if the registered user or recipient is applying for Donor position
            this.QuestionsAnswers = new HashSet<QuestionAnswer>();
            this.Articles = new HashSet<Article>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual Recipient Recipient { get; set; }

        public virtual Donor Donor { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionsAnswers { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

    }
}
