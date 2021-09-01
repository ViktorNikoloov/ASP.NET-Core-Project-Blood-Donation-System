namespace BloodDonation.Web.ViewModels.Paginatian
{
    using System;

    public class PagingViewModel
    {
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

        public string ActionName { get; set; }
    }
}
