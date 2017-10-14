using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Web.Helpers
{
    public interface IUserProvider
    {
        User FindByName(string username);

        void Update(User user);
    }
}