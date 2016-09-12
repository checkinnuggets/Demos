using System.Web.Mvc;
using ShortlistManager.Web.Infrastructure.ErrorHandling;

namespace ShortlistManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcExceptionFilter());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
