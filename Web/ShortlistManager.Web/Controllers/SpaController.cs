using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShortlistManager.Services;
using ShortlistManager.Services.Models;
using ShortlistManager.Web.Infrastructure.Auth;
using ShortlistManager.Web.Infrastructure.ErrorHandling;

namespace ShortlistManager.Web.Controllers
{
    [UserDetailsProviderFilter]
    public class SpaController : Controller
    {
        private readonly IShortlistService _shortlistService;

        public SpaController(IShortlistService shortlistService)
        {
            _shortlistService = shortlistService;
        }

        [HttpGet]
        public ActionResult Index(UserDetails userDetails)
        {
            return View();
        }


        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult ListPlayers(UserDetails userDetails)
        {
            var players = _shortlistService.PlayersForScout(userDetails.Id);
            return Json(players, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult GetPlayer(UserDetails userDetails, int id)
        {
            var playersForScout = _shortlistService.PlayersForScout(userDetails.Id);
            var playerWithId = playersForScout.SingleOrDefault(x => x.Id == id);

            return Json(playerWithId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public HttpStatusCodeResult SavePlayer(UserDetails userDetails, PlayerDto player)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ExtractErrorMessage(ModelState);
                throw new ValidationException(errorMessage);
            }

            _shortlistService.AddPlayer(userDetails.Id, player);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpPost]
        public HttpStatusCodeResult DeletePlayer(UserDetails userDetails, int id)
        {
            _shortlistService.RemovePlayer(userDetails.Id, id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private static IEnumerable<string> ExtractErrorMessage(ModelStateDictionary modelState)
        {
            return (from key in modelState.Keys
                    from msg in modelState[key].Errors.Select(x => x.ErrorMessage)
                    select $"{key}: {msg}");
        }

    }
}