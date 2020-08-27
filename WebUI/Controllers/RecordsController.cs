using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Records.Commands;
using Application.Records.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class RecordsController : BaseMvcController
    {
        // GET: RecordsController
        public async Task<ActionResult> Index()
        {
            var vm = await Mediator.Send(new RecoredList.RecoredsQuery());
            return View(vm);
        }

        // POST: RecordsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            
            try
            {
                await Mediator.Send(new DeleteRecored.DeleteRecoredCommand { Id = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
