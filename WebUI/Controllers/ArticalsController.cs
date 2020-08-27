using Application.Articals;
using Application.Articals.Commands;
using Application.Articals.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ArticalsController : BaseMvcController
    {
        // GET: ArticalsController
        public async Task<IActionResult> Index()
        {
            var articals = await Mediator.Send(new ArticalList.ArticalListQuery());
            var articlsVm = articals.Highlights;
            return View(articlsVm);
        }

        // GET: ArticalsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var artical = await Mediator.Send(new ArticalDetails.ArticalQuery { Id = id });
            var articalVm = artical.Highlight;
            return View(articalVm);
        }

        // GET: ArticalsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticalsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddArticalVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                await Mediator.Send(new AddArtical.AddArticalCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ArticalsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var artical = await Mediator.Send(new ArticalDetails.ArticalQuery { Id = id });
            var articalVm = artical.Highlight;
            var VM = new EditArticalVm
            {
                Details = articalVm.Details,
                Id = articalVm.Id,
                ImgUrl = articalVm.ImgUrl,
                ShortDescription = articalVm.ShortDescription,
                Title = articalVm.Title
            };
            return View(VM);
        }

        // POST: ArticalsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditArticalVm model)
        {
            try
            {
                model.Id = id;
                await Mediator.Send(new EditArtical.EditArticalCommand(model));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        // POST: ArticalsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteArtical.DeleteArticalCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
