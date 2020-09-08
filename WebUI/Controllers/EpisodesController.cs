using Application.Programs;
using Application.Programs.Commands;
using Application.Programs.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    public class EpisodesController : BaseMvcController
    {
        public async Task<IActionResult> Index(int id, int? page, int? limit, string title)
        {
            ViewBag.ProgramId = id;
            var episodes = await Mediator.Send(new EpisodesList.EpisodesListQuery(page, limit, title) { ProgramId = id });
            var vm = episodes.EpisodesDtos;
            return View(vm);
        }
        public ActionResult Create(int id)
        {
            AddEpisodeVm model = new AddEpisodeVm();
            model.ProgramId = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, AddEpisodeVm model)
        {
            model.ProgramId = id;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await Mediator.Send(new AddEpisode.AddEpisodesCommand(model));
                return RedirectToAction(nameof(Index), "Episodes", new { id = model.ProgramId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: EpisodesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var episode = await Mediator.Send(new EpisodeDetails.EpisodeDetailsQuery { Id = id });
            var vm = new EditEpisodeVm
            {
                Duration = episode.Duration,
                Title = episode.Title,
                Number = episode.Number,
                Guest = episode.Guest,
                GuestName = episode.GuestName,
                Url = episode.Url,
                ProgramId = episode.ProgramId
            };
            return View(vm);
        }

        // POST: ShowTimesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,EditEpisodeVm model)
        {
            try
            {
                model.Id = id;
                await Mediator.Send(new EditEpisode.EditEpisodeCommand(model));
                return RedirectToAction(nameof(Index), "Episodes", new { id = model.ProgramId });
            }
            catch
            {
                return View(model);
            }
        }

        // POST: EpisodesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var episode = await Mediator.Send(new EpisodeDetails.EpisodeDetailsQuery { Id = id });
            await Mediator.Send(new DeleteEpisode.DeleteEpisodeCommand { Id = id });
            return RedirectToAction(nameof(Index), "Episodes", new { id = episode.ProgramId });
        }

    }
}