namespace BloodDonation.Web.ViewModels.Donor
{
    using System.Collections.Generic;

    using BloodDonation.Web.ViewModels.Paginatian;

    public class AllAppointmentsListViewModel : PagingViewModel
    {
        public IEnumerable<AllAppointmentsInListViewModel> Appointments { get; set; }
    }
}
