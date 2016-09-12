using System;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;

namespace ShortlistManager.Web.Infrastructure.ErrorHandling
{
    public class MvcExceptionFilter : HandleErrorAttribute
    {
        public class Caught
        {
            public string ExceptionMessage { get; set; }
        }

        public override void OnException(ExceptionContext filterContext)
        {
            // only for stuff deriving from Application - everything else is a hard error.

            string errorMessage;

            if(TryGetErrorMessage(filterContext.Exception, out errorMessage ))
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    HandleAsynchronous(filterContext, errorMessage);
                else
                    HandleSynchronous(filterContext, errorMessage);

                // common stuff...
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
            }


            base.OnException(filterContext);
        }

        private static bool TryGetErrorMessage(Exception exception, out string errorMessage)
        {
            // handle DbValidationException and anything derived from ApplicationException.
            // anything else is a hard error.

            errorMessage = null;

            var validationException = exception as DbEntityValidationException;
            if (validationException != null)
            {
                errorMessage = validationException.ExtractErrorMessage();
            }

            else if (exception is ApplicationException)
            {
                errorMessage = exception.Message;
            }

            return errorMessage != null;
        }

        private static void HandleAsynchronous(ExceptionContext filterContext, string errorMessage)
        {
            filterContext.Result = new JsonResult
            {
                Data = new Caught { ExceptionMessage = errorMessage },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private static void HandleSynchronous(ExceptionContext filterContext, string errorMessage)
        {
            filterContext.Controller.TempData[TempDataKeys.HandledErrorMessage] = errorMessage;

            var request = filterContext.HttpContext.Request;
            var isFromSamePage = IsFromSamePage(request);

            // if we're going back to the same view, persist the model state.  There likely won't
            // be any errors, but it will allow us to easily repopulate the form values.
            if (isFromSamePage && request.HttpMethod == "POST")
            {
                filterContext.Controller.PersistModelState();
            }


            if ( request.UrlReferrer == null                            // don't know where they came from
                || IsFromExternalReferrer(request)                      // came from outside out application
                || (isFromSamePage && request.HttpMethod != "POST") )   // came from the same page (allowing for get/post actions with same name
            {
                // bounce to home
                filterContext.Result = new RedirectResult("~/");
            }
            else
            {
                filterContext.Result = new RedirectResult(request.UrlReferrer.ToString());
            }
        }



        private static bool IsFromExternalReferrer(HttpRequestBase request)
        {
            // if we don't know where the user came from or is going to, it's
            // effectively an external source
            if (request.UrlReferrer == null || request.Url == null)
                return true;

            return !string.Equals(request.UrlReferrer.Authority, request.Url.Authority, StringComparison.CurrentCultureIgnoreCase);
        }

        private static bool IsFromSamePage(HttpRequestBase request)
        {
            if (request.UrlReferrer == null || request.Url == null)
                return false;

            return string.Equals(request.UrlReferrer.AbsoluteUri, request.Url.AbsoluteUri, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}