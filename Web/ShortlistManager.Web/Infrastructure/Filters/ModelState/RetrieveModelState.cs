using System.Web.Mvc;

namespace ShortlistManager.Web.Infrastructure.Filters.ModelState
{
    public class RetrieveModelState : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelState = filterContext.Controller.RetrieveModelState();

            if (modelState == null)
                return;

            // if this is a view, merge in the model state.  if not, we don't need it anymote
            if (filterContext.Result is ViewResult)
            {
                filterContext.Controller.ViewData.ModelState.Merge(modelState);
            }
            else
            {
                filterContext.Controller.ClearStoredModelState();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}