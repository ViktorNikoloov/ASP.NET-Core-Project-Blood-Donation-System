namespace BloodDonation.Web.ViewModels.Recipient
{
    using System.Collections.Generic;

    using BloodDonation.Web.ViewModels.Paginatian;

    public class AllAppointmentsApplyByRecipientListViewModel : PagingViewModel
    {
        public IEnumerable<AllAppointmentsApplyByRecipientInListViewModel> Appointments { get; set; }
    }
}
