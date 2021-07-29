namespace BloodDonation.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using BloodDonation.Data.Models;

    public class UnapprovedUsersViewModel
    {
        public ICollection<ApplicationUser> Applicants { get; set; }
    }
}
