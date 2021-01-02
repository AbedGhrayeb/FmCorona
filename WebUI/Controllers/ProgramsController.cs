using Application.Presenters.Queries;
using Application.Programs;
using Application.Programs.Commands;
using Application.Programs.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ProgramsController : BaseMvcController
    {
        // GET: ProgramsController
        public async Task<IActionResult> Index()
        {
            var programs = await Mediator.Send(new ProgramsList.ProgramsListQuery());
            var vm = programs.ProgramsDtos;
            return View(vm);
        }

        // GET: ProgramsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var program = await Mediator.Send(new ProgramDetails.ProgramDetailsQuery { Id = id });
            return View(program);
        }

        // GET: ProgramsController/Create
        public async Task<IActionResult> Create()
        {
            var presentersEnvelope = await Mediator.Send(new PresentersList.PresentersListQuery());
            var presenters = presentersEnvelope.PresentersDtos;
            ViewData["Presinters"] = new SelectList(presenters, "Id", "FullName");
            return View();
        }

        // POST: ProgramsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProgramVm model)
        {
            var presentersEnvelope = await Mediator.Send(new PresentersList.PresentersListQuery());
            var presenters = presentersEnvelope.PresentersDtos;
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                ViewData["Presinters"] = new SelectList(presenters, "Id", "FullName");
                await Mediator.Send(new AddProgram.AddProgramCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Presinters"] = new SelectList(presenters, "Id", "FullName");
                return View(model);
            }
        }

        // GET: ProgramsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var presentersEnvelope = await Mediator.Send(new PresentersList.PresentersListQuery());
            var presenters = presentersEnvelope.PresentersDtos;
            ViewData["Presenters"] = new SelectList(presenters, "Id", "FullName");
            var program = await Mediator.Send(new ProgramDetails.ProgramDetailsQuery { Id = id });
            var vm = new EditProgramVm
            {
                Id = program.Id,
                Description = program.Description,
                ImgUrl = program.ImgUrl,
                Name = program.Name
            };
            return View(vm);
        }

        // POST: ProgramsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditProgramVm model)
        {
            var presentersEnvelope = await Mediator.Send(new PresentersList.PresentersListQuery());
            var presenters = presentersEnvelope.PresentersDtos;
            try
            {
                ViewData["Presinters"] = new SelectList(presenters, "Id", "FullName");

                model.Id = id;
                await Mediator.Send(new EditProgram.EditProgramCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Presinters"] = new SelectList(presenters, "Id", "FullName");

                return View(model);
            }
        }



        // POST: ProgramsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProgram.DeleteProgramCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
