namespace BloodDonation.Web.ViewModels.Appointment
{
    using System.Collections.Generic;

    using BloodDonation.Web.ViewModels.Paginatian;

    public class AppointmentsListViewModel : PagingViewModel
    {
        public IEnumerable<AppointmentInListViewModel> Appointments { get; set; }
    }
}
