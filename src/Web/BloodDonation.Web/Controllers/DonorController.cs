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

        public IActionResult BeADonor()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult BeADonor(DonorFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.TempData["Message"] = "Вашата заявка за \"Кръводарител\" се изпрати успешно, моля изчакайте одобрение от \"Администратор\"";

            return this.Redirect("/");
        }

        public IActionResult AppointmentsTakeByDonor(int id = GlobalConstants.PaginationStartPageNumber)
        {
            var userId = this.User.GetId();
            var isDonorExist = this.donorsService.CheckDonorExist(userId);

            if (!isDonorExist)
            {
                return this.Redirect("/Home/StatusCodeError");
            }

            const int ItemPerPage = 4;
            var viewModel = new AllAppointmentsListViewModel
            {
                ItemPerPage = ItemPerPage,
                PageNumber = id,
                AppointmentsCount = this.donorsService.GetAllAppointmentsTakeByDonorCount(),
                Appointments = this.donorsService.GetAll(userId, id, ItemPerPage),
            };

            return this.View(viewModel);
        }
    }
}
