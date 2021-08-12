namespace BloodDonation.Services.Data.ViewRenderService
{
    using System.Threading.Tasks;

    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
