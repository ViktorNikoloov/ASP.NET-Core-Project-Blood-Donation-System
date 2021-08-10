namespace BloodDonation.Web.ViewModels.Appointment
{
    using System;
    using System.Collections.Generic;

    public class AppointmentsListViewModel
    {
        public IEnumerable<AppointmentInListViewModel> Appointments { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage
            => this.PageNumber > 1;

        public int PreviousPageNumber
            => this.PageNumber - 1;

        public bool HasNextPage
            => this.PageNumber < this.PagesCount;

        public int NextPageNumber
            => this.PageNumber + 1;

        public int PagesCount
            => (int)Math.Ceiling((double)this.AppointmentsCount / this.ItemPerPage);

        public int AppointmentsCount { get; set; }

        public int ItemPerPage { get; set; }
    }
}
