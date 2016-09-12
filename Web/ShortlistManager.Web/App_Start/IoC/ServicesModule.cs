using ShortlistManager.Services;
using ShortlistManager.Services.Db.Entities;
using ShortlistManager.Services.Models;
using ShortlistManager.Services.Models.Mapping;
using Ninject.Modules;
using Ninject.Web.Common;
using ShortlistManager.Services.Db;

namespace ShortlistManager.Web.IoC
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContext>().To<ShortlistDb>().InRequestScope();

            Bind<IAuthService>().To<AuthService>();
            Bind<IShortlistService>().To<ShortlistService>();

            Bind<IMapper<Player, PlayerDto>>().To<PlayerDtoPlayerMapper>();
        }
    }

}