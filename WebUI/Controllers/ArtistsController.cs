using Application.Articals.Commands;
using Application.Artists;
using Application.Artists.Commands;
using Application.Artists.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ArtistsController : BaseMvcController
    {
        // GET: ArtistsController
        public async Task<IActionResult> Index()
        {
            var artists = await Mediator.Send(new ArtistsList.ArtistsListQuery());
            var artistsVm = artists.ArtistsDto;
            return View(artistsVm);
        }

        // GET: ArtistsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddArtistVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                await Mediator.Send(new AddArtist.AddArtistCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ArtistsController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model = await Mediator.Send(new ArtistDetails.ArtistDetailsQuery { Id = id });
            return View(model);
        }

        // POST: ArtistsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditArtistVm model)
        {
            try
            {
                model.Id = id;
                await Mediator.Send(new EditArtist.EditArtistCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // POST: ArtistsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteArtist.DeleteArtistCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
