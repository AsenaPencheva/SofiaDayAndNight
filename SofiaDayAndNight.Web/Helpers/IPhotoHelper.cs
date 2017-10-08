using System.Web;

using SofiaDayAndNight.Web.Models;

namespace SofiaDayAndNight.Web.Helpers
{
    public interface IPhotoHelper
    {
        ImageViewModel UploadImage(HttpPostedFileBase upload);
    }
}