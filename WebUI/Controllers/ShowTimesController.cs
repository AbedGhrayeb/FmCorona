using Application.Programs;
using Application.Programs.Commands;
using Application.Programs.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ShowTimesController : BaseMvcController
    {
        // GET: ShowTimesController
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.ProgramId = id;
            var vm = await Mediator.Send(new ProgramShowTimes.ProgramShowTimesQuery { ProgramId = id });
            return View(vm);
        }

        // GET: ShowTimesController/Create
        public ActionResult Create(int id)
        {
            AddShowTimeVm model = new AddShowTimeVm { ProgramId = id };
            return View(model);
        }

        // POST: ShowTimesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, AddShowTimeVm model)
        {
            model.ProgramId = id;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await Mediator.Send(new AddShowTime.AddShowTimeCommand(model));
                return RedirectToAction(nameof(Index), "ShowTimes", new { id = model.ProgramId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ShowTimesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vm = await Mediator.Send(new ShowTimeDetails.ShowTimeDetailsQuery { Id = id });
            return View(vm);
        }

        // POST: ShowTimesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ShowTime model)
        {
            try
            {
                model.Id = id;
                await Mediator.Send(new EditShowTime.EditShowTimeCommand(model));
                return RedirectToAction(nameof(Index), "ShowTimes", new { id = model.ProgramId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ShowTimesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShowTimesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
