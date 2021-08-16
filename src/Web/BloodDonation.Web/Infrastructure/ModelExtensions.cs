namespace BloodDonation.Web.Infrastructure
{
    using BloodDonation.Web.ViewModels.Appointment;

    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this AppointmentInListViewModel model)
            => model.RecipientFirstName + "-" + model.BloodType + "-" + model.BloodBankCount;
    }
}
