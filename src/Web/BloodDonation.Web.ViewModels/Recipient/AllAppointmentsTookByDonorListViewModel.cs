namespace BloodDonation.Web.ViewModels.Recipient
{
    using System.Collections.Generic;

    using BloodDonation.Web.ViewModels.Paginatian;

    public class AllAppointmentsTookByDonorListViewModel : PagingViewModel
    {
        public IEnumerable<AllAppointmentsTookByDonorInListViewModel> Appointments { get; set; }
    }
}
