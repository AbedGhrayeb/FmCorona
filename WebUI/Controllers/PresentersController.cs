using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Presenters;
using Application.Presenters.Commands;
using Application.Presenters.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PresentersController : BaseMvcController
    {
        // GET: PresentersController
        public async Task<IActionResult> Index()
        {
            var presenters = await Mediator.Send(new PresentersList.PresentersListQuery());
            var vm = presenters.PresentersDtos;
            return View(vm);
        }

        // GET: PresentersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var presenter = await Mediator.Send(new PresenterDetails.PresenterDetailsQuery { Id = id });
            var vm = presenter.PresenterDto;
            return View(vm);
        }

        // GET: PresentersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PresentersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddPresenterVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                await Mediator.Send(new AddPresenters.AddPresentersCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: PresentersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var presenter = await Mediator.Send(new PresenterDetails.PresenterDetailsQuery { Id = id });
            var dto = presenter.PresenterDto;
            var vm = new EditPresenterVm
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Bio = dto.Bio,
                ImgUrl = dto.ImgUrl
            };
            return View(vm);
        }

        // POST: PresentersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPresenterVm model)
        {
            try
            {
                model.Id = id;
                await Mediator.Send(new EditPresenter.EditPresenterCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        // POST: PresentersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id,DeletePresenter.DeletePresenterCommand command)
        {
            try
            {
                command.Id = id;
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
        // GET: PresentersController/SocialMedia/5
  
        public ActionResult SocialMedia(int id)
        {
            var model = new AddSocialMediaVm { PresenterId = id };
            return View(model);

        }
        // POST: PresentersController/SocialMedia/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SocialMedia(int id,AddSocialMediaVm model)
        {
            ViewBag.PresenterId = id;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await Mediator.Send(new AddSocialMedia.AddSocialMediaCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
