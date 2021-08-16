namespace BloodDonation.Web.Infrastructure.Filters
{
    using BloodDonation.Common;
    using Hangfire.Dashboard;

    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var isInRole = httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            return isInRole;
        }
    }
}
