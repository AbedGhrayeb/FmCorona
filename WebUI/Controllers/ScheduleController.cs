using Application.Programs;
using Application.Programs.Commands;
using Application.Programs.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ScheduleController : BaseMvcController
    {
        // GET: ScheduleController
        public async Task<IActionResult> Index(int? dayOfWeek = 0)
        {
            var schedule = await Mediator.Send(new WeeklySchedule.ScheduleQuery(dayOfWeek));
            var vm = schedule.ScheduleDtos;
            return View(vm);
        }

        // GET: Program Schedule Controller
        public async Task<IActionResult> Program(int id)
        {
            var program = await Mediator.Send(new ProgramDetails.ProgramDetailsQuery { Id = id });
            ViewBag.ProgramName = program.Name;
            ViewBag.ProgramId = id;
            var vm = await Mediator.Send(new ProgramSchedule.ProgramScheduleQuery { ProgramId = id });
            return View(vm);
        }


        // GET: ScheduleController/Create
        public IActionResult Create(int id)
        {
            var schedule = new AddScheduleVm { ProgramId = id };
            return View(schedule);
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, AddScheduleVm model)
        {

            model.ProgramId = id;
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                await Mediator.Send(new AddSchedule.EditScheduleCommand(model));
                return RedirectToAction(nameof(Program), new { id = model.ProgramId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ScheduleController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var vm = await Mediator.Send(new ScheduleDetails.ScheduleDetailsQuery { Id = id });
            return View(vm);
        }

        // POST: ScheduleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditScheduleVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                model.Id = id;
                await Mediator.Send(new EditSchedule.EditScheduleCommand(model));
                return RedirectToAction(nameof(Program), new { id = model.ProgramId });
            }
            catch
            {
                return View(model);
            }
        }

        // POST: ScheduleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await Mediator.Send(new ScheduleDetails.ScheduleDetailsQuery { Id = id });

            await Mediator.Send(new DeleteSchedule.DeleteScheduleCommand { Id = id });
            return RedirectToAction(nameof(Program), new { id = model.ProgramId });
        }
    }
}
