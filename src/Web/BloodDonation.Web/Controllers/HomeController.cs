﻿namespace BloodDonation.Web.Controllers
{
    using System.Diagnostics;

    using BloodDonation.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult StatusCodeError(int errorCode)
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
