using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ShortlistManager.Web.Infrastructure.Auth
{
    public class UserDetailsProviderFilter : ActionFilterAttribute
    {
        public const string UserNameParameterName = "userDetails";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var claims = ((ClaimsPrincipal) HttpContext.Current.User).Claims
                            .ToDictionary(claim => claim.Type);

            var userDetails = new UserDetails
            {
                Id = System.Convert.ToInt32(claims[AuthHelpers.UserIdClaimKey].Value),
                UserName = claims[AuthHelpers.UserNameClaimKey].Value
            };

            filterContext.ActionParameters[UserNameParameterName] = userDetails;
            
            base.OnActionExecuting(filterContext);
        }
    }
}