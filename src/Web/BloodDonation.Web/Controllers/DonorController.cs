namespace BloodDonation.Web.Controllers
{
    using BloodDonation.Common;
    using BloodDonation.Services.Data.Donor;
    using BloodDonation.Web.Infrastructure;
    using BloodDonation.Web.ViewModels.Donor;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.DonorRoleName)]
    public class DonorController : Controller
    {
        private readonly IDonorsService donorsService;

        public DonorController(IDonorsService donorsService)
        {
            this.donorsService = donorsService;
        }

        public IActionResult All(int id = GlobalConstants.PaginationStartPageNumber)
        {
            var userId = this.User.GetId();
            var isDonorExist = this.donorsService.CheckDonorExist(userId);
            var donorId = this.donorsService.GetDonorIdByUserId(userId);

            if (!isDonorExist)
            {
                return this.Redirect("/Home/StatusCodeError");
            }

            const int ItemPerPage = 2;
            var viewModel = new AllAppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.donorsService.GetAllAppointmentsTakeByDonorCount(donorId),
                Appointments = this.donorsService.GetAll(userId, id, ItemPerPage),
                ActionName = nameof(this.All),
            };

            return this.View(viewModel);
        }
    }
}
