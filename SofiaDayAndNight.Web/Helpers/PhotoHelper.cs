using System.Web;

using SofiaDayAndNight.Web.Models;

namespace SofiaDayAndNight.Web.Helpers
{
    public class PhotoHelper : IPhotoHelper
    {
        public ImageViewModel UploadImage(HttpPostedFileBase upload)
        {
            var image = new ImageViewModel
            {
                Name = System.IO.Path.GetFileName(upload.FileName),
                ContentType = upload.ContentType
            };
            using (var reader = new System.IO.BinaryReader(upload.InputStream))
            {
                image.Data = reader.ReadBytes(upload.ContentLength);
            }
            return image;
        }
    }
}