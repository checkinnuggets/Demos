using System.Web.Mvc;

namespace ShortlistManager.Web.Infrastructure.Filters.ModelState
{
    public class PersistModelState : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // if modelstate is not valid + we are redirecting, persist the modelstate to be retrieved by the target action

            if (filterContext.Controller.ViewData.ModelState.IsValid == false
                && (filterContext.Result is RedirectResult || filterContext.Result is RedirectToRouteResult))
            {
                filterContext.Controller.PersistModelState();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}