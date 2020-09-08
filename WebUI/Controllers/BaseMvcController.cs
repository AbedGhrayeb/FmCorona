using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace WebUI.Controllers
{
    [Authorize(Roles="admin")]
    public class BaseMvcController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator =
             HttpContext.RequestServices.GetService<IMediator>());
    }
}
