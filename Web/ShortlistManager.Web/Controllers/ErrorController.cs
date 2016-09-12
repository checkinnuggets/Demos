using System;
using System.Web.Mvc;
using ShortlistManager.Web.Models;

namespace ShortlistManager.Web.Controllers
{
    // Based on http://stackoverflow.com/a/5229581
    public class ErrorController : Controller
    {
        public ViewResult General(Exception exception)
        {
            var viewModel = new ErrorModel
            {
                Title="An error has occurred.",
                Description = "Some stuff about server side errors."
            };

            return View("Error", viewModel);
        }

        public ViewResult Http404()
        {
            var viewModel = new ErrorModel
            {
                Title = "Not Found",
                Description = "Some stuff about server not found errors."
            };

            return View("Error", viewModel);
        }

        public ViewResult Http403()
        {
            var viewModel = new ErrorModel
            {
                Title = "Unauthorized",
                Description = "Some stuff about unauthorized errors."
            };

            return View("Error", viewModel);
        }

        public JsonResult AjaxError()
        {
            return Json( new { ErrorMessage = "AJAX request failed." }, JsonRequestBehavior.AllowGet );
        }

    }
}