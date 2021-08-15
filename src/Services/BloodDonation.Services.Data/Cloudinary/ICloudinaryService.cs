namespace BloodDonation.Services.Data.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile file, string folderName, int width = 100, int height = 100);
    }
}
