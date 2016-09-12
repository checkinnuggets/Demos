using System.Web.Mvc;

namespace ShortlistManager.Web.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}