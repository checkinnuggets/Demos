using System.Linq;
using ShortlistManager.Services.Models;

namespace ShortlistManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContext _db;

        public AuthService(IDbContext db)
        {
            _db = db;
        }

        public bool TryAuthenticate(string userName, string password, out UserDto userDetails)
        {
            // Allow any login for the purposes of this demo, but make sure we setup the user in the database.

            var loweredUserName = userName.ToLower();
            var scout = _db.Scouts.SingleOrDefault(x => x.UserName.ToLower() == loweredUserName);

            if (scout == null)
            {
                scout = new Db.Entities.Scout { UserName = userName };
                _db.Scouts.Add(scout);

                _db.SaveChanges();
            }

            userDetails = new UserDto { Id = scout.Id, UserName = scout.UserName};

            return true;
        }
    }
}

