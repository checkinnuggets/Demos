using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using ShortlistManager.Web.Infrastructure.Auth;
using ShortlistManager.Web.Infrastructure.Validation;

namespace ShortlistManager.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // wire up our simple custom auth claims
            AntiForgeryConfig.UniqueClaimTypeIdentifier = AuthHelpers.UserIdClaimKey;

            // wire up FluentValidation
            FluentValidationModelValidatorProvider.Configure(provider => {
                provider.ValidatorFactory = new ValidatorFactory();
            });
        }

        protected void Application_Error()
        {
            // TODO (GG): fixup - this catches all exceptions
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();

            // Default to handling generic errors
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;

            // If it's a http exception, we can vary the response code and direct to a different
            // controller action to have different error pages/messages.
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 403:
                        routeData.Values["action"] = "Http403";
                        break;
                    case 404:
                        routeData.Values["action"] = "Http404";
                        break;
                }
            }

            // if it's an ajax request, we have a handler for that.
            if (new HttpRequestWrapper(Request).IsAjaxRequest())
            {
                routeData.Values["action"] = "AjaxError";
            }

            // finally, fire off the action
            IController errorsController = new Controllers.ErrorController();
            var requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(requestContext);
        }
    }
}
