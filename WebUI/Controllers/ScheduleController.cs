using Application.Programs;
using Application.Programs.Commands;
using Application.Programs.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ScheduleController : BaseMvcController
    {
        // GET: ScheduleController
        public async Task<IActionResult> Index(int? dayOfWeek=1)
        {
            var schedule = await Mediator.Send(new WeeklySchedule.ScheduleQuery(dayOfWeek));
            var vm = schedule.ScheduleDtos;
            return View(vm);
        }


        // GET: ScheduleController/Create
        public async Task<IActionResult> Create()
        {
            var programsEnvelope = await Mediator.Send(new ProgramsList.ProgramsListQuery());
            var programs = programsEnvelope.ProgramsDtos;
            ViewData["Programs"] = new SelectList(programs, "Id", "Name");
            return View();
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddScheduleVm model)
        {
            var programsEnvelope = await Mediator.Send(new ProgramsList.ProgramsListQuery());
            var programs = programsEnvelope.ProgramsDtos;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                ViewData["Programs"] = new SelectList(programs, "Id", "Name");
                await Mediator.Send(new AddSchedule.EditScheduleCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Programs"] = new SelectList(programs, "Id", "Name");
                return View(model);
            }
        }

        // GET: ScheduleController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var programsEnvelope = await Mediator.Send(new ProgramsList.ProgramsListQuery());
            var programs = programsEnvelope.ProgramsDtos;
            ViewData["Programs"] = new SelectList(programs, "Id", "Name");
            var vm = await Mediator.Send(new ScheduleDetails.ScheduleDetailsQuery { Id = id });
            return View(vm);
        }

        // POST: ScheduleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditScheduleVm model)
        {
            var programsEnvelope = await Mediator.Send(new ProgramsList.ProgramsListQuery());
            var programs = programsEnvelope.ProgramsDtos;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                ViewData["Programs"] = new SelectList(programs, "Id", "Name");
                await Mediator.Send(new EditSchedule.EditScheduleCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Programs"] = new SelectList(programs, "Id", "Name");
                return View(model);
            }
        }

        // POST: ScheduleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSchedule.DeleteScheduleCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
