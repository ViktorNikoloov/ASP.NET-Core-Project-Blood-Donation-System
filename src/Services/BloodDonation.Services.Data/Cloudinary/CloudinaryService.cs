namespace BloodDonation.Services.Data.Cloudinary
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPictureAsync(IFormFile file, string folderName, int width = 100, int height = 100)
        {
            UploadResult uploadResult = null;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                Folder = folderName,
                File = new FileDescription(file.Name, stream),
                AllowedFormats = new[] { "jpg", "png", "jfif", "exif", "gif", "bmp", "ppm", "pgm", "pbm", "pnm", "heif", "bat" },
                Format = "jpg",
                Overwrite = true,
                Transformation = new Transformation().Width(width).Height(height).Gravity("face").Radius("max").Border("2px_solid_white").Crop("thumb"),
            };

            uploadResult = this.cloudinaryUtility.Upload(uploadParams);

            if (uploadResult.Error is not null)
            {
                throw new InvalidOperationException("Error occurred.");
            }

            return uploadResult?.Url.ToString();
        }
    }
}
