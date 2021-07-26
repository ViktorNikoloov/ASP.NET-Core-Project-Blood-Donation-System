using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BloodDonation.Web.Areas.Identity.IdentityHostingStartup))]

namespace BloodDonation.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}