namespace BloodDonation.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum AppointmentStatus
    {
        [Display(Name = "Одобрена.")]
        Approved = 1,

        [Display(Name = "Неодобрена.")]
        Disapprove = 2,

        [Display(Name = "Чака одобрение.")]
        WaitingForApproval = 3,

        [Display(Name = "Спешна.")]
        Emergency = 4,
    }
}
