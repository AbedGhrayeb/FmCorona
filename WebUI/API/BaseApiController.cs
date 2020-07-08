using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator =
             HttpContext.RequestServices.GetService<IMediator>());
    }
}
