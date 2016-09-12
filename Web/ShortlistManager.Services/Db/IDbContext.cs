using System.Data.Entity;
using ShortlistManager.Services.Db.Entities;

namespace ShortlistManager.Services
{
    public interface IDbContext
    {
        IDbSet<Player> Players { get; set; }
        IDbSet<Scout> Scouts { get; set; }
        IDbSet<Club> Clubs { get; set; }
        void SaveChanges();
    }
}