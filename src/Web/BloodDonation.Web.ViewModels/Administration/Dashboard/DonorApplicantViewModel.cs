namespace BloodDonation.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using BloodDonation.Data.Models;

    public class DonorApplicantViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<QuestionAnswer> QuestionsAnswers { get; set; }
    }
}
