namespace BloodDonation.Web.ViewModels.Recipient
{
    using System.Collections.Generic;

    using BloodDonation.Web.ViewModels.Paginatian;

    public class AllAppointmentsListViewModel : PagingViewModel
    {
        public IEnumerable<AllAppointmentsInListViewModel> Appointments { get; set; }
    }
}
