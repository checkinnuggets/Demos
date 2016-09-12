using ShortlistManager.Services.Models;

namespace ShortlistManager.Services
{
    public interface IAuthService
    {
        bool TryAuthenticate(string userName, string password, out UserDto userDetails);
    }
}