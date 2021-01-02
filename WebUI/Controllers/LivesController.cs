using Application.Topics.Commands;
using Application.Topics.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class LivesController : BaseMvcController
    {
        // GET: LivesController
        public async Task<ActionResult> Index()
        {
            var lives = await Mediator.Send(new Lives.LivesQuery());
            return View(lives);
        }

        // POST: LivesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddLive.AddLiveCommand model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                await Mediator.Send(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

    }
}
