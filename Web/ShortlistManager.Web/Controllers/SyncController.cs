using System.Web.Mvc;
using ShortlistManager.Services;
using ShortlistManager.Services.Models;
using ShortlistManager.Web.Infrastructure.Auth;
using ShortlistManager.Web.Infrastructure.Filters.ModelState;

namespace ShortlistManager.Web.Controllers
{
    [UserDetailsProviderFilter]
    public class SyncController : Controller
    {
        /*
            > Using the PRG pattern
            > I would generally prefer to have a seperate view model class, rather than pushing 
              the dto straight to the view, but in this case the dto is just a bag of properties, and
              I'm reluctant to duplicate the object at each layer when there's little benefit to doing so.
         */

        private readonly IShortlistService _shortlistService;

        public SyncController(IShortlistService shortlistService)
        {
            _shortlistService = shortlistService;
        }


        [HttpGet]
        public ViewResult Index(UserDetails userDetails)
        {
            var players = _shortlistService.PlayersForScout(userDetails.Id);
            return View(players);
        }

        [HttpGet]
        [RetrieveModelState]
        public ViewResult Edit(UserDetails userDetails, int? id)
        {
            var player = id == null
                ? new PlayerDto()
                : _shortlistService.GetPlayer(userDetails.Id, id.Value);

            return View(player);
        }

        [HttpPost]
        [PersistModelState]
        public RedirectToRouteResult Edit(UserDetails userDetails, PlayerDto viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new {id = viewModel.Id});
            }

            _shortlistService.AddPlayer(userDetails.Id, viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult ConfirmDelete(UserDetails userDetails, int id)
        {
            var player = _shortlistService.GetPlayer(userDetails.Id, id);
            return View(player);
        }

        [HttpPost]
        public RedirectToRouteResult Delete(UserDetails userDetails, int id)
        {
            _shortlistService.RemovePlayer(userDetails.Id, id);
            return RedirectToAction("Index");
        }

    }
}