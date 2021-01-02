using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Advertisings;
using Application.Advertisings.Commands;
using Application.Advertisings.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AdvertisingsController : BaseMvcController
    {
        // GET: AdvertisingsController
        public async Task<ActionResult> Index()
        {
            var vm = await Mediator.Send(new AdvertisingsList.AdvertisingsQuery());
            return View(vm);
        }


        // GET: AdvertisingsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertisingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddAdvertisingVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                await Mediator.Send(new AddAdvertising.AddAdvertisingCommand(vm));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vm);
            }
        }

        // GET: AdvertisingsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var query = await Mediator.Send(new AdvertisingsList.AdvertisingsQuery());
            var advertising = query.Where(x => x.Id == id).FirstOrDefault();
            if (advertising == null) return NotFound();
            var vm = new EditAdvertisingVm { EndAt = advertising.EndAt, ImgUrl = advertising.ImgUrl, Sponsor = advertising.Sponsor, StartFrom = advertising.StartFrom, Url = advertising.Url };
            return View(vm);
        }

        // POST: AdvertisingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditAdvertisingVm vm)
        {
            vm.Id = id;
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                ViewBag.Id = id;
                await Mediator.Send(new EditAdvertising.EditAdvertisingCommand(vm));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vm);
            }
        }

        // POST: AdvertisingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var query = await Mediator.Send(new AdvertisingsList.AdvertisingsQuery());
            var vm = query.Where(x => x.Id == id);
            if (vm == null) return NotFound();
            try
            {
                await Mediator.Send(new DeleteAdvertising.DeleteAdvertisingCommand { Id = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
